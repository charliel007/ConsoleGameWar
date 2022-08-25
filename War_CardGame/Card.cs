namespace War_CardGame
{
    /*
    public enum Suit { Clubs, Diamonds, Hearts, Spades }
    public enum Value { Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

    public class Card
    {
        // public string Name { get; set; }
        public Suit Suit { get; }
        public Value Value { get; }
        public bool IsRed { get; set; }

        // constructor:
        public Card(Suit Suit, Value Value)
        {
            Suit suit = Suit;
            Value value = Value;
        }

        public override string ToString()   // new def of the ToString function
        {
            return $"{Value} of {Suit}";
        }
    }
    */

    public enum Suites { Clubs, Diamonds, Hearts, Spades }
    public class Card
    {
        public int Value
        {
            get;
            set;
        }
        public Suites Suite
        {
            get;
            set;
        }
        //Full name
        public string NamedValue
        {
            get
            {
                string name;
                switch (Value)
                {
                    case (11):
                        name = "Jack";
                        break;
                    case (12):
                        name = "Queen";
                        break;
                    case (13):
                        name = "King";
                        break;
                    case (14):
                        name = "Ace";
                        break;
                    default:
                        name = Value.ToString();
                        break;
                }
                return name;
            }
        }

        public string CardValue
        {
            get
            {
                string name;
                switch (Value)
                {
                    case (1):
                        name = " 1";
                        break;
                    case (2):
                        name = " 2";
                        break;
                    case (3):
                        name = " 3";
                        break;
                    case (4):
                        name = " 4";
                        break;
                    case (5):
                        name = " 5";
                        break;
                    case (6):
                        name = " 6";
                        break;
                    case (7):
                        name = " 7";
                        break;
                    case (8):
                        name = " 8";
                        break;
                    case (9):
                        name = " 9";
                        break;
                    case (10):
                        name = "10";
                        break;
                    case (11):
                        name = " J";
                        break;
                    case (12):
                        name = " Q";
                        break;
                    case (13):
                        name = " K";
                        break;
                    case (14):
                        name = " A";
                        break;
                    default:
                        name = Value.ToString();
                        break;
                }
                return name;
            }
        }

        public string Name
        {
            get
            {
                return NamedValue + " of " + Suite.ToString();
            }
        }

        public Card(int Value, Suites Suite)
        {
            this.Value = Value;
            this.Suite = Suite;
        }
    }

    public class Ascii
    {
        public List<string> Clubs(string c)
        {
            List<string> clubs = new List<string>();
            clubs.Add($".--------.");
            clubs.Add($"|{c} _    |");
            clubs.Add($"|  ( )   |");
            clubs.Add($"| (_x_)  |");
            clubs.Add($"|   Y {c} |");
            clubs.Add($"`--------'");
            return clubs;
        }
        public List<string> Spades(string s)
        {
            List<string> spades = new List<string>();
            spades.Add($".--------.");
            spades.Add($"|{s} .    |");
            spades.Add($"|  / \\   |");
            spades.Add($"| (_,_)  |");
            spades.Add($"|   I {s} |");
            spades.Add($"`--------'");
            return spades;
        }
        public List<string> Hearts(string h)
        {
            List<string> hearts = new List<string>();
            hearts.Add($".--------.");
            hearts.Add($"|{h}_  _  |");
            hearts.Add($"| ( \\/ ) |");
            hearts.Add($"|  \\  /  |");
            hearts.Add($"|   \\/{h} |");
            hearts.Add($"`--------'");
            return hearts;
        }
        public List<string> Diamonds(string d)
        {
            List<string> diamonds = new List<string>();
            diamonds.Add($".--------.");
            diamonds.Add($"|{d} /\\   |");
            diamonds.Add($"|  /  \\  |");
            diamonds.Add($"|  \\  /  |");
            diamonds.Add($"|   \\/{d} |");
            diamonds.Add($"`--------'");
            return diamonds;
        }
    }
}
