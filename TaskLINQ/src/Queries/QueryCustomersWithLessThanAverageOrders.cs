using System;
using System.Collections.Generic;
using System.Linq;
using LINQQueriesProject.Models;

namespace LINQQueriesProject.Queries
{
    public class QueryCustomersWithLessThanAverageOrders
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

                var customerOrdersByCity = from customer in customers
                                           join city in cities on customer.CityID equals city.ID
                                           join order in orders on customer.ID.ToString() equals order.CustomerID into customerOrders
                                           let orderCount = customerOrders.Count()
                                           group new { customer, orderCount } by city.Name into cityGroup
                                           let averageOrderCount = cityGroup.Average(c => c.orderCount)
                                           from customerOrder in cityGroup
                                           where customerOrder.orderCount < averageOrderCount
                                           select new
                                           {
                                               CityName = cityGroup.Key,
                                               CustomerName = customerOrder.customer.Name,
                                               OrderCount = customerOrder.orderCount
                                           };

                Console.WriteLine("\nКлиенты с заказами ниже среднего по их городу:");
                foreach (var customer in customerOrdersByCity)
                {
                    Console.WriteLine($"Город: {customer.CityName}, Имя клиента: {customer.CustomerName}, Кол-во заказов: {customer.OrderCount}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
            }
        }
    }
}
