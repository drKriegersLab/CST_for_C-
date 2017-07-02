using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFKI_petproject
{

    class MathExtended
    {
        /// <summary>
        /// function for calculating a convolution vector from two other vectors
        /// Polynomns are represented in vectors, where the last index is the zero order element in polynom.
        /// </summary>
        /// <param asdf="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double[] convolution(double[] a, double[] b)
        {
            double b_temp = 0;
            double[] c = new double[a.Length + b.Length - 1];
            Console.WriteLine(c.Length);
            for (int cyc1 = 0; cyc1 < c.Length; cyc1++)
            {
                c[cyc1] = 0;
                for (int cyc2 = 0; cyc2 < cyc1 + 1; cyc2++)
                {
                    Console.WriteLine("\tcyc1: {0}", cyc1);
                    Console.WriteLine("\tcyc2: {0}", cyc2);
                    if ((cyc1 - cyc2) >= b.Length)
                        c[cyc1] += a[cyc2] * 0;
                    else if (cyc2 >= a.Length)
                        c[cyc1] += 0 * b[cyc1 - cyc2];
                    else
                        c[cyc1] += a[cyc2] * b[cyc1 - cyc2];
                }
                Console.WriteLine("vége: {0}", c[cyc1]);
            }
            return c;
        }
        /// <summary>
        /// Function for summary two polyomial vector. The vector last element is the zero order coefficient
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double[] PolySum(double[] a, double[] b)
        {
            if (a.Length > b.Length)
                b = ResizeArray(b, a.Length, true);
            else
                a = ResizeArray(a, b.Length, true);

            double[] c = new double[a.Length];

            for (int i = 0; i < c.Length; i++)
            {
                c[i] = a[i] + b[i];
            }
            return c;
        }

        /// <summary>
        /// Function for resizing an array. You can give less or more in the Desires Size field
        /// </summary>
        /// <param name="oldArray"></param>
        /// <param name="DesiredSize"></param>
        /// <returns></returns>
        public static double[] ResizeArray(double[] oldArray, int DesiredSize)
        {
            if (DesiredSize >= 0)
            {
                double[] output = new double[DesiredSize];
                for (int i = 0; i < DesiredSize; i++)
                {
                    if (i < oldArray.Length)
                        output[i] = oldArray[i];
                    else
                        output[i] = 0;

                }
                return output;
            }
            else
            {
                Console.WriteLine("[ERR] you cannot give negative value in DesiredSize field. \n\tYou must use a non-negative value, which tell to the function,\n\twhat is the desired length of the new array");
                return new double[1] { 0 };
            }
        }
        /// <summary>
        /// Function for resizing an array. If the extra bool parameter is true, we can add or remove some element on the array's begining
        /// </summary>
        /// <param name="oldArray"></param>
        /// <param name="DesiredSize"></param>
        /// <param name="X"></param>
        /// <returns></returns>
        public static double[] ResizeArray(double[] oldArray, int DesiredSize, bool X)
        {
            if(DesiredSize >= 0)
            {
                double[] output = new double[DesiredSize];
                if (X)
                {
                    if (DesiredSize < oldArray.Length)
                    {
                        for (int i = (DesiredSize - oldArray.Length); i < output.Length; i++)
                        {
                            if (i >= 0)
                                output[i] = oldArray[i + (oldArray.Length - DesiredSize) - 1];
                        }
                    }
                    for (int i = 0; i < output.Length; i++)
                    {
                        if (DesiredSize - i > oldArray.Length)
                        {
                            output[i] = 0;
                        }
                        else
                        {
                            output[i] = oldArray[i - (DesiredSize - oldArray.Length)];
                        }

                    }
                }
                else
                    output = ResizeArray(oldArray, DesiredSize);
                return output;
            }
            else
            {
                Console.WriteLine("[ERR] you cannot give negative value in DesiredSize field. \n\tYou must use a non-negative value, which tell to the function,\n\twhat is the desired length of the new array");
                return new double[1] { 0 };
            }

        }
        public static void asdf()
        {
            Console.WriteLine("asdf");
        }
    }
}
