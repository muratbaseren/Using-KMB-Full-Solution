using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Using_KMB_Full_Solution.Infrastructure
{
    public class MyFunctionHelper
    {
        public static string ConcatStackTrace(Exception ex, string stackTrace = "")
        {
            stackTrace += ex.StackTrace + Environment.NewLine + Environment.NewLine;

            if (ex.InnerException != null)
                stackTrace = ConcatStackTrace(ex.InnerException, stackTrace);

            return stackTrace;
        }
    }
}
