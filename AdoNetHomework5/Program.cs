using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace AdoNetHomework5
{
	class Program
	{
		/*
		Создайте приложение по шаблону ConsoleApplication.  
		В этом приложении сделайте экземпляр класса DataSet с именем ShopDB. 
		Создайте объекты DataTable с именами Orders, Customers, Employees, 
			OrderDetails, Products со схемами, идентичными схемам таблицам 
			базы данных ShopDB(c ограничениями для столбцов). 
		Добавьте созданные таблицы в коллекцию таблиц ShopDB.  
		Для всех таблиц в ShopDB создайте ограничения PrimaryKey и ForeignKey. 
		*/
		static void Main()
		{
			var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("AppSettings.json", false, true);
			IConfigurationRoot configurationRoot = builder.Build();
			var providerName = configurationRoot.GetSection("AppConfig").GetChildren().Single(item => item.Key == "ProviderName").Value;
			var connectionString = configurationRoot.GetConnectionString("MyConnectionString");

			DisconnectedLayer disconnectedLayer = new DisconnectedLayer(connectionString, providerName);
		}
	}
}
