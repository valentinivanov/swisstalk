using Swisstalk.ORM.Decoding;
using Swisstalk.ORM.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Arbaloid.Tests
{
    public class SerializationDecodingTests
    {
        private struct PacketDecodeEntry
        {
            private readonly Packet packet;
            private readonly Type decodeType;

            public PacketDecodeEntry(object value, PacketType packetType, Type decodeType)
            {
                this.packet = new Packet(value, packetType);
                this.decodeType = decodeType;
            }

            public Packet Packet
            {
                get
                {
                    return packet;
                }
            }

            public Type DecodeType
            {
                get
                {
                    return decodeType;
                }
            }
        }

        private struct DecodeSampleChild
        {
            [Decodeable]
            [DecodeAlias("ChildValue")]
            private int atomic;

            [Decodeable]
            [DecodeAlias("StringArray")]
            private string[] array;

            public DecodeSampleChild(int atomic, params string[] array)
            {
                this.atomic = atomic;
                this.array = array;
            }

            public int Atomic
            {
                get
                {
                    return atomic;
                }
            }

            public string[] Array
            {
                get
                {
                    return array;
                }
            }
        }

        private struct DecodeSample
        {
            [Decodeable]
            [DecodeAlias("MainValue")]
            private int atomic;

            [Decodeable]
            [DecodeAlias("DirectChild")]
            private DecodeSampleChild dataset;

            [Decodeable]
            [DecodeAlias("ChildArray")]
            private List<DecodeSampleChild> listOfDatasets;

            public DecodeSample(int atomic, DecodeSampleChild dataset, params DecodeSampleChild[] arrayOfDatasets)
            {
                this.atomic = atomic;
                this.dataset = dataset;
                this.listOfDatasets = arrayOfDatasets.ToList();
            }

            public int Atomic
            {
                get
                {
                    return atomic;
                }
            }

            private DecodeSampleChild Dataset
            {
                get
                {
                    return dataset;
                }
            }

            private List<DecodeSampleChild> ListOfDatasets
            {
                get
                {
                    return listOfDatasets;
                }
            }
        }

        public static void Test()
        {
            PacketDecodeEntry[] atomicPackets = new PacketDecodeEntry[] 
            {
                new PacketDecodeEntry((int)1, PacketType.Atomic, typeof(int)),
                new PacketDecodeEntry(true, PacketType.Atomic, typeof(bool)),
                new PacketDecodeEntry("Hello", PacketType.Atomic, typeof(string)),
                new PacketDecodeEntry(1.1f, PacketType.Atomic, typeof(float)),
                new PacketDecodeEntry((int)1, PacketType.Atomic, typeof(double)),
                new PacketDecodeEntry(PacketType.Atomic, PacketType.Atomic, typeof(PacketType)),
                new PacketDecodeEntry("1.0", PacketType.Atomic, typeof(float)),
            };

            PacketDecodeEntry[] arrayPackets = new PacketDecodeEntry[]
            {
                new PacketDecodeEntry(CreateArrayOfAtomics(), PacketType.Array, typeof(int[])),
                new PacketDecodeEntry(CreateArrayOfAtomics(), PacketType.Array, typeof(List<int>)),
                new PacketDecodeEntry(CreateArrayOfArrayOfAtomics(), PacketType.Array, typeof(int[][])),
                new PacketDecodeEntry(CreateArrayOfArrayOfAtomics(), PacketType.Array, typeof(List<int[]>)),
                new PacketDecodeEntry(CreateArrayOfArrayOfAtomics(), PacketType.Array, typeof(List<int>[])),
            };

            PacketDecodeEntry[] datasetPackets = new PacketDecodeEntry[]
            {
                new PacketDecodeEntry(CreateDecodeSample(), PacketType.Dataset, typeof(DecodeSample)),
            };

            DecodeAndLog(atomicPackets);
            DecodeAndLog(arrayPackets);
            DecodeAndLog(datasetPackets);
        }

        private static PacketArray CreateArrayOfAtomics()
        {
            PacketArray packetArrayOfAtomics = new PacketArray();
            packetArrayOfAtomics.Add(new Packet((int)1, PacketType.Atomic));
            packetArrayOfAtomics.Add(new Packet((int)2, PacketType.Atomic));
            packetArrayOfAtomics.Add(new Packet((int)3, PacketType.Atomic));
            packetArrayOfAtomics.Add(new Packet((int)4, PacketType.Atomic));

            return packetArrayOfAtomics;
        }

        private static PacketArray CreateArrayOfArrayOfAtomics()
        {
            PacketArray packetArrayOfArrays = new PacketArray();
            packetArrayOfArrays.Add(new Packet(CreateArrayOfAtomics(), PacketType.Array));
            packetArrayOfArrays.Add(new Packet(CreateArrayOfAtomics(), PacketType.Array));
            packetArrayOfArrays.Add(new Packet(CreateArrayOfAtomics(), PacketType.Array));

            return packetArrayOfArrays;
        }

        private static PacketDataset CreateDecodeSample()
        {
            PacketDataset root = new PacketDataset();

            root["MainValue"] = new Packet(10, PacketType.Atomic);
            root["DirectChild"] = new Packet(CreateChild(25), PacketType.Dataset);
            root["ChildArray"] = new Packet(CreateArrayOfDatasets(CreateChild(101), CreateChild(102), CreateChild(103)), PacketType.Array);

            return root;
        }

        private static PacketArray CreateArrayOfStrings()
        {
            PacketArray packetArrayOfAtomics = new PacketArray();
            packetArrayOfAtomics.Add(new Packet("Hey", PacketType.Atomic));
            packetArrayOfAtomics.Add(new Packet("Ho", PacketType.Atomic));
            packetArrayOfAtomics.Add(new Packet("Hey", PacketType.Atomic));
            packetArrayOfAtomics.Add(new Packet("Ho", PacketType.Atomic));

            return packetArrayOfAtomics;
        }

        private static PacketDataset CreateChild(int key)
        {
            PacketDataset directChild = new PacketDataset();
            directChild["ChildValue"] = new Packet(key, PacketType.Atomic);
            directChild["StringArray"] = new Packet(CreateArrayOfStrings(), PacketType.Array);

            return directChild;
        }

        private static PacketArray CreateArrayOfDatasets(params PacketDataset[] datasets)
        {
            PacketArray pa = new PacketArray();
            foreach (PacketDataset d in datasets)
            {
                pa.Add(new Packet(d, PacketType.Dataset));
            }
            return pa;
        }

        private static void DecodeAndLog(PacketDecodeEntry[] packets)
        {
            foreach (PacketDecodeEntry p in packets)
            {
                object decodedValue = Decoder.Decode(p.Packet, p.DecodeType);
                Debug.Log(string.Format("Packet decoded value: {0}:{1}", decodedValue, decodedValue.GetType().Name));
            }
        }
    }
}
