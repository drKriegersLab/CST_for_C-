using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simplifier
{
    public  struct tfBlock {
        public string name;
        public string input;
        public string output;
        public double[] num;
        public double[] den;
    }
    public struct sumBlock
    {
        public string name;
        public int sign1;
        public string input1;
        public int sign2;
        public string input2;
        public string output;
    }

    public struct nodeBlock
    {
        public string name;
        public string input;
        public string output1;
        public string output2;
    }

    enum Blocktypes
    {
        tf,
        sum,
        node
    }

    enum PINS
    {
        input,
        input2,
        output,
        output2
    }
    public enum SIGN
    {
        minus = -1,
        plus = 1
    }

    /// <summary>
    /// Class for managing of transfer function's representations in this program. For example creating, storaging.
    /// </summary>
public class TransferFunction
    {
        private tfBlock newtfblock = new tfBlock();

        /// <summary>
        /// Create a virtual transfer function for calculations. This function return with a tf struct,
        /// but it won't be storaged in tf list.
        /// </summary>
        /// <param name="num"></param>
        /// <param name="den"></param>
        /// <param name="tf"></param>
        public TransferFunction(double[] num, double[] den, out tfBlock tf)
        {
            tf.name = "";
            tf.input = "";
            tf.output = "";
            tf.num = num;
            tf.den = den;
        }
        /// <summary>
        /// Create a new transfer function struct and storage it.
        /// The transfer function is represented in its numerator and denominator vectors
        /// </summary>
        /// <param name="name"></param>
        /// <param name="num"></param>
        /// <param name="den"></param>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public TransferFunction(string name, double[] num, double[] den, string input, string output)
        {
            newtfblock.name = name;
            newtfblock.input = input;
            newtfblock.output = output;
            newtfblock.num = num;
            newtfblock.den = den;
            StorageTf();
        }
        
        
        private void StorageTf()
        {
            lists.tflist.AddLast(newtfblock);
        }
    }
    /// <summary>
    /// Class for managing of sum's representations
    /// </summary>
public class Sum
    {
        private sumBlock newsum = new sumBlock();
        /// <summary>
        /// Create a new virtual sum element. This is necessary for some applications
        /// If the sign field is '1', the sum's sign will be '+', otherwise it will be '-'
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="output"></param>
        /// <param name="sum"></param>
        public Sum(int sign1, string input1,int sign2, string input2, string output, out sumBlock sum)
        {
            sum.name = "";
            sum.input1 = input1;
            sum.input2 = input2;
            if (sign1 == 1 || sign1 == -1)
                sum.sign1 = sign1;
            else
                sum.sign1 = 1;
            if (sign2 == 1 || sign2 == -1)
                sum.sign2 = sign2;
            else
                sum.sign2 = 1;
            sum.output = output;

        }

        /// <summary>
        /// Create a new sum element. A sum is represented with its in- or outputs and its sign. 
        /// If you type a "+" the sign will be '+', oherwhise it will be '-'
        /// </summary>
        /// <param name="name"></param>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="sign"></param>
        /// <param name="output"></param>
        public Sum(string name, string sign1, string input1, string sign2, string input2, string output)
        {
            newsum.name = name;
            newsum.input1 = input1;
            newsum.input2 = input2;
            newsum.output = output;

            if (sign1 == "+")
                newsum.sign1 = 1;
            else if (sign1 == "-")
                newsum.sign1 = -1;
            else
                newsum.sign1 = 1;
            StorageSum();
        }
        /*
        public Sum(string name, string input1, string input2, SIGN s , string output)
        {
            newsum.name = name;
            newsum.input1 = input1;
            newsum.input2 = input2;
            newsum.sign = Convert.ToInt32(s);
            newsum.output = output;
        }*/
        /// <summary>
        /// Create a new sum element.
        /// A sum is represented its in- or outputs and its sign.
        /// If you give '1' in the sign field, it will be '+', otherwise its will be '-'
        /// </summary>
        /// <param name="name"></param>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <param name="sign"></param>
        /// <param name="output"></param>
        public Sum(string name, int sign1, string input1, int sign2, string input2, string output)
        {
            newsum.name = name;
            newsum.input1 = input1;
            newsum.input2 = input2;
            if (sign1 == 1 || sign1 == -1)
                newsum.sign1 = sign1;
            else
                newsum.sign1 = 1;
            if (sign2 == 1 || sign2 == -1)
                newsum.sign2 = sign2;
            else
                newsum.sign2 = 1;
            newsum.output = output;
            StorageSum();
        }
        /// <summary>
        /// Készít egy Sum block-ot. Az előjelek itt megadhatóak SIGN enum-ként.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sign1"></param>
        /// <param name="input1"></param>
        /// <param name="sign2"></param>
        /// <param name="input2"></param>
        /// <param name="output"></param>
       public Sum(string name, SIGN sign1, string input1, SIGN sign2, string input2, string output)
        {
            newsum.name = name;
            newsum.input1 = input1;
            newsum.input2 = input2;
            newsum.output = output;

            if (sign1 == SIGN.minus || sign1 == SIGN.plus)
            {
                newsum.sign1 = (int)sign1;
            }
            else
                newsum.sign1 = 1;

            if (sign2 == SIGN.minus || sign2 == SIGN.plus)
            {
                newsum.sign2 = (int)sign2;
            }
            else
                newsum.sign2 = 1;

            StorageSum();

        }



        private void StorageSum()
        {
            lists.sumlist.AddLast(newsum);
        }
    }
    /// <summary>
    /// Class for managing of node's representations.
    /// </summary>
public class Node
    {
        private nodeBlock newNode;

        public Node(string input, string output1, string output2, out nodeBlock node)
        {
            node.name = "";
            node.input = input;
            node.output1 = output1;
            node.output2 = output2;
        }
        public Node(string name, string input, string output1, string output2)
        {
            newNode.name = name;
            newNode.input = input;
            newNode.output1 = output1;
            newNode.output2 = output2;
            StorageNode();
        }

       

        private void StorageNode()
        {
            lists.nodelist.AddLast(newNode);
        }
    }


}
