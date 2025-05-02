using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AJAXRequest.Models;
using Syncfusion.EJ2.Base;
using Newtonsoft.Json;
using System.Web.DynamicData;
using Newtonsoft.Json.Linq;

namespace AJAXRequest.Controllers
{
    public class HomeController : Controller
    {

        private static DataTable ordersTable = GetData();

        public ActionResult About()
        {

            ViewBag.EmployeeDataSource = EmployeeDetails.GetAllEmployees();
            ViewBag.DataSource = OrdersDetails.GetAllRecords();
            return View();
        }


        public ActionResult GridForeignKeyDataOperationHandler()
        {
            IEnumerable DataSource = EmployeeView.GetAllRecords();
            return Json(DataSource);
        }

        public ActionResult GridDataOperationHandler([FromBody] DataManagerRequest dm)
        {
            IEnumerable DataSource = OrdersDetails.GetAllRecords();
            DataOperations operation = new DataOperations();

            if (dm.Sorted != null && dm.Sorted.Count > 0)
            {
                string sortKey = dm.Sorted[0].Name;
                string sortDirection = dm.Sorted[0].Direction;

                if (sortKey == "EmployeeID")
                {
                    DataSource = SortForeignKeyColumn(sortDirection);
                }
                else
                {
                    DataSource = operation.PerformSorting(DataSource, dm.Sorted);
                }
            }

            int count = DataSource.Cast<OrdersDetails>().Count();
            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }
            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }

        private List<OrdersDetails> SortForeignKeyColumn(string sortDirection)
        {
            var empdata = EmployeeView.GetAllRecords();
            List<EmployeeView> sortedEmployees = (sortDirection == "ascending")
                ? empdata.OrderBy(e => e.FirstName).ToList()
                : empdata.OrderByDescending(e => e.FirstName).ToList();
            List<OrdersDetails> or = new List<OrdersDetails>();

            foreach (var emp in sortedEmployees)
            {
                var empOrders = OrdersDetails.GetAllRecords().Where(o => o.EmployeeID == emp.EmployeeID).ToList();
                or.AddRange(empOrders);
            }

            return or;
        }

        // ✅ Method to populate the DataTable with initial data
        private static DataTable GetData()
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[5] {
        new DataColumn("OrderID", typeof(int)),
        new DataColumn("CustomerID", typeof(string)),
        new DataColumn("EmployeeID", typeof(int)),
        new DataColumn("Freight", typeof(double)),
        new DataColumn("ShipCity", typeof(string)),
    });

            int code = 1000;
            int id = 0;
            for (int i = 1; i <= 15; i++)
            {
                dt.Rows.Add(code + 1, "ALFKI", id + 1, 2.3 * i, "New York");
                dt.Rows.Add(code + 2, "ANATR", id + 2, 3.3 * i, "Los Angeles");
                dt.Rows.Add(code + 3, "ANTON", id + 3, 4.3 * i, "Chicago");
                dt.Rows.Add(code + 4, "BLONP", id + 4, 5.3 * i, "Houston");
                dt.Rows.Add(code + 5, "BOLID", id + 5, 6.3 * i, "Miami");
                code += 5;
                id += 5;
            }
            return dt;
        }

        // ✅ Data Source for Syncfusion Grid
        public ActionResult UrlDatasource(DataManagerRequest dm)
        {
            IEnumerable DataSource = Utils.DataTableToJson(ordersTable);
            DataOperations operation = new DataOperations();
            int count = DataSource.Cast<object>().Count();

            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }

            return dm.RequiresCounts ? Json(new { result = DataSource, count = count }) : Json(DataSource);
        }
        // ✅ Insert action (Adding new record at the **top**)
        public ActionResult Insert(ExpandoObject value)
        {
            if (value != null)
            {
                DataRow newRow = ordersTable.NewRow();
                var dict = (IDictionary<string, object>)value;

                newRow["OrderID"] = dict.ContainsKey("OrderID") ? dict["OrderID"] : 0;
                newRow["CustomerID"] = dict.ContainsKey("CustomerID") ? dict["CustomerID"].ToString() : string.Empty;
                newRow["EmployeeID"] = dict.ContainsKey("EmployeeID") ? Convert.ToInt32(dict["EmployeeID"]) : 0;
                newRow["Freight"] = dict.ContainsKey("Freight") ? Convert.ToDouble(dict["Freight"]) : 0;
                newRow["ShipCity"] = dict.ContainsKey("ShipCity") ? dict["ShipCity"].ToString() : string.Empty;

                ordersTable.Rows.InsertAt(newRow, 0); // Insert at the top
            }

            return Json(value, JsonRequestBehavior.AllowGet);
        }
        // ✅ Update action
        public ActionResult Update(ExpandoObject value)
        {
            if (value != null)
            {
                var dict = (IDictionary<string, object>)value;
                long orderId = dict.ContainsKey("OrderID") ? Convert.ToInt64(dict["OrderID"]) : 0;

                DataRow rowToUpdate = ordersTable.Rows.Cast<DataRow>()
                    .FirstOrDefault(row => row.Field<long>("OrderID") == orderId);

                if (rowToUpdate != null)
                {
                    rowToUpdate["CustomerID"] = dict.ContainsKey("CustomerID") ? dict["CustomerID"].ToString() : string.Empty;
                    rowToUpdate["EmployeeID"] = dict.ContainsKey("EmployeeID") ? Convert.ToInt32(dict["EmployeeID"]) : 0;
                    rowToUpdate["Freight"] = dict.ContainsKey("Freight") ? Convert.ToDouble(dict["Freight"]) : 0;
                    rowToUpdate["ShipCity"] = dict.ContainsKey("ShipCity") ? dict["ShipCity"].ToString() : string.Empty;
                }
            }

            return Json(value, JsonRequestBehavior.AllowGet);
        }
        // ✅ Delete action
        public ActionResult Delete(long key)
        {
            long orderId = Convert.ToInt64(key);

            DataRow rowToDelete = ordersTable.Rows.Cast<DataRow>()
                .FirstOrDefault(row => row.Field<long>("OrderID") == orderId);

            if (rowToDelete != null)
            {
                ordersTable.Rows.Remove(rowToDelete);
            }

            return Json(new { Key = key }, JsonRequestBehavior.AllowGet);
        }

        public class OrdersDetails
        {
            public OrdersDetails() { }
            public OrdersDetails(int orderID, string customerId, int employeeId, double freight, bool verified, DateTime orderDate, string shipCity, string shipName, string shipCountry, DateTime shippedDate, string shipAddress)
            {
                this.OrderID = orderID;
                this.CustomerID = customerId;
                this.EmployeeID = employeeId;
                this.Freight = freight;
                this.Verified = verified;
                this.OrderDate = orderDate;
                this.ShipCity = shipCity;
                this.ShipName = shipName;
                this.ShipCountry = shipCountry;
                this.ShippedDate = shippedDate;
                this.ShipAddress = shipAddress;
            }

            public static List<OrdersDetails> GetAllRecords()
            {
                return new List<OrdersDetails>()
        {
            new OrdersDetails(1001, "ALFKI", 1, 10.2, true, new DateTime(2024, 1, 10), "New York", "Alpha Traders", "USA", new DateTime(2024, 1, 15), "123 Main St"),
            new OrdersDetails(1002, "ANATR", 2, 15.5, false, new DateTime(2024, 2, 12), "Los Angeles", "Beta Exports", "USA", new DateTime(2024, 2, 18), "456 Elm St"),
            new OrdersDetails(1003, "ANTON", 3, 20.7, true, new DateTime(2024, 3, 5), "Chicago", "Gamma Supplies", "USA", new DateTime(2024, 3, 10), "789 Oak St"),
            new OrdersDetails(1004, "BLONP", 4, 25.3, false, new DateTime(2024, 4, 8), "Houston", "Delta Distributors", "USA", new DateTime(2024, 4, 14), "101 Pine St"),
            new OrdersDetails(1005, "BOLID", 5, 30.1, true, new DateTime(2024, 5, 14), "Phoenix", "Epsilon Ltd", "USA", new DateTime(2024, 5, 20), "202 Birch St"),
            new OrdersDetails(1006, "BONAP", 6, 12.8, false, new DateTime(2024, 6, 20), "San Antonio", "Zeta Enterprises", "USA", new DateTime(2024, 6, 25), "303 Cedar St"),
            new OrdersDetails(1007, "BOTTM", 7, 18.6, true, new DateTime(2024, 7, 2), "San Diego", "Eta Group", "USA", new DateTime(2024, 7, 8), "404 Spruce St"),
            new OrdersDetails(1008, "BSBEV", 8, 22.4, false, new DateTime(2024, 8, 9), "Dallas", "Theta Suppliers", "USA", new DateTime(2024, 8, 15), "505 Maple St"),
            new OrdersDetails(1009, "CACTU", 9, 27.9, true, new DateTime(2024, 9, 11), "San Jose", "Iota Traders", "USA", new DateTime(2024, 9, 17), "606 Walnut St"),
            new OrdersDetails(1010, "CENTC", 10, 33.2, false, new DateTime(2024, 10, 15), "Austin", "Kappa Logistics", "USA", new DateTime(2024, 10, 21), "707 Ash St"),
            new OrdersDetails(1011, "CHOPS", 11, 17.5, true, new DateTime(2024, 11, 3), "San Francisco", "Lambda Shipping", "USA", new DateTime(2024, 11, 9), "808 Chestnut St"),
            new OrdersDetails(1012, "COMMI", 12, 21.9, false, new DateTime(2024, 12, 7), "Denver", "Mu Warehouse", "USA", new DateTime(2024, 12, 13), "909 Redwood St")
        };
            }

            public int? OrderID { get; set; }
            public string CustomerID { get; set; }
            public int? EmployeeID { get; set; }
            public double? Freight { get; set; }
            public string ShipCity { get; set; }
            public bool Verified { get; set; }
            public DateTime OrderDate { get; set; }
            public string ShipName { get; set; }
            public string ShipCountry { get; set; }
            public DateTime? ShippedDate { get; set; }
            public string ShipAddress { get; set; }
        }

        public class EmployeeDetails
        {
            public EmployeeDetails() { }
            public EmployeeDetails(int employeeID, string firstName, string lastName, string city)
            {
                this.EmployeeID = employeeID;
                this.FirstName = firstName;
                this.LastName = lastName;
                this.City = city;
            }

            public static List<EmployeeDetails> GetAllEmployees()
            {
                return new List<EmployeeDetails>()
        {
            new EmployeeDetails(1, "John", "Doe", "New York"),
            new EmployeeDetails(2, "Jane", "Smith", "Los Angeles"),
            new EmployeeDetails(1, "Michael", "Brown", "Chicago"),
            new EmployeeDetails(4, "Emily", "Davis", "Houston"),
            new EmployeeDetails(5, "Chris", "Wilson", "Phoenix"),
            new EmployeeDetails(6, "David", "Martinez", "San Antonio"),
            new EmployeeDetails(3, "Sophia", "Garcia", "San Diego"),
            new EmployeeDetails(2, "Daniel", "Anderson", "Dallas"),
            new EmployeeDetails(9, "Olivia", "Taylor", "San Jose"),
            new EmployeeDetails(1, "Liam", "Harris", "Austin"),
            new EmployeeDetails(11, "Mia", "Clark", "San Francisco"),
            new EmployeeDetails(12, "Ethan", "Young", "Denver")
        };
            }

            public int EmployeeID { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string City { get; set; }
        }

    }
}