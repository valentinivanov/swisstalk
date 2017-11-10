using Swisstalk.ORM.Client;
using Swisstalk.ORM.Client.CSV.Query;
using Swisstalk.ORM.Decoding;
using Swisstalk.ORM.Query;
using Swisstalk.ORM.Transport;


namespace Swisstalk.Tests
{
    public static class ORMQueryTests
    {
        private class SampleSettings
        {
            [Decodeable]
            private float setting1;

            [Decodeable]
            private int setting2;

            [Decodeable]
            private string setting3;
        }

        private class JoinedSettings
        {
            [Decodeable]
            private SampleSettings[] all;

            [Decodeable]
            private float[] firstColumn;

            [Decodeable]
            private SampleSettings firstRow;
        }

        public static void Test()
        {
            TestSingleAtomic();
            TestArrayAtomic();
            TestSingleComposite();
            TestArrayComposite();
            TestDatasetBuilder();
        }

        private static void TestSingleAtomic()
        {
            IQuery q1 = new QueryBuilder().Select("Data2")
                                          .Atomic().Single()
                                          .From("D:/Projects/temp/test.csv")
                                          .Build();

            Packet p1 = q1.Execute();
            
            int value1 = Decoder.DecodeAs<int>(p1);
        }

        private static void TestArrayAtomic()
        {
            IQuery q2 = new QueryBuilder().Select("Data3")
                                          .Atomic().Array()
                                          .From("D:/Projects/temp/test.csv")
                                          .Build();

            Packet p2 = q2.Execute();

            string[] value2 = Decoder.DecodeAs<string[]>(p2);
        }

        private static void TestSingleComposite()
        {
            IQuery q1 = new QueryBuilder().SelectAs("Data1", "setting1")
                                          .SelectAs("Data2", "setting2")
                                          .SelectAs("Data3", "setting3")
                                          .Composite().Single()
                                          .From("D:/Projects/temp/test.csv")
                                          .Build();
            Packet p1 = q1.Execute();

            SampleSettings s = Decoder.DecodeAs<SampleSettings>(p1);
        }

        private static void TestArrayComposite()
        {
            IQuery q1 = new QueryBuilder().SelectAs("Data1", "setting1")
                                          .SelectAs("Data2", "setting2")
                                          .SelectAs("Data3", "setting3")
                                          .Composite().Array()
                                          .From("D:/Projects/temp/test.csv")
                                          .Build();
            Packet p1 = q1.Execute();

            SampleSettings[] s = Decoder.DecodeAs<SampleSettings[]>(p1);
        }

        private static void TestDatasetBuilder()
        {
            DatasetBuilder d = new DatasetBuilder();

            d.RequireField("all", new QueryBuilder().SelectAs("Data1", "setting1")
                                          .SelectAs("Data2", "setting2")
                                          .SelectAs("Data3", "setting3")
                                          .Composite().Array()
                                          .From("D:/Projects/temp/test.csv")
                                          .Build());
            d.RequireField("firstColumn", new QueryBuilder().Select("Data1")
                                          .Atomic().Array()
                                          .From("D:/Projects/temp/test.csv")
                                          .Build());
            d.RequireField("firstRow", new QueryBuilder().SelectAs("Data1", "setting1")
                                          .SelectAs("Data2", "setting2")
                                          .SelectAs("Data3", "setting3")
                                          .Composite().Single()
                                          .From("D:/Projects/temp/test.csv")
                                          .Build());

            Packet p = d.Execute();

            JoinedSettings s = Decoder.DecodeAs<JoinedSettings>(p);
        }
    }
}
