using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using KoffieMachineDomain;
using KoffieMachineDomain.Decorators;
using KoffieMachineDomain.Enums;
using KoffieMachineDomain.Factory;
using KoffieMachineDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using TeaAndChocoLibrary;

namespace Dpint_wk456_KoffieMachine.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Dictionary<string, double> _cashOnCards;
        public ObservableCollection<string> LogText { get; private set; }

        // additions
        private TeaBlendRepository _teaBlendRepository;
        private CultureInfo _currentCulture;

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

            _cashOnCards = new Dictionary<string, double>();
            _cashOnCards["Arjen"] = 5.0;
            _cashOnCards["Bert"] = 3.5;
            _cashOnCards["Chris"] = 7.0;
            _cashOnCards["Daan"] = 6.0;

            PaymentCardUsernames = new ObservableCollection<string>(_cashOnCards.Keys);
            SelectedPaymentCardUsername = PaymentCardUsernames[0];

            _teaBlendRepository = new TeaBlendRepository();
          
            TeaBlendNames = new ObservableCollection<string>(_teaBlendRepository.BlendNames);
            SelectedTeaBlend = TeaBlendNames[0];

            // puts euro sign at the front of the numbers instead of behind them.
            _currentCulture = new CultureInfo("nl-NL");
            _currentCulture.NumberFormat.CurrencyPositivePattern = 0;
            _currentCulture.NumberFormat.CurrencyNegativePattern = 2;
            _currentCulture.NumberFormat.CurrencyDecimalSeparator = ".";
        }

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

        public RelayCommand PayByCardCommand => new RelayCommand(() =>
        {
            PayDrink(payWithCard: true);
        });

        public ICommand PayByCoinCommand => new RelayCommand<double>(coinValue =>
        {
            PayDrink(payWithCard: false, insertedMoney: coinValue);
        });

        private void PayDrink(bool payWithCard, double insertedMoney = 0)
        {
            if (_selectedDrink != null && payWithCard)
            {
                insertedMoney = _cashOnCards[SelectedPaymentCardUsername];
                if (RemainingPriceToPay <= insertedMoney)
                {
                    _cashOnCards[SelectedPaymentCardUsername] = insertedMoney - RemainingPriceToPay;
                    RemainingPriceToPay = 0;
                }
                else // Pay what you can, fill up with coins later.
                {
                    _cashOnCards[SelectedPaymentCardUsername] = 0;

                    RemainingPriceToPay -= insertedMoney;
                }
                LogText.Add($"Inserted {insertedMoney.ToString("C", CultureInfo.CurrentCulture)}, Remaining: {RemainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}.");
                RaisePropertyChanged(() => PaymentCardRemainingAmount);
            }
            else if (_selectedDrink != null && !payWithCard)
            {
                RemainingPriceToPay = Math.Max(Math.Round(RemainingPriceToPay - insertedMoney, 2), 0);
                LogText.Add($"Inserted {insertedMoney.ToString("C", CultureInfo.CurrentCulture)}, Remaining: {RemainingPriceToPay.ToString("C", CultureInfo.CurrentCulture)}.");
            }

            if (_selectedDrink != null && RemainingPriceToPay == 0)
            {
                _selectedDrink.LogDrinkMaking(LogText);
                LogText.Add("------------------");
                _selectedDrink = null;
            }
        }

        public double PaymentCardRemainingAmount => _cashOnCards.ContainsKey(SelectedPaymentCardUsername ?? "") ? _cashOnCards[SelectedPaymentCardUsername] : 0;

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

        // added 
        private string _selectedTeaBlend;
        public string SelectedTeaBlend
        {
            get { return _selectedTeaBlend; }
            set { _selectedTeaBlend = value; RaisePropertyChanged(() => SelectedTeaBlend); }
        }

        private ObservableCollection<string> _teaBlendNames;

        public ObservableCollection<string> TeaBlendNames
        {
            get => _teaBlendNames;
            set { _teaBlendNames = value; RaisePropertyChanged(() => TeaBlendNames); }
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

            if (CheckSelectedDrink(drinkInformation.Name))
                return;
            RaisePropertyChanged(() => RemainingPriceToPay);
            RaisePropertyChanged(() => SelectedDrinkName);
            RaisePropertyChanged(() => SelectedDrinkPrice);
        });

        //Helper method
        private bool CheckSelectedDrink(string drinkName)
        {
            if (_selectedDrink == null)
            {
                LogText.Add($"Could not make {drinkName}, recipe not found.");
                return true;
            }
            return false;
        }

        #endregion Coffee buttons
    }
}