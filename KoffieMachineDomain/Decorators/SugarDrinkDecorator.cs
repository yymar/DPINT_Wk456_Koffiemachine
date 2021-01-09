using KoffieMachineDomain.Enums;
using KoffieMachineDomain.Interfaces;
using System.Collections.Generic;

namespace KoffieMachineDomain.Decorators
{
    public class SugarDrinkDecorator : BaseDrinkDecorator
    {
        public Amount SugarAmount { get; set; }

        public SugarDrinkDecorator(IDrink drink, Amount sugarAmount) : base(drink)
        {
            _drink = drink;
            SugarAmount = sugarAmount;
            Name = _drink.Name;
        }

        public override double GetPrice()
        {
            return _drink.GetPrice() + 0.10;
        }

        public override ICollection<string> LogDrinkMaking(ICollection<string> log)
        {
            log.Add($"Setting sugar amount to {SugarAmount}.");
            log.Add("Adding sugar...");
            return base.LogDrinkMaking(log);
        }
    }
}
