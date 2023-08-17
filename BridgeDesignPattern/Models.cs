namespace BridgeDesignPattern
{
    public interface IFileOperation
    {
        void SaveData();
        void DeleteData();
    }

    public class FileOperation : IFileOperation
    {
        public void SaveData() 
        {
            Console.WriteLine("Saved Data to file");
        }

        public void DeleteData()
        {
            Console.WriteLine("Deleted Data from file");
        }
    }

    public class DatabaseOperation : IFileOperation 
    {
        public void SaveData()
        {
            Console.WriteLine("Saved Data to database");
        }

        public void DeleteData()
        {
            Console.WriteLine("Deleted Data from database");
        }
    }

    public abstract class FileOperationAbstract
    {
        protected IFileOperation fileOperation;
        public abstract void SaveData();
        public abstract void DeleteData();
    }

    public class FileConrete : FileOperationAbstract
    {
        public FileConrete(IFileOperation fileOperation)
        {
            this.fileOperation = fileOperation;
        }

        public override void DeleteData()
        {
            fileOperation.DeleteData();
        }

        public override void SaveData()
        {
            fileOperation.SaveData();
        }
    }

    public class DatabaseConcrete : FileOperationAbstract
    {
        public DatabaseConcrete(IFileOperation fileOperation)
        {
            this.fileOperation = fileOperation;
        }

        public override void DeleteData()
        {
            fileOperation.DeleteData();
        }

        public override void SaveData()
        {
            fileOperation.SaveData();
        }
    }
}
