using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Db.Models
{
    public class Call
    {
        public int Id { get; set; }

        public int AgentId { get; set; }
        public Agent? Agent { get; set; }

        public string AudioFileUrl { get; set; } = null!;
        public string? TranscriptionText { get; set; }
    }
}
