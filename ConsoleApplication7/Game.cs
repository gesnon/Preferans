using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApplication7.enums;

namespace ConsoleApplication7
{
    internal class Game
    {

        private  int MaxRound;
        public List<Round> rounds = new List<Round>();


        public Game(int MaxRound)
        {

            this.MaxRound = MaxRound;
        }

        public void StartGame()
        {
            while (rounds.Count < MaxRound)
            {
                Console.WriteLine($"раздача {rounds.Count+1}");
                var round = new Round();
                round.StartRound();
                rounds.Add(round);
            }
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
        public Round GetRound(int num) { return rounds[num-1]; }

        #region API
        public void GetBotsNames()
        {

        }

        #endregion

        public void VConsol()
        {
            Console.WriteLine("Боты играют по правилам Сочинской конвенции");
            Console.WriteLine();
            Console.WriteLine("За недобор на висте вистующему в гору пишется как за недобор на игре, если играющий недобирает взяток, то пасовавший игрок записывает только консоляцию (премию за подсад), а висты за фактически набранные взятки пишет только вистовавший. Консоляция пишется в дополнение к вистам за фактически набранные взятки, обоим игрокам. ");
            Console.WriteLine();
            Console.WriteLine("Запись за набранные при распасах взятки производится в гору. Базовая стоимость взятки в данном случае равно 2 очкам. Если игрок не взял ни одной взятки, то ему в пулю записывается стоимость одной взятки. ");
            Console.WriteLine();
            Console.WriteLine("вистующие боты всегда играют <<взакрытую>> ");
            Console.WriteLine();
            Console.WriteLine("Имена ботов инициализированы как bot_1, bot_2 и bot_3 соответственно");
            Console.WriteLine();
            Console.WriteLine("Раздачу карт боты совершают по очереди начиная с первого бота");
            Console.WriteLine();
            Console.WriteLine("для начала игры нашмите на любую кнопку");
        }
        public void VConsol2() 
        {
            Console.WriteLine("нажмите 1 чтобы получить данные определённой раздачи");
            Console.WriteLine("нажмите 2 чтобы получить данные о процессе торговли в определённой раздаче");
            Console.WriteLine("нажмите 3 чтобы получить данные о заявке бота и реакции других ботов");
            Console.WriteLine("нажмите 4 чтобы получить данные о процессе розыгрыша");
            Console.WriteLine("нажмите 5 чтобы получить данные о результатах розыгрыша");
            Console.WriteLine("нажмите 6 чтобы получить данные о полном прозессе раздачи");
            Console.WriteLine("нажмите 7 чтобыполучить данные о текущем состоянии очков ботов");
            Console.WriteLine("нажмите 8 чтобы получить данные о текущего результата игрока");
            Console.WriteLine("нажмите 0 для завершения работы");
        }
    }
}