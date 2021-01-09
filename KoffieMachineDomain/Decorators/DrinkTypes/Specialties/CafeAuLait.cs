using KoffieMachineDomain.Decorators;
using KoffieMachineDomain.Enums;
using KoffieMachineDomain.Interfaces;
using System.Collections.Generic;

namespace KoffieMachineDomain
{
    public class CafeAuLait : BaseDrinkDecorator
    {
        public CafeAuLait(IDrink drink, Strength coffeeStrength) : base(drink)
        {
            Name = "CafeAuLait";
            DrinkStrength = coffeeStrength;
            BasePrice = 3.50;
        }
        public Strength DrinkStrength { get; set; }

        public override double GetPrice()
        {
            return BasePrice;
        }

        public override ICollection<string> LogDrinkMaking(ICollection<string> log)
        {
            log.Add($"Setting coffee strength to {DrinkStrength}.");
            log.Add("Filling with CafeAuLait...");
            log.Add($"Finished making {Name}");
            return base.LogDrinkMaking(log);
        }
    }
}
