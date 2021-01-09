using KoffieMachineDomain.Decorators;
using KoffieMachineDomain.Enums;
using KoffieMachineDomain.Interfaces;
using System.Collections.Generic;

namespace KoffieMachineDomain
{
    public class Capuccino : BaseDrinkDecorator
    {
        public Capuccino(IDrink drink, Strength coffeeStrength) : base(drink)
        {
            _drink = drink;
            Name = "Espresso";
            DrinkStrength = coffeeStrength;
            BasePrice = 2.50;
        }

        public Strength DrinkStrength { get; set; }

        public override double GetPrice()
        {
            return BasePrice;
        }

        public override ICollection<string> LogDrinkMaking(ICollection<string> log)
        {
            log.Add($"Setting coffee strength to {DrinkStrength}.");
            log.Add("Filling with Cappuccino...");
            log.Add($"Finished making {Name}");
            return log;
        }
    }
}