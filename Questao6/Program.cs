using System;
using System.Collections.Generic;

namespace Questao6
{
    class Program
    {
        static void Main(string[] args)
        {
           List<int> arr = new List<int>{23,5,12,364,63,1,3,4,34,87,10};
           arr = arraySort(arr);
           foreach (var item in arr)
           {
               System.Console.Write($"{item}  ");
           }
        }

        private static List<int> arraySort(List<int> arr)
        {
            return quickSort(arr);
        }
        private static List<int> quickSort(List<int> arr)
        {
            if (arr.Count <= 1)
            {
                return arr;
            }
            arr = orderPivos(arr);
            if (arr.Count == 2)
            {
                return arr;
            }
            int pivo1 = 0;
            int pivo2 = arr.Count-1;
            var itemPivo1 = new List<int>{arr[pivo1]};
            var itemPivo2 = new List<int>{arr[pivo2]};

            arr.RemoveAt(pivo2);
            arr.RemoveAt(pivo1);

            var arrLeft = new List<int>();
            var arrMid = new List<int>();
            var arrRight = new List<int>();
            foreach (var item in arr)
            {
                if (item < itemPivo1[0])
                {
                    arrLeft.Add(item);
                }
                else if (item > itemPivo2[0])
                {
                    arrRight.Add(item);
                }
                else
                {
                    arrMid.Add(item);
                }
            }
            var retorno = new List<int>();
            retorno.AddRange(quickSort(arrLeft));
            retorno.AddRange(itemPivo1);
            retorno.AddRange(quickSort(arrMid));
            retorno.AddRange(itemPivo2);
            retorno.AddRange(quickSort(arrRight));
            return retorno;
        }
        private static List<int> orderPivos(List<int> arr)
        {
            if (arr[0] > arr[arr.Count-1])
            {
                arr[0] += arr[arr.Count-1];
                arr[arr.Count-1] = arr[0] - arr[arr.Count-1];
                arr[0] -= arr[arr.Count-1];
            }
            return arr;
        }
    }
}
