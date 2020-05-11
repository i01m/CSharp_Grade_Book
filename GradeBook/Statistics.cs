namespace GradeBook
{
    public class Statistics
    {


        public double Average
        {
            get
            {
                return Sum / Count;
            }
        }
        public double High;
        public double Low;
        public char Letter
        {
            get
            {
                switch (Average)
                {
                    case var d when d >= 90.0:
                        return 'A';                        

                    case var d when d >= 80.0:
                        return 'B';

                    case var d when d >= 70.0:
                        return 'C';

                    case var d when d >= 60.0:
                        return 'D';

                    default:
                        return 'F';

                }
            }
        }
        public double Sum;
        public int Count;

        public void Add(double number)
        {
            Sum += number;
            Count += 1;
            High = System.Math.Max(number, High);
            Low = System.Math.Min(number, Low);
        }

        public Statistics()
        {           
            High = double.MinValue; // assigns the most min value from the class double
            //its also example of using a STATIC method that belongs to a type (double) and not to it objects
            Low = double.MaxValue;
            Sum = 0.0;
            Count = 0;
        }
    }

        

}