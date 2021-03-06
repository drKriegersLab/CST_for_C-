﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simplifier
{
    /// <summary>
    /// Class for managing functions of equivalence connections
    /// </summary>
    class Connections
    {
        //Node Output Pins: nXoY -- nodeX.outputY
        private enum NodeOutputPINs {n1o1 = 11, n1o2 = 12, n2o1 = 21, n2o2 = 22};
        private static tfBlock TempTf = new tfBlock();
        /// <summary>
        /// This function will be reduce a parallel connection.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="tf1"></param>
        /// <param name="tf2"></param>
        /// <param name="sum"></param>
        public static void parallel(tfBlock tf1, tfBlock tf2,nodeBlock node, sumBlock sum)
        {
            tfBlock newtf = new tfBlock();
            newtf.num = MathExtended.PolySum(MathExtended.convolution(tf1.num, tf2.den), MathExtended.convolution(tf2.num, tf1.den), sum.sign1, sum.sign2);
            newtf.den = MathExtended.convolution(tf1.den, tf2.den);
            newtf.name = blockmanager.NameCounter("parallel", Blocktypes.tf);
            newtf.input = blockmanager.InputChange(node.input, newtf.name, node.name);
            newtf.output = blockmanager.OutputChange(sum.output, newtf.name, sum.name);
            blockmanager.Change_Tf(tf1, newtf);
            blockmanager.RemoveBlockFromList(tf2.name, Blocktypes.tf);
            blockmanager.RemoveBlockFromList(node.name, Blocktypes.node);
            blockmanager.RemoveBlockFromList(sum.name, Blocktypes.sum);

            Console.WriteLine("\tSimplified a parallel connection: ({0})", newtf.name);
            ConsistencyResult result;
            result = Check.consistency();
            if (!result.success)
                Check.ReadConsistencyResult(result);
        }
        /// <summary>
        /// This function will be calculate one reduced transfer function from two tfs, which are connected in parallel
        /// This function can not modify the lists of blocks!
        /// </summary>
        /// <param name="tf1"></param>
        /// <param name="tf2"></param>
        /// <param name="sign"></param>
        /// <param name="newtf"></param>
        public static tfBlock parallel(tfBlock tf1, tfBlock tf2, SIGN sign1, SIGN sign2)
        {
            tfBlock newtf;
            newtf.name = "";
            newtf.input = "";
            newtf.output = "";

            newtf.num = MathExtended.PolySum(MathExtended.convolution(tf1.num, tf2.den), MathExtended.convolution(tf2.num, tf1.den), sign1, sign2);
            newtf.den = MathExtended.convolution(tf1.den, tf2.den);
            return newtf;
        }

        /// <summary>
        /// This function will be reduce a series connection.
        /// </summary>
        /// <param name="tf1"></param>
        /// <param name="tf2"></param>

        public static tfBlock series(tfBlock tf1, tfBlock tf2, params bool[] virtual_switcher)
        {
            tfBlock newtf;
            if ((virtual_switcher.Length > 0) && (virtual_switcher[0] == true) )
            {
                newtf.num = MathExtended.convolution(tf1.num, tf2.num);
                newtf.den = MathExtended.convolution(tf1.den, tf2.den);

                newtf.name = "";
                newtf.input = "";
                newtf.output = "";
            }
            else
            {
                newtf.num = MathExtended.convolution(tf1.num, tf2.num);
                newtf.den = MathExtended.convolution(tf1.den, tf2.den);

                newtf.name = blockmanager.NameCounter("series", Blocktypes.tf);
                newtf.input = blockmanager.InputChange(tf1.input, newtf.name, tf1.name);
                newtf.output = blockmanager.OutputChange(tf2.output, newtf.name, tf2.name);
                blockmanager.Change_Tf(tf1, newtf);
                blockmanager.RemoveBlockFromList(tf2.name, Blocktypes.tf);

                Console.WriteLine("\tSimplified a series connection: ({0})", newtf.name);
                blockmanager.SystemStat();
                ConsistencyResult result;
                result = Check.consistency();
                if (!result.success)
                {
                    Check.ReadConsistencyResult(result);
                    Console.ReadKey();
                }
            }
            return newtf;

        }
        
       /// <summary>
       /// Implementation of feedback connection reducing
       /// </summary>
       /// <param name="tf1"></param>
       /// <param name="tf2"></param>
       /// <param name="sum"></param>
       /// <param name="node"></param>
        public static void feedback(tfBlock tf1, tfBlock tf2, sumBlock sum, nodeBlock node)
        {
            tfBlock tf;
            if (sum.sign2 < 0)
                tf = parallel(identicalTf(), series(tf1, tf2, true), SIGN.plus, SIGN.plus);
            else
                tf = parallel(identicalTf(), series(tf1, tf2, true), SIGN.plus, SIGN.minus);
            tf = series(tf1, inverseTf(tf, true), true);

            tf.name = blockmanager.NameCounter("feedback", Blocktypes.tf);
            tf.input = blockmanager.InputChange(sum.input1, tf.name, sum.name);
            //tf.output:
            blockmanager.SearchTf_byName(node.output1, out TempTf); //előfordulhat, hogy a node outpu1-én van a visszacsatolás ága!
            if (TempTf.name == tf2.name) //ha az első outputon van a visszacsatolási ág
                tf.output = blockmanager.OutputChange(node.output2, tf.name, node.name);
            else //egyébként
                tf.output = blockmanager.OutputChange(node.output1, tf.name, node.name);

            blockmanager.Change_Tf(tf1, tf);
            blockmanager.RemoveBlockFromList(tf2.name, Blocktypes.tf);
            blockmanager.RemoveBlockFromList(sum.name, Blocktypes.sum);
            blockmanager.RemoveBlockFromList(node.name, Blocktypes.node);

            Console.WriteLine("\tSimplified a feedback connection: ({0})", tf.name);
            ConsistencyResult result = Check.consistency();
            if (!result.success)
            {
                Check.ReadConsistencyResult(result);
                Console.ReadKey();
            }

        }

        /// <summary>
        /// Helper Function, which creates an inverse transfer function. 
        /// </summary>
        /// <param name="tf"></param>
        /// <param name="virtual_switcher"></param>
        /// <returns></returns>
        public static tfBlock inverseTf(tfBlock tf, params bool[] virtual_switcher)
        {
            tfBlock newtf = tf;
            newtf.num = tf.den;
            newtf.den = tf.num;
            if ((virtual_switcher.Length > 0) && (virtual_switcher[0] == true))
            {
                return newtf;
            }
            else
            {
                blockmanager.Change_Tf(tf, newtf);
                return newtf;
            }
        }

        public static void identicalTf(string name, string input, string output, params bool[] virtual_switcher)
        {
            new TransferFunction(name, new double[1] { 1 }, new double[1] { 1 }, input, output);
        }
        public static tfBlock identicalTf()
        {
            tfBlock tf;
            new TransferFunction(new double[1] { 1 }, new double[1] { 1 }, out tf);
            return tf;
        }

        public static void swapper(sumBlock sum1, sumBlock sum2)
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

            blockmanager.InputChange(sum1.input1, new_sum1.name, sum1.name);
            blockmanager.OutputChange(sum2.output, new_sum2.name, sum2.name);

            Check.Consistency_CheckAndPause();
        }

        public static void swapper(nodeBlock node1, nodeBlock node2)
        {
            nodeBlock new_node1, new_node2;

            new_node1.name = node2.name;
            new_node1.input = node1.input;
            new_node1.output1 = node1.name;
            new_node1.output2 = node2.output2;

            new_node2.name = node1.name;
            new_node2.input = new_node1.name;
            new_node2.output1 = node2.output2;
            new_node2.output2 = node1.output2;

            blockmanager.Change_Node(node1, new_node1);
            blockmanager.Change_Node(node2, new_node2);

            blockmanager.InputChange(node1.input, new_node1.name, node1.name);
            blockmanager.OutputChange(node2.output1, new_node2.name, node2.name);

            Check.Consistency_CheckAndPause();

        }

        public static void swapper(nodeBlock node1, nodeBlock node2, string output1, string output2, string output3)
        {
            nodeBlock new_node1, new_node2;
            new_node1.name = node2.name;
            new_node1.input = node1.input;
            new_node1.output1 = node2.name;
            new_node2.name = node1.name;
            new_node2.input = new_node1.name;
            new_node2.output1 = output1;
            new_node1.output2 = output2;
            new_node2.output2 = output3;

            blockmanager.Change_Node(node1, new_node1);
            blockmanager.Change_Node(node2, new_node2);

            blockmanager.InputChange(node1.input, new_node1.name, node1.name);

            //eldöntjük melyik output melyik old_node-hoz tartozik, 
            //és ez alapján már ki tudjuk cserélni a PIN túloldalán levő block inputját
            //out1
            if (output1 == node1.output1)
                blockmanager.OutputChange(node1.output1, new_node2.name, node1.name);
            if (output1 == node1.output2)
                blockmanager.OutputChange(node1.output2, new_node2.name, node1.name);
            if (output1 == node2.output1)
                blockmanager.OutputChange(node2.output1, new_node2.name, node2.name);
            if (output1 == node2.output2)
                blockmanager.OutputChange(node2.output2, new_node2.name, node2.name);

            //out2
            if (output2 == node1.output1)
                blockmanager.OutputChange(node1.output1, new_node1.name, node1.name);
            if (output2 == node1.output2)
                blockmanager.OutputChange(node1.output2, new_node1.name, node1.name);
            if (output2 == node2.output1)
                blockmanager.OutputChange(node2.output1, new_node1.name, node2.name);
            if (output2 == node2.output2)
                blockmanager.OutputChange(node2.output2, new_node1.name, node2.name);

            //out3
            if (output3 == node1.output1)
                blockmanager.OutputChange(node1.output1, new_node2.name, node1.name);
            if (output3 == node1.output2)
                blockmanager.OutputChange(node1.output2, new_node2.name, node1.name);
            if (output3 == node2.output1)
                blockmanager.OutputChange(node2.output1, new_node2.name, node2.name);
            if (output3 == node2.output2)
                blockmanager.OutputChange(node2.output2, new_node2.name, node2.name);
            
            //konzisztencia viszgálat
            Check.Consistency_CheckAndPause();
            
        }

        public static void swapper(sumBlock old_sum, tfBlock old_tf)
        {
            tfBlock new_tf;
            new_tf = old_tf;
            new_tf.input = old_sum.input1;
            new_tf.output = old_sum.name;
            //az eredeti tf lecserélése az eltoltra
            blockmanager.Change_Tf(old_tf, new_tf);
            string ParallelTfName = blockmanager.NameCounter(old_tf.name, Blocktypes.tf);
            //a párhuzamos, elolt tf létrehozása
            new TransferFunction(ParallelTfName, old_tf.num, old_tf.den, old_sum.input2, old_sum.name);
            

            sumBlock new_sum;
            new_sum.name = old_sum.name;
            new_sum.input1 = new_tf.name;
            new_sum.sign1 = old_sum.sign1;
            new_sum.input2 = ParallelTfName;
            new_sum.sign2 = old_sum.sign2;
            new_sum.output = old_tf.output;
            //az eredeti sum lecserélése az eltoltra
            blockmanager.Change_Sum(old_sum, new_sum);

            //in- és outputokra kötött blokkok megfelelő PIN-jeinek átírása
            blockmanager.InputChange(old_sum.input1, new_tf.name, old_sum.name);
            blockmanager.InputChange(old_sum.input2, ParallelTfName, old_sum.name);
            blockmanager.OutputChange(old_tf.output, new_sum.name, old_tf.name);
            

            //ellenőrzés, hogy minden rendben van-e
            Check.Consistency_CheckAndPause();
        }

        public static void swapper(tfBlock old_tf, sumBlock old_sum)
        {
            tfBlock new_tf = old_tf;
            new_tf.input = old_sum.name;
            new_tf.output = old_sum.output;
            //az eredeti tf cserélése az eloltra
            blockmanager.Change_Tf(old_tf, new_tf);
            //az inverz, párhuzamos tf hozzáadása

            tfBlock inv_tf = inverseTf(old_tf, true);
            inv_tf.name = blockmanager.NameCounter(old_tf.name, Blocktypes.tf);
            lists.tflist.AddLast(inv_tf);

            //eltolt sum létrehozása és a régi törlése a listából
            blockmanager.RemoveBlockFromList(old_sum.name, Blocktypes.sum);
            new Sum(old_sum.name, old_sum.sign1, old_tf.input, old_sum.sign2, inv_tf.name, new_tf.name);

            //in- és outputokra kötött blokkok megfelelő PIN-jeinek átírása
            blockmanager.InputChange(old_tf.input, old_sum.name, old_tf.name);
            blockmanager.InputChange(old_sum.input2, inv_tf.name, old_sum.name);
            blockmanager.OutputChange(old_sum.output, new_tf.name, old_sum.name);

            //ellenőrzés
            Check.Consistency_CheckAndPause();
            
        }

        public static void swapper(tfBlock old_tf, nodeBlock old_node)
        {
            tfBlock new_tf = old_tf;
            //az eredeti tf lecserélése az eltoltra a listában
            new_tf.input = old_node.name;
            new_tf.output = old_node.output1;
            blockmanager.Change_Tf(old_tf, new_tf);
            //párhuzamos tf létrehozása
            string para_tfname = blockmanager.NameCounter(old_tf.name, Blocktypes.tf);
            new TransferFunction(para_tfname, old_tf.num, old_tf.den, old_node.name, old_node.output2);
            //eltolt node létrehozása, és a régi törlése
            blockmanager.RemoveBlockFromList(old_node.name, Blocktypes.node);
            new Node(old_node.name, old_tf.input, old_tf.name, para_tfname);
            //in- és outputokra kötött blokkok megfelelő PIN-jeinek átírása
            blockmanager.InputChange(old_tf.input, old_node.name, old_tf.name);
            blockmanager.OutputChange(old_node.output1, old_tf.name, old_node.name);
            blockmanager.OutputChange(old_node.output2, para_tfname, old_node.name);
            //konzisztencia check
            Check.Consistency_CheckAndPause();
        }

        public static void swapper(nodeBlock old_node, tfBlock old_tf)
        {
            tfBlock new_tf = old_tf;
            //az eredeti tf lecserélése az eltoltra
            new_tf.input = old_node.input;
            new_tf.output = old_node.name;
            blockmanager.Change_Tf(old_tf, new_tf);
            //párhuzamos inverz tf létrehozása
            tfBlock inv_tf = inverseTf(old_tf, true);
            inv_tf.name = blockmanager.NameCounter(old_tf.name, Blocktypes.tf);
            lists.tflist.AddLast(inv_tf);
            //eltolt node létrehozása
            blockmanager.RemoveBlockFromList(old_node.name, Blocktypes.node);
            new Node(old_node.name, new_tf.input, old_tf.output, inv_tf.name);
            //in- és outputokra kötött blokkok megfelelő PIN.jeinek átírása
            blockmanager.InputChange(old_node.input, new_tf.name, old_node.name);
            blockmanager.OutputChange(old_tf.output, old_node.name, old_tf.name);
            blockmanager.OutputChange(old_node.output2, inv_tf.name, old_node.name);
            //konzisztencia check
            Check.Consistency_CheckAndPause();
        }


    }
}
