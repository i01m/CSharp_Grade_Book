using System;
using Xunit;

namespace GradeBook.Tests
{
    //1.1. Reference Types are defined by Classes (to see that definition: put cursor on a type like Book, press F12
    //and see how it is defined
    //1.2. Value Types are defined by Struts
    //2.1. Parameters ALWAYS are passed by value (value copied from an original item, leaving that item unchanged)
    //2.2 To pass parameter by Reference the word REF needs to be used when passing parameter and in that method definition

    public delegate string WriteLogDelegate(string logMessage);


    public class TypeTests
    {
        int count = 0;

        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = ReturnMessage;

            //long way
            //log = new WriteLogDelegate(ReturnMessage);

            //short way
            log += ReturnMessage;
            log += IncrementCount;

            var result = log("Hello!");
            Assert.Equal(3, count);
        }

        string IncrementCount(string message)
        {
            count++;
            return message;
        }

        string ReturnMessage(string message)
        {
            count++;
            return message;
        }



        [Fact]
        public void StringBehaveLikeValueTypes()
        //even tho String is Reference Type, it acts like Value type. String is immutable (cannot be changed)
        //The reference is passed into a method, but it creates a NEW COPY of that string and makes 
        //changes to that copy leaving the original string the same

        {
            string name = "Mike";
            var upper = MakeUpperCase(name);

            Assert.Equal("Mike", name);
            Assert.Equal("MIKE", upper);
        }

        private string MakeUpperCase(string parameter)
        {
            return parameter.ToUpper();
        }

        [Fact]
        public void Test1()
        {
            var x = GetInt();
            SetInt(ref x);

            Assert.Equal(42, x);
        }

        private void SetInt(ref int z)
        {
            z = 42;
        }
          

    
        private int GetInt()
        {
            return 3;
        }

        [Fact]
        public void CSharpIsPassByReference()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(ref book1, "New Name");//ref indicates that the reference is being passed into the method

            Assert.Equal("New Name", book1.Name);    
        }

        private void GetBookSetName(ref InMemoryBook book, string name)
            //"ref" copies the reference(pointer) to address in memory for book1 
            //and when we change name in this method, the name of book1 is changed too
        {
            book = new InMemoryBook(name);         
        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New Name"); //value of book1 is passed (copied)

            Assert.Equal("Book 1", book1.Name);

        }

        private void GetBookSetName(InMemoryBook book, string name)
        //value of book1 is copied and when this method changes the name, 
        //it New Name is assigned to the copied/newly created object and NOT to book1
        //book1 keeps its original name "Book 1" since the parameter was passed by value
        {
            book = new InMemoryBook(name);
            book.Name = name;
        }


        [Fact]
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");

            Assert.Equal("New Name", book1.Name);
            
        }

        private void SetName(InMemoryBook book, string name)
        {
            book.Name = name;
        }

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book1, book2);
        }

        [Fact]
        public void GetBookReturnsSameObject()
        {
            var book1 = GetBook("Book 1");
            var book2 = book1;

            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
            
        }

        InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }
    }
}
