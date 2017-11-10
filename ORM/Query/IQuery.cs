using Swisstalk.ORM.Transport;
using System;

namespace Swisstalk.ORM.Query
{
    /*
        CSVQueryBuilder qb = new CSVQueryBuilder();

        IQuery q = qb.Select("Field1", FieldFormat.Numeric)
                     .SelectAs("Field2", "SomeOtherName", FieldFormat.Numeric)
                     .SelectAtomic(FieldFormat.Numeric) //to select array of atomic values without any name
                     .SelectAll(new FieldFormat[] {FieldFormat.Numeric, FieldFormat.Numeric, FieldFormat.Numeric}) //to select all without any filtering
                     .From("c://Users/username/Projects/test.csv")
                     .As(PacketType.Dataset)
                     .Build();
    
        Packet p = q.Execute();
     */

    public interface IQuery : IDisposable
    {
        Packet Execute();
    }
}
