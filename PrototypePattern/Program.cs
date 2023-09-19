namespace PrototypePattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database
            {
                DatabaseName = "EmployeeDB",
                ServerName = "Server1",
                UserName = "UNAME1",
                Password = "UPASS1"
            };


            Console.WriteLine("== BEFORE DATA CHANGE ==");
            Console.WriteLine($"Original Object# Database Name:  {database.DatabaseName}");
            Database clonedDatabase = database.Clone() as Database;
            Console.WriteLine($"Cloned Object# Database Name:  {database.DatabaseName}");

            Console.WriteLine("== AFTER DATA CHANGE ==");
            clonedDatabase.DatabaseName = "SomeOtherDB";
            Console.WriteLine($"Original Object# DatabaseName - {database.DatabaseName}");
            Console.WriteLine($"Cloned Object# DatabaseName - {clonedDatabase.DatabaseName}");
        }
    }

    public class Database : ICloneable
    {        
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ServerName { get; set; }
        
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}