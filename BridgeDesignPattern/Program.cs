using BridgeDesignPattern;

IFileOperation fileOperation = new FileOperation();
IFileOperation databaseOperation = new DatabaseOperation();

FileOperationAbstract file = new FileConrete(fileOperation);
FileOperationAbstract db = new DatabaseConcrete(databaseOperation);

file.SaveData();
file.DeleteData();

db.DeleteData();
db.SaveData();