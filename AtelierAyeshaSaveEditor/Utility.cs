using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AtelierAyeshaSaveEditor {
    class Utility {
        public static void decryptSave( string gameid, string saveDir ) {
            // update PFD
            System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo( "pfdtool.exe", "-g " + gameid + " -d \"" + saveDir + "\" USR-DATA" );
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            proc.WaitForExit();
        }

        public static void encryptSave( string gameid, string saveDir ) {
            // update PFD
            System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo( "pfdtool.exe", "-g " + gameid + " -u \"" + saveDir + "\"" );
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            proc.WaitForExit();
            // encrypt savedata
            procStartInfo = new System.Diagnostics.ProcessStartInfo( "pfdtool.exe", "-g " + gameid + " -e \"" + saveDir + "\" USR-DATA" );
            procStartInfo.RedirectStandardOutput = true;
            procStartInfo.UseShellExecute = false;
            procStartInfo.CreateNoWindow = true;
            proc = new System.Diagnostics.Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
            proc.WaitForExit();
        }
    }
}
