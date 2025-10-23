using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soundboard.Board.Models
{
    public class Binding
    {
        public required Keys Key { get; set; }
        public required IList<Guid> SoundEffects { get; set; }
    }
}
