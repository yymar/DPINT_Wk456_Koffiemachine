using KoffieMachineDomain.Decorators;
using KoffieMachineDomain.Enums;
using KoffieMachineDomain.Interfaces;
using TeaAndChocoLibrary;

namespace KoffieMachineDomain.Factory
{
    public class DrinkFactory
    {
        // all 12 options
        public const string COFFEE = "Coffee";
        public const string ESPRESSO = "Espresso";
        public const string CAPUCCINO = "Capuccino";
        public const string WIENER_MELANGE = "Wiener Melange";
        public const string CAFE_AU_LAIT = "Cafe au Lait";
        public const string CHOCOLATE = "Chocolate";
        public const string CHOCOLATE_DELUXE = "Chocolate Deluxe";
        public const string TEA = "Tea";
        public const string IRISH_COFFEE = "Irish Coffee";
        public const string SPANISH_COFFEE = "Spanish Coffee";
        public const string ITALIAN_COFFEE = "Italian Coffee";
        public const string COFFEE_CHOC = "CoffeeChoc";

        public static IDrink CreateDrink(DrinkInformation info, Strength strength, Amount milkAmount, Amount sugarAmount)
        {
            IDrink drink = new Drink(info, strength, milkAmount, sugarAmount);

            switch (info.Name)
            {
                case COFFEE:
                    if (info.Type is DrinkTypes.Milk)
                    {
                        return new MilkDrinkDecorator(new Coffee(drink, strength), milkAmount);
                    }
                    else if (info.Type is DrinkTypes.Sugar)
                    {
                        return new SugarDrinkDecorator(new Coffee(drink, strength), sugarAmount);
                    }
                    else if (info.Type is DrinkTypes.SugarMilk)
                    {
                        return new SugarDrinkDecorator(new MilkDrinkDecorator(new Coffee(drink, strength), milkAmount), sugarAmount);
                    }
                    return new Coffee(drink, strength);
                case ESPRESSO:
                    if (info.Type is DrinkTypes.Milk)
                    {
                        return new MilkDrinkDecorator(new Espresso(drink, strength), milkAmount);
                    }
                    else if (info.Type is DrinkTypes.Sugar)
                    {
                        return new SugarDrinkDecorator(new Espresso(drink, strength), sugarAmount);
                    }
                    else if (info.Type is DrinkTypes.SugarMilk)
                    {
                        return new SugarDrinkDecorator(new MilkDrinkDecorator(new Espresso(drink, strength), milkAmount), sugarAmount);
                    }
                    return new Espresso(drink, strength);
                case CAPUCCINO:
                    if (info.Type is DrinkTypes.Sugar)
                    {
                        return new SugarDrinkDecorator(new Capuccino(drink, strength), sugarAmount);
                    }
                    return new Capuccino(drink, strength);
                case WIENER_MELANGE:
                    if (info.Type is DrinkTypes.Sugar)
                    {
                        return new SugarDrinkDecorator(new WienerMelange(drink, strength), sugarAmount);
                    }
                    return new WienerMelange(drink, strength);
                case CAFE_AU_LAIT:
                    return new CafeAuLait(drink, strength);
                case CHOCOLATE:
                    return new HotChocolateWrapper(drink, new HotChocolate());
                case CHOCOLATE_DELUXE:
                    return new HotChocolateDeluxeWrapper(drink, new HotChocolate());
                case TEA:
                    if (info.Type is DrinkTypes.Sugar)
                    {
                        return new SugarDrinkDecorator(new TeaWrapper(drink, new Tea() { Blend = info.Blend }), sugarAmount);
                    }
                    return new TeaWrapper(drink, new Tea() { Blend = info.Blend });
                case IRISH_COFFEE:
                    return new IrishCoffee(drink, strength);
                case SPANISH_COFFEE:
                    return new SpanishCoffee(drink, strength);
                case ITALIAN_COFFEE:
                    return new ItalianCoffee(drink, strength);
                case COFFEE_CHOC:
                    return new CoffeeChoc(drink, new HotChocolate(), new Coffee(drink, strength));
                default:
                    return drink;
            }

            return drink;
        }
    }
}