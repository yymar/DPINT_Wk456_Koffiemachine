using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain.Payments
{
    public class CashPaymentController
    {
        public double PayDrink(double cashAmount, double price)
        {
            // returns any cashAmount higher than 0.0 if not, returns 0.0
            return Math.Max(Math.Round(price - cashAmount, 2), 0.0);
        }
    }
}
