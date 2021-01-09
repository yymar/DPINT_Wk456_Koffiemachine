using KoffieMachineDomain.Decorators;
using KoffieMachineDomain.Enums;
using KoffieMachineDomain.Interfaces;
using System.Collections.Generic;

namespace KoffieMachineDomain
{
    using TeaAndChocoLibrary;

    public class HotChocolateWrapper : BaseDrinkDecorator
    {
        private readonly HotChocolate _hotChocolate;

        public HotChocolateWrapper(IDrink drink, HotChocolate choco) : base(drink)
        {
            _drink = drink;
            _hotChocolate = choco;
            Name = choco.GetNameOfDrink();
            BasePrice = 1.20;

        }

        public override double GetPrice()
        {
            return BasePrice;
        }

        public override ICollection<string> LogDrinkMaking(ICollection<string> log)
        {
            var buildsteps = _hotChocolate.GetBuildSteps();

            foreach (string s in buildsteps)
            {
                log.Add(s);
            }

            return log;
        }
    }
}
