using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace UBT.AI4.Bio.DivMobi.DatabaseConnector.Serializer
{
    public interface ISerializerTransaction : IDisposable
    {
        void Rollback();
        void Commit();
        void Guard(DbCommand com);
    }

    public class SerializerTransaction : ISerializerTransaction
    {
        private DbTransaction _transaction;

        public SerializerTransaction(DbTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            _transaction.Dispose();
            _transaction = null;
        }

        public void Guard(DbCommand command)
        {
            command.Transaction = _transaction;
        }
    }

    public class SerializerTransactionDummy : ISerializerTransaction
    {
        

        public void Rollback()
        {

        }

        public void Commit()
        {
            
        }

        public void Guard(DbCommand command)
        {

        }

        public void Dispose()
        {

        }
    }


}
