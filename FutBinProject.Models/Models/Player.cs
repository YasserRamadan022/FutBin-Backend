using FutBinProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutBinProject.Models.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Age { get; set; }
        public int Price { get; set; }
        public int Rate { get; set; }
        public PlayerTypeEnum Type { get; set; }
        public List<LineUpPlayer> LineUpPlayers { get; set; }
    }
}
