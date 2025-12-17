using Hackathon.Db.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Db
{
    public interface ICallsRepository
    {
        Task AddAsync(Call call);
        Task<List<Call>> GetAllAsync();
        Task RemoveAsync(Call call);
        Task<Call?> TryGetByIdAsync(int id);
    }
}
