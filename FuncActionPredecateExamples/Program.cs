namespace FuncActionPredecateExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            // ===============================
            // STUDENT LIST
            // ===============================
            List<Student> students = new List<Student>()
            {
                new Student{Id=1, Name="Aryan", Marks=85, City="Ahmedabad"},
                new Student{Id=2, Name="Katik", Marks=45, City="Surat"},
                new Student{Id=3, Name="Priya", Marks=72, City="Ahmedabad"},
                new Student{Id=4, Name="Karan", Marks=30, City="Baroda"}
            };

            // ============================================================
            // 1️⃣ FUNC EXAMPLE (returns value)
            // Used when we need calculation or result
            // ============================================================

            // Func<parameter, returnType>
            // Calculate grade based on marks
            Func<int, string> getGrade = (marks) =>
            {
                if (marks >= 70) return "A";
                else if (marks >= 50) return "B";
                else return "Fail";
            };

            Console.WriteLine("=== FUNC Example (Grade Calculation) ===");
            foreach (var s in students)
            {
                string grade = getGrade(s.Marks); // calling func
                Console.WriteLine($"{s.Name} Grade: {grade}");
            }


            // ============================================================
            // 2️⃣ ACTION EXAMPLE (no return)
            // Used for printing/logging
            // ============================================================

            // Action<parameters> → no return
            Action<Student> printStudent = (s) =>
            {
                Console.WriteLine($"Id:{s.Id} Name:{s.Name} Marks:{s.Marks} City:{s.City}");
            };

            Console.WriteLine("\n=== ACTION Example (Print Students) ===");
            students.ForEach(printStudent); // calling action for each student


            // ============================================================
            // 3️⃣ PREDICATE EXAMPLE (returns bool)
            // Used for filtering data
            // ============================================================

            // Predicate<T> returns true/false
            Predicate<Student> isPassed = (s) => s.Marks >= 50;

            List<Student> passedStudents = students.FilterStudents(isPassed);

            Console.WriteLine("\n=== PREDICATE Example (Passed Students) ===");
            foreach (var s in passedStudents)
                Console.WriteLine($"{s.Name} Passed");


            // ============================================================
            // COMBINED REAL USAGE (VERY IMPORTANT)
            // Filter + calculate + print
            // ============================================================

            Console.WriteLine("\n=== REAL COMBINATION ===");

            students
                .FilterStudents(s => s.City == "Ahmedabad") // Predicate filter
                .ForEach(s =>
                {
                    string grade = getGrade(s.Marks); // Func
                    printStudent(s);                  // Action
                    Console.WriteLine($"Grade: {grade}\n");
                });

            Console.ReadKey();
        }
    }

    // ============================================================
    // EXTENSION METHOD (Custom LINQ like Where)
    // ============================================================
    public static class StudentExtension
    {
        // this keyword → makes it extension method
        public static List<Student> FilterStudents(this List<Student> students, Predicate<Student> predicate)
        {
            List<Student> result = new List<Student>();

            foreach (var s in students)
            {
                if (predicate(s)) // condition check
                    result.Add(s);
            }
            return result;
        }
    }

    // ============================================================
    // MODEL CLASS
    // ============================================================
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Marks { get; set; }
        public string City { get; set; }
    }
}
