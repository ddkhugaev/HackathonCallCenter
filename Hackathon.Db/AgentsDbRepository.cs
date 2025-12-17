using Hackathon.Db.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Db
{
    public class AgentsDbRepository : IAgentsRepository
    {
        private readonly DatabaseContext databaseContext;

        public AgentsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task AddAsync(Agent agent)
        {
            await databaseContext.Agents.AddAsync(agent);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<List<Agent>> GetAllAsync()
        {
            return await databaseContext.Agents.ToListAsync();
        }

        public async Task RemoveAsync(Agent agent)
        {
            databaseContext.Agents.Remove(agent);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<Agent?> TryGetByIdAsync(int id)
        {
            return await databaseContext.Agents.FirstOrDefaultAsync(agent => agent.Id == id);
        }
    }
}
