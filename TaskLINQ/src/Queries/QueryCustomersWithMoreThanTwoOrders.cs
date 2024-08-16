using System;
using System.Collections.Generic;
using System.Linq;
using LINQQueriesProject.Models;

namespace LINQQueriesProject.Queries
{
    public class QueryCustomersWithMoreThanTwoOrders
    {
        public static void Execute(List<Customer> customers, List<Order> orders)
        {
            try
            {
                if (customers == null || orders == null)
                {
                    Console.WriteLine("Списки клиентов или заказов не инициализированы.");
                    return;
                }

                var customersWithMoreThanTwoOrders = (from customer in customers
                                                      join order in orders on customer.ID.ToString() equals order.CustomerID into customerOrders
                                                      where customerOrders.Count() > 2
                                                      orderby customer.Name
                                                      select customer).ToList();

                if (customersWithMoreThanTwoOrders.Any())
                {
                    Console.WriteLine("\nКлиенты с более чем 2 заказами:");
                    foreach (var customer in customersWithMoreThanTwoOrders)
                    {
                        Console.WriteLine($"Имя: {customer.Name}");
                    }
                }
                else
                {
                    Console.WriteLine("Нет клиентов с более чем 2 заказами.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
            }
        }
    }
}
