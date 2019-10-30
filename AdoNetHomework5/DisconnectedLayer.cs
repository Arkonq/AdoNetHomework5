using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace AdoNetHomework5
{
	public class DisconnectedLayer
	{
		private readonly DbProviderFactory providerFactory;
		private readonly DbConnection connection;
		private readonly DataSet dataSet;

		public DataTable Orders { get; set; }
		public DataTable Customers { get; set; }
		public DataTable Employees { get; set; }
		public DataTable OrderDetails { get; set; }
		public DataTable Products { get; set; }

		public DisconnectedLayer(string connectionString, string provider)
		{
			DbProviderFactories.RegisterFactory(provider, SqlClientFactory.Instance);
			providerFactory = DbProviderFactories.GetFactory(provider);
			connection = providerFactory.CreateConnection();
			connection.ConnectionString = connectionString;
			dataSet = new DataSet("ShopDb_HW5");
		}

		public void Action()
		{
			CreateCustomersTable();
			CreateEmployeesTable();
			CreateProductTable();
			CreateOrdersTable();
			CreateOrderDetailsTable();

			dataSet.Tables.Add(Orders);
			dataSet.Tables.Add(Employees);
			dataSet.Tables.Add(Customers);
			dataSet.Tables.Add(OrderDetails);
			dataSet.Tables.Add(Products);

			dataSet.Relations.Add("EmployeeOrders", Employees.Columns["Id"], Orders.Columns["EmployeeId"]);
			dataSet.Relations.Add("CutomersOrders", Customers.Columns["Id"], Orders.Columns["CutomerId"]);
			dataSet.Relations.Add("OrderOrderDetail", Orders.Columns["Id"], OrderDetails.Columns["OrderId"]);
			dataSet.Relations.Add("ProductsOrderDetail", Products.Columns["Id"], OrderDetails.Columns["ProductId"]);
		}

		private void CreateOrdersTable()
		{
			Orders = new DataTable("Orders");

			Orders.Columns.Add(new DataColumn
			{
				ColumnName = "Id",
				AutoIncrement = true,
				AutoIncrementSeed = 1,
				AutoIncrementStep = 1,
				Unique = true,
				DataType = typeof(int),
			});

			Orders.PrimaryKey = new DataColumn[] { Orders.Columns["Id"] };

			Orders.Columns.Add(new DataColumn
			{
				ColumnName = "CutomerId",
				Unique = false,
				AllowDBNull = false,
				DataType = typeof(int)
			});

			Orders.Columns.Add(new DataColumn
			{
				ColumnName = "EmployeeId",
				Unique = false,
				AllowDBNull = false,
				DataType = typeof(int)
			});

			Orders.Columns.Add(new DataColumn
			{
				ColumnName = "OrderDate",
				Unique = false,
				AllowDBNull = false,
				DataType = typeof(DateTime)
			});
		}

		private void CreateEmployeesTable()
		{
			Employees = new DataTable("Employees");
			Employees.Columns.Add(new DataColumn
			{
				ColumnName = "Id",
				AutoIncrement = true,
				AutoIncrementSeed = 1,
				AutoIncrementStep = 1,
				Unique = true,
				DataType = typeof(int),
			});
			Employees.PrimaryKey = new DataColumn[] { Employees.Columns["Id"] };

			Employees.Columns.Add(new DataColumn
			{
				ColumnName = "LastName",
				Unique = false,
				AllowDBNull = false,
				DataType = typeof(string)
			});

			Employees.Columns.Add(new DataColumn
			{
				ColumnName = "FirstName",
				Unique = false,
				AllowDBNull = false,
				DataType = typeof(string)
			});
		}

		private void CreateCustomersTable()
		{
			Customers = new DataTable("Customers");

			Customers.Columns.Add(new DataColumn
			{
				ColumnName = "Id",
				AutoIncrement = true,
				AutoIncrementSeed = 1,
				AutoIncrementStep = 1,
				Unique = true,
				DataType = typeof(int),
			});
			Customers.PrimaryKey = new DataColumn[] { Customers.Columns["Id"] };

			Customers.Columns.Add(new DataColumn
			{
				ColumnName = "LastName",
				Unique = false,
				AllowDBNull = false,
				DataType = typeof(string)
			});

			Customers.Columns.Add(new DataColumn
			{
				ColumnName = "FirstName",
				Unique = false,
				AllowDBNull = false,
				DataType = typeof(string)
			});
		}

		private void CreateProductTable()
		{
			Products = new DataTable("Products");

			Products.Columns.Add(new DataColumn
			{
				ColumnName = "Id",
				AutoIncrement = true,
				AutoIncrementSeed = 1,
				AutoIncrementStep = 1,
				Unique = true,
				DataType = typeof(int),
			});
			Products.PrimaryKey = new DataColumn[] { Products.Columns["Id"] };

			Products.Columns.Add(new DataColumn
			{
				ColumnName = "Name",
				Unique = false,
				AllowDBNull = false,
				DataType = typeof(string)
			});
		}

		private void CreateOrderDetailsTable()
		{
			OrderDetails = new DataTable("OrderDetails");
			OrderDetails.Columns.Add(new DataColumn
			{
				ColumnName = "OrderId",
				AutoIncrement = true,
				AutoIncrementSeed = 1,
				AutoIncrementStep = 1,
				Unique = true,
				DataType = typeof(int),
			});
			OrderDetails.PrimaryKey = new DataColumn[] { OrderDetails.Columns["OrderId"] };

			OrderDetails.Columns.Add(new DataColumn
			{
				ColumnName = "ProductId",
				Unique = false,
				AllowDBNull = false,
				DataType = typeof(int)
			});
		}
	}
}