using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication7.enums;

namespace ConsoleApplication7
{
    internal class Deck
    {
        public List<Card> deck = new List<Card>();

        public Deck()
        {
            foreach (Suits suit in Enum.GetValues(typeof(Suits)))
            foreach (Values value in Enum.GetValues(typeof(Values))) deck.Add(new Card(value, suit));

            var rand = new Random();
            for (var i = 0; i < deck.Count; i++)
            {
                var r = rand.Next(0, 32);
                var card1 = deck[i];
                deck[i] = deck[r];
                deck[r] = card1;
            }
        }

        public void DealCards(List<Bot> bots, List<Card> prikup)
        {
            foreach (var bot in bots)
            {
                bot.hand = new List<Divide>();
                bot.hand.AddRange(deck.Take(10)
                    .GroupBy(item => item.suit)
                    .Select(group => new Divide
                    {
                        Suit = group.Key,
                        Cards = group.OrderByDescending(card => card.value).ToList()
                    })
                    .OrderByDescending(q => q.Cards.Count)
                    .ToList());
                bot.copyHand.AddRange(deck.Take(10)
                    .GroupBy(item => item.suit)
                    .Select(group => new Divide
                    {
                        Suit = group.Key,
                        Cards = group.OrderByDescending(card => card.value).ToList()
                    })
                    .OrderByDescending(q => q.Cards.Count)
                    .ToList());
                deck.RemoveRange(0, 10);                
                
            }
            prikup.AddRange(deck.Take(2).ToList());
        }
    }
}