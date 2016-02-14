using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace AtelierAyeshaSaveEditor {
    class AtelierAyeshaDataType {

        public static long CollectionBasketOffset = 0x392bc;
        public static long BoxOffset = 0x3a1c0;
        public static long money = 0xbd4c;
        public static long pt = 0x9d381;
        public static long pt2 = 0x9d385;


        public static Dictionary<ushort, string> ItemList = new Dictionary<ushort, string>();
        public static Dictionary<ushort, string> EffectList = new Dictionary<ushort, string>();
        public static Dictionary<ushort, string> PotentialList = new Dictionary<ushort, string>();

        public static void InitLists() {
            if (!File.Exists("item.txt") || !File.Exists("effect.txt") || !File.Exists("potential.txt")) {
                throw new Exception("物品、效果、潛能列表不存在");
            }
            StreamReader itemstream = new StreamReader(new FileStream("item.txt", FileMode.Open));
            string temp;
            while ((temp = itemstream.ReadLine()) != null) {
                string[] splited = temp.Split(',');
                ItemList.Add((ushort)IPAddress.NetworkToHostOrder((short)Convert.ToUInt16(splited[0], 16)), splited[1]);
            }
            StreamReader effectstream = new StreamReader(new FileStream("effect.txt", FileMode.Open));
            while ((temp = effectstream.ReadLine()) != null) {
                string[] splited = temp.Split(',');
                EffectList.Add((ushort)IPAddress.NetworkToHostOrder((short)Convert.ToUInt16(splited[0], 16)), splited[1]);
            }
            StreamReader potentialstream = new StreamReader(new FileStream("potential.txt", FileMode.Open));
            while ((temp = potentialstream.ReadLine()) != null) {
                string[] splited = temp.Split(',');
                PotentialList.Add((ushort)IPAddress.NetworkToHostOrder((short)Convert.ToUInt16(splited[0], 16)), splited[1]);
            }

            itemstream.Close();
            effectstream.Close();
            potentialstream.Close();
        }

    }
}
