using System;
using System.Collections.Generic;
using System.Linq;
using LINQQueriesProject.Models;

namespace LINQQueriesProject.Queries
{
    public class QueryCustomersWithLowestOrders
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

                var customersWithLowestOrders = (from customer in customers
                                                 join city in cities on customer.CityID equals city.ID
                                                 join order in orders on customer.ID.ToString() equals order.CustomerID into customerOrders
                                                 let totalOrderValue = customerOrders.Sum(o => o.Price)
                                                 orderby totalOrderValue
                                                 select new
                                                 {
                                                     CustomerName = customer.Name,
                                                     CityName = city.Name,
                                                     OrderCount = customerOrders.Count(),
                                                     TotalOrderValue = totalOrderValue
                                                 })
                                                .Take(3)
                                                .ToList();

                if (customersWithLowestOrders.Any())
                {
                    Console.WriteLine("\nТоп 3 клиентов с наименьшими заказами:");
                    foreach (var customer in customersWithLowestOrders)
                    {
                        Console.WriteLine($"Имя: {customer.CustomerName}, Город: {customer.CityName}, Кол-во заказов: {customer.OrderCount}, " +
                                          $"Сумма заказов: {customer.TotalOrderValue}");
                    }
                }
                else
                {
                    Console.WriteLine("Нет данных для анализа.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
            }
        }
    }
}
