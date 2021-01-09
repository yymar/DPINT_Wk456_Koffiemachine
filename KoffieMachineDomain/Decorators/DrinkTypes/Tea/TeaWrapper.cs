using KoffieMachineDomain.Decorators;
using KoffieMachineDomain.Enums;
using KoffieMachineDomain.Interfaces;
using System.Collections.Generic;

namespace KoffieMachineDomain
{
    using TeaAndChocoLibrary;

    public class TeaWrapper : BaseDrinkDecorator
    {
        private readonly Tea _tea;
        public TeaWrapper(IDrink drink, Tea tea) : base(drink)
        {
            _drink = drink;
            _tea = tea;
            TeaBlend = tea.Blend;

            BasePrice = 1.33;
            Name = "Tea";
            SugarAmount = (Amount)tea.AmountOfSugar;
        }

        public Amount SugarAmount { get; set; }
        public TeaBlend TeaBlend { get; set; }

        public override ICollection<string> LogDrinkMaking(ICollection<string> log)
        {
            log.Add("Filling with hot water...");
            log.Add($"Finished making {Name} with {TeaBlend.Name}");
            return log;
        }
    }
}
