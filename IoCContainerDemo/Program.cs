using System;

namespace IoCContainerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Resolver resolver = new Resolver();

            resolver.Register<Shopper, Shopper>();
            //resolver.Register<ICreditCard, MasterCard>();
            resolver.Register<ICreditCard, Visa>();


            var shopper = resolver.Resolve<Shopper>();

            shopper.Charge();

            Console.Read();
        }
    }

    public class Visa : ICreditCard
    {
        public string Charge()
        {
            return "Charging with the Visa!";
        }
    }

    public class MasterCard : ICreditCard
    {
        public string Charge()
        {
            return "Swiping the MasterCard!";
        }
    }

    public class Shopper
    {
        private readonly ICreditCard creditCard;

        public Shopper(ICreditCard creditCard)
        {
            this.creditCard = creditCard;
        }

        public void Charge()
        {
            var chargeMessage = creditCard.Charge();
            Console.WriteLine(chargeMessage);
        }
    }

    public interface ICreditCard
    {
        string Charge();
    }
}
