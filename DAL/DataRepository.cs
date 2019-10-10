using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileDelivery.Models;

namespace FileDelivery.DAL
{
    public interface IDataService
    { 
        void AddEntry(Entry entry);
    }

    public class DataRepository : IDataService
    {
        protected readonly AppDBContext _context;

        public DataRepository(AppDBContext context)
        {
            _context = context;
        }

        public void AddEntry(Entry entry)
        {
            throw new NotImplementedException();
        }
    }
}
