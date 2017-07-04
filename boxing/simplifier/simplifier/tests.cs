using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace simplifier
{
    public class tests
    {
#if teszt_01
        public static void teszt1()
        {
            string tf1name = "tf01";
            string tf2name = "tf02";
            string tf3name = "tf03";
            string tf4name = "tf04";
            string tf5name = "tf05";
            string node1name = "node01";
            new TransferFunction(tf1name, new double[2] { 1, 2 }, new double[3] { 1, 2, 1 }, "start", node1name);


            //string node2name = "node02";
            string sum1name = "sum01";
            //  string sum2name = "sum02";
            new Node(node1name, tf1name, tf2name, tf3name);
            new TransferFunction(tf2name, new double[3] { 5, 1, 2 }, new double[4] { 1, 1, 2, 1 }, node1name, sum1name);
            new TransferFunction(tf3name, new double[1] { 1 }, new double[2] { 1, 1 }, node1name, sum1name);
            new Sum(sum1name, tf2name, tf3name, Convert.ToInt32(SIGN.plus), tf4name);
            new TransferFunction(tf4name, new double[2] { 1, 2 }, new double[3] { 3, 2, 1 }, sum1name, tf5name);
            new TransferFunction(tf5name, new double[2] { 2, 2 }, new double[3] { 1, 1, 1 }, tf4name, "stop");
            //new Node(node2name, node1name, "stop", "stop");

            // new Sum(sum1name, "start", "start", 1, sum2name);
            //new Sum(sum2name, sum1name, "start", -1, "stop");
            Check.ReadConsistencyResult(Check.consistency());

            blockmanager.tfStat();
            blockmanager.Sumstat();
            blockmanager.NodeStat();


            /*
            Console.WriteLine(blockmanager.NameCounter("tf", Blocktypes.tf));
            Console.WriteLine(blockmanager.NameCounter("sum", Blocktypes.sum));
            Console.WriteLine(blockmanager.NameCounter("node", Blocktypes.node));

            Console.WriteLine("####\t####\t####\t####\n###  InputChange ###");
            Console.WriteLine(blockmanager.InputChange(tf2name, "tf3","tf3"));


            blockmanager.tfStat();
            Console.WriteLine("\t\t####\t####\t####\t####\n\t\t###  InputChange: node ###");
            nodeBlock node;
            blockmanager.SearchNode_byName(node2name, out node);
            LinkedListNode<nodeBlock> mark = lists.nodelist.Find(node);
            string oldNodeName = node.name;
            node.name = "node_asdf";
            node.input = blockmanager.InputChange(node.input, node.name, oldNodeName);
            lists.nodelist.AddAfter(mark, node);
            lists.nodelist.Remove(mark);
            blockmanager.NodeStat();

            blockmanager.tfStat();
            Console.WriteLine("\t\t####\t####\t####\t####\n\t\t###  InputChange: sum ###");
            sumBlock sum;
            blockmanager.SearchSum_byName(sum2name, out sum);
            LinkedListNode<sumBlock> mark2 = lists.sumlist.Find(sum);
            string oldSumName = sum.name;
            sum.name = blockmanager.NameCounter("sum", Blocktypes.sum);
            sum.input1 = blockmanager.InputChange(sum.input1, sum.name, oldSumName);
            lists.sumlist.AddAfter(mark2, sum);
            lists.sumlist.Remove(mark2);
            blockmanager.Sumstat();
            */


            nodeBlock node;
            tfBlock tf1;
            tfBlock tf2;
            sumBlock sum;
            blockmanager.SearchTf_byName(tf2name, out tf1);
            blockmanager.SearchTf_byName(tf3name, out tf2);
            blockmanager.SearchNode_byName(node1name, out node);
            blockmanager.SearchSum_byName(sum1name, out sum);

            Connections.parallel(tf1, tf2, node, sum);
            blockmanager.SystemStat();


            tfBlock tf4, tf5;
            blockmanager.SearchTf_byName(tf4name, out tf4);
            blockmanager.SearchTf_byName(tf5name, out tf5);
            Connections.series(tf4, tf5);
            blockmanager.SystemStat();
        }

        public static void teszt2()
        {

            string tf1name = "tf01";
            string tf2name = "tf02";
            string tf3name = "tf03";
            string tf4name = "tf04";

            string node1name = "node01";

            string sum1name = "sum01";

            new TransferFunction(tf1name, new double[2] { 2, 2 }, new double[3] { 1, 2, 3 }, "start", sum1name);
            new TransferFunction(tf2name, new double[1] { -1 }, new double[2] { 1, 2 }, sum1name, node1name);
            new TransferFunction(tf3name, new double[1] { 1 }, new double[3] { 1, 2, 3 }, node1name, sum1name);
            new TransferFunction(tf4name, new double[2] { 1, 2 }, new double[2] { 1, 2 }, node1name, "stop");
            new Sum(sum1name, tf1name, tf3name, -1, tf2name);

            new Node(node1name, tf2name, tf4name, tf3name);

            //   Check.ReadConsistencyResult(Check.consistency());


            tfBlock tf1, tf2;
            sumBlock sum;
            nodeBlock node;
            blockmanager.SearchTf_byName(tf2name, out tf1);
            blockmanager.SearchTf_byName(tf3name, out tf2);
            blockmanager.SearchSum_byName(sum1name, out sum);
            blockmanager.SearchNode_byName(node1name, out node);

            Connections.feedback(tf1, tf2, sum, node);
            blockmanager.SystemStat();
            Check.ReadConsistencyResult(Check.consistency());

        }

        /*
        SCANNING TESZTEK
        */
        //SERIES scanning

        public static void scanning_series()
        {
            string tf1name = "tf01";
            string tf2name = "tf02";
            string tf3name = "tf03";
            string tf4name = "tf04";


            new TransferFunction(tf1name, new double[2] { 2, 2 }, new double[3] { 1, 2, 3 }, "start", tf2name);
            new TransferFunction(tf2name, new double[1] { -1 }, new double[2] { 1, 2 }, tf1name, tf3name);
            new TransferFunction(tf3name, new double[1] { 1 }, new double[3] { 1, 2, 3 }, tf2name, tf4name);
            new TransferFunction(tf4name, new double[2] { 1, 2 }, new double[2] { 1, 2 }, tf3name, "stop");

            Check.ReadConsistencyResult(Check.consistency());
            blockmanager.SystemStat();


            while (BlockSimplifier.Simplify_Series()) ;

            blockmanager.SystemStat();
            Check.ReadConsistencyResult(Check.consistency());
        }

        //PARALLEL scanning

        public static void scanning_parallel()
        {
            string tf1name = "tf01";
            string tf2name = "tf02";
            string tf3name = "tf03";
            string tf4name = "tf04";

            string node1name = "node01";

            string sum1name = "sum01";

            new TransferFunction(tf1name, new double[2] { 2, 2 }, new double[3] { 1, 2, 3 }, "start", node1name);
            new TransferFunction(tf2name, new double[1] { -1 }, new double[2] { 1, 2 }, node1name, sum1name);
            new TransferFunction(tf3name, new double[1] { 1 }, new double[3] { 1, 2, 3 }, node1name, sum1name);
            new TransferFunction(tf4name, new double[2] { 1, 2 }, new double[2] { 1, 2 }, sum1name, "stop");

            new Node(node1name, tf1name, tf2name, tf3name);

            new Sum(sum1name, tf2name, tf3name, -1, tf4name);
            Check.ReadConsistencyResult(Check.consistency());
            blockmanager.SystemStat();

            while (BlockSimplifier.Simplify_Parallel()) ;

            blockmanager.SystemStat();
            Check.ReadConsistencyResult(Check.consistency());
        }

        public static void scanning_feedback()
        {
            string tf1name = "tf01";
            string tf2name = "tf02";
            string tf3name = "tf03";
            string tf4name = "tf04";

            string node1name = "node01";

            string sum1name = "sum01";

            new TransferFunction(tf1name, new double[2] { 2, 2 }, new double[3] { 1, 2, 3 }, "start", sum1name);
            new TransferFunction(tf2name, new double[1] { -1 }, new double[2] { 1, 2 }, sum1name, node1name);
            new TransferFunction(tf3name, new double[1] { 1 }, new double[3] { 1, 2, 3 }, node1name, sum1name);
            new TransferFunction(tf4name, new double[2] { 1, 2 }, new double[2] { 1, 2 }, node1name, "stop");

            new Node(node1name, tf2name, tf3name, tf4name);

            new Sum(sum1name, tf1name, tf3name, -1, tf2name);
            Check.ReadConsistencyResult(Check.consistency());
            blockmanager.SystemStat();
            /*
            tfBlock tf1;
            tfBlock tf2 = tf1 = new tfBlock();
            nodeBlock node = new nodeBlock();
            sumBlock sum = new sumBlock();

            Console.WriteLine(BlockScanner.Scanning_Feedback(out tf1, out tf2, out node, out sum));*/

            while (BlockSimplifier.Simplify_Feedback()) ;
            blockmanager.SystemStat();
            Check.ReadConsistencyResult(Check.consistency());
        }

        /*
        SIMPLIFIER KÖR TESZTELÉSE
        */
        public static void test_simplifier()
        {
            string tf1name = "tf01";
            string tf2name = "tf02";
            string tf3name = "tf03";
            string tf4name = "tf04";
            string tf5name = "tf05";

            string node1name = "node01";
            string node2name = "node02";

            string sum1name = "sum01";
            string sum2name = "sum02";

            new TransferFunction(tf1name, new double[2] { 2, 2 }, new double[3] { 1, 2, 3 }, node2name, sum1name);
            new TransferFunction(tf2name, new double[1] { -1 }, new double[2] { 1, 2 }, sum1name, node1name);
            new TransferFunction(tf3name, new double[1] { 1 }, new double[3] { 1, 2, 3 }, node1name, sum1name);
            new TransferFunction(tf4name, new double[2] { 1, 2 }, new double[2] { 1, 2 }, node1name, sum2name);
            new TransferFunction(tf5name, new double[1] { 5 }, new double[3] { 5, 5, 5 }, node2name, sum2name);

            new Node(node1name, tf2name, tf3name, tf4name);
            new Node(node2name, "start", tf1name, tf5name);

            new Sum(sum1name, SIGN.plus, tf1name, SIGN.minus, tf3name, tf2name);
            new Sum(sum2name, SIGN.plus, tf4name, SIGN.minus, tf5name, "stop");

            ConsistencyResult result = Check.consistency();
            Check.ReadConsistencyResult(result);
            if (!result.success)
                Console.ReadKey();

            blockmanager.SystemStat();

            BlockSimplifier.SimplifierCircle();

            blockmanager.SystemStat();
        }
#endif
        
        public class test_swapper
        {
            static string tf01name = "tf01";
            static string tf02name = "tf02";
            static string tf03name = "tf03";
            static string tf04name = "tf04";
            static string sum01name = "sum01";
            static string sum02name = "sum02";
            /*
            new TransferFunction(tf01name, new double[1] { 1 }, new double[2] { 1, 2 }, , );
                new TransferFunction(tf02name, new double[1] { 2 }, new double[2] { 2, 2 }, , );
                new TransferFunction(tf03name, new double[2] { 1, 2 }, new double[3] { 1, 2, 3 }, , );
                new TransferFunction(tf04name, new double[2] { 2, 2 }, new double[3] { 1, 1, 1 }, , );*/

            public static void swapper_sum_sum()
            {
                new TransferFunction(tf01name, new double[1] { 1 }, new double[2] { 1, 2 }, "start",sum01name);
                new TransferFunction(tf02name, new double[1] { 2 }, new double[2] { 2, 2 }, sum02name, "stop");
                new TransferFunction(tf03name, new double[2] { 1, 2 }, new double[3] { 1, 2, 3 }, "start" , sum01name );
                new TransferFunction(tf04name, new double[2] { 2, 2 }, new double[3] { 1, 1, 1 }, "start" , sum02name );

                new Sum(sum01name, SIGN.plus, tf01name, SIGN.plus, tf03name, sum02name);
                new Sum(sum02name, SIGN.plus, sum01name, SIGN.plus, tf04name, tf02name);

                Check.ReadConsistencyResult(Check.consistency());
            }
        }





    }
}
