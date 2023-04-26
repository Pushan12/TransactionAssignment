using TransactionAssignment.Data;

namespace TransactionAssignment.Helper
{
    public interface IFileProcesserFactory
    {
        public IFileProcesser GetProcessor(IFormFile file);
    }

    public class FileProcesserFactory : IFileProcesserFactory
    {
        
        public IFileProcesser GetProcessor(IFormFile file)
        {
            switch(Path.GetExtension(file.FileName))
            {
                case ".csv":
                    return new CsvFileProcessor();
                case ".xml":
                    return new XmlFileProcessor();
                default :
                    return null;
            }
        }
    }
}
