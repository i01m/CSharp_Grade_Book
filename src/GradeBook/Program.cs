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
            var book = new Book("Developer's Grade Book");
            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.5);
            book.ShowStatistics();            
        }
    }    
}
