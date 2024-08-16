using System;
using System.Collections.Generic;
using System.Linq;
using LINQQueriesProject.Models;

namespace LINQQueriesProject.Queries
{
    public class QueryCustomerDetails
    {
        public static void Execute(List<Customer> customers, List<Order> orders, List<City> cities)
        {
            try
            {
                if (customers == null || orders == null || cities == null)
                {
                    Console.WriteLine("Списки клиентов, заказов или городов не инициализированы.");
                    return;
                }

                var customerDetails = from customer in customers
                                      join city in cities on customer.CityID equals city.ID
                                      join order in orders on customer.ID.ToString() equals order.CustomerID into customerOrders
                                      from order in customerOrders.DefaultIfEmpty()
                                      group order by new { customer, city } into grouped
                                      select new
                                      {
                                          CustomerName = grouped.Key.customer.Name,
                                          CityName = grouped.Key.city.Name,
                                          CityCode = grouped.Key.city.CityCode,
                                          OrderCount = grouped.Count(o => o != null),
                                          LastOrderDate = grouped.Any(o => o != null) ?
                                                          grouped.Max(o => o != null ? o.Date : DateTime.MinValue) :
                                                          (DateTime?)null
                                      };

                Console.WriteLine("\nПодробности клиентов:");
                foreach (var detail in customerDetails)
                {
                    string lastOrderDateDisplay = detail.LastOrderDate.HasValue
                        ? detail.LastOrderDate.Value.ToString("yyyy-MM-dd") // Форматируйте дату как вам нужно
                        : "Нет даты последнего заказа";

                    Console.WriteLine($"Имя: {detail.CustomerName}, Город: {detail.CityName}, Код города: {detail.CityCode}, " +
                                      $"Количество заказов: {detail.OrderCount}, Дата последнего заказа: {lastOrderDateDisplay}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
            }
        }
    }
}
