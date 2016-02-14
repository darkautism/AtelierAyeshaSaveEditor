using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AtelierAyeshaSaveEditor {
    class BasketItem {

        CollectionBasketItem basket;
        public BasketItem() {
            basket = new CollectionBasketItem();
            basket.Potential = new ushort[5] { 0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF };
            basket.Effect = new ushort[4] { 0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF };
            basket.ID = 0xFFFF;
            
        }

        public BasketItem(BinaryReader SaveDataFile) {
            int CollectionBasketStructSize = Marshal.SizeOf(typeof(CollectionBasketItem));
            byte[] readBuffer = new byte[CollectionBasketStructSize];
            SaveDataFile.Read(readBuffer, 0, CollectionBasketStructSize);
            GCHandle handle = GCHandle.Alloc(readBuffer, GCHandleType.Pinned);
            CollectionBasketItem aStruct = (CollectionBasketItem)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(CollectionBasketItem));
            handle.Free();
            basket = aStruct;
        }

        public byte[] ToByteArray() {
            int CollectionBasketStructSize = Marshal.SizeOf(typeof(CollectionBasketItem));
            byte[] readBuffer = new byte[CollectionBasketStructSize];
            GCHandle handle = GCHandle.Alloc(readBuffer, GCHandleType.Pinned);
            Marshal.StructureToPtr(basket, handle.AddrOfPinnedObject(), false);
            handle.Free();
            return readBuffer;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0, Size = 32, CharSet = CharSet.Ansi)]
        public struct CollectionBasketItem {
            [MarshalAs(UnmanagedType.U2)]
            public ushort Unknowflag0; // 目前猜測是存在與否
            [MarshalAs(UnmanagedType.U2)]
            public ushort ID;
            [MarshalAs(UnmanagedType.U2)]
            public ushort Quality; // 品質
            [MarshalAs(UnmanagedType.U2)]
            public ushort Unknowflag3;
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U2, SizeConst = 5)]
            public ushort[] Potential; // 潛能
            [MarshalAs(UnmanagedType.U2)]
            public ushort isAppraisal; // 鑑定與否
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U2, SizeConst = 4)]
            public ushort[] Effect; // 效果
            [MarshalAs(UnmanagedType.U2)]
            public ushort Unknowflag1;
            [MarshalAs(UnmanagedType.U2)]
            public ushort Count; // 目前猜測是存在與否
        }

        public override string ToString() {
            StringBuilder s = new StringBuilder();
            bool isFirst = true;
            s.Append("{ name: ").Append(AtelierAyeshaDataType.ItemList[ID]).Append(", Effect:[");
            for (int i = 0; i < 4; i++) {
                if (Effect[i] != 0xFFFF) {
                    if (isFirst) {
                        isFirst = false;
                    } else {
                        s.Append(", ");
                    }

                    s.Append("Effect ").Append(i + 1);
                    s.Append(":").Append(AtelierAyeshaDataType.EffectList[Effect[i]]);
                }
            }
            isFirst = true;
            s.Append("], Potential: [");
            for (int i = 0; i < 5; i++) {
                if (Potential[i] != 0xFFFF) {
                    if (isFirst) {
                        isFirst = false;
                    } else {
                        s.Append(", ");
                    }
                    s.Append("Potential ").Append(i + 1);
                    s.Append(":").Append(AtelierAyeshaDataType.PotentialList[Potential[i]]);
                }
            }
            s.Append("], Quality:").Append(Quality).Append("}");
            return s.ToString();
        }


        public ushort ID { get { return basket.ID; } set { basket.ID = value; } }
        public string Name {
            get {
                if (AtelierAyeshaDataType.ItemList.ContainsKey(ID))
                    return AtelierAyeshaDataType.ItemList[ID];
                else return "Unknow (" + ID + ")";
            }
        }
        public int Quality { get { return basket.Quality >> 9; } set { basket.Quality = Convert.ToUInt16((value << 9) + 0x42); } }
        public ushort[] Potential { get { return basket.Potential; } set { basket.Potential = value; } }
        public ushort[] Effect { get { return basket.Effect; } set { basket.Effect = value; } }
        public int Count { get { return basket.Count >> 8; } set { basket.Count = (ushort)(value << 8); } }
        public bool isAppraisal { get { return basket.isAppraisal == 0; } set { basket.isAppraisal = (value) ? (ushort)0 : (ushort)1; } }
    }
}
