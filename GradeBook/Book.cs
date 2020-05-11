using System;
using System.Collections.Generic;
using System.IO;


namespace GradeBook
{
    public delegate void GradeAddedDelegate(object sender, EventArgs args);

    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }

        //older way to write properties
        // public string Name
        //{
        //    get
        //    {
        //        return name;
        //    }

        //    set
        //    {
        //        if (!String.IsNullOrEmpty(value))
        //        {
        //            name = value;
        //        }
        //        else
        //        {
        //            Console.WriteLine("You are passing an empty name");
        //        }
        //    }
        //}

        //private string name;

        //new way to write properties-compiler generate the rest
        public string Name
        {
            get;
            set;
        }
    }
    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }

    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public abstract Statistics GetStatistics();
       
    }

    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {           
            Name = name;
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {

            using (var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
            }

            if (GradeAdded != null)
            {
               GradeAdded(this, new EventArgs());
            }           
        }

        public override Statistics GetStatistics()
        {

            var result = new Statistics();

            //for (var index = 0; index <)

            using (var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }

                return result;

            }
        }
    }


    public class InMemoryBook : Book
    {
        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            Name = name; // "this" says that find the field with the name "name" in current class and assign incoming variable "name" to it
        }

        public void AddGrade (char letter)
        {

            switch (letter)
            {
                case 'A':
                    AddGrade(90);
                    break;

                case 'B':
                    AddGrade(80);
                    break;

                case 'C':
                    AddGrade(70);
                    break;

                default:
                    AddGrade(0);
                    break;
            }
        }

        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {                
                grades.Add(grade);
                if (GradeAdded != null) //checking if there are some listeners
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public override event GradeAddedDelegate GradeAdded;

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
          
            foreach (double grade in grades)
            {
                result.Add(grade);               
            }
                       
            return result;
        }

        private List<double> grades;
               
       

    }
}