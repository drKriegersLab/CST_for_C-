﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simplifier
{

    public struct ConsistencyResult
    {
        public bool success;
        public string ErrorBlockName;
        public int type;
        public int pinNum;
    }
class Check
{
        /// <summary>
        /// Class for checking mainly parameters in the system
        /// Consistency checking: 
        ///         Checks connections in the system. If every block's inputs and
        ///         outputs are connected, the result passes, otherwise the method
        ///         returns a structure, which consists of the problematic structure's
        ///         name, block type and which PIN is wrong
        /// </summary>
        private static tfBlock TempTf = new tfBlock();
        private static sumBlock TempSum = new sumBlock();
        private static nodeBlock TempNode = new nodeBlock();
        public static ConsistencyResult consistency()
        {

            foreach (tfBlock tf in lists.tflist)
            {
                if(!consistency_InputPINcheck(tf.input, tf.name))
                    return consistency_result(false, tf.name, Blocktypes.tf, PINS.input);
                if(!consistency_OutputPINcheck(tf.output, tf.name))
                    return consistency_result(false, tf.name, Blocktypes.tf, PINS.output);
            }
            foreach(sumBlock sum in lists.sumlist)
            {
                if (!consistency_InputPINcheck(sum.input1, sum.name))
                    return consistency_result(false, sum.name, Blocktypes.sum, PINS.input);
                if (!consistency_InputPINcheck(sum.input2, sum.name))
                    return consistency_result(false, sum.name, Blocktypes.sum, PINS.input2);
                if (!consistency_OutputPINcheck(sum.output, sum.name))
                    return consistency_result(false, sum.name, Blocktypes.sum, PINS.output);
            }
            foreach(nodeBlock node in lists.nodelist)
            {
                if (!consistency_InputPINcheck(node.input, node.name))
                    return consistency_result(false, node.name, Blocktypes.node, PINS.input);
                if (!consistency_OutputPINcheck(node.output1, node.name))
                    return consistency_result(false, node.name, Blocktypes.node, PINS.output);
                if (!consistency_OutputPINcheck(node.output2, node.name))
                    return consistency_result(false, node.name, Blocktypes.node, PINS.output2);
            }
            return consistency_result(true, "system is consistent", 0, 0);

        }

        public static bool consistency_InputPINcheck(string ConnectedBlockName, string BlockName)
        {
            if (String.Equals(ConnectedBlockName, "start", StringComparison.OrdinalIgnoreCase))
                return true;

            if (blockmanager.SearchTf_byName(ConnectedBlockName, out TempTf))
                if (TempTf.output == BlockName)
                    return true;

            if (blockmanager.SearchSum_byName(ConnectedBlockName, out TempSum))
                if (TempSum.output == BlockName)
                    return true;

            if (blockmanager.SearchNode_byName(ConnectedBlockName, out TempNode))
                if ((TempNode.output1 == BlockName) || (TempNode.output2 == BlockName))
                    return true;
            return false;
        }

        public static bool consistency_OutputPINcheck(string ConnectedBlockName, string BlockName)
        {
            if (String.Equals(ConnectedBlockName, "stop", StringComparison.OrdinalIgnoreCase))
                return true;

            if (blockmanager.SearchTf_byName(ConnectedBlockName, out TempTf))
                if (TempTf.input == BlockName)
                    return true;

            if (blockmanager.SearchSum_byName(ConnectedBlockName, out TempSum))
                if ((TempSum.input1 == BlockName) || (TempSum.input2 == BlockName))
                    return true;

            if (blockmanager.SearchNode_byName(ConnectedBlockName, out TempNode))
                if (TempNode.input == BlockName)
                    return true;
            return false;
        }

        public static ConsistencyResult consistency_result(bool success, string Block, Blocktypes type, PINS PINnum)
        {
            ConsistencyResult result;
            result.success = success;
            result.ErrorBlockName = Block;
            result.type = Convert.ToInt32(type);
            result.pinNum = Convert.ToInt32(PINnum);
            return result;
        }

        public static void ReadConsistencyResult(ConsistencyResult result)
        {
            Console.WriteLine("********************************************\nConsistency result, after checking the consistency:");
            Console.WriteLine();
            if (result.success)
            {
                Console.WriteLine("success: \t{0}", result.success);
                Console.WriteLine("\t\t{0}", result.ErrorBlockName);
            }
            else
            {
                Console.WriteLine("success: \t\t\t{0}", result.success);
                Console.WriteLine("name of block with problem:\t{0}", result.ErrorBlockName);
                Console.WriteLine("block type:\t\t\t{0}", (Blocktypes)result.type);
                Console.WriteLine("which pin is wrong:\t\t{0}", (PINS)result.pinNum);
            }
        }

        public static void Consistency_CheckAndPause()
        {
            ConsistencyResult result = Check.consistency();
            if (!result.success)
            {
                Check.ReadConsistencyResult(result);
                Console.ReadKey();
            }
        }
}
}
