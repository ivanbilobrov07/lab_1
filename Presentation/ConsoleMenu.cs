using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;

namespace Presentation
{
    internal class ConsoleMenu
    {
        static private string getPropertyValue(Func<string, bool> isValid, string message, string errorMessage)
        {
            string value;

            Console.WriteLine(message);
            value = Console.ReadLine()!;

            while (true)
            {
                try
                {
                    if (isValid(value))
                    {
                        return value;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(errorMessage);
                    value = Console.ReadLine()!;
                }
            }
        }

        static private int getPropertyValue(Func<int, bool> isValid, string message, string errorMessage)
        {
            string value;

            Console.WriteLine(message);
            value = Console.ReadLine()!;

            while (true)
            {
                try
                {
                    int parsedValue = int.Parse(value);
                    if (isValid(parsedValue))
                    {
                        return parsedValue;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please, enter only a number");
                    value = Console.ReadLine()!;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(errorMessage);
                    value = Console.ReadLine()!;
                }
            }
        }

        static private int getUserIndexFromList(string askMessage, string[] values)
        {
            Console.WriteLine(askMessage);

            int index = -1;

            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {values[i]}");
            }

            bool firstIteration = true;

            while (!Helpers.isValidIndex(index, values.Length))
            {
                if (!firstIteration) Console.WriteLine("Please choose the index from the list above");

                try
                {
                    index = int.Parse(Console.ReadLine()!) - 1;
                }
                catch
                {
                    index = -1;
                }

                firstIteration = false;
            }

            return index;
        }

        static public string defineSerealizationType()
        {
            string[] types = { "JSON", "Binary", "XML", "Custom" };
            int typeIndex = getUserIndexFromList("Choose type of serialization to work with: ", types);

            return types[typeIndex];
        }

        static public string getFileName()
        {
            Console.WriteLine("Enter the name of the file without extension");

            string fileName = Console.ReadLine()!;

            while (fileName.Contains("."))
            {
                Console.WriteLine("Please enter without extension");
                fileName = Console.ReadLine()!;
            }

            return fileName;
        }

        static public Entities defineEntityForAddition()
        {
            string[] entities = { "Student", "Acrobat", "Taxi driver" };
            int entityIndex = getUserIndexFromList("Choose an entity to work with", entities);

            if (entityIndex == 0)
            {
                return Entities.Student;
            }
            else if (entityIndex == 1)
            {
                return Entities.Acrobat;
            }
            else
            {
                return Entities.TaxiDriver;
            }
        }

        static public Entities defineEntityForPrint()
        {
            string[] entities = { "All entities", "Student", "Acrobat", "Taxi driver" };
            int entityIndex = getUserIndexFromList("Choose an entity to work with", entities);

            if (entityIndex == 0)
            {
                return Entities.Any;
            }
            else if (entityIndex == 1)
            {
                return Entities.Student;
            }
            else if (entityIndex == 2)
            {
                return Entities.Acrobat;
            }
            else 
            {
                return Entities.TaxiDriver;
            }
        }

        static public Actions defineAction()
        {
            string[] actions = { "Add a new entity to file", 
                "Delete an entity from file", 
                "Print entities", 
                "Search students", 
                "Edit an entity", 
                "Make an entity do something",
                "Exit" };
            int actionIndex = getUserIndexFromList("Choose an action, you want to do: ", actions);

            switch (actionIndex)
            {
                case 0: return Actions.Add;
                case 1: return Actions.Remove;
                case 2: return Actions.Print;
                case 3: return Actions.CalculatePercentage;
                case 4: return Actions.Edit;
                case 5: return Actions.DoSomething;
                case 6: return Actions.Stop;
                default: return Actions.Print;
            }
        }

        static public string GetStudentId()
        {
            return getPropertyValue(StudentValidation.isValidStudentId, "Enter the the student id:", "Please, enter the valid student id:");
        }

        static public string GetFirstName()
        {
            return getPropertyValue(StudentValidation.isValidName, "Enter the first name:", "Please, enter the valid first name:");
        }

        static public string GetSecondName()
        {
            return getPropertyValue(StudentValidation.isValidName, "Enter the last name:", "Please, enter the valid last name:");
        }

        static public Student CreateStudent()
        {
            string firstName = getPropertyValue(StudentValidation.isValidName, "Enter the first name of the student:", "Please, enter the valid first name:");
            string lastName = getPropertyValue(StudentValidation.isValidName, "Enter the last name of the student:", "Please, enter the valid last name:");
            string studentId = GetStudentId();
            int course = getPropertyValue(StudentValidation.isValidCourse, "Enter the course of the student:", "Please, enter the valid course:");
            string hometown = getPropertyValue(StudentValidation.isValidHometown, "Enter the hometown:", "Please, enter the valid hometown:");
            string passportNumber = getPropertyValue(StudentValidation.isValidPassportNumber, "Enter the passport number:", "Please, enter the valid passport number:");

            return new Student(firstName, lastName, course, studentId, hometown, passportNumber);
        }

        static public Acrobat CreateAcrobat()
        {
            string firstName = getPropertyValue(StudentValidation.isValidName, "Enter the first name of the acrobat:", "Please, enter the valid first name:");
            string lastName = getPropertyValue(StudentValidation.isValidName, "Enter the last name of the acrobat:", "Please, enter the valid last name:");

            return new Acrobat(firstName, lastName);
        }

        static public TaxiDriver CreateTaxiDriver()
        {
            string firstName = getPropertyValue(StudentValidation.isValidName, "Enter the first name of the taxi driver:", "Please, enter the valid first name:");
            string lastName = getPropertyValue(StudentValidation.isValidName, "Enter the last name of the taxi driver:", "Please, enter the valid last name:");

            return new TaxiDriver(firstName, lastName);
        }

        static public void PrintEntities(string[] entities)
        {
            int index = 1;

            foreach (string ent in entities)
            {
                Console.WriteLine(index + ") " + ent);
                index++;
            }
        }

        public static int AskForIndexHandler(int length)
        {
            Console.WriteLine("Chose an entitie`s index");
            int index;

            while (true)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out index) && index >= 1 && index <= length)
                {
                    return index - 1;
                }

                Console.WriteLine("Invalid input. Please enter a valid index.");
            }
        }

        static public void PerformStudentAction(Student student)
        {
            string[] actions = { "Study", "Dance" };
            int actionIndex = getUserIndexFromList("Choose antion to do: ", actions);

            if (actionIndex == 0)
            {
                Console.Clear();
                Console.WriteLine(student.FirstName + " is studying now");
            }
            else
            {
                Console.Clear();
                Console.WriteLine(student.FirstName + " is dancing now");
            }
        }

        static public void PerformAcrobatAction(Acrobat acrobat)
        {
            string[] actions = { "Trics", "Dance" };
            int actionIndex = getUserIndexFromList("Choose antion to do: ", actions);

            if (actionIndex == 0)
            {
                Console.Clear();
                Console.WriteLine(acrobat.FirstName + " is doint trics now");
            }
            else
            {
                Console.Clear();
                Console.WriteLine(acrobat.FirstName + " is dancing now");
            }
        }

        static public void PerformTaxiDriverAction(TaxiDriver taxiDriver)
        {
            string[] actions = { "Driving", "Dance" };
            int actionIndex = getUserIndexFromList("Choose antion to do: ", actions);

            if (actionIndex == 0)
            {
                Console.Clear();
                Console.WriteLine(taxiDriver.FirstName + " is driving now");
            }
            else
            {
                Console.Clear();
                Console.WriteLine(taxiDriver.FirstName + " is dancing now");
            }
        }

        static public Student EditStudent(Student student)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(student.ToString());

                string[] fields = { "FirstName", "LastName", "Student Id", "Course", "Hometown", "Passport Number", "Confirm" };
                int fieldIndex = getUserIndexFromList("What field do you want to edit: ", fields);

                switch (fieldIndex)
                {
                    case 0:
                        {
                            string firstName = getPropertyValue(StudentValidation.isValidName, "Enter the first name of the student:", "Please, enter the valid first name:");
                            student.FirstName = firstName;
                            break;
                        }
                    case 1:
                        {
                            string lastName = getPropertyValue(StudentValidation.isValidName, "Enter the last name of the student:", "Please, enter the valid last name:");
                            student.LastName = lastName;
                            break;
                        }
                    case 2:
                        {
                            string studentId = GetStudentId();
                            student.StudentId = studentId;
                            break;
                        }
                    case 3:
                        {
                            int course = getPropertyValue(StudentValidation.isValidCourse, "Enter the course of the student:", "Please, enter the valid course:");
                            student.Course = course;
                            break;
                        }
                    case 4:
                        {
                            string hometown = getPropertyValue(StudentValidation.isValidHometown, "Enter the hometown:", "Please, enter the valid hometown:");
                            student.Hometown = hometown;
                            break;
                        }
                    case 5:
                        {
                            string passportNumber = getPropertyValue(StudentValidation.isValidPassportNumber, "Enter the passport number:", "Please, enter the valid passport number:");
                            student.PassportNumber = passportNumber;
                            break;
                        }
                    case 6:
                        {
                            Console.Clear();
                            return student;
                        }
                }
            }
        }

        static public Acrobat EditAcrobat(Acrobat acrobat)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(acrobat.ToString());

                string[] fields = { "FirstName", "LastName", "Confirm" };
                int fieldIndex = getUserIndexFromList("What field do you want to edit: ", fields);

                switch (fieldIndex)
                {
                    case 0:
                        {
                            string firstName = getPropertyValue(StudentValidation.isValidName, "Enter the first name of the actobat:", "Please, enter the valid first name:");
                            acrobat.FirstName = firstName;
                            break;
                        }
                    case 1:
                        {
                            string lastName = getPropertyValue(StudentValidation.isValidName, "Enter the last name of the actobat:", "Please, enter the valid last name:");
                            acrobat.LastName = lastName;
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            return acrobat;
                        }
                }
            }
        }

        static public TaxiDriver EditTaxiDriver(TaxiDriver taxiDriver)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(taxiDriver.ToString());

                string[] fields = { "FirstName", "LastName", "Confirm" };
                int fieldIndex = getUserIndexFromList("What field do you want to edit: ", fields);

                switch (fieldIndex)
                {
                    case 0:
                        {
                            string firstName = getPropertyValue(StudentValidation.isValidName, "Enter the first name of the taxi driver:", "Please, enter the valid first name:");
                            taxiDriver.FirstName = firstName;
                            break;
                        }
                    case 1:
                        {
                            string lastName = getPropertyValue(StudentValidation.isValidName, "Enter the last name of the taxi driver:", "Please, enter the valid last name:");
                            taxiDriver.LastName = lastName;
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            return taxiDriver;
                        }
                }
            }
        }
    }
    //public class ConsoleMenu
    //{
    //    static public void AskUser()
    //    {
    //       Console.WriteLine("Select a command: \n\n" +
    //           "1 - view the entities \n" +
    //           "2 - add a new entity \n" +
    //           "3 - delete an entity \n" +
    //           "4 - make an entity do something \n" +
    //           "5 - SEARCH \n" +
    //           "9 - EXIT \n" );
    //    }

    //    static public void AskUserAboutShowingEntity()
    //    {
    //        Console.WriteLine("1 - show all \n" +
    //           "2 - show only students \n" +
    //           "3 - show only taxi drivers \n" +
    //           "4 - show only acrobats \n");
    //    }

    //    static public void AskUserAboutAddingEntity()
    //    {
    //        Console.WriteLine(
    //           "1 - add new student \n" +
    //           "2 - add new taxi driver \n" +
    //           "3 - add new acrobat \n");
    //    }

    //    static public Student GetStudentData()
    //    {
    //        string firstName;
    //        string lastName;
    //        int course;
    //        int studentId;
    //        string hometown;
    //        int passportNumber;

    //        Console.WriteLine("Enter student firstname: ");
    //        firstName = Console.ReadLine();
    //        Console.WriteLine();

    //        Console.WriteLine("Enter student lastname: ");
    //        lastName = Console.ReadLine();
    //        Console.WriteLine();

    //        Console.WriteLine("Enter student course: ");
    //        string courseStr = Console.ReadLine();
    //        while (!int.TryParse(courseStr, out course))
    //        {
    //            Console.WriteLine("You can asign only a number");
    //            courseStr = Console.ReadLine();
    //        }
    //        Console.WriteLine();

    //        Console.WriteLine("Enter student id: ");
    //        string studentIdStr = Console.ReadLine();
    //        while (!int.TryParse(studentIdStr, out studentId))
    //        {
    //            Console.WriteLine("You can asign only a number");
    //            studentIdStr = Console.ReadLine();
    //        }
    //        Console.WriteLine();

    //        Console.WriteLine("Enter student hometown: ");
    //        hometown = Console.ReadLine();
    //        Console.WriteLine();

    //        Console.WriteLine("Enter student number of passport: ");
    //        string passportNumberStr = Console.ReadLine();
    //        while (!int.TryParse(passportNumberStr, out passportNumber))
    //        {
    //            Console.WriteLine("You can asign only a number");
    //            passportNumberStr = Console.ReadLine();
    //        }
    //        Console.WriteLine();

    //        return new Student(firstName, lastName, course, studentId, hometown, passportNumber);
    //    }

    //    static public TaxiDriver GetTaxiDriverData()
    //    {
    //        string firstName;
    //        string lastName;

    //        Console.WriteLine("Enter taxi driver firstname: ");
    //        firstName = Console.ReadLine();
    //        Console.WriteLine();

    //        Console.WriteLine("Enter taxi driver lastname: ");
    //        lastName = Console.ReadLine();
    //        Console.WriteLine();

    //        return new TaxiDriver(firstName, lastName);
    //    }

    //    static public Acrobat GetAcrobatData()
    //    {
    //        string firstName;
    //        string lastName;

    //        Console.WriteLine("Enter acrobat firstname: ");
    //        firstName = Console.ReadLine();
    //        Console.WriteLine();

    //        Console.WriteLine("Enter acrobat lastname: ");
    //        lastName = Console.ReadLine();
    //        Console.WriteLine();

    //        return new Acrobat(firstName, lastName);
    //    }

    //    static public void PrintDBData(string[] data)
    //    {
    //        if (data.Length == 0)
    //        {
    //            Console.WriteLine("There are not such entities");
    //        }

    //        for(int i = 0; i < data.Length; i++)
    //        {
    //            Console.WriteLine(i + 1 + ": " + data[i]);
    //        }
    //    }
    //}
}
