using KoffieMachineDomain.Decorators;
using KoffieMachineDomain.Enums;
using KoffieMachineDomain.Interfaces;
using System.Collections.Generic;

namespace KoffieMachineDomain
{
    public class IrishCoffee : BaseDrinkDecorator
    {
        public IrishCoffee(IDrink drink, Strength coffeeStrength) : base(drink)
        {
            _drink = drink;
            Name = "Irish Coffee";
            DrinkStrength = coffeeStrength;
            BasePrice = 5;
        }

        public Strength DrinkStrength { get; set; }

        public override ICollection<string> LogDrinkMaking(ICollection<string> log)
        {
            log.Add($"Setting coffee strength to {DrinkStrength}.");
            log.Add("Filling with whisky...");
            log.Add("Filling with coffee...");
            log.Add("Filling with sugar...");
            log.Add("Filling with whipcream...");
            log.Add($"Finished making {Name}");
            return log;
        }
    }
}
