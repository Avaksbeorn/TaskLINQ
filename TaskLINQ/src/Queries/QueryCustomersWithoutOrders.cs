using System;
using System.Collections.Generic;
using System.Linq;
using LINQQueriesProject.Models;

namespace LINQQueriesProject.Queries
{
    public class QueryCustomersWithoutOrders
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

                var customerIdsWithOrders = new HashSet<string>(orders.Select(order => order.CustomerID));

                var customersWithoutOrders = customers.Where(customer => !customerIdsWithOrders.Contains(customer.ID.ToString()));

                Console.WriteLine("\nКлиенты без заказов:");
                foreach (var customer in customersWithoutOrders)
                {
                    Console.WriteLine($"Имя: {customer.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
            }
        }
    }
}
