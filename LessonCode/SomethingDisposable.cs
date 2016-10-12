using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LessonCode
{
    class SomethingDisposable : IDisposable
    {
        private int a;
        public static void Some()
        {
            Console.WriteLine("Some");
        }
        public SomethingDisposable()
        {
            Console.WriteLine("Call constructor");
        }

        public void Dispose()
        {
            Console.WriteLine("Call dispose");
        }
    }
}
