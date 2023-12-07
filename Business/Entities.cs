using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public interface IDance
    {
        public string Dance();
    }

    public interface IStudy
    {
        public string Study();
    }

    public interface IDrive
    {
        public string Drive();
    }

    public interface IDoTricts
    {
        public string DoTrics();
    }

    public class Person : IPerson
    {
        private string lastName;
        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public Person(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }
    }

    public class Student : Person, IStudy, IDance
    {
        private int course;
        private string studentId;
        private string hometown;
        private string passportNumber;

        public string Dance()
        {
            return FirstName + LastName + " is dancing!";
        }

        public string Study()
        {
            return FirstName + LastName + " is studying!";
        }

        public int Course
        {
            get { return course; }
            set { course = value; }
        }

        public string StudentId
        {
            get { return studentId; }
            set { studentId = value; }
        }

        public string Hometown
        {
            get { return hometown; }
            set { hometown = value; }
        }

        public string PassportNumber
        {
            get { return passportNumber; }
            set { passportNumber = value; }
        }

        public Student(string firstName, string lastName, int course, string studentId, string hometown, string passportNumber) : base(firstName, lastName)
        {
            this.course = course;
            this.studentId = studentId;
            this.hometown = hometown;
            this.passportNumber = passportNumber;
        }

        public override string ToString()
        {
            return "Student - " + FirstName + " " + LastName + "; Course: " + Course + "; Student Id: " + StudentId + "; Hometown: " + Hometown + "; Passport Number: " + PassportNumber + ";";
        }
    }
    public class TaxiDriver : Person, IDrive, IDance
    {
        public TaxiDriver(string firstName, string lastName) : base(firstName, lastName) { }

        public string Dance()
        {
            return FirstName + LastName + " is dancing!";
        }

        public string Drive()
        {
            return FirstName + LastName + " is driving!";
        }

        public override string ToString()
        {
            return "TaxiDriver - " + FirstName + " " + LastName + ";";
        }
    }

    public class Acrobat : Person, IDance, IDoTricts
    {
        public Acrobat(string firstName, string lastName) : base(firstName, lastName) { }

        public string Dance()
        {
            return FirstName + LastName + " is dancing!";
        }

        public string DoTrics()
        {
            return FirstName + LastName + " is dancing!";
        }

        public override string ToString()
        {
            return "Acrobat - " + FirstName + " " + LastName  + ";";
        }
    }
}