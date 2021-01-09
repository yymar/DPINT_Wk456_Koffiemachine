using KoffieMachineDomain.Decorators;
using KoffieMachineDomain.Enums;
using KoffieMachineDomain.Interfaces;
using System.Collections.Generic;

namespace KoffieMachineDomain
{
    public class Espresso : BaseDrinkDecorator
    {
        public Espresso(IDrink drink, Strength coffeeStrength) : base(drink)
        {
            _drink = drink;
            Name = "Espresso";
            DrinkStrength = coffeeStrength;
            BasePrice = 2.50;
        }

        public Strength DrinkStrength { get; set; }

        public override ICollection<string> LogDrinkMaking(ICollection<string> log)
        {
            log.Add($"Setting coffee strength to {DrinkStrength}.");
            log.Add("Filling with Espresso...");
            log.Add($"Finished making {Name}");
            return log;
        }
    }
}