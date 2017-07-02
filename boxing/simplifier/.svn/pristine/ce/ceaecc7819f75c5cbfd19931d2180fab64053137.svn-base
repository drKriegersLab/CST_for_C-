using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simplifier
{
    class blockmanager
    {
        /// <summary>
        /// Simple function to show all information of a transfer function block
        /// </summary>
        /// <param name="tf"></param>
        public static void tfDatas(tfBlock tf)
        {
            Console.WriteLine("\n### Details of a transfer function x##");
            Console.WriteLine("\ttf name:   {0}", tf.name);
            Console.WriteLine("\ttf input:  {0}", tf.input);
            Console.WriteLine("\ttf output: {0}", tf.output);
            Console.Write("\ttf numerator:\t");
            foreach (double element in tf.num)
                Console.Write(" {0} ", element);
            Console.Write("\n\ttf denominator:\t");
            foreach (double element in tf.den)
                Console.Write(" {0} ", element);
            Console.WriteLine();
        }
        /// <summary>
        /// Function for collect all definied transfer functions and their all datas
        /// </summary>
        public static void tfStat()
        {
            Console.WriteLine("\t\t#################\n\t\tStats of tfs\n");
            Console.WriteLine("Number of tf-s: {0}", lists.tflist.Count);
            foreach (tfBlock element in lists.tflist)
                tfDatas(element);
        }
        /// <summary>
        /// Functino for collect all information from a Node
        /// </summary>
        /// <param name="node"></param>
        public static void NodeDatas(nodeBlock node)
        {
            Console.WriteLine("\n####### Details of a Node block #######");
            Console.WriteLine("node name: {0}", node.name);
            Console.WriteLine("node graph: ");
            Console.WriteLine();
            Console.Write("{0} --> [{1}] --> {2}\n\t\t     -> {3}", node.input, node.name, node.output1, node.output2);
            Console.WriteLine();

        }

        /// <summary>
        /// Function for collect all definied Node-s and their all datas.
        /// </summary>

        public static void NodeStat()
        {
            Console.WriteLine("\t\t#################\n\t\tStats of Nodes\n");
            Console.WriteLine("Number of Node-s: {0}", lists.nodelist.Count);
            foreach (nodeBlock element in lists.nodelist)
                NodeDatas(element);
        }

        /// <summary>
        /// Function for collect all information from a Sum block.
        /// </summary>
        /// <param name="sum"></param>

        public static void SumDatas(sumBlock sum)
        {
            string sign1;
            string sign2;
            if (sum.sign1 == -1)
                sign1 = "-";
            else
                sign1 = "+";

            if (sum.sign2 == -1)
                sign2 = "-";
            else
                sign2 = "+";

            Console.WriteLine("\n####### Details of a Sum block #######");
            Console.WriteLine("sum name: {0}", sum.name);
            Console.WriteLine("sum graph: ");
            Console.WriteLine();

            Console.Write("[{0}] [{1}] --> \n\t\t --> [{2}]  --> [{3}] \n[{4}] [{5}] -->\n", sign1, sum.input1, sum.name, sum.output, sign2, sum.input2);
            Console.WriteLine();
        }

        /// <summary>
        /// Function for collect all definied Sum-s and their all datas.
        /// </summary>

        public static void Sumstat()
        {
            Console.WriteLine("\t\t#################\n\t\tStats of Sums\n");
            Console.WriteLine("Number of Sum-s: {0}", lists.sumlist.Count);
            foreach (sumBlock element in lists.sumlist)
                SumDatas(element);
        }

        public static void SystemStat()
        {
            Console.WriteLine("\t\t#################\n\t\tStat of all Block in the System\n");
            tfStat();
            Sumstat();
            NodeStat();

        }

        /// <summary>
        /// Function for cuonting names of blocks. Search and give an unique serial number, which is never used before.
        /// </summary>
        /// <param name="basename"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string NameCounter(string basename, Blocktypes type)
        {
            int counter = 0;
            string newname = "";
            int tempcounter = 0;
            string tempstring = "";
            bool found = false;
            // Parsing the names, and read their serial number and create a new unique number
            switch (type)
            {
                case Blocktypes.tf:
                    {
                        found = false;
                        foreach (var element in lists.tflist)
                        {
                            tempstring = "";
                            if ((element.name[0] == Convert.ToChar(basename[0])) && (element.name.Length >= (basename.Length + 2)))
                            {
                                for (int i = 0; i < basename.Length; i++)
                                    tempstring += element.name[i];
                                found = (tempstring == basename);
                                if (found)
                                {
                                    tempcounter = (Convert.ToInt32(element.name[basename.Length]) - 48) * 10 + (Convert.ToInt32(element.name[basename.Length + 1]) - 48);
                                    if (tempcounter > counter)
                                        counter = tempcounter;
                                }
                            }
                        }
                        break;
                    }
                case Blocktypes.sum:
                    {
                        found = false;
                        foreach (var element in lists.sumlist)
                        {
                            tempstring = "";
                            if ((element.name[0] == Convert.ToChar(basename[0])) && (element.name.Length >= (basename.Length + 2)))
                            {
                                for (int i = 0; i < basename.Length; i++)
                                    tempstring += element.name[i];
                                found = (tempstring == basename);
                                if (found)
                                {
                                    tempcounter = (Convert.ToInt32(element.name[basename.Length]) - 48) * 10 + (Convert.ToInt32(element.name[basename.Length + 1]) - 48);
                                    if (tempcounter > counter)
                                        counter = tempcounter;
                                }
                            }
                        }
                        break;
                    }
                case Blocktypes.node:
                    {
                        found = false;
                        foreach (var element in lists.nodelist)
                        {
                            tempstring = "";
                            if ((element.name[0] == Convert.ToChar(basename[0])) && (element.name.Length >= (basename.Length + 2)))
                            {
                                for (int i = 0; i < basename.Length; i++)
                                    tempstring += element.name[i];
                                found = (tempstring == basename);
                                if (found)
                                {
                                    tempcounter = (Convert.ToInt32(element.name[basename.Length]) - 48) * 10 + (Convert.ToInt32(element.name[basename.Length + 1]) - 48);
                                    if (tempcounter > counter)
                                        counter = tempcounter;
                                }
                            }
                        }
                        break;
                    }
            }
            //if never defined similar name, which is given, the new serial number will be '00'
            if (found)
            {
                if (counter < 10)
                    newname = basename + "0" + Convert.ToString(counter + 1);
                else
                    newname = basename + Convert.ToString(counter + 1);
            }
            else
                newname = basename + "00";
            return newname;
        }

        /// <summary>
        /// This function is useful for creating a new element inside the system.
        /// \nSearch the block, which is on the new block input, and change its output pin and return its name
        /// </summary>
        /// <param name="old_InputPin"></param>
        /// <param name="new_BlockName"></param>
        /// <param name="old_inputBlockName"></param>
        /// <returns></returns>
        public static string InputChange(string old_InputPin, string new_BlockName, string old_inputBlockName)
        {
            string newInputPin = "";
            tfBlock TempTf = new tfBlock();
            sumBlock TempSum = new sumBlock();
            nodeBlock TempNode = new nodeBlock();

            if (String.Equals(old_InputPin, "start", StringComparison.OrdinalIgnoreCase))
            {
                return "start";
            }
            if (SearchTf_byName(old_InputPin, out TempTf))
            {
                LinkedListNode<tfBlock> mark = lists.tflist.Find(TempTf);
                TempTf.output = new_BlockName;
                lists.tflist.AddAfter(mark, TempTf);
                lists.tflist.Remove(mark);
                return TempTf.name;
            }
            if (SearchSum_byName(old_InputPin, out TempSum))
            {
                LinkedListNode<sumBlock> mark = lists.sumlist.Find(TempSum);
                TempSum.output = new_BlockName;
                lists.sumlist.AddAfter(mark, TempSum);
                lists.sumlist.Remove(mark);
                return TempSum.name;
            }
            if (SearchNode_byName(old_InputPin, out TempNode))
            {
                LinkedListNode<nodeBlock> mark = lists.nodelist.Find(TempNode);
                if (TempNode.output1 == old_inputBlockName)
                    TempNode.output1 = new_BlockName;
                else
                    TempNode.output2 = new_BlockName;
                lists.nodelist.AddAfter(mark, TempNode);
                lists.nodelist.Remove(mark);
                return TempNode.name;
            }
            else
            {
                Console.WriteLine("[ERR]: could not found block with this name: {0}", old_InputPin);
                Console.ReadKey();
            }


            return newInputPin;
        }
        /// <summary>
        /// Function for searching the block, which is in the old block's output, change its inputPIN, and return its name.
        /// Thus the consistence is permanent.
        /// </summary>
        /// <param name="old_OutputPin"></param>
        /// <param name="new_Blockname"></param>
        /// <param name="old_OutputBlockName"></param>
        /// <returns></returns>
        public static string OutputChange(string old_OutputPin, string new_Blockname, string old_OutputBlockName)
        {
            string newOutputPin = "";
            tfBlock TempTf = new tfBlock();
            sumBlock TempSum = new sumBlock();
            nodeBlock TempNode = new nodeBlock();

            if (String.Equals(old_OutputPin, "stop", StringComparison.OrdinalIgnoreCase))
                return "stop";
            if(SearchTf_byName(old_OutputPin, out TempTf))
            {
                LinkedListNode<tfBlock> mark = lists.tflist.Find(TempTf);
                TempTf.input = new_Blockname;
                lists.tflist.AddAfter(mark, TempTf);
                lists.tflist.Remove(mark);
                return TempTf.name;
            }
            if(SearchSum_byName(old_OutputPin, out TempSum))
            {
                LinkedListNode<sumBlock> mark = lists.sumlist.Find(TempSum);
                if (TempSum.input1 == old_OutputBlockName)
                    TempSum.input1 = new_Blockname;
                else
                    TempSum.input2 = new_Blockname;
                lists.sumlist.AddAfter(mark, TempSum);
                lists.sumlist.Remove(mark);
                return TempSum.name;
            }
            if(SearchNode_byName(old_OutputPin, out TempNode))
            {
                LinkedListNode<nodeBlock> mark = lists.nodelist.Find(TempNode);
                TempNode.input = new_Blockname;
                lists.nodelist.AddAfter(mark, TempNode);
                lists.nodelist.Remove(mark);
                return TempNode.name;
            }
            else
            {
                Console.WriteLine("[ERR]: could not found block with this name: {0}", old_OutputPin);
                Console.ReadKey();
            }

            return newOutputPin;


        }
        /// <summary>
        /// Search Transfer Function by name
        /// The output paramter is bool, becaouse it is make simple the work with this.
        /// The founded parameter is defined in parameters field.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="tf"></param>
        /// <returns></returns>

        public static bool SearchTf_byName(string name, out tfBlock tf)
        {
            bool result = false;
            new TransferFunction(new double[0], new double[0], out tf);

            foreach (tfBlock element in lists.tflist)
            {
                if (element.name == name)
                {
                    result = true;
                    tf = element;
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// Search Sum Block by name
        /// The output paramter is bool, becaouse it is make simple the work with this.
        /// The founded parameter is defined in parameters field.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public static bool SearchSum_byName(string name, out sumBlock sum)
        {
            bool result = false;
            new Sum(1, "", 1, "", "", out sum);
            foreach (sumBlock element in lists.sumlist)
            {
                if (element.name == name)
                {
                    result = true;
                    sum = element;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Search Node Block by name
        /// The output paramter is bool, becaouse it is make simple the work with this.
        /// The founded parameter is defined in parameters field.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static bool SearchNode_byName(string name, out nodeBlock node)
        {
            bool result = false;
            new Node("", "", "", out node);
            foreach (nodeBlock element in lists.nodelist)
            {
                if (element.name == name)
                {
                    result = true;
                    node = element;
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// Search a block by name and remove it
        /// Output parameter is bool, which is tell you  the function is suceeded.
        /// </summary>
        /// <param name="name"></param>
        public static bool RemoveBlockFromList(string name)
        {
            bool found = false;

            tfBlock tf = new tfBlock();
            if (SearchTf_byName(name, out tf))
            {
                LinkedListNode<tfBlock> mark = lists.tflist.Find(tf);
                lists.tflist.Remove(mark);
                return true;
            }
            sumBlock Sum = new sumBlock();
            if (SearchSum_byName(name, out Sum))
            {
                LinkedListNode<sumBlock> mark = lists.sumlist.Find(Sum);
                lists.sumlist.Remove(mark);
                return true;
            }
            nodeBlock Node = new nodeBlock();
            if (SearchNode_byName(name, out Node))
            {
                LinkedListNode<nodeBlock> mark = lists.nodelist.Find(Node);
                lists.nodelist.Remove(mark);
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Search a block by name and remove it. 
        /// You can specify the Block type.
        /// Output parameter is bool, which is tell you  the function is suceeded.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool RemoveBlockFromList(string name, Blocktypes type)
        {
            tfBlock tf = new tfBlock();
            if (type == Blocktypes.tf)
            {
                if (SearchTf_byName(name, out tf))
                {
                    LinkedListNode<tfBlock> mark = lists.tflist.Find(tf);
                    lists.tflist.Remove(mark);
                    return true;
                }
                else
                    return false;
            }
            if (type == Blocktypes.sum)
            {
                sumBlock Sum = new sumBlock();
                if (SearchSum_byName(name, out Sum))
                {
                    LinkedListNode<sumBlock> mark = lists.sumlist.Find(Sum);
                    lists.sumlist.Remove(mark);
                    return true;
                }
                else
                    return false;
            }
            if (type == Blocktypes.node)
            {
                nodeBlock Node = new nodeBlock();
                if (SearchNode_byName(name, out Node))
                {
                    LinkedListNode<nodeBlock> mark = lists.nodelist.Find(Node);
                    lists.nodelist.Remove(mark);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
                return false;
        }

        /// <summary>
        /// Function for simplify the changing a tf an another. 
        /// </summary>
        /// <param name="old_tf"></param>
        /// <param name="new_tf"></param>
        public static void Change_Tf(tfBlock old_tf, tfBlock new_tf)
        {
            LinkedListNode<tfBlock> mark = lists.tflist.Find(old_tf);
            lists.tflist.AddAfter(mark, new_tf);
            lists.tflist.Remove(mark);
        }

        public static void Change_Sum(sumBlock old_sum, sumBlock new_sum)
        {
            LinkedListNode<sumBlock> mark = lists.sumlist.Find(old_sum);
            lists.sumlist.AddAfter(mark, new_sum);
            lists.sumlist.Remove(mark);
        }

        public static void Change_Node(nodeBlock old_node, nodeBlock new_node)
        {
            LinkedListNode<nodeBlock> mark = lists.nodelist.Find(old_node);
            lists.nodelist.AddAfter(mark, new_node);
            lists.nodelist.Remove(mark);
        }
    }
}
