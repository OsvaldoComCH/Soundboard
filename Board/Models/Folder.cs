using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundboard.Board.Models
{
    public class Folder
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required Keys Activator { get; set; }
    }
}
