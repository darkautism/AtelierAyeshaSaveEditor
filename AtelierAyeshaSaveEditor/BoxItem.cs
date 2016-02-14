using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace AtelierAyeshaSaveEditor {
    class BoxItem {

        CollectionBoxItem boxitem;
        public BoxItem() {
            boxitem = new CollectionBoxItem();
            boxitem.Potential = new ushort[5] { 0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF };
            boxitem.Effect = new ushort[4] { 0xFFFF, 0xFFFF, 0xFFFF, 0xFFFF };
            boxitem.ID = 0xFFFF;
        }

        public BoxItem(BinaryReader SaveDataFile) {
            int CollectionBasketStructSize = Marshal.SizeOf(typeof(CollectionBoxItem));
            byte[] readBuffer = new byte[CollectionBasketStructSize];
            SaveDataFile.Read(readBuffer, 0, CollectionBasketStructSize);
            GCHandle handle = GCHandle.Alloc(readBuffer, GCHandleType.Pinned);
            boxitem = (CollectionBoxItem)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(CollectionBoxItem));
            handle.Free();
        }

        public byte[] ToByteArray() {
            int CollectionBasketStructSize = Marshal.SizeOf(typeof(CollectionBoxItem));
            byte[] readBuffer = new byte[CollectionBasketStructSize];
            GCHandle handle = GCHandle.Alloc(readBuffer, GCHandleType.Pinned);
            Marshal.StructureToPtr(boxitem, handle.AddrOfPinnedObject(), false);
            handle.Free();
            return readBuffer;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 0, Size = 32, CharSet = CharSet.Ansi)]
        public struct CollectionBoxItem {
            [MarshalAs(UnmanagedType.U2)]
            public ushort Unknowflag1; // 猜測是counter
            [MarshalAs(UnmanagedType.U2)]
            public ushort ID;
            [MarshalAs(UnmanagedType.U2)]
            public ushort Quality; // 品質
            [MarshalAs(UnmanagedType.U2)]
            public ushort Unknowflag2; // empty
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U2, SizeConst = 5)]
            public ushort[] Potential; // 潛能
            [MarshalAs(UnmanagedType.U2)]
            public ushort isAppraisal; // empty
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U2, SizeConst = 4)]
            public ushort[] Effect; // 效果
            [MarshalAs(UnmanagedType.U2)]
            public ushort Unknowflag4; // empty
            [MarshalAs(UnmanagedType.U2)]
            public ushort Count; // 多少個
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
            s.Append("], Quality:").Append(Quality).Append(", Count:").Append(Count).Append("}");
            return s.ToString();
        }

        public ushort ID { get { return boxitem.ID; } set { boxitem.ID = value; } }
        public string Name {
            get {
                if (AtelierAyeshaDataType.ItemList.ContainsKey(ID))
                    return AtelierAyeshaDataType.ItemList[ID];
                else return "Unknow (" + ID + ")";
            }
        }
        public int Quality { get { return boxitem.Quality >> 9; } set { boxitem.Quality = (ushort)((value << 9) + 0x42); } }
        public ushort[] Potential { get { return boxitem.Potential; } set { boxitem.Potential = value; } }
        public ushort[] Effect { get { return boxitem.Effect; } set { boxitem.Effect = value; } }
        public int Count { get { return boxitem.Count >> 8; } set { boxitem.Count = (ushort)(value << 8); } }
        public bool isAppraisal { get { return boxitem.isAppraisal == 0; } set { boxitem.isAppraisal = (value) ? (ushort)0 : (ushort)1; } }
    }
}
