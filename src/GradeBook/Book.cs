using System;
using System.Collections.Generic;


namespace GradeBook
{
    public class Book
    {

        
        public Book(string name)
        {
            grades = new List<double>();
            this.name = name; // "this" says that find the field with the name "name" in current class and assign incoming variable "name" to it
        }

        public void AddGrade(double grade)
        {
            grades.Add(grade);
        }

        public void ShowStatistics(){
            var result = 0.0;
            var highGrade = double.MinValue; // assigns the most min value from the class double
            //its also example of using a STATIC method that belongs to a type (double) and not to it objects

            var lowGrade = double.MaxValue;
            foreach (double number in grades)
            {
                highGrade = System.Math.Max(number,highGrade);
                lowGrade = Math.Min(number,lowGrade);
                result += number; 
            }
            
            result /= grades.Count;
            Console.WriteLine($"The lowest grade is {lowGrade}");                
            Console.WriteLine ($"The highest grade is {highGrade}");
            Console.WriteLine($"The average grade is {result:N1}");
        }   

        private List<double> grades;
        private string name;
    }
}