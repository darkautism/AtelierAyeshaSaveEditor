using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AtelierAyeshaSaveEditor {
    class ParamSFOParser {
        PsfHdr psfHdr;
        PsfSec[] psfSec;
        Encoding encode = Encoding.UTF8;
        SFOPair[] pairs; // 寫入要用
        Dictionary<string, SFOPair> pairs_dic; // 拿取好用
        string filePath;
        public ParamSFOParser(string filePath ) {
            this.filePath = filePath;
            BinaryReader bs = new BinaryReader(new FileStream(filePath, FileMode.Open));
            byte[] tmpbuffer = bs.ReadBytes(Marshal.SizeOf(typeof(PsfHdr)));
            GCHandle handle = GCHandle.Alloc(tmpbuffer, GCHandleType.Pinned);
            psfHdr = (PsfHdr) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PsfHdr));
            handle.Free();
            
            Console.Write("{0}個區塊", psfHdr.nsects);
            psfSec = new PsfSec[psfHdr.nsects];
            for (int i = 0; i < psfHdr.nsects; i++ ) {
                tmpbuffer = bs.ReadBytes(Marshal.SizeOf(typeof(PsfSec)));
                handle = GCHandle.Alloc(tmpbuffer, GCHandleType.Pinned);
                psfSec[i] = (PsfSec)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(PsfSec));
                handle.Free();
            }

            pairs_dic = new Dictionary<string, SFOPair>();
            pairs = new SFOPair[psfHdr.nsects];
            for (int i = 0; i < psfHdr.nsects; i++) {
                bs.BaseStream.Position = psfSec[i].label_off + psfHdr.label_ptr;
                tmpbuffer = readByteString(bs);
                pairs[i] = new SFOPair();
                pairs[i].lable = encode.GetString(tmpbuffer);
                pairs[i].psfsec = psfSec[i];
                bs.BaseStream.Position = psfSec[i].data_off + psfHdr.data_ptr;
                tmpbuffer = bs.ReadBytes(psfSec[i].datafield_used);
                pairs[i].type = psfSec[i].data_type;
                switch (psfSec[i].data_type) {
                    case 0:
                        pairs[i].value = tmpbuffer;
                        break;
                    case 2:
                        pairs[i].value = encode.GetString(tmpbuffer);
                        break;
                    case 4 :
                        pairs[i].value = BitConverter.ToInt32(tmpbuffer, 0);
                        break;
                    default:
                        break;
                }

                pairs_dic.Add(pairs[i].lable, pairs[i]);
                Console.WriteLine("Label:{0}, Type:{1:X}, Value:{2}", pairs[i].lable, pairs[i].type, pairs[i].value);
            }


            
            bs.Close();
            saveSFO();
            // Console.Read();
        }

        public void saveSFO() {
            BinaryWriter bw = new BinaryWriter(new FileStream(filePath, FileMode.Open));
            byte[] tmpBuffer;

            for (int i = 0; i < psfHdr.nsects; i++) {
                bw.BaseStream.Position = psfSec[i].data_off + psfHdr.data_ptr;
                switch (psfSec[i].data_type) {
                    case 0:
                        tmpBuffer=(byte[])pairs[i].value;
                        break;
                    case 2:
                        tmpBuffer=encode.GetBytes((string)pairs[i].value);
                        break;
                    case 4:
                        tmpBuffer = BitConverter.GetBytes((int)pairs[i].value);
                        break;
                    default:
                        tmpBuffer=(byte[])pairs[i].value;
                        break;
                }

                psfSec[i].datafield_used = tmpBuffer.Length;
                bw.Write(tmpBuffer);
            }

            bw.BaseStream.Position = 0;
            tmpBuffer = new byte[Marshal.SizeOf(typeof(PsfHdr))];
            GCHandle handle = GCHandle.Alloc(tmpBuffer, GCHandleType.Pinned);
            Marshal.StructureToPtr(psfHdr, handle.AddrOfPinnedObject(), false);
            handle.Free();
            bw.Write(tmpBuffer);

            for (int i = 0; i < psfHdr.nsects; i++) {
                tmpBuffer = new byte[Marshal.SizeOf(typeof(PsfSec))];
                handle = GCHandle.Alloc(tmpBuffer, GCHandleType.Pinned);
                Marshal.StructureToPtr(psfSec[i], handle.AddrOfPinnedObject(), false);
                handle.Free();
                bw.Write(tmpBuffer);
            }
            bw.Close();
        }

        public object getValue(string key) {
            return pairs_dic[key].value;
        }

        class SFOPair {
            public string lable;
            public byte type;
            public object value;
            public PsfSec psfsec;
        }

        private byte[] readByteString(BinaryReader bs) {
            List<byte> array = new List<byte>();
            byte tmp;
            while ( ( tmp = bs.ReadByte()) != 0 ) {
                array.Add(tmp);
            }
            return array.ToArray();
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        public struct PsfHdr {

            /// char[4]
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U1)]
            public char[] psf;

            /// byte[4]
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I1)]
            public byte[] unknow;

            /// int
            public int label_ptr;

            /// int
            public int data_ptr;

            /// int
            public int nsects;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 0, Size = 16, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
        public struct PsfSec {

            /// short
            public short label_off;

            /// byte
            public byte unknow;

            /// byte
            public byte data_type;

            /// int
            public int datafield_used;

            /// int
            public int datafield_size;

            /// int
            public int data_off;
        }

    }
}
