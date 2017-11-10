using Swisstalk.ORM.Transport;
using System;

namespace Swisstalk.ORM.Client.CSV.Query
{
    public static class DataRowConverter
    {
        private class ReaderGuard : IDisposable
        {
            private readonly IDataRowReader reader;

            public ReaderGuard(IDataRowReader reader)
            {
                this.reader = reader;
                this.reader.Open();
            }

            public void Dispose()
            {
                reader.Close();
            }
        }

        public static PacketDataset DatasetFrom(IDataRowReader reader)
        {
            using (ReaderGuard rg = new ReaderGuard(reader))
            {
                if (reader.HasNext())
                {
                    return DatasetFromRow(reader.Headers, reader.ReadNext());
                }
                else
                {
                    return new PacketDataset();
                }
            }
        }

        public static PacketArray DatasetArrayFrom(IDataRowReader reader)
        {
            PacketArray array = new PacketArray();

            using (ReaderGuard rg = new ReaderGuard(reader))
            {
                while (reader.HasNext())
                {
                    array.Add(new Packet(DatasetFromRow(reader.Headers, reader.ReadNext()), PacketType.Dataset));
                }
            }

            return array;
        }

        public static PacketArray AtomicArrayFrom(IDataRowReader reader)
        {
            PacketArray array = new PacketArray();

            using (ReaderGuard rg = new ReaderGuard(reader))
            {
                while (reader.HasNext())
                {
                    array.Add(new Packet(reader.ReadNext().FetchField(0), PacketType.Atomic));
                }
            }

            return array;
        }

        public static Packet AtomicValueFrom(IDataRowReader reader)
        {
            using (ReaderGuard rg = new ReaderGuard(reader))
            {
                if (reader.HasNext())
                {
                    return new Packet(reader.ReadNext().FetchField(0), PacketType.Atomic);
                }
            }

            return new Packet(null, PacketType.Atomic);
        }

        private static PacketDataset DatasetFromRow(string[] headers, IDataRow row)
        {
            PacketDataset dataset = new PacketDataset();

            for (int i = 0; i < headers.Length; ++i)
            {
                dataset[headers[i]] = new Packet(row.FetchField(i), PacketType.Atomic);
            }

            return dataset;
        }
    }
}
