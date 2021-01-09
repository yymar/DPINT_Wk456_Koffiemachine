using KoffieMachineDomain.Interfaces;
using System;
using System.Collections.Generic;
  
namespace KoffieMachineDomain.Decorators
{
    public abstract class BaseDrinkDecorator : IDrink
    {
        public string Name { get => _drink.Name; set => _drink.Name = value; }
        public double BasePrice { get => _drink.BasePrice; set => _drink.BasePrice = value; }

        protected IDrink _drink;

        public BaseDrinkDecorator(IDrink drink)
        {
            _drink = drink;
        }

        public virtual double GetPrice()
        {
            if (_drink != null)
            {
                return _drink.BasePrice;
            }
            else
            {
                return 0.0;
            }
        }

        public virtual ICollection<string> LogDrinkMaking(ICollection<string> log)
        {
            if (_drink != null)
            {
                return _drink.LogDrinkMaking(log);
            }
            else
            {
                Console.WriteLine("_drink == null");
                return null;
            }
        }
    }
}
