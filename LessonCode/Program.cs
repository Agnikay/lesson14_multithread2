using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LessonCode
{

    class Program
    {
        static int a = 42;
        static object syncA = new object();
        static object syncB = new object();
        static ManualResetEvent manualReset = new ManualResetEvent(false);
        static AutoResetEvent autoReset = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            //Thread thread = new Thread(MethodA);
            //Thread thread2 = new Thread(MethodB);
            //thread.IsBackground = true;
            //thread.Start();
            //thread2.Start();

            //Task t1 = Task.Run(() => TestTask());
            //Task t2 = Task.Factory.StartNew(() => TestTask2());

            //Task<string> t3 = Task.Run(() => 
            //{
            //    Thread.Sleep(5000);
            //    return "Hello from task with result";
            //});
            //Console.WriteLine("Start wait until task finishes");
            //t3.Wait();
            //Console.WriteLine("Task ich been finished: {0}", t3.Result);

            //Task<int> t4 = Task.Run(() =>
            //{
            //    Console.WriteLine("Start task1");                
            //    Thread.Sleep(1000);
            //    Console.WriteLine("Task1 finished, return");
            //    return 1;
            //});

            //t4.ContinueWith((taskToCo) =>
            //{
            //    Console.WriteLine("Task2 started");
            //    Console.WriteLine("From previous task: {0}", taskToCo.Result);
            //});
            //Task.Run(() =>
            //{
            //    Thread.Sleep(2000);
            //    autoReset.Set();
            //    manualReset.Set();
            //});
            //Console.WriteLine("Wait for manual");
            //manualReset.WaitOne();
            //Console.WriteLine("Yes");
            //manualReset.WaitOne();
            //Console.WriteLine("Again!");
            //manualReset.Reset();
            //autoReset.WaitOne();
            //Console.WriteLine("Auto");
            //autoReset.WaitOne();
            //Console.WriteLine("Auto again");

            //TestReadAsync();

            TestOwnAsync();

             
            Console.WriteLine("Hello from main thread: {0}", Thread.CurrentThread.ManagedThreadId);
            Console.ReadLine();
            Console.WriteLine("Good bye!!!");
        }

        static async private void TestOwnAsync()
        {
            try
            {
                Console.WriteLine("Start task");
                var a = HeavyTask();
                Console.WriteLine("After calling heavy");
                await a;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Heavy exception: {0}", ex);
            }
        }

        static async void TestReadAsync()
        {
            using (StreamReader reader = new StreamReader(@"D:\Linux\android.tgz"))
            {
                //string a = await reader.ReadToEndAsync();
                Task<string> t = reader.ReadToEndAsync();
                Console.WriteLine("Run read");

                string a = await t;
                Console.WriteLine("");
            }
        }

        private static Task<string> HeavyTask()
        {
            return Task.Run(() =>
            {
                Console.WriteLine("Start heavy");
                //Thread.Sleep(1000);
                Console.WriteLine("Throw!");
                throw new Exception("AAAAAA");
                return "Hello from Heavy";
            });
        }

        static void TestTask()
        {
            Console.WriteLine("Hello from Task: {0}", Thread.CurrentThread.ManagedThreadId);
        }

        static void TestTask2()
        {
            Console.WriteLine("Hello from Task ran from StartNew: {0}", Thread.CurrentThread.ManagedThreadId);
        }

        static void TestConstructor()
        {
            using (SomethingDisposable some = new SomethingDisposable())
            {                
                Console.WriteLine("Inside using");
            }
        }

        static void MethodA(object state)
        {
            lock (syncA)
            {
                Console.WriteLine("MethodA took syncA");
                lock (syncB)
                {
                    Console.WriteLine("MethoA took syncB");
                }
            }
        }

        static void MethodB(object state)
        {
            lock (syncB)
            {
                Console.WriteLine("MethodB took syncB");
                lock (syncA)
                {
                    Console.WriteLine("MethodB took syncA");
                }
            }
        }

        static void DoSomething(object state)
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine("Hello from thread: {0}", Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(100);
            }
        }

        static void PrintA()
        {
            Console.WriteLine("Run PrintA");
            //lock (syncA)
            //{
                Console.WriteLine("PrinA took lock");
                if (a == 42)
                {
                    Console.WriteLine("a == 42");
                    for (int i = 0; i < 100000000; i++)
                    {
                        continue;
                    }
                    Console.WriteLine("I expect that a = 42 and it is: {0}", a);
                }
                else
                {
                    Console.WriteLine("a == {0}", a);
                }
            //}
        }

        static void Increment()
        {
            Console.WriteLine("Run Increment");
            //lock (syncA)
            //{
                Console.WriteLine("Increment a");
                a++;
            //}
        }
    }
}
