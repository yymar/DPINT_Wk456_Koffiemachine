using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain
{
    public class HotChocolate : Drink
    {
        public bool IsDeluxe { get; set; }

        public override string Name 
        {
            get
            {
                if (IsDeluxe)
                {
                    return "Chocomel deluxe";
                }
                else
                {
                    return "Chocomel";
                }
            }
        }

        public override double GetPrice()
        {
            double price = BaseDrinkPrice * 1.5;
            if (IsDeluxe)
            {
                price += 0.25;
            }

            return price;
        }

        public override void LogDrinkMaking(ICollection<string> log)
        {
            base.LogDrinkMaking(log);
            log.Add("Filling with milk...");
            log.Add("Adding chocolate...");

            if (IsDeluxe)
            {
                log.Add("Adding cream topping...");
                log.Add("Adding cinnamon topping...");
            }

            log.Add($"Finished making {Name}");
        }
    }
}
