using System;
using System.Collections.Generic;
using System.Linq;
using LINQQueriesProject.Models;

namespace LINQQueriesProject.Queries
{
    public class QueryCityWithHighestOrderValue
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

                var cityWithHighestOrderValue = (from order in orders
                                                 join customer in customers on order.CustomerID.ToString() equals customer.ID.ToString()
                                                 join city in cities on customer.CityID equals city.ID
                                                 group order by city.Name into cityGroup
                                                 select new
                                                 {
                                                     CityName = cityGroup.Key,
                                                     TotalOrderValue = cityGroup.Sum(o => o.Price)
                                                 })
                                                .OrderByDescending(c => c.TotalOrderValue)
                                                .FirstOrDefault();

                if (cityWithHighestOrderValue != null)
                {
                    Console.WriteLine($"\nГород с наибольшей суммой заказов: {cityWithHighestOrderValue.CityName}, " +
                                      $"Сумма заказов: {cityWithHighestOrderValue.TotalOrderValue}");
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
