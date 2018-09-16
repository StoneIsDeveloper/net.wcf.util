using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Demo.Run();
            // Demo.UseStack();
            //Demo.UseQueue();
            //Demo.UserSorted();
            // Demo.Observe();    

            // DatabaseDemo.RunProcodureTran();
            DatabaseDemo.RunTran();

            Console.ReadKey();
        }
    }
    public class SortedPeopleByAge : IComparer<Person>
    {
        //升序
        public int Compare(Person first, Person last)
        {
            if (first.Age > last.Age)
            {
                return 1;
            }
            if (first.Age < last.Age)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    public static class Demo
    {

        public static void Observe()
        {
            ObservableCollection<Person> people = new ObservableCollection<Person>()
            {
                new Person { FirstName = "aa", LastName = "abc", Age = 22 },
                new Person { FirstName = "bb", LastName = "abc", Age = 15 },
            };

            people.CollectionChanged += People_CollectionChanged;

            people.Add(new Person { FirstName = "bb", LastName = "abc", Age = 50 });
            people.RemoveAt(1);

        }

        private static void People_CollectionChanged(object sender,
            System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Console.WriteLine("Action for this event {0}", e.Action);

            //1.是移除项
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                Console.WriteLine("here are the old items");
                foreach (Person p in e.OldItems)
                {
                    Console.WriteLine(p.ToString());
                }
                Console.WriteLine();
            }

            //2.是添加项
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                Console.WriteLine("here are the new items");
                foreach (Person p in e.NewItems)
                {
                    Console.WriteLine(p.ToString());
                }
                Console.WriteLine();
            }

        }

        public static void UserSorted()
        {
            SortedSet<Person> sortedSet = new SortedSet<Person>(new SortedPeopleByAge())
            {
                new Person { FirstName = "aa", LastName = "abc", Age = 22 },
                new Person { FirstName = "bb", LastName = "abc", Age = 15 },
                new Person { FirstName = "bb", LastName = "abc", Age = 50},
                new Person { FirstName = "dd", LastName = "abc", Age = 30 }
            };

            foreach (Person p in sortedSet)
            {
                Console.WriteLine(p);
            }
            Console.WriteLine("add new person.......");
            sortedSet.Add(new Person { FirstName = "yy", LastName = "abc", Age = 25 });
            sortedSet.Add(new Person { FirstName = "uu", LastName = "abc", Age = 10 });
            foreach (Person p in sortedSet)
            {
                Console.WriteLine(p);
            }
            Console.ReadKey();

        }

        /// <summary>
        /// Queue  先进先出
        /// </summary>
        public static void UseQueue()
        {
            Queue<Person> queue = new Queue<Person>();
            queue.Enqueue(new Person { FirstName = "aa", LastName = "abc", Age = 22 });
            queue.Enqueue(new Person { FirstName = "bb", LastName = "abc", Age = 22 });
            queue.Enqueue(new Person { FirstName = "cc", LastName = "abc", Age = 22 });

            Console.WriteLine("{0} is first in line", queue.Peek().FirstName);

            GetCoffe(queue.Dequeue());
            GetCoffe(queue.Dequeue());
            GetCoffe(queue.Dequeue());
            try
            {
                GetCoffe(queue.Dequeue());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();


        }

        public static void GetCoffe(Person p)
        {
            Console.WriteLine("{0} get coffe", p.FirstName);
        }

        /// <summary>
        /// Stack  先进后出
        /// </summary>
        public static void UseStack()
        {
            Stack<Person> list = new Stack<Person>();
            list.Push(new Person { FirstName = "a", LastName = "abc", Age = 22 });
            list.Push(new Person { FirstName = "f", LastName = "xyz", Age = 99 });

            Console.WriteLine("peek:{0}", list.Peek().ToString());
            Console.WriteLine("pop off:{0}", list.Pop().ToString());
            Console.WriteLine("peek:{0}", list.Peek().ToString());
            Console.ReadKey();
        }

        public static void Run()
        {
            List<string> list = new List<string>();
            List<Person> people = new List<Person>()
            {
                new Person{ FirstName = "a",LastName = "b",Age = 22 },
                new Person{ FirstName = "d", LastName="c",Age = 33  }
            };
            people.Insert(2, new Person { FirstName = "dd", LastName = "dd", Age = 40 });
            Person[] array = people.ToArray();

            var enumrator = people.GetEnumerator();

            Console.ReadLine();

        }
    }

    public class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        override
        public string ToString()
        {
            return FirstName + "_" + LastName + "_" + Age;
        }
    }
}
