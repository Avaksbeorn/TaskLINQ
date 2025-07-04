﻿using System;
using System.Collections.Generic;
using LINQQueriesProject.Models;
using LINQQueriesProject.Services;
using LINQQueriesProject.Queries;

namespace LINQQueriesProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // Инициализация данных из файлов          
            while (true)
            {
                Console.WriteLine("Выберите вариант:");
                Console.WriteLine("1. Выполнение запросов LINQ");
                Console.WriteLine("2. Вычисление PI с помощью Parallel LINQ");
                Console.WriteLine("3. Выход");
                Console.Write("Введите ваш выбор: ");

                var choice = Console.ReadLine();
                if (choice == "1")
                {
                    var customers = DataLoader.LoadDataFromFile<List<Customer>>("C:\\Users\\Админ\\source\\repos\\Avaksbeorn\\TaskLINQ\\TaskLINQ\\TableJson\\customers.json");
                    var orders = DataLoader.LoadDataFromFile<List<Order>>("C:\\Users\\Админ\\source\\repos\\Avaksbeorn\\TaskLINQ\\TaskLINQ\\TableJson\\orders.json");
                    var cities = DataLoader.LoadDataFromFile<List<City>>("C:\\Users\\Админ\\source\\repos\\Avaksbeorn\\TaskLINQ\\TaskLINQ\\TableJson\\cities.json");
                    // Запросы LINQ
                    QueryCustomersInLosAngeles.Execute(customers, cities);
                    QueryCustomersWithoutOrders.Execute(customers, orders);
                    QueryCustomerDetails.Execute(customers, orders, cities);
                    QueryCustomersWithMoreThanTwoOrders.Execute(customers, orders);
                    QueryGroupedByCityCustomers.Execute(customers, orders, cities);
                    QueryCustomersWithLessThanAverageOrders.Execute(customers, orders, cities);
                    QueryCityWithHighestOrderValue.Execute(customers, orders, cities);
                    QueryCustomersWithLowestOrders.Execute(customers, orders, cities);
                }
                else if (choice == "2")
                {
                    // Вычисление числа Пи
                    PLINQ.RunPiCalculation();
                }
                else if (choice == "3")
                {
                    // Выход из программы
                    break;
                }
                else
                {
                    Console.WriteLine("Ошибка, попробуйте еще раз.");
                }
            }
        }
    }
}
