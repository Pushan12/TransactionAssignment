using TransactionAssignment.Data;

namespace TransactionAssignment.Helper
{
    public abstract class FileFactory
    {
        public abstract IFileProcesser GetProcesser();

        public static FileFactory CreateFileFactory(IFormFile file)
        {
            switch (Path.GetExtension(file.FileName))
            {
                case ".csv":
                    return new CsvFileFactory();
                case ".xml":
                    return new XmlFileFactory();
                default:
                    return null;
            }
        }
    }


    public class CsvFileFactory : FileFactory
    {
        public override IFileProcesser GetProcesser()
        {
            return new CsvFileProcessor();
        }
    }

    public class XmlFileFactory : FileFactory
    {
        public override IFileProcesser GetProcesser()
        {
            return new XmlFileProcessor();
        }
    }

    public class DefaultFileFactory : FileFactory
    {
        public override IFileProcesser GetProcesser()
        {
            return new DefaultFileProcessor();
        }
    }


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
                    return new DefaultFileProcessor();
            }
        }
    }
}
