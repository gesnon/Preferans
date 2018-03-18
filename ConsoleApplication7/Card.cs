using ConsoleApplication7.enums;

namespace ConsoleApplication7
{
    internal class Card
    {
        public Suits suit;
        public Values value;


        public Card(Values value, Suits suit)
        {
            this.value = value;
            this.suit = suit;
        }

        public override string ToString()
        {
            return value + " " + suit;
        }
    }
}