using KoffieMachineDomain.Enums;
using TeaAndChocoLibrary;

namespace KoffieMachineDomain
{
    public class DrinkInformation
    {
        public string Name { get; set; }
        public DrinkTypes Type { get; set; }
        public TeaBlend Blend { get; set; }
    }
}
