//#define DOG
//#define LINKEDLIST
//#define STRINGPARSER
#define param_list

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kacatos
{
#if DOG
    class Dog
    {
        private string name;
        private int age;

        public Dog(string name, int age)
        {
            this. name = name;
            this. age = age;
        }

        public Dog() : this("kutyuska", 3) { }
        public Dog(int age)
        {
            this.name = "nevnelkuzli";
            this.age = age;
        }
        public string Name
        {
            get { return name;  }
            set { name = value;  }
        }
    }
#endif

    class Program
    {
        static void Main(string[] args)
        {
#if DOG
            Console.WriteLine("foglalt memória: {0}", GC.GetTotalMemory(false));
            Dog a = new Dog("Rex", 2);
            Dog b = new Dog();
            Dog d = new Dog(5);
            Console.WriteLine("foglalt memória: {0}", GC.GetTotalMemory(false));
            Dog e = new Dog()
            {
                Name = "ekutya"
            };
            Console.WriteLine(e.Name);
            Console.WriteLine("foglalt memória: {0}", GC.GetTotalMemory(false));
            GC.Collect();

            Console.WriteLine(e.Name);
            Console.WriteLine("foglalt memória: {0}", GC.GetTotalMemory(false));
#endif

#if LINKEDLIST
            LinkedList<int> linked = new LinkedList<int>();
            for (int i = 0; i<10; i++)
            {
                linked.AddLast(i);
            }
            int j = 0;
            foreach(int element in linked)
            {
                Console.WriteLine(j);
                if (j > 5)
                    break;
                j++;

            }
            Console.WriteLine(j);
                
#endif
#if STRINGPARSER

            string a = "hehe";
            string b = "hehe";
            string c = "";
            int counter = 0;
            int tempcounter = 0;
            bool found = false;
            Console.WriteLine(a[0]);

            found = false;
            if ((b[0] == Convert.ToChar("h")) && (b.Length >= (a.Length+2)))
            {
                for (int i = 0; i<a.Length; i++)
                    c += b[i];
                found = (c == a);
                if (found)
                {
                    tempcounter = (Convert.ToInt32(b[a.Length])-48)*10 + (Convert.ToInt32(b[a.Length + 1])-48);
                    if (tempcounter > counter)
                        counter = tempcounter;
                }
            }
            if (found)
            {
                if (counter <= 10)
                    b = a + "0" + Convert.ToString(counter);
                else
                    b = a + Convert.ToString(counter);
            }
            Console.WriteLine(b);

            
#endif

            param_list_function("asdf", 123, 512);
            Console.ReadKey();
        }

#if param_list
        static void param_list_function(string a, params int[] list)
        {
            Console.WriteLine("param_list_function");
            if (list.Length > 0)
            {
                Console.WriteLine("nem üres");
                for (int i = 0; i < list.Length; i++)
                    Console.WriteLine("i: {0}", list[i]);
            }
        }
#endif
    }
}
