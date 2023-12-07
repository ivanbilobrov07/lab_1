using System.Text.Json;
using System.Text;

namespace Database
{
    public class DB
    {
        private string filePath;
        public DB(string filePath)
        {
            this.filePath = filePath;
        }

        public string Read()
        {
            string output = "";

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    output += line + "\n";
                }
            }

            return output;
        }

        public void Write(string data)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(data);
            }
        }

        public void Rewrite(string data)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                writer.WriteLine(data);
            }
        }
    }
}