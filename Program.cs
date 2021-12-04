using System;
using System.Collections.Generic;

namespace List
{
    class Program
    {
        public void FindElementsForSum(List<uint> list, ulong sum, out int start, out int end)
        {
            start = 0;
            end = 0;
            
            if (list.Count == 0)
            {
                return;
            }
            
            ulong curSum = 0;
            //предположим, что у нас List с обращением к [i] за константу,
            //если LinkedList то реализация немного изменится (например end++ -> end.next и list[end] -> end.data)
            //сложность O(2n) для худшего случая
            while (start <= end)
            {
                if (curSum < sum)
                {
                    if (end == list.Count)
                    {
                        break;
                    }
                    curSum += list[end];
                    end++;
                }
                else if (sum == curSum)
                {
                    end = end == 0 ? 1 : end;
                    return;
                }
                else if (curSum > sum)
                {
                    curSum -= list[start];
                    start++;
                }
            }
            start = 0;
            end = 0;
            return;
        }

        //Наивная реализация для проверки
        public static void FindElementsNaive(List<uint> list, ulong sum, out int start, out int end)
        {
            end = 0;
            start = 0;

            //перебираем все пары
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i; j <= list.Count; j++)
                {
                    ulong cursum = 0;
                    //находим сумму от i до j
                    for (int index = i; index < j; index++)
                    {
                        cursum += list[index];
                        if (cursum == sum)
                        {
                            start = i;
                            end = j;
                            return;
                        }
                    }
                }
            }
        }

        public static void Test(List<uint> list, ulong sum)
        {
            int start = 0;
            int end = 0;
            int startExp = 0;
            int endExp = 0;
            new Program().FindElementsForSum(list, sum, out start, out end);
            FindElementsNaive(list, sum, out startExp, out endExp);
            Console.WriteLine("Test results expected: {0},{1}, got: {2},{3}", startExp, endExp, start, end);
            Console.WriteLine("Results = exprected results: {0}", start == startExp && end == endExp);
        }

        static void Main(string[] args)
        {
            Test(new List<uint> { 0, 1, 2, 3, 4, 5, 6, 7 }, 11);//примеры по образцу
            Test(new List<uint> { 4, 5, 6, 7 }, 18);
            Test(new List<uint> { 0, 1, 2, 3, 4, 5, 6, 7 }, 88);
            Test(new List<uint> { 0 }, 0); // один элемент, он же искомый
            Test(new List<uint> { 0, 10 }, 10); // два элемента, второй искомый
            Test(new List<uint> { 0 }, 1); // один элемент, искомых нет
            Test(new List<uint> { 0,0,0,0,0,0,0,1,9 }, 10); // последний элемент
            Test(new List<uint> { 0, 0, 0, 0, 0, 0, 1000, 10 }, 10); // худший случай
        }
    }
}
