// ===============================================================
// COVARIANCE & CONTRAVARIANCE DELEGATE EXAMPLE (DIFFERENT EXAMPLE)
// Real-world example: Payment Processing System
// Demonstrates:
// 1. Covariance → return derived(Child) type instead of base
// 2. Contravariance → accept base(Parent) parameter instead of derived
// ===============================================================

using System;
using System.IO;

namespace CovarianceContravarianceExample
{
    internal class Program
    {
        // =========================================================
        // COVARIANCE DELEGATE
        // Delegate expects base class return type (Payment)
        // But method can return derived type (CreditCard, UPI)
        // =========================================================
        delegate Payment PaymentCreator(string user);

        // =========================================================
        // CONTRAVARIANCE DELEGATE
        // Delegate expects derived type parameter
        // But method can accept base type
        // =========================================================
        delegate void ProcessCreditCard(CreditCard payment);
        delegate void ProcessUPI(UPI payment);

        static void Main(string[] args)
        {
            // ==================== COVARIANCE ====================
            // Delegate expecting Payment
            // But method returning CreditCard (derived)
            PaymentCreator creator = PaymentFactory.CreateCreditCard;

            Payment p1 = creator("Aryan");
            Console.WriteLine(p1.GetInfo());

            // Assign method returning UPI (derived)
            creator = PaymentFactory.CreateUPI;
            Payment p2 = creator("Rahul");
            Console.WriteLine(p2.GetInfo());


            Console.WriteLine("\n--- CONTRAVARIANCE ---");

            // ==================== CONTRAVARIANCE ====================
            // Delegate expects CreditCard
            // But method accepts base Payment
            ProcessCreditCard creditDel = LogPayment;
            creditDel(p1 as CreditCard);

            // Delegate expects UPI
            ProcessUPI upiDel = LogPayment;
            upiDel(p2 as UPI);

            Console.ReadKey();
        }


        // =========================================================
        // COMMON METHOD (BASE CLASS PARAMETER)
        // This method accepts Payment (base class)
        // But delegates expect derived types → contravariance
        // =========================================================
        static void LogPayment(Payment payment)
        {
            if (payment is CreditCard)
            {
                using (StreamWriter sw = new StreamWriter("credit_log.txt", true))
                {
                    sw.WriteLine(payment.GetInfo());
                    Console.WriteLine("Credit card log saved");
                }
            }
            else if (payment is UPI)
            {
                using (StreamWriter sw = new StreamWriter("upi_log.txt", true))
                {
                    sw.WriteLine(payment.GetInfo());
                    Console.WriteLine("UPI log saved");
                }
            }
        }


        // =========================================================
        // FACTORY CLASS → returns derived objects
        // =========================================================
        public static class PaymentFactory
        {
            public static CreditCard CreateCreditCard(string user)
            {
                return new CreditCard { User = user, CardNumber = "1234-XXXX" };
            }

            public static UPI CreateUPI(string user)
            {
                return new UPI { User = user, UpiId = user + "@upi" };
            }
        }


        // =========================================================
        // BASE CLASS
        // =========================================================
        public abstract class Payment
        {
            public string User { get; set; }

            public virtual string GetInfo()
            {
                return $"User: {User}";
            }
        }

        // =========================================================
        // DERIVED CLASS 1
        // =========================================================
        public class CreditCard : Payment
        {
            public string CardNumber { get; set; }

            public override string GetInfo()
            {
                return $"CreditCard - {User} using {CardNumber}";
            }
        }

        // =========================================================
        // DERIVED CLASS 2
        // =========================================================
        public class UPI : Payment
        {
            public string UpiId { get; set; }

            public override string GetInfo()
            {
                return $"UPI - {User} using {UpiId}";
            }
        }
    }
}
