using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        // "static" means that the method only associated with a class and ONLY available with that class
        //static method cannot be available with the object of that class        
        {
            IBook book = new DiskBook("Developer's Grade Book");
            book.GradeAdded += OnGradeAdded;

            EnterGrades(book);

            var stats = book.GetStatistics();

            Console.WriteLine($"The lowest grade is {stats.Low}");
            Console.WriteLine($"The highest grade is {stats.High}");
            Console.WriteLine($"The average grade is {stats.Average:N1}");
            Console.WriteLine($"The letter grade is {stats.Letter}");

            Console.WriteLine("\nPress any key to exit ...");
            Console.ReadKey();
        }

        private static void EnterGrades(IBook book)
        {
            while (true)
            {
                Console.Write("Enter another grade or a letter q to display results: ");
                var input = Console.ReadLine();

                if (input == "q")
                {
                    break;
                }

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    //*** this code executes regardless of anything above it
                    //could be useful if there is need to closed network socket or file or ect.
                }
            }
        }

        static void OnGradeAdded(object sender, EventArgs args)
        {
            Console.WriteLine("Grade added");
        }
    }
}
