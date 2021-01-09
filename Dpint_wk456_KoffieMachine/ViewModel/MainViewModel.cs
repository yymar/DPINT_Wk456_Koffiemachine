using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using KoffieMachineDomain;
using KoffieMachineDomain.Enums;
using KoffieMachineDomain.Factory;
using KoffieMachineDomain.Interfaces;
using KoffieMachineDomain.Payments;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using TeaAndChocoLibrary;

namespace Dpint_wk456_KoffieMachine.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        // additions
        private TeaBlendRepository _teaBlendRepository;
        private CultureInfo _currentCulture;
        private CashPaymentController _cashPaymentController;
        private CardPaymentController _cardPaymentController;

        public MainViewModel()
        {
            _coffeeStrength = Strength.Normal;
            _sugarAmount = Amount.Normal;
            _milkAmount = Amount.Normal;

            LogText = new ObservableCollection<string>
            {
                "Starting up...",
                "Done, what would you like to drink?"
            };

            // intialise payment controllers
            _cashPaymentController = new CashPaymentController();
            _cardPaymentController = new CardPaymentController();

            // gets Key values from _cashOnCard dictionary in CashPaymentController.cs (cardholder names)
            PaymentCardUsernames = new ObservableCollection<string>(_cardPaymentController.GetCardNames());
            // sets selected user to first entry of ObservableCollection PaymentCardUsernames.
            SelectedPaymentCardUsername = PaymentCardUsernames[0];

            // new teablend repository from dependency
            _teaBlendRepository = new TeaBlendRepository();
            TeaBlendNames = new ObservableCollection<string>(_teaBlendRepository.BlendNames);
            SelectedTeaBlend = TeaBlendNames[0];

            // formats the euro sign to the front instead of the back.
            _currentCulture = new CultureInfo("nl-NL");
            _currentCulture.NumberFormat.CurrencyPositivePattern = 0;
            _currentCulture.NumberFormat.CurrencyNegativePattern = 2;
            _currentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
        }

        public ObservableCollection<string> LogText { get; private set; }

        private IDrink _selectedDrink;
        public string SelectedDrinkName
        {
            get { return _selectedDrink?.Name; }
        }

        public double? SelectedDrinkPrice
        {
            get { return _selectedDrink?.GetPrice(); }
        }

        #region Payment

        #region New Payment Code

        public RelayCommand PayByCardCommand => new RelayCommand(() =>
        {
            // prevents exception, can't pay if there's no drink selected
            if (_selectedDrink == null) { return; }

            RemainingPriceToPay = _cardPaymentController.PayDrink(SelectedPaymentCardUsername, RemainingPriceToPay);

            double cardAmountLeft = _cardPaymentController.GetCardAmountLeft(SelectedPaymentCardUsername);
            LogText.Add($"Card Amount Left {cardAmountLeft.ToString("C", _currentCulture)}, Remaining Price to Pay: {RemainingPriceToPay.ToString("C", _currentCulture)}.");
            CheckPriceToPay();
            RaisePropertyChanged(() => PaymentCardRemainingAmount);
        });

        public ICommand PayByCoinCommand => new RelayCommand<double>(coinValue =>
        {
            // prevents exception, can't pay if there's no drink selected
            if (_selectedDrink == null) { return; }
            RemainingPriceToPay = _cashPaymentController.PayDrink(coinValue, RemainingPriceToPay);
            LogText.Add($"Inserted {coinValue.ToString("C", _currentCulture)}, Remaining: {RemainingPriceToPay.ToString("C", _currentCulture)}.");
            CheckPriceToPay();
        });

        public double PaymentCardRemainingAmount => _cardPaymentController.GetCardAmountLeft(SelectedPaymentCardUsername);

        private void CheckPriceToPay()
        {
            if (RemainingPriceToPay == 0.0)
            {
                _selectedDrink.LogDrinkMaking(LogText);
                LogText.Add("------------------");
                _selectedDrink = null;
            }
        }

        #endregion New Payment Code

        #region Original Remaining Payment Code

        public ObservableCollection<string> PaymentCardUsernames { get; set; }

        private string _selectedPaymentCardUsername;
        public string SelectedPaymentCardUsername
        {
            get { return _selectedPaymentCardUsername; }
            set
            {
                _selectedPaymentCardUsername = value;
                RaisePropertyChanged(() => SelectedPaymentCardUsername);
                RaisePropertyChanged(() => PaymentCardRemainingAmount);
            }
        }

        private double _remainingPriceToPay;
        public double RemainingPriceToPay
        {
            get { return _remainingPriceToPay; }
            set { _remainingPriceToPay = value; RaisePropertyChanged(() => RemainingPriceToPay); }
        }
        
        #endregion Original Remaining Payment Code

        #endregion Payment

        #region Coffee buttons

        private Strength _coffeeStrength;
        public Strength CoffeeStrength
        {
            get { return _coffeeStrength; }
            set { _coffeeStrength = value; RaisePropertyChanged(() => CoffeeStrength); }
        }

        private Amount _sugarAmount;
        public Amount SugarAmount
        {
            get { return _sugarAmount; }
            set { _sugarAmount = value; RaisePropertyChanged(() => SugarAmount); }
        }

        private Amount _milkAmount;
        public Amount MilkAmount
        {
            get { return _milkAmount; }
            set { _milkAmount = value; RaisePropertyChanged(() => MilkAmount); }
        }

        #region Newly Added Coffee (tea) Buttons Code

        private ObservableCollection<string> _teaBlendNames;
        public ObservableCollection<string> TeaBlendNames
        {
            get => _teaBlendNames;
            set { _teaBlendNames = value; RaisePropertyChanged(() => TeaBlendNames); }
        }

        private string _selectedTeaBlend;
        public string SelectedTeaBlend
        {
            get { return _selectedTeaBlend; }
            set { _selectedTeaBlend = value; RaisePropertyChanged(() => SelectedTeaBlend); }
        }

        public ICommand DrinkCommand => new RelayCommand<DrinkInformation>((drinkInformation) =>
        {
            drinkInformation.Blend = _teaBlendRepository.GetTeaBlend(SelectedTeaBlend);

            switch (drinkInformation.Type)
            {
                case DrinkTypes.Normal:
                    _selectedDrink = DrinkFactory.CreateDrink(drinkInformation, CoffeeStrength, MilkAmount, SugarAmount);
                    RemainingPriceToPay = _selectedDrink.GetPrice();
                    LogText.Add($"Selected {SelectedDrinkName}, price: {RemainingPriceToPay.ToString("C", _currentCulture)}");
                    break;
                case DrinkTypes.Sugar:
                    _selectedDrink = DrinkFactory.CreateDrink(drinkInformation, CoffeeStrength, MilkAmount, SugarAmount);
                    RemainingPriceToPay = _selectedDrink.GetPrice();
                    LogText.Add($"Selected {SelectedDrinkName} with sugar, price: {RemainingPriceToPay.ToString("C", _currentCulture)}");
                    break;
                case DrinkTypes.Milk:
                    _selectedDrink = DrinkFactory.CreateDrink(drinkInformation, CoffeeStrength, MilkAmount, SugarAmount);
                    RemainingPriceToPay = _selectedDrink.GetPrice();
                    LogText.Add($"Selected {SelectedDrinkName} with milk, price: {RemainingPriceToPay.ToString("C", _currentCulture)}");
                    break;
                case DrinkTypes.SugarMilk:
                    _selectedDrink = DrinkFactory.CreateDrink(drinkInformation, CoffeeStrength, MilkAmount, SugarAmount);
                    RemainingPriceToPay = _selectedDrink.GetPrice();
                    LogText.Add($"Selected {SelectedDrinkName} with sugar & milk, price: {RemainingPriceToPay.ToString("C", _currentCulture)}");
                    break;
            }

            RaisePropertyChanged(() => RemainingPriceToPay);
            RaisePropertyChanged(() => SelectedDrinkName);
            RaisePropertyChanged(() => SelectedDrinkPrice);
        });

        #endregion Newly Added Coffee (tea) Buttons Code

        #endregion Coffee buttons
    }
}