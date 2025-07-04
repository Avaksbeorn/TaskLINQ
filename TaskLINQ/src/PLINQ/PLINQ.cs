using System;
using System.Diagnostics;
using System.Linq;

namespace LINQQueriesProject
{
    public static class PLINQ
    {
        private static Random random = new Random();

        // Метод для вычисления числа Пи методом Монте-Карло
        private static double CalculatePi(int numPoints, bool usePLINQ)
        {
            // Создаем последовательность точек. Генерируем 'numPoints' случайных точек.
            // Каждая точка представляет собой объект с двумя свойствами: X и Y, значения которых находятся в диапазоне от 0 до 1.
            var points = Enumerable.Range(0, numPoints).Select(_ => new
            {
                X = random.NextDouble(),
                Y = random.NextDouble()
            });

            // Если usePLINQ равно true, используем параллельный запрос (PLINQ) для подсчета. В противном случае используем обычный LINQ.
            var insideCircleCount = usePLINQ
                ? points.AsParallel().Count(p => (p.X * p.X + p.Y * p.Y) <= 1)
                : points.Count(p => (p.X * p.X + p.Y * p.Y) <= 1);

            // Вычисляем приближенное значение числа Пи по формуле: 4 * (число точек в круге / общее число точек).
            return (4.0 * insideCircleCount) / numPoints;
        }


        public static void RunPiCalculation()
        {
            int numPoints = 100000000; // Количество случайных точек для вычислений

            // Вычисление последовательным методом
            Stopwatch stopwatch = Stopwatch.StartNew();
            double piSequential = CalculatePi(numPoints, usePLINQ: false);
            stopwatch.Stop();
            Console.WriteLine($"Вычисление PI: {piSequential}");
            Console.WriteLine($"Время вычисления: {stopwatch.ElapsedMilliseconds} ms");

            // Вычисление параллельным методом (PLINQ)
            stopwatch.Restart();
            double piParallel = CalculatePi(numPoints, usePLINQ: true);
            stopwatch.Stop();
            Console.WriteLine($"Параллельное вычисление PI: {piParallel}");
            Console.WriteLine($"Время вычисления: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
