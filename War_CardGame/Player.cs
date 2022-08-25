using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace War_CardGame
{
    public class Player
    {
        public string? Name { get; set; }

        // empty constructor:
        public Player() { }

        // HUMAN PLAYER constructor:
        public Player(string name)
        {
            name = Name;
        }
    }
}
