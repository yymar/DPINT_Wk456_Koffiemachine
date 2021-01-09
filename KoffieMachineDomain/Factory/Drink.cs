using KoffieMachineDomain.Enums;
using KoffieMachineDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeaAndChocoLibrary;

namespace KoffieMachineDomain
{
    public class Drink : IDrink
    {
        //public static readonly double SugarPrice = 0.1;
        //public static readonly double MilkPrice = 0.15;

        protected const double BaseDrinkPrice = 1.0;

        public Drink(DrinkInformation info, Strength strength, Amount milkAmount, Amount sugarAmount)
        {
            // set initial values
            DrinkInformation = info;
            Name = info.Name;
            DrinkType = info.Type;
            TeaBlend = info.Blend;

            BasePrice = BaseDrinkPrice;
            Strength = strength;
            MilkAmount = milkAmount;
            SugarAmount = sugarAmount;
        }

        public DrinkInformation DrinkInformation { get; set; }
        public string Name { get; set; }
        public DrinkTypes DrinkType { get; set; }
        public TeaBlend TeaBlend { get; set; }
        public double BasePrice { get; set; }
        public Strength Strength { get; set; }
        public Amount MilkAmount { get; set; }
        public Amount SugarAmount { get; set; }

        public double GetPrice()
        {
            return BasePrice;
        }

        public ICollection<string> LogDrinkMaking(ICollection<string> log)
        {
            log.Add($"Making {Name}...");
            log.Add($"Heating up...");
            return log;
        }
    }
}
