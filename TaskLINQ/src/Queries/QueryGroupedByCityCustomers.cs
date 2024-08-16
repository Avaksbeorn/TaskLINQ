using System;
using System.Collections.Generic;
using System.Linq;
using LINQQueriesProject.Models;

namespace LINQQueriesProject.Queries
{
    public class QueryGroupedByCityCustomers
    {
        public static void Execute(List<Customer> customers, List<Order> orders, List<City> cities)
        {
            try
            {
                if (customers == null || orders == null || cities == null)
                {
                    Console.WriteLine("Один из списков данных не инициализирован.");
                    return;
                }

                var groupedByCityCustomers = from customer in customers
                                             join city in cities on customer.CityID equals city.ID
                                             join order in orders on customer.ID.ToString() equals order.CustomerID into customerOrders
                                             where customerOrders.Any()
                                             group new { customer, Orders = customerOrders } by city.Name into cityGroup
                                             select new
                                             {
                                                 CityName = cityGroup.Key,
                                                 Customers = cityGroup.Select(g => new
                                                 {
                                                     CustomerName = g.customer.Name,
                                                     OrderCount = g.Orders.Count()
                                                 })
                                             };

                Console.WriteLine("\nКлиенты, сгруппированные по городам:");
                foreach (var cityGroup in groupedByCityCustomers)
                {
                    Console.WriteLine($"Город: {cityGroup.CityName}");
                    foreach (var customer in cityGroup.Customers)
                    {
                        Console.WriteLine($"  Имя: {customer.CustomerName}, Кол-во заказов: {customer.OrderCount}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
            }
        }
    }
}
