using Airline.DataProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airline.Logic.Services
{
    public interface ICommitService
    {
        void Commit();
    }
    public class CommitService : ICommitService
    {
        private readonly AirlineContext _context;

        public CommitService(AirlineContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
