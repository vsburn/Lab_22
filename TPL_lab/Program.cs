using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPL_lab
{
    class Program
    {
        #region Задача
        /* 
         * Сформировать массив случайных целых чисел (размер  задается пользователем).
         * Вычислить сумму чисел массива и максимальное число в массиве.  
         * Реализовать  решение  задачи  с  использованием  механизма  задач продолжения.
        */
        #endregion
        static void Main(string[] args)
        {
            Console.Write("Введите размер массива: ");
            int size = Convert.ToInt32(Console.ReadLine());
            Console.Write("Максимально допустимое значение случайных чисел: ");
            int maxValue = Convert.ToInt32(Console.ReadLine());

            Task<int[]> taskArrayCreation = new Task<int[]>(() => arrayCreation(size, maxValue));
            Task<int[]> taskSum = taskArrayCreation.ContinueWith(array => DisplaySum(array.Result));
            Task taskFindMax = taskSum.ContinueWith(array => DisplayMax(array.Result));
            taskArrayCreation.Start();
            taskFindMax.Wait();

            Console.WriteLine("\nMain завершился");
            Console.ReadLine();
        }
        static int[] arrayCreation(int size, int maxValue)
        {

            int[] array = new int[size];
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(maxValue);
                Console.WriteLine(array[i]);
            }
            return array;
        }
        static int[] DisplaySum(int[] array)
        {
            int sum = 0;
            foreach (int a in array)
            {
                sum += a;
            }
            Console.WriteLine($"\nСумма значений: {sum}");
            return array;
        }
        static void DisplayMax(int[] array)
        {
            int max = array[0];
            foreach (int a in array)
            {
                if (a > max)
                    max = a;
            }
            Console.WriteLine($"\nМаксимальное число: {max}");
        }
    }
}
