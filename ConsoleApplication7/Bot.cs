using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication7.enums;

namespace ConsoleApplication7
{
    internal class Bot
    {
        public int expectVzyatki;
        public Strategues GameStrategue;
        public List<Divide> hand;
        public List<Divide> copyHand;
        public string name;
        public Orders order;
        public int score;
        public Statuses status;
        public Strategues strategue;
        public Turns turn;
        public WinLose WinLose;

        public Bot(string name, Statuses status, Turns turn, Orders order)
        {
            this.name = name;
            this.status = status;
            this.turn = turn;
            this.order = order;
            strategue = Strategues.WAITING;
            WinLose = WinLose.LOSE;
            GameStrategue = Strategues.PASS;
            hand = new List<Divide>();
            copyHand = new List<Divide>();
            expectVzyatki = new int();
        }


        public void EvaluateHand()
        {
            var currentCounter = new int();
            foreach (var subHand in hand) currentCounter += Opredelenie(subHand.Cards);
            expectVzyatki = currentCounter;
        }

        public void ShowHand()
        {
            Console.WriteLine("Рука у " + name);
            foreach (var subHand in copyHand)
            foreach (var card in subHand.Cards)
                Console.WriteLine(card);
            Console.WriteLine();
        }

        public void changeHand(List<Card> prikup, List<Card> sbros)
        {
            var currentSbrossbros = new List<Card>();
            var uselessCard = new List<Card>();
            var ListSbrosa = new List<Card>(prikup);
            var newHand = hand.SelectMany(q => q.Cards).ToList();
            foreach (var suit in hand) uselessCard.AddRange(OpredelenieForChenge(suit.Cards));
            Console.WriteLine("после всех проверок бесполезнЫми картами оказались :");
            Console.WriteLine();
            foreach (var w in uselessCard) Console.WriteLine(w);
            if (uselessCard.Count == 0)
            {
                Console.WriteLine("бот посчитал, что карты прикупа для него бесполезны и сбросил их");
                return;
            }
            newHand = newHand.Except(uselessCard).ToList();
            ListSbrosa.AddRange(uselessCard);
            ListSbrosa = ListSbrosa.OrderByDescending(q => q.value).ToList();
            currentSbrossbros.AddRange(ListSbrosa.GetRange(ListSbrosa.Count - 2, 2));
            ListSbrosa.RemoveRange(ListSbrosa.Count - 2, 2);
            newHand.AddRange(ListSbrosa);
            hand = newHand.GroupBy(item => item.suit)
                .Select(group => new Divide
                {
                    Suit = group.Key,
                    Cards = group.OrderByDescending(card => card.value).ToList()
                })
                .OrderByDescending(q => q.Cards.Count)
                .ToList();
            sbros.AddRange(currentSbrossbros);
            Console.WriteLine();
            Console.WriteLine("в сброс отправляются карты :");
            foreach (var card in currentSbrossbros) Console.WriteLine(card + "   ");
            Console.WriteLine();
            Console.Write("Новая ");
            ShowHand();
        }

        public List<Card> OpredelenieForChenge(List<Card> listCards)
        {
            var tempCards = new List<Card>(listCards);
            var uselessCards = new List<Card>();

            for (var num = 0; num < tempCards.Count; num++)
            {
                if (tempCards.Count != 0)
                    for (var num_2 = 0; num_2 < 4; num_2++)
                        if (tempCards.Count > 3 && num_2 == 3 && tempCards[num_2].value == Values.Jack)
                        {
                            tempCards.RemoveRange(0, 4);
                            uselessCards.AddRange(tempCards);
                            num = 0;
                        }
                if (tempCards.Count != 0 && tempCards.Count >= 6)
                {
                    for (var num_1 = 0; num_1 < 6; num_1++) tempCards.Remove(tempCards[tempCards.Count - 1]);
                    uselessCards.AddRange(tempCards);
                    num = 0;
                }
                if (tempCards.Count != 0)
                    for (var num_5 = 0; num_5 < 3; num_5++)
                        if (tempCards.Count > 3 && num_5 == 2 && tempCards[num_5].value == Values.Queen)
                        {
                            tempCards.RemoveRange(0, 3);
                            uselessCards.AddRange(tempCards);
                            num = 0;
                        }
                if (tempCards.Count != 0 && tempCards.Count >= 5)
                {
                    for (var num_1 = 0; num_1 < 5; num_1++) tempCards.Remove(tempCards[tempCards.Count - 1]);
                    uselessCards.AddRange(tempCards);
                    num = 0;
                }
                if (tempCards.Count != 0)
                    for (var num_7 = 0; num_7 < 2; num_7++)
                        if (tempCards.Count > 1 && num_7 == 1 && tempCards[num_7].value == Values.King)
                        {
                            tempCards.RemoveRange(0, 2);
                            uselessCards.AddRange(tempCards);
                            num = 0;
                        }
                if (tempCards.Count != 0 && num == 0 && tempCards[num].value == Values.Ace)
                {
                    tempCards.Remove(tempCards[num]);
                    uselessCards.AddRange(tempCards);
                    num = 0;
                }
                if (tempCards.Count != 0 && tempCards[num].value == Values.King && tempCards.Count >= 2)
                {
                    tempCards.Remove(tempCards[num]);
                    tempCards.Remove(tempCards[tempCards.Count - 1]);
                    uselessCards.AddRange(tempCards);
                    num = 0;
                }
                if (tempCards.Count != 0 && tempCards[num].value == Values.Queen && tempCards.Count >= 3)
                {
                    tempCards.Remove(tempCards[num]);
                    for (var num_3 = 0; num_3 < 2; num_3++) tempCards.Remove(tempCards[tempCards.Count - 1]);
                    uselessCards.AddRange(tempCards);
                    num = 0;
                }
                if (tempCards.Count != 0 && tempCards[num].value == Values.Jack && tempCards.Count >= 4)
                {
                    tempCards.Remove(tempCards[num]);
                    for (var num_3 = 0; num_3 < 3; num_3++) tempCards.Remove(tempCards[tempCards.Count - 1]);
                    uselessCards.AddRange(tempCards);
                    num = 0;
                }
            }
            return uselessCards;
        }

        public int Opredelenie(List<Card> listCards)
        {
            var currentCounter = new int();
            var tempCards = new List<Card>(listCards);
            for (var num = 0; num < tempCards.Count; num++)
            {
                if (tempCards.Count != 0)
                    for (var num_1 = 0; num_1 < 4; num_1++)
                        if (tempCards.Count > 3 && num_1 == 3 && tempCards[num_1].value == Values.Jack)
                        {
                            currentCounter += 4;
                            tempCards.RemoveRange(0, 4);
                            num = 0;
                        }
                if (tempCards.Count != 0 && tempCards.Count >= 6)
                {
                    currentCounter += 4;
                    for (var num_1 = 0; num_1 < 6; num_1++) tempCards.Remove(tempCards[tempCards.Count - 1]);
                }
                if (tempCards.Count != 0)
                    for (var num_1 = 0; num_1 < 3; num_1++)
                        if (tempCards.Count > 2 && num_1 == 2 && tempCards[num_1].value == Values.Queen)
                        {
                            currentCounter += 3;
                            tempCards.RemoveRange(0, 3);
                            num = 0;
                        }
                if (tempCards.Count != 0 && tempCards.Count >= 5)
                {
                    currentCounter += 3;
                    for (var num_1 = 0; num_1 < 5; num_1++) tempCards.Remove(tempCards[tempCards.Count - 1]);
                }
                if (tempCards.Count != 0)
                    for (var num_1 = 0; num_1 < 2; num_1++)
                        if (tempCards.Count > 1 && num_1 == 1 && tempCards[num_1].value == Values.King)
                        {
                            currentCounter += 2;
                            tempCards.RemoveRange(0, 2);
                            num = 0;
                        }
                if (tempCards.Count != 0 && num == 0 && tempCards[num].value == Values.Ace)
                {
                    currentCounter += 1;
                    tempCards.Remove(tempCards[num]);
                }
                if (tempCards.Count != 0 && tempCards[num].value == Values.King && tempCards.Count >= 2)
                {
                    currentCounter += 1;
                    tempCards.Remove(tempCards[num]);
                    tempCards.Remove(tempCards[tempCards.Count - 1]);
                }
                if (tempCards.Count != 0 && tempCards[num].value == Values.Queen && tempCards.Count >= 3)
                {
                    currentCounter += 1;
                    tempCards.Remove(tempCards[num]);
                    tempCards.Remove(tempCards[tempCards.Count - 1]);
                }
                if (tempCards.Count != 0 && tempCards[num].value == Values.Jack && tempCards.Count >= 4)
                {
                    currentCounter += 1;
                    tempCards.Remove(tempCards[num]);
                    tempCards.Remove(tempCards[tempCards.Count - 1]);
                }
            }
            return currentCounter;
        }

        public Card MakeAMove(Suits? trump, Suits? lastCard)
        {
            Card card;
            var trumpList = hand.FirstOrDefault(q => q.Suit == trump);
            if (trumpList != null && trumpList.Cards.Count != 0)
            {
                card = trumpList.Cards[0];
                trumpList.Cards.Remove(card);
                Console.WriteLine(name + " threw " + card);
                return card;
            }
            var lastList = hand.FirstOrDefault(q => q.Suit == lastCard);
            if (lastList != null && lastList.Cards.Count != 0)
            {
                card = lastList.Cards[0];
                lastList.Cards.Remove(card);
                Console.WriteLine(name + " threw " + card);
                return card;
            }
            card = hand.First(q => q.Cards.Count > 0).Cards[0];
            Console.WriteLine(name + " threw " + card);
            hand.First(q => q.Suit == card.suit).Cards.Remove(card);
            return card;
        }


        public override string ToString()
        {
            return name;
        }
    }
}