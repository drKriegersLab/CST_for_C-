//#define CONVOLUTION
//#define RESIZE
#define POLYSUM

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKI_petproject
{
class Program
{
static void Main(string[] args)
        {
#if CONVOLUTION

            double[] a = new double[4] { 1, 2, 3,108 };
            double[] b = new double[3] { 1, 0,3 };
            double[] result = MathExtended.convolution(b,a);
           

            foreach (double element in result)
                Console.WriteLine(element);
#endif


#if RESIZE
            double[] v = new double[5] { 1, 2, 3, 4, 5 };
            double[] result;
            Console.WriteLine("v vector: ");
            foreach (double element in v)
                Console.Write("  {0}  ", element);
            Console.WriteLine();

            result = MathExtended.ResizeArray(v, -3, true);
            Console.WriteLine("resized vector:");
            foreach (double element in result)
                Console.Write("  {0}  ",element);

#endif

#if POLYSUM
            double[] a = new double[2] { 1, 2 };
            double[] b = new double[5] { 1, 2, 3, 4, 5 };

            double[] c = MathExtended.PolySum(a, b);
            foreach (double element in c)
                Console.Write("  {0}  ", element);
#endif

            Console.ReadKey();
        }
}

}

