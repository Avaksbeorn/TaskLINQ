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

                var losAngelesCustomers = from customer in customers
                                          join city in cities on customer.CityID equals city.ID
                                          where city.Name == "Лос-Анджелес"
                                          select customer;

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
