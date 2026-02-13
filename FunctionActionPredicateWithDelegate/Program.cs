namespace FunctionActionPredicateWithDelegate
{
    class Program
    {
        // ============================================
        // CUSTOM DELEGATE (user defined)
        // used for grade calculation
        // ============================================
        delegate string GradeDelegate(int marks);

        static void Main(string[] args)
        {
            Console.WriteLine("=== Delegate + Func + Action + Predicate Demo ===\n");

            // Student list
            List<Student> students = new List<Student>()
            {
                new Student{Id=1, Name="Aryan", Marks=85},
                new Student{Id=2, Name="Rahul", Marks=40},
                new Student{Id=3, Name="Priya", Marks=72},
                new Student{Id=4, Name="Amit", Marks=30}
            };

            // ====================================================
            // 1. CUSTOM DELEGATE USAGE
            // ====================================================
            Console.WriteLine("---- Custom Delegate Example ----");

            GradeDelegate gradeDel = GetGrade; // delegate holds method
            foreach (var s in students)
            {
                string grade = gradeDel(s.Marks); // delegate call
                Console.WriteLine($"{s.Name} Grade: {grade}");
            }


            // ====================================================
            // 2. FUNC (returns value)
            // Func<parameter, return>
            // ====================================================
            Console.WriteLine("\n---- Func Example ----");

            Func<int, int, int> add = (a, b) => a + b;
            int sum = add(10, 20);
            Console.WriteLine($"Sum using Func: {sum}");

            // Func to calculate bonus salary
            Func<int, int> bonus = m => m + 5;
            Console.WriteLine($"Bonus marks added: {bonus(80)}");


            // ====================================================
            // 3. ACTION (no return)
            // used for printing/logging
            // ====================================================
            Console.WriteLine("\n---- Action Example ----");

            Action<Student> printStudent = s =>
                Console.WriteLine($"Id:{s.Id} Name:{s.Name} Marks:{s.Marks}");

            students.ForEach(printStudent); // action called


            // ====================================================
            // 4. PREDICATE (returns bool)
            // used for filtering
            // ====================================================
            Console.WriteLine("\n---- Predicate Example ----");

            Predicate<Student> isPass = s => s.Marks >= 50;

            List<Student> passed = students.FindAll(isPass);

            Console.WriteLine("Passed Students:");
            foreach (var s in passed)
                Console.WriteLine(s.Name);


            // ====================================================
            // 5. MULTICAST DELEGATE
            // multiple methods in one delegate
            // ====================================================
            Console.WriteLine("\n---- Multicast Delegate ----");

            Action<string> log = LogToScreen;
            log += LogToFile; // multicast

            log("System started");


            // ====================================================
            // 6. DELEGATE AS PARAMETER
            // ====================================================
            Console.WriteLine("\n---- Delegate as Parameter ----");

            ProcessNumber(100, PrintNumber);
            ProcessNumber(200, PrintDouble);

            Console.ReadKey();
        }

        // ====================================================
        // METHOD FOR CUSTOM DELEGATE
        // ====================================================
        static string GetGrade(int marks)
        {
            if (marks >= 75) return "A";
            if (marks >= 50) return "B";
            return "Fail";
        }

        // ====================================================
        // MULTICAST METHODS
        // ====================================================
        static void LogToScreen(string msg)
        {
            Console.WriteLine($"Screen Log: {msg}");
        }

        static void LogToFile(string msg)
        {
            Console.WriteLine($"File Log: {msg}");
        }

        // ====================================================
        // DELEGATE AS PARAMETER METHODS
        // ====================================================
        static void ProcessNumber(int num, Action<int> action)
        {
            action(num);
        }

        static void PrintNumber(int n)
        {
            Console.WriteLine($"Number: {n}");
        }

        static void PrintDouble(int n)
        {
            Console.WriteLine($"Double: {n * 2}");
        }
    }

    // ====================================================
    // STUDENT MODEL CLASS
    // ====================================================
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Marks { get; set; }
    }
}
