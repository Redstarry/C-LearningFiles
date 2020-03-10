using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 同步_异步
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"###########同步开始测试  {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}*******");
            int l = 3;
            int m = 4;
            int n = l + m;
            for (int i = 0; i < 5; i++)
            {
                string name = string.Format($"btnSync_click_{i} ");
                this.DoSomethingLong(name);
            }
            Console.WriteLine($"###########同步结束测试  {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}*******");

        }

        private void DoSomethingLong(string name)
        {
            Console.WriteLine($"###########方法中开始 {name}  {Thread.CurrentThread.ManagedThreadId.ToString("00")}  {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}*******");
            Thread.Sleep(2000);
            Console.WriteLine($"###########方法中结束 {name}  {Thread.CurrentThread.ManagedThreadId.ToString("00")}  {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} *******");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine($"###########btnSync_click Start {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}*******");
            int l = 3;
            int m = 4;
            int n = l + m;
            Action<string> action = this.DoSomethingLong;
            for (int i = 0; i < 5; i++)
            {
                string name = string.Format($"btnSync_click_{i} ");
                action.BeginInvoke(name, null, null);
            }
            Console.WriteLine($"###########btnSync_click End {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}*******");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //IAsyncResult asyncResult = null;
            //Console.WriteLine($"***********btnAdvanced_click Start {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("HH:mm:ss.fff")}******");
            //{
            //    //AsyncCallback callback = ar =>
            //    //{
            //    //    Console.WriteLine(ar.AsyncState);
            //    //    Console.WriteLine("计算结束。。。");
            //    //};
            //    Action<string> action = this.DoSomethingLong;
            //    asyncResult = action.BeginInvoke("BtnAdvanced_Click ", null, null);
            //    asyncResult.AsyncWaitHandle.WaitOne();//等待asyncResult任务完成后，后面的代码才执行。
            //    //asyncResult.AsyncWaitHandle.WaitOne();
            //}
            //Console.WriteLine($"***********btnAdvanced_click End {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("HH:mm:ss.fff")}******");

            Func<int> func = () =>
            {
                return DateTime.Now.Day;
            };
            IAsyncResult asyncResult = func.BeginInvoke(tp =>
            {
                int days = func.EndInvoke(tp);
                Console.WriteLine(days);
                Console.WriteLine(days);
                Console.WriteLine(days);
                Console.WriteLine(days);
                Console.WriteLine(days);
            }, null);
            int day = func.EndInvoke(asyncResult);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //DoSomething doSomething = DoSomethingLong;
            //Action<string> action = DoSomethingLong;


            //ThreadStart thread = () =>
            // {
            //     DoSomethingLong("tp");

            // };
            //Thread thread1 = new Thread(thread);
            //thread1.Start();
            {
                //Action threadStart = () =>
                //{
                //    Thread.Sleep(2000);
                //    Console.WriteLine($"***********开启新线程以后执行的任务 Start {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("HH:mm:ss.fff")}******");

                //};

                //Action action = () =>
                //{
                //    Console.WriteLine($"***********新线程任务执行完成后执行 End {Thread.CurrentThread.ManagedThreadId.ToString("00")} {DateTime.Now.ToString("HH:mm:ss.fff")}******");

                //};
                //NewThread(threadStart, action);
            }
            {

                Func<int> func = () =>
                {
                    return DateTime.Now.Day;
                };
                Func<int> Result = ThreadWith(func);
                int FuncResult = Result.Invoke();
                Console.WriteLine(FuncResult);
            }
        }
        //public delegate void DoSomething(string name);

        public Func<T> ThreadWith<T>(Func<T> func)
        {
            T t = default(T);
            ThreadStart threadStart = () =>
             {
                 t = func.Invoke();
             };
            Thread thread = new Thread(threadStart);
            thread.Start();
            return new Func<T>(() =>
            {
                thread.Join();
                return t;
            });
        }
        public void NewThread(Action threadStart, Action func)
        {
            ThreadStart NewthreadStart = () =>
            {
                threadStart.Invoke();
                func.Invoke();
            };
            Thread thread = new Thread(NewthreadStart);
            thread.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Console.WriteLine($"ThreadPool 开始测试... {Thread.CurrentThread.ManagedThreadId},{DateTime.Now.ToString("HH:mm:ss.fff")}");
            ManualResetEvent manualResetEvent = new ManualResetEvent(false);
            ThreadPool.QueueUserWorkItem(o =>
            {
                Text_threadPool();
                manualResetEvent.Set();
            });

            manualResetEvent.WaitOne();//当false的时候 Set() 打开，WaitOne 生效； 当true的时候，Set()关闭，WaitOne() 不生效。
            Console.WriteLine($"ThreadPool 结束测试... {Thread.CurrentThread.ManagedThreadId},{DateTime.Now.ToString("HH:mm:ss.fff")}");

        }
        public void Text_threadPool()
        {
            Console.WriteLine($"ThreadPool 开启线程... {Thread.CurrentThread.ManagedThreadId},{DateTime.Now.ToString("HH:mm:ss.fff")}");
            Thread.Sleep(2000);
            Console.WriteLine($"ThreadPool 结束线程... {Thread.CurrentThread.ManagedThreadId},{DateTime.Now.ToString("HH:mm:ss.fff")}");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //{ 
            //    Console.WriteLine($"Task 开始测试... {Thread.CurrentThread.ManagedThreadId},{DateTime.Now.ToString("HH:mm:ss.fff")}");
            //    //{
            //    //    Task task = new Task(() =>
            //    //   {
            //    //       Console.WriteLine($"Task 开启线程... {Thread.CurrentThread.ManagedThreadId},{DateTime.Now.ToString("HH:mm:ss.fff")}");
            //    //    //Thread.Sleep(2000);
            //    //    Console.WriteLine($"Task 结束线程... {Thread.CurrentThread.ManagedThreadId},{DateTime.Now.ToString("HH:mm:ss.fff")}");
            //    //   });
            //    //    task.Start();
            //    //    //task.RunSynchronously(); //Task 同步
            //    //    //Thread.Sleep(5000);
            //    //}
            //    Task task = Task.Run(()=>
            //    {
            //        Console.WriteLine($"Task 开启线程... {Thread.CurrentThread.ManagedThreadId},{DateTime.Now.ToString("HH:mm:ss.fff")}");
            //        Console.WriteLine($"Task 结束线程... {Thread.CurrentThread.ManagedThreadId},{DateTime.Now.ToString("HH:mm:ss.fff")}");

            //    });
            //    Console.WriteLine($"Task 结束测试... {Thread.CurrentThread.ManagedThreadId},{DateTime.Now.ToString("HH:mm:ss.fff")}");
            //}
            {
                TaskFactory taskFactory = new TaskFactory();
                List<Task> tasks = new List<Task>();
                Console.WriteLine("老师开始讲课");
                lecture("第一章");
                lecture("第二章");
                lecture("第三章");
                lecture("第四章");
                lecture("第五章");
                Console.WriteLine("讲课完毕");
                Console.WriteLine("开始做作业，需要多个人共同完成。");
                tasks.Add(taskFactory.StartNew(() => { homework("小明", "美工"); }));
                tasks.Add(taskFactory.StartNew(() => { homework("小王", "前端"); }));
                tasks.Add(taskFactory.StartNew(() => { homework("小微", "后台"); }));
                //Task.WaitAll(tasks.ToArray());
                //Task.WaitAny(tasks.ToArray());
                taskFactory.ContinueWhenAll(tasks.ToArray(), t =>
                 {
                     Console.WriteLine("做完了，老师点评。。。");
                 });


            }
        }
        public void lecture(string str)
        {
            Console.WriteLine($"{str} 开始学习。。。 ");
            long Result = 0;
            for (int i = 0; i < 1000_000; i++)
            {
                Result += i;
            }

        }
        public void homework(string name, string work)
        {
            Console.WriteLine($"{name},完成{work}");
            long Result = 0;
            for (int i = 0; i < 1000_000; i++)
            {
                Result += i;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Console.WriteLine("*************Invoke Start*****************");
            Parallel.Invoke(
                () => { Console.WriteLine($"Invoke_01  {Thread.CurrentThread.ManagedThreadId}  {DateTime.Now.ToString("HH:mm:ss.fff")}"); },
                () => { Console.WriteLine($"Invoke_02  {Thread.CurrentThread.ManagedThreadId}  {DateTime.Now.ToString("HH:mm:ss.fff")}"); },
                () => { Console.WriteLine($"Invoke_03  {Thread.CurrentThread.ManagedThreadId}  {DateTime.Now.ToString("HH:mm:ss.fff")}"); },
                () => { Console.WriteLine($"Invoke_04  {Thread.CurrentThread.ManagedThreadId}  {DateTime.Now.ToString("HH:mm:ss.fff")}"); });
            Console.WriteLine("*************Invoke End*****************");

            Console.WriteLine("**************For Start************************");
            Parallel.For(0, 10, t => { Console.WriteLine($"For  {Thread.CurrentThread.ManagedThreadId}  {DateTime.Now.ToString("HH:mm:ss.fff")}"); });
            Console.WriteLine("**************For End************************");

            Console.WriteLine("**************Foreach Start************************");
            Parallel.ForEach(new int[] { 1, 3, 5, 7, 9}, t => { Console.WriteLine($"Foreach  {Thread.CurrentThread.ManagedThreadId}  {DateTime.Now.ToString("HH:mm:ss.fff")}"); });
            Console.WriteLine("**************Foreach End************************");
            Console.WriteLine("*************Invoke ParallelOptions Start**********************");
            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = 3;
            Parallel.Invoke(parallelOptions,
                () => { DoSomethingLong("ParallelOptions"); },
                () => { DoSomethingLong("ParallelOptions");  },
                () => { DoSomethingLong("ParallelOptions"); },
                () => { DoSomethingLong("ParallelOptions"); },
                () => { DoSomethingLong("ParallelOptions"); });
            Console.WriteLine("*************Invoke ParallelOptions End**********************");

        }

        private void button8_Click(object sender, EventArgs e)
        {
            List<Task> tasks = new List<Task>();
            try
            {
                for (int i = 0; i < 1000; i++)
                {
                    string name = $"button8_Click_{i}";
                    tasks.Add(Task.Run(()=> {
                        Console.WriteLine(name);
                        if (name.Equals("button8_Click_10"))
                        {
                            throw new Exception("button8_Click_10异常");
                        }
                        else if (name.Equals("button8_Click_15"))
                        {
                            throw new Exception("button8_Click_15异常");
                        }
                    }));
                }
                Task.WaitAll(tasks.ToArray());
            }
            
            catch (AggregateException aex)
            {
                foreach (var ex in aex.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
