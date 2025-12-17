using Hackathon.Db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Db
{
    public class CallsDbRepository : ICallsRepository
    {
        private readonly DatabaseContext databaseContext;

        public CallsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task AddAsync(Call call)
        {
            await databaseContext.Calls.AddAsync(call);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<List<Call>> GetAllAsync()
        {
            return await databaseContext.Calls.ToListAsync();
        }

        public async Task RemoveAsync(Call call)
        {
            databaseContext.Calls.Remove(call);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<Call?> TryGetByIdAsync(int id)
        {
            return await databaseContext.Calls.Include(c => c.Agent).FirstOrDefaultAsync(call => call.Id == id);
        }
    }
}
