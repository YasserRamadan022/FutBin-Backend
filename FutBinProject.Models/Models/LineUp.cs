using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutBinProject.Models.Models
{
    public class LineUp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public List<LineUpPlayer> lineUpPlayers { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
