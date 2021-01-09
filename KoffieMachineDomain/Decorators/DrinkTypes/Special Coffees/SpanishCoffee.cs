using KoffieMachineDomain.Decorators;
using KoffieMachineDomain.Enums;
using KoffieMachineDomain.Interfaces;
using System.Collections.Generic;

namespace KoffieMachineDomain
{
    public class SpanishCoffee : BaseDrinkDecorator
    {
        public SpanishCoffee(IDrink drink, Strength coffeeStrength) : base(drink)
        {
            _drink = drink;
            Name = "Spanish Coffee";
            DrinkStrength = coffeeStrength;
            BasePrice = 5;
        }

        public Strength DrinkStrength { get; set; }

        public override ICollection<string> LogDrinkMaking(ICollection<string> log)
        {
            log.Add($"Setting coffee strength to {DrinkStrength}.");
            log.Add("Filling with cointreau...");
            log.Add("Filling with cognac...");
            log.Add("Filling with coffee...");
            log.Add("Filling with sugar...");
            log.Add("Filling with whipcream...");
            log.Add($"Finished making {Name}");
            return log;
        }
    }
}
