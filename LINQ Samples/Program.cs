using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQ_Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProjectionSamples();
            int[] numbers = { 1, 2, 3, 4, 5 };
            Console.WriteLine(numbers.Aggregate((x1, x2) => x1 * x2).ToString());
            var defaultOrFirst = from n in numbers
                                 //where n > 3
                                 select n;

            numbers.SelectMany(i=> i.ToString()).ToList().ForEach(i => Console.WriteLine(i));
            Console.WriteLine(numbers.Single(i=> i==2));
            Console.WriteLine(numbers.SingleOrDefault(i => i == 3));
            //Console.WriteLine(defaultOrFirst.ToList().FirstOrDefault());

            string[] fruits = { "Apple", "Oranges", "Apple", "Banana","Apple", "Orange"};
            (from f in fruits
             group f by f into g
             select new { Key = g.Key, Count = g.Count() }).ToList().ForEach(i => Console.WriteLine(string.Format("Fruit:{0} Number{1}", i.Key, i.Count)));
            Console.WriteLine("Using Lambda");
            fruits.GroupBy(f => f).Select(i => new { Key = i.Key, Count = i.Count() }).ToList().ForEach(i => Console.WriteLine(string.Format("Fruit:{0} Number{1}", i.Key, i.Count)));

            fruits = (from f in fruits
                      group f by f into g
                      where g.Count() > 2
                      select g.Key + "(" + g.Count() + ")").ToArray();
            Console.WriteLine("------");
            string[] fruits1 = { "Apple", "Oranges", "Ananas", "Banana", "Olive" };
            var grpFLetter = from f in fruits1
                             group f by f.First() into g
                             select new { Name = g.Key, Val = g };
            foreach (var f in grpFLetter)
            {
                foreach (var g in f.Val)
                { 
                    Console.WriteLine(string.Format("{0}-{1}",f.Name,g)); 
                }
            }

            Console.ReadLine();
        }

    }
}
