using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoffieMachineDomain.Payments
{
    public class CardPaymentController
    {
        private Dictionary<string, double> _cashOnCards;

        public CardPaymentController()
        {
            // hard codes users & amounts of balance
            InitialiseCards();
        }

        public void InitialiseCards()
        {
            _cashOnCards = new Dictionary<string, double>
            {
                ["Arjen"] = 5.0,
                ["Martijn"] = 3.5,
                ["Yoran"] = 7.0,
                ["Daan"] = 6.0
            };
        }
        public double PayDrink(string name, double price)
        {
            double cashOnCard = GetCardAmountLeft(name);

            if (price <= cashOnCard)
            {
                _cashOnCards[name] = cashOnCard - price;
                return 0.0;
            }
            else
            {
                _cashOnCards[name] = 0.0;
                return price - cashOnCard;
            }
        }

        public ICollection<string> GetCardNames()
        {
            return _cashOnCards.Keys;
        }

        public double GetCardAmountLeft(string name)
        {
            if (_cashOnCards.ContainsKey(name))
            {
                return _cashOnCards[name];
            }

            return -1.0;
        }
    }
}
