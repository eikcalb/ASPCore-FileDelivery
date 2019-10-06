using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDelivery.Models;

namespace FileDelivery.DAL
{
    interface IDataService
    { 
        void AddEntry(Entry entry);
    }

    public class DataRepository : IDataService
    {
        public DataRepository()
        {

        }

        public void AddEntry(Entry entry)
        {
            throw new NotImplementedException();
        }
    }
}
