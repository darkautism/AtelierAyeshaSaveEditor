using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AtelierAyeshaSaveEditor {
    class Program {
        public static long CollectionBasketOffset = 0x392bc;
        public static long BoxOffset = 0x3a1c0;
        public static long money = 0xbd4e;
        public static long pt = 0x9d383;
        public static long pt2 = 0x9d387;

        [STAThread]
        static void Main(string[] args) {
            // new ParamSFOParser(@"D:\ISO\PS3\savedata\BLAS50502-LIST-02\PARAM.SFO");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            Application.Run( new Form1() );
            
        }


    }
}
