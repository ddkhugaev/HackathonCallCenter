using Hackathon.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Db
{
    public interface IAgentsRepository
    {
        Task AddAsync(Agent agent);
        Task<List<Agent>> GetAllAsync();
        Task RemoveAsync(Agent agent);
        Task<Agent?> TryGetByIdAsync(Guid id);
        Task<Agent?> TryGetByFullNameAsync(string fullName);
    }
}
