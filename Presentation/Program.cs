using System;
using System.Reflection;
using System.Reflection.Metadata;
using Business;
using Database;

namespace Presentation
{
    public enum Actions
    {
        Add,
        Remove,
        Print,
        Edit,
        CalculatePercentage,
        Stop,
        DoSomething,
    }
    public enum Entities
    {
        Any,
        Student,
        Acrobat,
        TaxiDriver,
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string DB_PATH = AppDomain.CurrentDomain.BaseDirectory + "DB.txt";
            DatabaseService dbService = new DatabaseService(DB_PATH);

            while (true)
            {
                Actions action = ConsoleMenu.defineAction();

                while (action != Actions.Stop)
                {
                    Console.Clear();
                    switch (action)
                    {
                        case Actions.Add:
                            {
                                Entities entity = ConsoleMenu.defineEntityForAddition();

                                Person person; 

                                if(entity == Entities.Student)
                                {
                                    person = ConsoleMenu.CreateStudent();
                                } else if(entity == Entities.Acrobat)
                                {
                                    person = ConsoleMenu.CreateAcrobat();
                                } else
                                {
                                    person = ConsoleMenu.CreateTaxiDriver();
                                }

                                try
                                {
                                    string output = dbService.AddEntity(person);
                                    Console.WriteLine("{" + output + "} was added successfully");
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }

                                action = ConsoleMenu.defineAction();
                                break;
                            }
                        case Actions.Remove:
                            {
                                string[] data = dbService.GetAll();

                                ConsoleMenu.PrintEntities(data);

                                int index = ConsoleMenu.AskForIndexHandler(data.Length);

                                string entity = dbService.DeleteEntity(index);

                                Console.Clear();
                                Console.WriteLine("{" + entity + "} was deleted successfully");

                                action = ConsoleMenu.defineAction();
                                break;
                            }
                        case Actions.Print:
                            {
                                Entities entity = ConsoleMenu.defineEntityForPrint();
                                string[] entities;

                                if (entity == Entities.Any)
                                {
                                    entities = dbService.GetAll();
                                }
                                else if (entity == Entities.Student)
                                {
                                    entities = dbService.GetStudents();
                                }
                                else if (entity == Entities.Acrobat)
                                {
                                    entities = dbService.GetAcrobats();
                                }
                                else 
                                {
                                    entities = dbService.GetTaxiDrivers();
                                }

                                if(entities.Length == 0)
                                {
                                    Console.WriteLine("There is not such entity in DB");
                                }
                                else
                                {
                                    ConsoleMenu.PrintEntities(entities);
                                }

                                action = ConsoleMenu.defineAction();
                                break;
                            }
                        case Actions.DoSomething:
                            {
                                string[] data = dbService.GetAll();

                                ConsoleMenu.PrintEntities(data);

                                int index = ConsoleMenu.AskForIndexHandler(data.Length);

                                Console.Clear();
                                Console.WriteLine(data[index]);

                                Person pers = dbService.GenerateEntity(data[index]);

                                if (pers is Student)
                                {
                                    ConsoleMenu.PerformStudentAction((Student)pers);
                                } else if (pers is Acrobat)
                                {
                                    ConsoleMenu.PerformAcrobatAction((Acrobat)pers);
                                } else
                                {
                                    ConsoleMenu.PerformTaxiDriverAction((TaxiDriver)pers);
                                }

                                action = ConsoleMenu.defineAction();
                                break;
                            }
                        case Actions.Edit:
                            {
                                string[] data = dbService.GetAll();

                                ConsoleMenu.PrintEntities(data);

                                int index = ConsoleMenu.AskForIndexHandler(data.Length);

                                Console.Clear();

                                Person pers = dbService.GenerateEntity(data[index]);

                                if (pers is Student)
                                {
                                    Student newStudent = ConsoleMenu.EditStudent((Student)pers);
                                    Console.WriteLine("{" + newStudent.ToString() + "} was added successfully");

                                    dbService.EditEntity(newStudent, index);
                                }
                                else if (pers is Acrobat)
                                {
                                    Acrobat newAcrobat = ConsoleMenu.EditAcrobat((Acrobat)pers);
                                    Console.WriteLine("{" + newAcrobat.ToString() + "} was added successfully");

                                    dbService.EditEntity(newAcrobat, index);
                                }
                                else
                                {
                                    TaxiDriver newTaxiDriver = ConsoleMenu.EditTaxiDriver((TaxiDriver)pers);
                                    Console.WriteLine("{" + newTaxiDriver.ToString() + "} was added successfully");

                                    dbService.EditEntity(newTaxiDriver, index);
                                }

                                action = ConsoleMenu.defineAction();
                                break;
                            }
                        case Actions.CalculatePercentage:
                            {
                                float percentage = dbService.CalculatePercentage();
                                Console.WriteLine("The percentage of 1 year students who came from other cities - " + Math.Round(percentage) + "%");

                                action = ConsoleMenu.defineAction();
                                break;
                            }
                        case Actions.Stop:
                            {
                                return;
                            }
                    }
                }             
            }         
        }
    }
}