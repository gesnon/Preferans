using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication7.enums;

namespace ConsoleApplication7
{
    internal class SaveResults
    {
        private int bet;
        private List<Bot> bots;
        private GameeTypes gameType;
        private List<Card> prikup;
        private List<Card> sbros;
        public List<KeyValuePair<Bot, Card>> table;
        private List<Card> threws;
        private Suits trump;
        private List<string> winners;
        public Score score;
       
         public SaveResults(int bet, List<Bot> bots, GameeTypes gameType, List<Card> prikup, List<Card> sbros, List<KeyValuePair<Bot, Card>> table, List<Card> threws, Suits trump, List<string> winners, Score score) 
        {
            this.bet = bet;
            this.bots = bots;
            this.gameType = gameType;
            this.prikup = prikup;
            this.sbros = sbros;
            this.table = table;this.threws = threws;
            this.trump = trump;this.winners = winners;
            this.score = score;
         }

       
    }
}