using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmbossingFilter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        

        //[DllImport(@"D:\VS projects\EmbossingFilter\x64\Debug\FilterAsm.dll")]
        //static extern int MyProc1(int a, int b);

        //[DllImport(@"D:\VS projects\EmbossingFilter\x64\Debug\FilterCpp.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern int RunCpp(int a, int b);

        [STAThread]
        static void Main()
        {
            //int x = 5, y = 13;
            //int retVal = MyProc1(x, y);
            //Console.WriteLine("ASM value: " + retVal);

            //int value = RunCpp(x, y);
            //Console.WriteLine("Cpp value: " + value);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
