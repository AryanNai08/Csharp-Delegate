namespace CallbackUsingDelegate
{
    // Delegate that accepts int parameter and returns void
    // Used for callback methods like Print or Cost
    public delegate void PrintHelpDel(int amount);

    // Delegate that accepts string parameter
    public delegate void MessageDel(string message);

    public class Program
    {
        static void Main(string[] args)
        {
            // Creating object of Program class (needed for instance methods)
            Program p = new Program();

            // PASSING DELEGATE AS PARAMETER (Callback concept)
            // Print method reference passed to PrintNumber
            p.PrintNumber(100, p.Print);

            // Cost method reference passed to PrintNumber
            p.PrintNumber(200, p.Cost);

            // METHOD RETURNING DELEGATE
            // Demo() returns a delegate which points to Message method
            // Then we immediately call returned delegate
            p.Demo()("Hello from Demo");
        }

        // Method 1: matches delegate signature (int parameter)
        public void Print(int price)
        {
            Console.WriteLine($"Price: {price}");
        }

        // Method 2: matches delegate signature (int parameter)
        public void Cost(int cost)
        {
            Console.WriteLine($"Cost: {cost}");
        }

        // Method that accepts delegate as parameter
        // This is CALLBACK FUNCTION concept
        public void PrintNumber(int amount, PrintHelpDel printHelpDel)
        {
            // Calling delegate → actually calling Print or Cost
            printHelpDel(amount);
        }

        // Static method matching MessageDel delegate signature
        public static void Message(string message)
        {
            Console.WriteLine($"Message: {message}");
        }

        // Method returning delegate
        // Returns reference of Message method
        public MessageDel Demo()
        {
            return Message;
        }
    }
}
