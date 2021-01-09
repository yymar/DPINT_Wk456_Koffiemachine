using KoffieMachineDomain.Decorators;
using KoffieMachineDomain.Enums;
using KoffieMachineDomain.Interfaces;
using System.Collections.Generic;

namespace KoffieMachineDomain
{
    public class WienerMelange : BaseDrinkDecorator
    {
        public WienerMelange(IDrink drink, Strength coffeeStrength) : base(drink)
        {
            _drink = drink;
            Name = "Wiener Melange";
            DrinkStrength = coffeeStrength;
            BasePrice = 1.50;
        }

        public Strength DrinkStrength { get; set; }

        public override double GetPrice()
        {
            return BasePrice;
        }

        public override ICollection<string> LogDrinkMaking(ICollection<string> log)
        {
            log.Add($"Setting coffee strength to {DrinkStrength}.");
            log.Add("Filling with coffee...");
            log.Add($"Finished making {Name}");
            return log;
        }
    }
}
