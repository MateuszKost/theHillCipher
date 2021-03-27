using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using JACsharpLIB;


namespace JAproject
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Calculator calc = new Calculator();
            //console.writeline("addition: " + calc.add(3, 2));
            //console.writeline("subtraction: " + calc.subtract(3, 2));
            //console.writeline("multiplication: " + calc.multiply(3, 2));
            //console.writeline("division: " + calc.divide(3, 2));
                       
           // TypeDef int(_fastcall * MyProc1)(int, int);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
