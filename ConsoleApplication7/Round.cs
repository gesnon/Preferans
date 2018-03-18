using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication7.enums;

namespace ConsoleApplication7
{
    internal class Round
    {
        public  List<Bot> bots;
        private List<Card> currenPrukup;
        private int currentBet;
        private GameeTypes currentGameType;
        private List<Card> currentSbros = new List<Card>();
        private Suits? currentTrump;
        public List<Divide> testHand = new List<Divide>();
        public Score LocalScore = new Score();
        public List<KeyValuePair<Bot, Card>> table = new List<KeyValuePair<Bot, Card>>();
        public  List<string> winners = new List<string>();

        public Round()
        {
 
            currenPrukup = new List<Card>();
            var deck = new Deck();
            bots = new List<Bot>();
            bots.Add(new Bot("bot_1", Statuses.DILLER, Turns.thirdTurn, Orders.First));
            bots.Add(new Bot("bot_2", Statuses.NOTHING, Turns.firstTurn, Orders.Second));
            bots.Add(new Bot("bot_3", Statuses.NOTHING, Turns.SecontTurn, Orders.Third));
            deck.DealCards(bots, currenPrukup);
            Console.WriteLine();
            EvaluateHandAndWriteResult();
            Console.WriteLine();
            WritePrikup();
            currentTrump = GetTrump();
            Console.WriteLine("козырь {0}",currentTrump);
            Console.WriteLine();
            currentBet = Zayavka();
            PrepareGame();
            
        }

        private void PrepareGame()
        {
            if (currentBet >= 4)
            {   foreach (var f in bots) { f.ShowHand(); }
                currentGameType = GameeTypes.NaVzyatki;
                Console.WriteLine("тип текущей игры -" + currentGameType);
                foreach (var q in bots)
                {
                    if (q.strategue!=enums.Strategues.GAME) q.strategue = Strategues.PASS;
                    
                }
                Console.WriteLine("козырь - " + currentTrump);
                Console.WriteLine("заявка -" + currentBet);
                Console.WriteLine();
                bots.First(q => q.strategue == Strategues.GAME).changeHand(currenPrukup, currentSbros);
                foreach (var bot in bots)
                {
                    if (bot.strategue != Strategues.GAME && bot.expectVzyatki > 0) bot.GameStrategue = Strategues.VIST;
                    if (bot.strategue == Strategues.GAME) bot.GameStrategue = Strategues.GAME;
                }
                Console.WriteLine("стратегия на торге");
                foreach (var bot1 in bots) { if (bot1.strategue == enums.Strategues.GAME) { Console.WriteLine(bot1.name + " " + bot1.strategue + " " + currentBet + " " + currentTrump); } if (bot1.strategue != enums.Strategues.GAME) { Console.WriteLine(bot1.name + " " + bot1.strategue); } } 
                Console.WriteLine("стратегия на игру");
                foreach (var bot2 in bots) { Console.WriteLine(bot2.name + " " + bot2.GameStrategue); }
            }
            if (currentBet <= 3 && currentBet > 0)
            {
                currentGameType = GameeTypes.Raspas;
                Console.WriteLine("тип текущей игры -" + currentGameType);
                foreach (var bot1 in bots) { bot1.strategue = Strategues.PASS; bot1.GameStrategue = enums.Strategues.GAME; }
                currentTrump = currenPrukup[0].suit;
                Console.WriteLine("козырь - " + currentTrump);

                Console.WriteLine("стратегия на торге");
                foreach (var bot1 in bots) { Console.WriteLine(bot1.name + " " + bot1.strategue); }
                Console.WriteLine("стратегия на игру");
                foreach (var bot2 in bots) { Console.WriteLine(bot2.name + " " + bot2.GameStrategue); }
            }
            if (currentBet == 0)
            {
                currentGameType = GameeTypes.Mizer;
                Console.WriteLine("тип текущей игры -" + currentGameType);
                currentTrump = null;
            }
        }
        private void EvaluateHandAndWriteResult()
        {
            foreach (var bot in bots)
            {
                bot.EvaluateHand();
                Console.WriteLine("у {0} предположительно взяток - {1}",bot.name,bot.expectVzyatki);

            }

        }

        private int Zayavka()
        {
            var bot = bots.FirstOrDefault(q => q.strategue == Strategues.GAME);
            if (bot != null)
            {
                if (bot.expectVzyatki == 4) return 6;
                if (bot.expectVzyatki == 5) return 6;
                if (bot.expectVzyatki == 6) return 6;
                return bot.expectVzyatki;
            }
            return 0;

        }

        private void WritePrikup()
        {
            Console.WriteLine("прикуп ");

            Console.WriteLine(string.Join(", ", currenPrukup));
        }

        public Suits GetTrump()
        {
            var bot = bots.Where(b => b.expectVzyatki == bots.Max(q => q.expectVzyatki))
                .OrderByDescending(q => q.hand[0].Cards.Count)
                .First();
            bot.strategue = Strategues.GAME;
            bot.GameStrategue = Strategues.GAME;
            return bot.hand[0].Suit;
        }

        public void StartRound()
        {   
            for (var i = 0; i < 10; i++)
            {
                foreach (var bot in bots) 
                    table.Add(table.Any()
                        ? new KeyValuePair<Bot, Card>(bot,
                            bot.MakeAMove(currentTrump, table[table.Count - 1].Value.suit))
                        : new KeyValuePair<Bot, Card>(bot, bot.MakeAMove(currentTrump, currentTrump)));
                bots = GetWinner();
            }
            LocalScore.Zapis(bots, currentBet, currentGameType);
            LocalScore.ShowScore();
            foreach (var q in bots) { Console.WriteLine(" количество очков у " + q.name + " " + q.score);
                //ClearBot(q);
            }
        }

        private List<Bot> GetWinner()
        {
            var last3 = table.GetRange(table.Count - 3, 3).OrderByDescending(q => q.Value.value).ToList();
            var qwe = last3.FirstOrDefault(q => q.Value.suit == currentTrump);
            if (!qwe.Equals(new KeyValuePair<Bot, Card>()))
            {
                qwe.Key.score++;
                qwe.Key.WinLose = WinLose.WIN;
                Console.WriteLine("победил " + qwe.Key.name);
                winners.Add("победил " + qwe.Key);
            }
            else
            {
                var player = last3.First(q => q.Value.suit == table[table.Count - 3].Value.suit).Key;
                player.score++;
                player.WinLose = WinLose.WIN;
                Console.WriteLine("победил " + player.name);
                winners.Add("победил " + player);
                Console.WriteLine();
            }
            return circleTurn();
        }      

        public List<Bot> circleTurn()
        {
            bots = bots.OrderBy(q => q.order).ToList();
            if (bots[0].WinLose == WinLose.WIN)
            {
                bots[0].turn = Turns.firstTurn;
                bots[1].turn = Turns.SecontTurn;
                bots[2].turn = Turns.thirdTurn;
            }
            if (bots[1].WinLose == WinLose.WIN)
            {
                bots[0].turn = Turns.thirdTurn;
                bots[1].turn = Turns.firstTurn;
                bots[2].turn = Turns.SecontTurn;
            }
            if (bots[2].WinLose == WinLose.WIN)
            {
                bots[0].turn = Turns.SecontTurn;
                bots[1].turn = Turns.thirdTurn;
                bots[2].turn = Turns.firstTurn;
            }
            foreach (var q in bots) q.WinLose = WinLose.LOSE;
            return bots.OrderBy(q => q.turn).ToList();
        }
        public void circleDiller(List<Bot> bots)
        {
            bots.OrderBy(q => q.order).ToList();

            foreach (var bot in bots)
            {
                if (bot.status == Statuses.DILLER && bot.name == "bot_1")
                {
                    bots[0].turn = Turns.SecontTurn;
                    bots[0].status = Statuses.NOTHING;
                    bots[1].status = Statuses.DILLER;
                    bots[1].turn = Turns.thirdTurn;
                    bots[2].turn = Turns.firstTurn;
                }
                if (bot.status == Statuses.DILLER && bot.name == "bot_2")
                {
                    bots[0].turn = Turns.firstTurn;
                    bots[1].status = Statuses.NOTHING;
                    bots[1].turn = Turns.SecontTurn;
                    bots[2].turn = Turns.thirdTurn;
                    bots[2].status = Statuses.DILLER;
                }
                if (bot.status == Statuses.DILLER && bot.name == "bot_3")
                {
                    bots[0].turn = Turns.thirdTurn;
                    bots[0].status = Statuses.DILLER;
                    bots[1].turn = Turns.firstTurn;
                    bots[2].turn = Turns.SecontTurn;
                    bots[2].status = Statuses.NOTHING;
                }
            }
        }
        public void Razdecha()
        {   
            List<Card> prikup = currenPrukup;
            List<Bot> bots4 = new List<Bot>();
            bots4.AddRange(bots);
            bots4.OrderBy(q=>q.order);
            foreach (var r in bots4) { r.ShowHand(); }
            foreach (var q in bots4)
            {
                if (q.status == enums.Statuses.DILLER) { Console.WriteLine("DILLER - " + q.name); }
            }
            Console.WriteLine("порядок действий при торговле и выкладывании карт на стол");
            foreach (var q in bots4) { Console.WriteLine(q.turn + " " + q.name); }

            Console.WriteLine("Прикуп :" + prikup[0] + ", " + prikup[1]);
        }

        public void Torgovlya()
        {
            enums.Suits? trump = currentTrump;
            int bet = currentBet;
            List<Bot> bots2 = new List<Bot>();
            bots2.AddRange(bots);
            enums.GameeTypes gameType = currentGameType;
            List<Card> prikup = currenPrukup;
            if (gameType == enums.GameeTypes.NaVzyatki)
            {
                Console.WriteLine("Прикуп : " + prikup[0] + ", " + prikup[1]);
                Console.WriteLine("стратегия на торге");
                foreach (var q in bots2) { if (q.strategue == enums.Strategues.GAME) { Console.WriteLine(q.name + "- заявка  " + bet + " " + trump); } }
                foreach (var w in bots2) { if (w.strategue == enums.Strategues.PASS) { Console.WriteLine(w.name + " " + w.strategue); } }
                //новая рука бота

            }

            if (gameType == enums.GameeTypes.Raspas)
            {
                foreach (var q in bots2) { Console.WriteLine(q.name + " " + q.strategue); }
                Console.WriteLine("козырем является первая карты прикупа;");
                Console.WriteLine("Прикуп : " + prikup[0] + ", " + prikup[1]);
            }

            if (gameType == enums.GameeTypes.Mizer)
            {
                foreach (var e in bots2) { if (e.strategue == enums.Strategues.GAME) { Console.WriteLine(e.name + "- заявка  " + gameType); } }
                foreach (var r in bots2) { if (r.strategue == enums.Strategues.PASS) { Console.WriteLine(r.name + " " + r.strategue); } }
            }
        }

        public void PrintZayavka()
        {
            enums.GameeTypes gameType = currentGameType;
            int bet = currentBet;
            enums.Suits? trump = currentTrump;
            List<Bot> bots2 = new List<Bot>();
            bots2.AddRange(bots);

            if (gameType == enums.GameeTypes.NaVzyatki)
            {
                Console.WriteLine("стратегия на игру");
                foreach (var q in bots2)
                {
                    if (q.GameStrategue == enums.Strategues.GAME) { Console.WriteLine(q.name + " заявка -" + bet + " " + trump); }
                }
                foreach (var w in bots2) { if (w.GameStrategue != enums.Strategues.GAME) { Console.WriteLine(w.name + " " + w.GameStrategue); } }
                Console.WriteLine("финальная заявка - " + bet + " " + trump);
            }

        }

        public void Threws()
        {
            List<Bot> bots2 = new List<Bot>();
            bots2.AddRange(bots);
            for (var num = 1; num < winners.Count; num++)
            {
                Console.WriteLine(table[3 * num].Key.name + ":" + table[3 * num].Value + "  " +table[3 * num + 1].Key.name + ":" + table[3 * num + 1].Value +"  "+
                                  table[3 * num + 2].Key.name + ":" + table[3 * num + 2].Value + " " +
                                  winners[num]);
            }
           
        }

        public void ShowScore()
        {
            List<Bot> bots2 = new List<Bot>();
            bots2.AddRange(bots);
            foreach (var q in bots2) { Console.WriteLine(q.name + " взял " + q.score + " взяток"); }

            LocalScore.ShowScore();

        }

        public void FullGame()
        {
            Razdecha();
            Torgovlya();
            PrintZayavka();
            Threws();
            ShowScore();
        }

        public void getCurrentScore()
        {
            Score score = LocalScore;
            List<Bot> bots2 = new List<Bot>();
            bots2.AddRange(bots);
            foreach (var q in bots2)
            {
                if (q.name == "bot_1")
                {
                    Console.WriteLine("bot_1 пуля - " + score.bot_1Bullet);
                    Console.WriteLine("bot_1 гора - " + score.bot_1Gora);
                    Console.WriteLine("висты на bot_1 от bot_2 - " + score.VistFormBot_2NaBot1);
                    Console.WriteLine("висты на bot_1 от bot_3 - " + score.VistFormBot_3NaBot1);
                }
                if (q.name == "bot_2")
                {
                    Console.WriteLine("bot_2 пуля - " + score.bot_2Bullet);
                    Console.WriteLine("bot_2 гора - " + score.bot_2Gora);
                    Console.WriteLine("висты на bot_2 от bot_1 - " + score.VistFormBot_1NaBot2);
                    Console.WriteLine("висты на bot_2 от bot_3 - " + score.VistFormBot_3NaBot2);
                }
                if (q.name == "bot_3")
                {
                    Console.WriteLine("bot_3 пуля - " + score.bot_3Bullet);
                    Console.WriteLine("bot_3 гора - " + score.bot_3Gora);
                    Console.WriteLine("висты на bot_3 от bot_1 - " + score.VistFormBot_1NaBot3);
                    Console.WriteLine("висты на bot_3 от bot_2 - " + score.VistFormBot_2NaBot3);
                }
            }
        }

        public void average(int num, List<Round> rounds)
        {
            Score score = ScoreSummForAverage(num, rounds);
            Score currentScore = score;

            List<int> gora = new List<int>();
            gora.Add(score.bot_1Gora); gora.Add(score.bot_2Gora); gora.Add(score.bot_3Gora);
            gora = gora.OrderBy(q => q).ToList();


            currentScore.bot_1Gora -= gora[0]; currentScore.bot_2Gora -= gora[0]; currentScore.bot_3Gora -= gora[0];
            // добавляю висты за гору 
            currentScore.VistFormBot_2NaBot1 += ((currentScore.bot_1Gora / 3) * num); currentScore.VistFormBot_3NaBot1 += ((currentScore.bot_1Gora / 3) * num);
            currentScore.VistFormBot_1NaBot2 += ((currentScore.bot_2Gora / 3) * num); currentScore.VistFormBot_3NaBot2 += ((currentScore.bot_2Gora / 3) * num);
            currentScore.VistFormBot_1NaBot3 += ((currentScore.bot_3Gora / 3) * num); currentScore.VistFormBot_2NaBot3 += ((currentScore.bot_3Gora / 3) * num);

            //считаю висты 
            int vistBot1NaBot2 = (currentScore.VistFormBot_1NaBot2 - currentScore.VistFormBot_2NaBot1);
            int vistBot1NaBot3 = (currentScore.VistFormBot_1NaBot3 - currentScore.VistFormBot_3NaBot1);

            int vistBot2NaBot1 = (currentScore.VistFormBot_2NaBot1 - currentScore.VistFormBot_1NaBot2);
            int vistBot2NaBot3 = (currentScore.VistFormBot_2NaBot3 - currentScore.VistFormBot_3NaBot2);

            int vistBot3NaBot1 = (currentScore.VistFormBot_3NaBot1 - currentScore.VistFormBot_1NaBot3);
            int vistBot3NaBot2 = (currentScore.VistFormBot_3NaBot2 - currentScore.VistFormBot_2NaBot3);

            // считаю окончательные очки
            int ScoreBot_1 = (vistBot1NaBot2 + vistBot1NaBot3);
            int ScoreBot_2 = (vistBot2NaBot1 + vistBot2NaBot3);
            int ScoreBot_3 = (vistBot3NaBot1 + vistBot3NaBot1);

            Console.WriteLine("У bot_1 - " + ScoreBot_1 + " очков");
            Console.WriteLine("У bot_2 - " + ScoreBot_2 + " очков");
            Console.WriteLine("У bot_3 - " + ScoreBot_3 + " очков");

        }

        public void ScoreSumm(int num, List<Round> rounds)
        {
            Score currentScore = new Score();
            for (int num_1 = 0; num_1 < num; num_1++)
            {
                currentScore.bot_1Bullet += rounds[num_1].LocalScore.bot_1Bullet;
                currentScore.bot_2Bullet += rounds[num_1].LocalScore.bot_2Bullet;
                currentScore.bot_3Bullet += rounds[num_1].LocalScore.bot_2Bullet;

                currentScore.bot_1Gora += rounds[num_1].LocalScore.bot_1Gora;
                currentScore.bot_2Gora += rounds[num_1].LocalScore.bot_2Gora;
                currentScore.bot_3Gora += rounds[num_1].LocalScore.bot_3Gora;

                currentScore.VistFormBot_2NaBot1 += rounds[num_1].LocalScore.VistFormBot_2NaBot1;
                currentScore.VistFormBot_3NaBot1 += rounds[num_1].LocalScore.VistFormBot_3NaBot1;

                currentScore.VistFormBot_1NaBot2 += rounds[num_1].LocalScore.VistFormBot_1NaBot2;
                currentScore.VistFormBot_3NaBot2 += rounds[num_1].LocalScore.VistFormBot_3NaBot2;

                currentScore.VistFormBot_1NaBot3 += rounds[num_1].LocalScore.VistFormBot_1NaBot3;
                currentScore.VistFormBot_2NaBot3 += rounds[num_1].LocalScore.VistFormBot_2NaBot3;

                            }
            currentScore.ShowScore();
        }

        public Score ScoreSummForAverage(int num, List<Round> rounds)
        {
            Score currentScore = new Score();
            for (int num_1 = 0; num_1 < num; num_1++)
            {
                currentScore.bot_1Bullet += rounds[num_1].LocalScore.bot_1Bullet;
                currentScore.bot_2Bullet += rounds[num_1].LocalScore.bot_2Bullet;
                currentScore.bot_3Bullet += rounds[num_1].LocalScore.bot_2Bullet;

                currentScore.bot_1Gora += rounds[num_1].LocalScore.bot_1Gora;
                currentScore.bot_2Gora += rounds[num_1].LocalScore.bot_2Gora;
                currentScore.bot_3Gora += rounds[num_1].LocalScore.bot_3Gora;

                currentScore.VistFormBot_2NaBot1 += rounds[num_1].LocalScore.VistFormBot_2NaBot1;
                currentScore.VistFormBot_3NaBot1 += rounds[num_1].LocalScore.VistFormBot_3NaBot1;

                currentScore.VistFormBot_1NaBot2 += rounds[num_1].LocalScore.VistFormBot_1NaBot2;
                currentScore.VistFormBot_3NaBot2 += rounds[num_1].LocalScore.VistFormBot_3NaBot2;

                currentScore.VistFormBot_1NaBot3 += rounds[num_1].LocalScore.VistFormBot_1NaBot3;
                currentScore.VistFormBot_2NaBot3 += rounds[num_1].LocalScore.VistFormBot_2NaBot3;

            }
            return currentScore;
        }
    }
}