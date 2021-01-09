using KoffieMachineDomain.Decorators;
using KoffieMachineDomain.Enums;
using KoffieMachineDomain.Interfaces;
using System.Collections.Generic;

namespace KoffieMachineDomain
{
    using TeaAndChocoLibrary;
    public class CoffeeChoc : BaseDrinkDecorator
    {
        private readonly HotChocolate _hotChocolate;
        private readonly Coffee _coffee;

        public CoffeeChoc(IDrink drink, HotChocolate choco, Coffee coffee) : base(drink)
        {
            _drink = drink;
            _hotChocolate = choco;
            _coffee = coffee;
            Name = choco.GetNameOfDrink() + " Coffee";
            BasePrice = 5;
        }

        public override double GetPrice()
        {
            return BasePrice;
        }

        public override ICollection<string> LogDrinkMaking(ICollection<string> log)
        {
            // Choc
            var buildsteps = _hotChocolate.GetBuildSteps();
            foreach (string s in buildsteps)
            {
                log.Add(s);
            }
            // Coffee
            _coffee.LogDrinkMaking(log);

            // CoffeeChoc
            log.Add($"Mixing to make {Name}");

            return log;
        }
    }
}
