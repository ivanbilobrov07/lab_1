using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace Business
{
    public class DatabaseService
    {
        private DB db;
        public DatabaseService(string filePath)
        {
            db = new DB(filePath);
        }

        private string[] GetByQuery(string query)
        {
            string data = db.Read();
            string[] dataArr = data.Split('\n');
            string[] result = new string[dataArr.Length];
            int index = 0;

            foreach (string str in dataArr)
            {
                if (str == "") continue;

                if (str.Contains(query))
                {
                    result[index++] = str;
                }
            }

            Array.Resize(ref result, index);
            return result;
        }

        public float CalculatePercentage()
        {
            Student[] students = new Student[GetStudents().Length];
            int index = 0;

            foreach (string studentStr in GetStudents())
            {
                students[index++] = ((Student)GenerateEntity(studentStr));
            }

            if(students.Length == 0) return 0;

            float suitableCases = 0;

            foreach(Student student in students)
            {
                if (student.Course == 1 && student.Hometown.ToLower() != "kyiv")
                {
                    suitableCases++;
                }
            }

            return suitableCases / students.Length * 100;
        }

        public Person GenerateEntity(string data)
        {
            string[] fields = data.Split(' ');
            int index = 0;

            foreach (string field in fields)
            {
                if (field.Contains(';'))
                {
                    fields[index] = field.Substring(0, field.Length - 1);
                }
                index++;
            }

            string firstName = fields[2];
            string lastName = fields[3];

            if (fields[0] == "Student")
            {
                int course = int.Parse(fields[5]);
                string studentId = fields[8];
                string hometown = fields[10];
                string passportNumber = fields[13];

                return new Student(firstName, lastName, course, studentId, hometown, passportNumber);
            }
            else if (fields[0] == "Acrobat")
            {
                return new Acrobat(firstName, lastName);
            }
            else
            {
                return new TaxiDriver(firstName, lastName);
            }
        }

        public string[] GetAll()
        {
            return GetByQuery("");
        }

        public string[] GetStudents()
        {
            return GetByQuery("Student");
        }

        public string[] GetTaxiDrivers()
        {
            return GetByQuery("TaxiDriver");
        }

        public string[] GetAcrobats()
        {
            return GetByQuery("Acrobat");
        }

        public string AddEntity(Person person)
        {
            db.Write(person.ToString());

            return person.ToString();
        }

        public string EditEntity(Person person, int index)
        {
            string[] allEntities = GetAll();
            allEntities[index] = person.ToString();

            db.Rewrite(string.Join("\n", allEntities));

            return person.ToString();
        }

        public string DeleteEntity(int index)
        {
            string[] dataArr = GetAll();
            string[] newDataArr = new string[dataArr.Length - 1];
            int newDataArrIndex = 0;

            for (int i = 0; i < dataArr.Length; i++)
            {
                if (i != index)
                {
                    newDataArr[newDataArrIndex] = dataArr[i];
                    newDataArrIndex++;
                }
            }

            string result = string.Join("\n", newDataArr);

            db.Rewrite(result);
            return dataArr[index];
        }
    }
}
