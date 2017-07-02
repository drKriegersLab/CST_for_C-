using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simplifier
{
    class BlockSimplifier
    {
        private static tfBlock temptf1 = new tfBlock();
        private static tfBlock temptf2 = new tfBlock();
        private static nodeBlock tempnode1 = new nodeBlock();
        private static sumBlock tempsum1 = new sumBlock();

        public static bool Simplify_Series()
        {
            if (BlockScanner.Scanning_Series(out temptf1, out temptf2))
            {
                Connections.series(temptf1, temptf2);
                return true;
            }
            return false;
        }

        public static bool Simplify_Parallel()
        {
            if (BlockScanner.Scanning_Parallel(out temptf1, out temptf2, out tempnode1, out tempsum1))
            {
                Connections.parallel(temptf1, temptf2, tempnode1, tempsum1);
                return true;
            }
            return false;
        }

        public static bool Simplify_Feedback()
        {
            if(BlockScanner.Scanning_Feedback(out temptf1, out temptf2, out tempnode1, out tempsum1))
            {
                Connections.feedback(temptf1, temptf2, tempsum1, tempnode1);
                return true;
            }
            return false;
        }

        public static void SimplifierCircle()
        {

            bool flag_modification = true;
            
            while(flag_modification)
            {
                flag_modification = false;
                while(Simplify_Series()) { flag_modification = true; };
                while(Simplify_Parallel()) { flag_modification = true; };
                while(Simplify_Feedback()) { flag_modification = true; };
                ConsistencyResult result = Check.consistency();
                if (!result.success)
                {
                    Check.ReadConsistencyResult(result);
                    Console.ReadKey();
                }
            }

            Console.WriteLine("[DONE] Simplification has been suceed");

        }
    }
}
