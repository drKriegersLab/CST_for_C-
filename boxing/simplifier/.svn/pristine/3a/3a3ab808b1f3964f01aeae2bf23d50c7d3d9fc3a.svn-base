using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simplifier
{
public class BlockScanner
{
        public static bool Scanning_Series(out tfBlock tf1, out tfBlock tf2)
        {
            tfBlock TempTf = tf1 = tf2 = new tfBlock();

            foreach (tfBlock element in lists.tflist)
            {
                if ((element.input == TempTf.name) && (TempTf.output == element.name))
                {
                    tf1 = TempTf;
                    tf2 = element;
                    Console.WriteLine("\tFound blocks in series connection: ({0}) --> ({1})", tf1.name, tf2.name);
                    return true;
                }
                if ((element.output == TempTf.name) && (TempTf.input == element.name))
                {
                    tf1 = element;
                    tf2 = TempTf;
                    Console.WriteLine("\tFound blocks in series connection: ({0}) --> ({1})", tf1.name, tf2.name);
                    return true;
                }
                TempTf = element;
            }
            return false;
        }

        public static bool Scanning_Parallel(out tfBlock tf1, out tfBlock tf2, out nodeBlock node, out sumBlock sum)
        {
            tfBlock TempTf = tf1 = tf2 = new tfBlock();
            node = new nodeBlock();
            sum = new sumBlock();

            foreach(tfBlock element in lists.tflist)
            {
                if ((element.input == TempTf.input) && (element.output == TempTf.output))
                {
                    tf1 = element;
                    tf2 = TempTf;
                    if (blockmanager.SearchSum_byName(tf1.output, out sum))
                    {
                        if (blockmanager.SearchNode_byName(tf1.input, out node))
                        {
                            Console.WriteLine("\tFound a parallel connection: ({0}) --> ({1}) , ({2}) --> ({3})", node.name, tf1.name, tf2.name, sum.name);
                            return true;
                        }
                    }
                }
                TempTf = element;
            }
            return false;
        }

        public static bool Scanning_Feedback(out tfBlock tf1, out tfBlock tf2, out nodeBlock node, out sumBlock sum)
        {
            tfBlock TempTf = tf1 = tf2 = new tfBlock();
            node = new nodeBlock();
            sum = new sumBlock();
            foreach (tfBlock element in lists.tflist)
            {
                if ((element.input == TempTf.output) && (element.output == TempTf.input))
                {
                    if (blockmanager.SearchSum_byName(element.input, out sum))
                    {
                        tf1 = element;
                        tf2 = TempTf;
                        //a folyamatos konzisztencia check-ek miatt nem kell külön megvizsgálni, hogy ez a node az a node-e, mert annak kell lennie.
                        if (blockmanager.SearchNode_byName(tf1.output, out node))
                        {
                            Console.WriteLine("\tFound a feedback connection: ({0}) --> ({1}) --> ({2}) --> ({3})", sum.name, tf1.name, node.name, tf2.name);
                            return true;
                        }
                    }
                    if (blockmanager.SearchSum_byName(TempTf.input, out sum))
                    {
                        tf1 = TempTf;
                        tf2 = element;
                        if (blockmanager.SearchNode_byName(tf1.output, out node))
                        {
                            Console.WriteLine("\tFound a feedback connection: ({0}) --> ({1}) --> ({2}) --> ({3})", sum.name, tf1.name, node.name, tf2.name);
                            return true;
                        }
                    }
                }
                TempTf = element;
            }
            return false;
        }
}
}
