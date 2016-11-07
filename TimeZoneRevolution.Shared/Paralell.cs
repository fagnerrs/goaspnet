using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeZoneRevolution.Shared
{
    public class Paralell
    {
        public string Process()
        {
            Stopwatch sw = Stopwatch.StartNew();

            DoIt();

            return "Elapsed = " + sw.ElapsedMilliseconds.ToString();
        }

        public string ProcessAsync()
        {
            Stopwatch sw = Stopwatch.StartNew();

            DoItAsync();

            return "Elapsed = " + sw.ElapsedMilliseconds.ToString();
        }

        private bool IsPrime(int p)
        {
            int upperBound = (int)Math.Sqrt(p);
            for (int i = 2; i <= upperBound; i++)
            {
                if (p % i == 0) return false;
            }
            return true;
        }

        private void DoIt()
        {
            IEnumerable<int> arr = Enumerable.Range(2, 4000000);

            var q =
              (from n in arr
              where IsPrime(n)
              select n);

            var list = q.ToList();
            Console.WriteLine(list.Count.ToString());
        }

        private void DoItAsync()
        {
            IEnumerable<int> arr = Enumerable.Range(2, 4000000);

            var q =
              (from n in arr.AsParallel()
               where IsPrime(n)
               select n);

            var list = q.ToList();
            Console.WriteLine(list.Count.ToString());
        }

    }
}
