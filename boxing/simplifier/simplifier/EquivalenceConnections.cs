using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simplifier
{
    public class EquivalenceConnections
    {
        public static void Swapper_Sum(sumBlock sum1, sumBlock sum2)
        {
            sumBlock new_sum1, new_sum2;
            new_sum1.sign1 = sum1.sign1;
            new_sum1.input1 = sum1.input1;
            new_sum1.sign2 = sum2.sign2;
            new_sum1.input2 = sum2.input2;
            new_sum1.name = sum2.name;
            new_sum1.output = sum1.name;

            new_sum2.sign1 = 1;
            new_sum2.input1 = new_sum1.name;
            new_sum2.sign2 = sum1.sign2;
            new_sum2.input2 = sum1.input2;
            new_sum2.name = sum1.name;
            new_sum2.output = sum2.output;

            blockmanager.Change_Sum(sum1, new_sum1);
            blockmanager.Change_Sum(sum2, new_sum2);

        }

        

    }
}
