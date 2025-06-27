using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AJAXRequest.Models
{
    public class OrdersDetails
    {
        public OrdersDetails()
        {
        }
        public OrdersDetails(int OrderID, string CustomerId, int EmployeeId, double Freight, bool Verified, DateTime OrderDate, string ShipCity, string ShipName, string ShipCountry, DateTime ShippedDate, string ShipAddress)
        {
            this.OrderID = OrderID;
            this.CustomerID = CustomerId;
            this.EmployeeID = EmployeeId;
            this.Freight = Freight;
            this.ShipCity = ShipCity;
            this.Verified = Verified;
            this.OrderDate = OrderDate;
            this.ShipName = ShipName;
            this.ShipCountry = ShipCountry;
            this.ShippedDate = ShippedDate;
            this.ShipAddress = ShipAddress;
        }
        public static List<OrdersDetails> GetAllRecords()
        {
            return new List<OrdersDetails>
    {
        new OrdersDetails(10001, "ALFKI", 1, 32.5, true, new DateTime(1991, 05, 15, 10, 40, 00), "Berlin", "Simons bistro", "Denmark", new DateTime(1996, 07, 16), "Kirchgasse 6"),
        new OrdersDetails(10002, "ANATR", 2, 41.2, false, new DateTime(1992, 06, 25, 12, 30, 00), "Madrid", "Antonio Moreno", "Spain", new DateTime(1997, 08, 11), "Calle del Rosal 4"),
        new OrdersDetails(10003, "ANTON", 3, 23.8, true, new DateTime(1993, 07, 30, 14, 20, 00), "London", "Around the Horn", "UK", new DateTime(1998, 09, 22), "120 Hanover Sq."),
        new OrdersDetails(10004, "AROUT", 4, 55.7, false, new DateTime(1994, 08, 10, 16, 10, 00), "Paris", "Bon app'", "France", new DateTime(1999, 10, 05), "12, rue des Bouchers"),
        new OrdersDetails(10005, "BERGS", 5, 19.4, true, new DateTime(1995, 09, 20, 18, 00, 00), "Berlin", "Frankenversand", "Germany", new DateTime(2000, 11, 15), "Berliner Platz 43"),
        new OrdersDetails(10006, "BLAUS", 6, 44.6, false, new DateTime(1996, 10, 15, 09, 50, 00), "Lisbon", "Hanari Carnes", "Portugal", new DateTime(2001, 12, 20), "Rua do Paço 67"),
        new OrdersDetails(10007, "BLONP", 7, 38.1, true, new DateTime(1997, 11, 05, 11, 40, 00), "Rome", "La maison d’Asie", "Italy", new DateTime(2002, 01, 25), "67, rue de l’Abbaye"),
        new OrdersDetails(10008, "BOLID", 8, 28.9, false, new DateTime(1998, 12, 12, 13, 30, 00), "Amsterdam", "Laughing Bacchus", "Netherlands", new DateTime(2003, 02, 28), "Boulevard Tirou 255"),
        new OrdersDetails(10009, "BONAP", 9, 61.5, true, new DateTime(1999, 01, 18, 15, 20, 00), "Vienna", "Magazzini Alimentari", "Austria", new DateTime(2004, 03, 10), "Via Ludovico il Moro 22"),
        new OrdersDetails(10010, "BOTTM", 10, 29.3, false, new DateTime(2000, 02, 22, 17, 10, 00), "Stockholm", "Océano Atlántico", "Sweden", new DateTime(2005, 04, 15), "Avda. de la Constitución 2222"),
        new OrdersDetails(10011, "BSBEV", 11, 47.2, true, new DateTime(2001, 03, 10, 08, 50, 00), "Oslo", "Québec Asparagus", "Norway", new DateTime(2006, 05, 20), "Rue Joseph-Bens 532"),
        new OrdersDetails(10012, "CACTU", 12, 39.8, false, new DateTime(2002, 04, 15, 10, 40, 00), "Helsinki", "Rattlesnake Canyon", "Finland", new DateTime(2007, 06, 25), "Berguvsvägen 8"),
        new OrdersDetails(10013, "CENTC", 13, 27.6, true, new DateTime(2003, 05, 20, 12, 30, 00), "Brussels", "Ricardo Adocicados", "Belgium", new DateTime(2008, 07, 30), "Sierras de Granada 999"),
        new OrdersDetails(10014, "CHOPS", 14, 58.2, false, new DateTime(2004, 06, 25, 14, 20, 00), "Zurich", "SICILIA BRASILEIRA", "Switzerland", new DateTime(2009, 08, 15), "Mataderos 2312"),
        new OrdersDetails(10015, "COMMI", 15, 31.7, true, new DateTime(2005, 07, 30, 16, 10, 00), "Prague", "Thüringer Rostbratwurst", "Czech Republic", new DateTime(2010, 09, 22), "1900 Oak St."),
        new OrdersDetails(10016, "CONSH", 16, 45.9, false, new DateTime(2006, 08, 10, 18, 00, 00), "Warsaw", "Victoria Ash", "Poland", new DateTime(2011, 10, 05), "5-401 Cherry St."),
        new OrdersDetails(10017, "DRACD", 17, 22.3, true, new DateTime(2007, 09, 20, 09, 50, 00), "Budapest", "Wellington Importadora", "Hungary", new DateTime(2012, 11, 15), "47 W 13th St."),
        new OrdersDetails(10018, "DUMON", 18, 34.5, false, new DateTime(2008, 10, 15, 11, 40, 00), "Copenhagen", "Zaanse Snoepfabriek", "Denmark", new DateTime(2013, 12, 20), "Kloveniersburgwal 39"),
        new OrdersDetails(10019, "EASTC", 19, 26.7, true, new DateTime(2009, 11, 05, 13, 30, 00), "Athens", "Ziegler", "Greece", new DateTime(2014, 01, 25), "65, avenue de l'Europe"),
        new OrdersDetails(10020, "ERNSH", 20, 49.8, false, new DateTime(2010, 12, 12, 15, 20, 00), "Dublin", "Magazzini Alimentari", "Ireland", new DateTime(2015, 02, 28), "Carretera de La Sagra 12")
    };
        }




        public int OrderID { get; set; } // primary key
        public string? CustomerID { get; set; }
        public int EmployeeID { get; set; } // foreign key
        public double? Freight { get; set; }
        public string? ShipCity { get; set; }
        public bool? Verified { get; set; }
        public DateTime? OrderDate { get; set; }

        public string? ShipName { get; set; }

        public string? ShipCountry { get; set; }

        public DateTime? ShippedDate { get; set; }
        public string? ShipAddress { get; set; }
    }
}