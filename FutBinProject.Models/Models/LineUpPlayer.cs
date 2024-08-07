using FutBinProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutBinProject.Models.Models
{
    public class LineUpPlayer
    {
        public int Id { get; set; }
        [ForeignKey("Player")]
        public int PlayerId { get; set; }
        [ForeignKey("LineUp")]
        public int LineUpId { get; set; }
        public string Name { get; set; }
        public string Image {  get; set; }
        public int Age { get; set; }
        public int Price { get; set; }
        public int Rate { get; set; }
        public PlayerTypeEnum Type { get; set; }
        public Player Player { get; set; }
        public LineUp LineUp { get; set; }
    }
}
