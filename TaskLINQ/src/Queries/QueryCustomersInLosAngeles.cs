using System;
using System.Collections.Generic;
using System.Linq;
using LINQQueriesProject.Models;

namespace LINQQueriesProject.Queries
{
    public class QueryCustomersInLosAngeles
    {
        public static void Execute(List<Customer> customers, List<City> cities)
        {
            try
            {
                if (customers == null || cities == null)
                {
                    Console.WriteLine("Списки клиентов или городов не инициализированы.");
                    return;
                }

                var cityDict = cities.ToDictionary(city => city.ID);

                var losAngelesCityId = cityDict.Values.FirstOrDefault(city => city.Name == "Лос-Анджелес")?.ID;

                if (losAngelesCityId == null)
                {
                    Console.WriteLine("Город Лос-Анджелес не найден.");
                    return;
                }

                var losAngelesCustomers = customers.Where(customer => customer.CityID == losAngelesCityId);

                Console.WriteLine("\nКлиенты, проживающие в Лос-Анджелесе:");
                foreach (var customer in losAngelesCustomers)
                {
                    Console.WriteLine($"Имя: {customer.Name}, Город: Лос-Анджелес");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
            }
        }
    }
}
