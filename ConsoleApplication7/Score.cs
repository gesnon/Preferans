using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication7
{
    class Score
    {
        public int bot_1Bullet;
        public int bot_1Gora;
        public int VistFormBot_1NaBot2;
        public int VistFormBot_1NaBot3;


        public int bot_2Bullet;
        public int bot_2Gora;
        public int VistFormBot_2NaBot1;
        public int VistFormBot_2NaBot3;


        public int bot_3Bullet;
        public int bot_3Gora;
        public int VistFormBot_3NaBot1;
        public int VistFormBot_3NaBot2;

        public int NedoborGamera;
        public int PereborGamera;
        public int NedoborVisters;
        public Score()
        {

        }

        public void Zapis(List<Bot> bots, int currentBet, enums.GameeTypes CurrentGameType)
        {
            if (CurrentGameType == enums.GameeTypes.NaVzyatki) { ZapisRezyltatovForNormalGame(bots, currentBet); }
            if (CurrentGameType == enums.GameeTypes.Mizer) { ZapisRezyltatovForMizer(bots); }
            if (CurrentGameType == enums.GameeTypes.Raspas) { ZapisRezyltatovForRasps(bots); }

        }


        public void ZapisRezyltatovForNormalGame(List<Bot> bots, int currentBet)
        {

            {
                if (currentBet == 6)
                {
                    foreach (var bot1 in bots) { if (bot1.GameStrategue == enums.Strategues.GAME) { NedoborGamera = 6 - bot1.score; if (NedoborGamera < 0) { NedoborGamera = 0; } PereborGamera = bot1.score - 6; NedoborVisters = 4 - (10 - bot1.score); } if (NedoborVisters < 0) { NedoborVisters = 0; } }

                    foreach (var bot in bots)
                    {
                        if (bot.name == "bot_1")
                        {
                            if (bot.GameStrategue == enums.Strategues.GAME) { if (bot.score >= 6) { bot_1Bullet += 2; } if (bot.score < 6) { bot_1Gora += (2 * (6 - bot.score)); } }
                            if (bot.GameStrategue == enums.Strategues.VIST) { /*if (bot.score >= 2) */{ foreach (var bot2 in bots) { if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_2") { VistFormBot_1NaBot2 += (bot.score * 2 + NedoborGamera * 2); } if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_3") { VistFormBot_1NaBot3 += (bot.score * 2 + NedoborGamera * 2); } } } /*if (bot.score < 2) { bot_1Gora += 2 * NedoborVisters; }*/  if (NedoborVisters > 0 && bot.score < 2) { bot_1Gora += (4 - 2 * bot.score) /*bot_1Gora += 2 * NedoborVisters*/ ; } }
                            if (bot.GameStrategue == enums.Strategues.PASS) { if (NedoborGamera > 0) { foreach (var bot2 in bots) { if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_2") { VistFormBot_1NaBot2 += (NedoborGamera * 2); } if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_3") { VistFormBot_1NaBot3 += (NedoborGamera * 2); } } } }
                        }
                        if (bot.name == "bot_2")
                        {
                            if (bot.GameStrategue == enums.Strategues.GAME) { if (bot.score >= 6) { bot_2Bullet += 2; } if (bot.score < 6) { bot_2Gora += (2 * (6 - bot.score)); } }
                            if (bot.GameStrategue == enums.Strategues.VIST) { /*if (bot.score >= 2)*/ { foreach (var bot2 in bots) { if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_1") { VistFormBot_2NaBot1 += (bot.score * 2 + NedoborGamera * 2); } if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_3") { VistFormBot_2NaBot3 += (bot.score * 2 + NedoborGamera * 2); } } } /*if (bot.score < 2) { bot_2Gora += 2 * NedoborVisters; }*/  if (NedoborVisters > 0 && bot.score < 2) { bot_2Gora += (4 - 2 * bot.score)/*bot_2Gora += 2 * NedoborVisters */; } }
                            if (bot.GameStrategue == enums.Strategues.PASS) { if (NedoborGamera > 0) { foreach (var bot2 in bots) { if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_1") { VistFormBot_2NaBot1 += (NedoborGamera * 2); } if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_3") { VistFormBot_2NaBot3 += (NedoborGamera * 2); } } } }
                        }

                        if (bot.name == "bot_3")
                        {
                            if (bot.GameStrategue == enums.Strategues.GAME) { if (bot.score >= 6) { bot_3Bullet += 2; } if (bot.score < 6) { bot_3Gora += (2 * (6 - bot.score)); } }
                            if (bot.GameStrategue == enums.Strategues.VIST) { /*if (bot.score >= 2) */{ foreach (var bot2 in bots) { if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_1") { VistFormBot_3NaBot1 += (bot.score * 2 + NedoborGamera * 2); } if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_2") { VistFormBot_3NaBot2 += (bot.score * 2 + NedoborGamera * 2); } } } /*if (bot.score < 2) { bot_3Gora += 2 * NedoborVisters; }*/if (NedoborVisters > 0 && bot.score < 2) { bot_3Gora += (4 - 2 * bot.score)/*bot_3Gora += 2 * NedoborVisters*/ ; } }
                            if (bot.GameStrategue == enums.Strategues.PASS) { if (NedoborGamera > 0) { foreach (var bot2 in bots) { if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_1") { VistFormBot_3NaBot1 += (NedoborGamera * 2); } if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_2") { VistFormBot_3NaBot2 += (NedoborGamera * 2); } } } }
                        }

                    }
                }


                if (currentBet == 7)
                {
                    foreach (var bot1 in bots)
                    {
                        if (bot1.GameStrategue == enums.Strategues.GAME) { NedoborGamera = 7 - bot1.score; if (NedoborGamera < 0) { NedoborGamera = 0; } PereborGamera = bot1.score - 7; NedoborVisters = 2 - (10 - bot1.score); if (NedoborVisters < 0) { NedoborVisters = 0; } }

                        foreach (var bot in bots)
                        {
                            if (bot.name == "bot_1")
                            {
                                if (bot.GameStrategue == enums.Strategues.GAME) { if (bot.score >= 7) { bot_1Bullet += 4; } if (bot.score < 7) { bot_1Gora += (4 * (7 - bot.score)); } }
                                if (bot.GameStrategue == enums.Strategues.VIST) { /*if (bot.score >= 2)*/ { foreach (var bot2 in bots) { if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_2") { VistFormBot_1NaBot2 += (bot.score * 4 + NedoborGamera * 4); } if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_3") { VistFormBot_1NaBot3 += (bot.score * 4 + NedoborGamera * 4); } } } /*if (bot.score < 2) { bot_1Gora += 4 * NedoborVisters; }*/ if (NedoborVisters > 0 && bot.score < 1) { bot_1Gora += (4 - 4 * bot.score); } }
                                if (bot.GameStrategue == enums.Strategues.PASS) { if (NedoborGamera > 0) { foreach (var bot2 in bots) { if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_2") { VistFormBot_1NaBot2 += NedoborGamera * 4; } if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_3") { VistFormBot_1NaBot3 += NedoborGamera * 2; } } } }
                            }
                            if (bot.name == "bot_2")
                            {
                                if (bot.GameStrategue == enums.Strategues.GAME) { if (bot.score >= 7) { bot_2Bullet += 4; } if (bot.score < 7) { bot_2Gora += (4 * (7 - bot.score)); } }
                                if (bot.GameStrategue == enums.Strategues.VIST) { if (bot.score >= 2) { foreach (var bot2 in bots) { if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_1") { VistFormBot_2NaBot1 += (bot.score * 4 + NedoborGamera * 4); } if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_3") { VistFormBot_2NaBot3 += bot.score * 4 + NedoborGamera * 4; } } } /*if (bot.score < 2) { bot_2Gora += 4 * NedoborVisters; }*/ if (NedoborVisters > 0 && bot.score < 1) { bot_2Gora += (4 - 4 * bot.score); } }
                                if (bot.GameStrategue == enums.Strategues.PASS) { if (NedoborGamera > 0) { foreach (var bot2 in bots) { if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_1") { VistFormBot_2NaBot1 += NedoborGamera * 4; } if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_3") { VistFormBot_2NaBot3 += NedoborGamera * 4; } } } }
                            }

                            if (bot.name == "bot_3")
                            {
                                if (bot.GameStrategue == enums.Strategues.GAME) { if (bot.score >= 7) { bot_3Bullet += 4; } if (bot.score < 7) { bot_3Gora += (4 * (6 - bot.score)); } }
                                if (bot.GameStrategue == enums.Strategues.VIST) { if (bot.score >= 2) { foreach (var bot2 in bots) { if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_1") { VistFormBot_3NaBot1 += (bot.score * 4 + NedoborGamera * 4); } if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_2") { VistFormBot_3NaBot2 += bot.score * 4 + NedoborGamera * 4; } } } /*if (bot.score < 2) { bot_3Gora += 4 * NedoborVisters; }*/ if (NedoborVisters > 0 && bot.score < 1) { bot_3Gora += (4 - 4 * bot.score); } }
                                if (bot.GameStrategue == enums.Strategues.PASS) { if (NedoborGamera > 0) { foreach (var bot2 in bots) { if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_1") { VistFormBot_3NaBot1 += NedoborGamera * 4; } if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_2") { VistFormBot_3NaBot2 += NedoborGamera * 4; } } } }
                            }

                        }
                    }
                }

                if (currentBet == 8)
                {
                    foreach (var bot1 in bots)
                    {
                        if (bot1.GameStrategue == enums.Strategues.GAME) { NedoborGamera = 8 - bot1.score; if (NedoborGamera < 0) { NedoborGamera = 0; } PereborGamera = bot1.score - 8; NedoborVisters = 1 - (10 - bot1.score); if (NedoborVisters < 2) { NedoborVisters = 0; } }

                        foreach (var bot in bots)
                        {
                            if (bot.name == "bot_1")
                            {
                                if (bot.GameStrategue == enums.Strategues.GAME) { if (bot.score >= 8) { bot_1Bullet += 6; } if (bot.score < 8) { bot_1Gora += (6 * (8 - bot.score)); } }
                                if (bot.GameStrategue == enums.Strategues.VIST) { /*if (bot.score >= 2)*/ { foreach (var bot2 in bots) { if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_2") { VistFormBot_1NaBot2 += (bot.score * 6 + NedoborGamera * 6); } if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_3") { VistFormBot_1NaBot3 += (bot.score * 6 + NedoborGamera * 6); } } } /*if (bot.score < 2) { bot_1Gora += 6 * NedoborVisters; }*/ if (NedoborVisters > 0 && bot.score < 1) { bot_1Gora += (6 - 6 * bot.score); } }
                                if (bot.GameStrategue == enums.Strategues.PASS) { if (NedoborGamera > 0) { foreach (var bot2 in bots) { if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_2") { VistFormBot_1NaBot2 += NedoborGamera * 2; } if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_3") { VistFormBot_1NaBot3 += NedoborGamera * 2; } } } }
                            }
                            if (bot.name == "bot_2")
                            {
                                if (bot.GameStrategue == enums.Strategues.GAME) { if (bot.score >= 8) { bot_2Bullet += 6; } if (bot.score < 8) { bot_2Gora += (6 * (8 - bot.score)); } }
                                if (bot.GameStrategue == enums.Strategues.VIST) { /*if (bot.score >= 2)*/ { foreach (var bot2 in bots) { if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_1") { VistFormBot_2NaBot1 += (bot.score * 6 + NedoborGamera * 6); } if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_3") { VistFormBot_2NaBot3 += (bot.score * 6 + NedoborGamera * 6); } } } /*if (bot.score < 2) { bot_2Gora += 6 * NedoborVisters; }*/ if (NedoborVisters > 0 && bot.score < 1) { bot_2Gora += (6 - 6 * bot.score); } }
                                if (bot.GameStrategue == enums.Strategues.PASS) { if (NedoborGamera > 0) { foreach (var bot2 in bots) { if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_1") { VistFormBot_2NaBot1 += NedoborGamera * 6; } if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_3") { VistFormBot_2NaBot3 += NedoborGamera * 6; } } } }
                            }

                            if (bot.name == "bot_3")
                            {
                                if (bot.GameStrategue == enums.Strategues.GAME) { if (bot.score >= 8) { bot_3Bullet += 6; } if (bot.score < 8) { bot_3Gora += (6 * (8 - bot.score)); } }
                                if (bot.GameStrategue == enums.Strategues.VIST) { /*if (bot.score >= 2)*/ { foreach (var bot2 in bots) { if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_1") { VistFormBot_3NaBot1 += (bot.score * 6 + NedoborGamera * 6); } if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_2") { VistFormBot_3NaBot2 += (bot.score * 6 + NedoborGamera * 6); } } } /*if (bot.score < 2) { bot_3Gora += 6 * NedoborVisters; }*/ if (NedoborVisters > 0 && bot.score < 1) { bot_3Gora += (6 - 6 * bot.score); } }
                                if (bot.GameStrategue == enums.Strategues.PASS) { if (NedoborGamera > 0) { foreach (var bot2 in bots) { if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_1") { VistFormBot_3NaBot1 += NedoborGamera * 6; } if (bot2.GameStrategue == enums.Strategues.GAME && bot2.name == "bot_2") { VistFormBot_3NaBot2 += NedoborGamera * 6; } } } }
                            }

                        }
                    }

                }

            }
        }

        public void ZapisRezyltatovForMizer(List<Bot> bots)
        {
            foreach (var bot in bots)
            {
                if (bot.strategue == enums.Strategues.GAME)
                {
                    if (bot.score == 0)
                    {
                        if (bot.name == "bot_1") { bot_1Bullet += 10; }
                        if (bot.name == "bot_2") { bot_2Bullet += 10; }
                        if (bot.name == "bot_3") { bot_3Bullet += 10; }
                    }
                    if (bot.score > 0)
                    {
                        if (bot.name == "bot_1") { bot_1Gora += 10 * bot.score; }
                        if (bot.name == "bot_2") { bot_2Gora += 10 * bot.score; }
                        if (bot.name == "bot_3") { bot_3Gora += 10 * bot.score; }
                    }

                }
            }
        }

        public void ZapisRezyltatovForRasps(List<Bot> bots)
        {
            foreach (var bot in bots)
            {
                if (bot.score == 0)
                {
                    if (bot.name == "bot_1") { bot_1Bullet += 2; }
                    if (bot.name == "bot_2") { bot_2Bullet += 2; }
                    if (bot.name == "bot_3") { bot_3Bullet += 2; }
                }
                if (bot.score > 0)
                {
                    if (bot.name == "bot_1") { bot_1Gora += 2 * bot.score; }
                    if (bot.name == "bot_2") { bot_2Gora += 2 * bot.score; }
                    if (bot.name == "bot_3") { bot_3Gora += 2 * bot.score; }
                }
            }
        }

        public void ShowScore()
        {
            Console.WriteLine("у бота №1 в пуле " + bot_1Bullet + " очков");
            Console.WriteLine("у бота №2 в пуле " + bot_2Bullet + " очков");
            Console.WriteLine("у бота №3 в пуле " + bot_3Bullet + " очков");

            Console.WriteLine("у бота №1 в горе " + bot_1Gora + " очков");
            Console.WriteLine("у бота №2 в горе " + bot_2Gora + " очков");
            Console.WriteLine("у бота №3 в горе " + bot_3Gora + " очков");

            Console.WriteLine("висты на бота №1 от бота №2 " + VistFormBot_2NaBot1);
            Console.WriteLine("висты на бота №1 от бота №3 " + VistFormBot_3NaBot1);

            Console.WriteLine("висты на бота №2 от бота №1 " + VistFormBot_1NaBot2);
            Console.WriteLine("висты на бота №2 от бота №3 " + VistFormBot_3NaBot2);

            Console.WriteLine("висты на бота №3 от бота №1 " + VistFormBot_1NaBot3);
            Console.WriteLine("висты на бота №3 от бота №2 " + VistFormBot_2NaBot3);
        }
    }
}
