using KoffieMachineDomain.Enums;
using KoffieMachineDomain.Interfaces;
using System.Collections.Generic;

namespace KoffieMachineDomain.Decorators
{
    public class MilkDrinkDecorator : BaseDrinkDecorator
    {
        public Amount MilkAmount { get; set; }

        public MilkDrinkDecorator(IDrink drink, Amount milkAmount) : base(drink)
        {
            _drink = drink;
            MilkAmount = milkAmount;
            BasePrice += 0.15;
            Name = _drink.Name;
        }

        public override double GetPrice()
        {
            return _drink.GetPrice();
        }

        public override ICollection<string> LogDrinkMaking(ICollection<string> log)
        {
            log.Add($"Setting milk amount to {MilkAmount}.");
            log.Add("Adding milk...");

            return _drink.LogDrinkMaking(log);
        }
    }
}
