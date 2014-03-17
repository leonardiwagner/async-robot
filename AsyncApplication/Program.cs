using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncApplication
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Test test = new Test();

            test.DoTest(0);
            test.DoTest(1);
            test.DoTest(2);
            test.DoTest(3);
            test.DoTest(4);
            


            test.DoTestAsync(0);
            test.DoTestAsync(1);
            test.DoTestAsync(2);
            test.DoTestAsync(3);
            test.DoTestAsync(4);

            Console.Read();
        }
    }
}
