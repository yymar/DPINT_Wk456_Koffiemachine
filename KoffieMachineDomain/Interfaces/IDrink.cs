using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain.Interfaces
{
    public interface IDrink
    {
        string Name { get; set; }
        double BasePrice { get; set; }
        double GetPrice();
        ICollection<string> LogDrinkMaking(ICollection<string> log);
    }
}
