using System;
using System.Collections.Generic;
using System.IO;
using ConsoleApplication7.enums;

namespace ConsoleApplication7
{
    internal class Program
    {
        private static void Main()
        {


            var game1 = new Game(0);
            game1.VConsol();
            Console.WriteLine("выберите количество раундов");
            int NUM = Convert.ToInt32( Console.ReadLine());
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            try
            {
                ostrm = new FileStream("./Redirect.txt", FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open Redirect.txt for writing");
                Console.WriteLine(e.Message);
                return;
            }
            Console.SetOut(writer);
            var game = new Game(NUM);
            game.StartGame();


            WriteToFile(); Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();
            Console.WriteLine("Done");

            game.VConsol2();

            while (true)
            {
                Console.WriteLine("Выберите действие ");

                String num = Console.ReadLine();              
                switch (num)
                {
                    case "1":
                        Console.WriteLine("Выберите номер раздачи");
                        int num_1 = Convert.ToInt32(Console.ReadLine());
                        if (Convert.ToInt32(num_1) > NUM) { Console.WriteLine("номер раздачи который был указан не существует, выберите номер раздачи и действие повторно"); }
                            if (Convert.ToInt32(num_1) <= NUM)
                            game.GetRound(num_1).Razdecha();
                        break;
                    case "2":
                        Console.WriteLine("Выберите номер раздачи");
                        int num_2 = Convert.ToInt32(Console.ReadLine());
                        if (Convert.ToInt32(num_2) > NUM) { Console.WriteLine("номер раздачи который был указан не существует, выберите номер раздачи и действие повторно"); }
                        if (Convert.ToInt32(num_2) <= NUM)
                            game.GetRound(num_2).Torgovlya();
                        break;
                    case "3":
                        Console.WriteLine("Выберите номер раздачи");
                        int num_3 = Convert.ToInt32(Console.ReadLine());
                        if (Convert.ToInt32(num_3) > NUM) { Console.WriteLine("номер раздачи который был указан не существует, выберите номер раздачи и действие повторно"); }
                        if (Convert.ToInt32(num_3) <= NUM)
                            game.GetRound(num_3).PrintZayavka();
                        break;
                    case "4":
                        Console.WriteLine("Выберите номер раздачи");
                        int num_4 = Convert.ToInt32(Console.ReadLine());
                        if (Convert.ToInt32(num_4) > NUM) { Console.WriteLine("номер раздачи который был указан не существует, выберите номер раздачи и действие повторно"); }
                        if (Convert.ToInt32(num_4) <= NUM)
                            game.GetRound(num_4).Threws();
                        break;
                    case "5":
                        Console.WriteLine("Выберите номер раздачи");
                        int num_5 = Convert.ToInt32(Console.ReadLine());
                        if (Convert.ToInt32(num_5) > NUM) { Console.WriteLine("номер раздачи который был указан не существует, выберите номер раздачи и действие повторно"); }
                        if (Convert.ToInt32(num_5) <= NUM)
                            game.GetRound(num_5).ShowScore();
                        break;
                    case "6":
                        Console.WriteLine("Выберите номер раздачи");
                        int num_6 = Convert.ToInt32(Console.ReadLine());
                        if (Convert.ToInt32(num_6) > NUM) { Console.WriteLine("номер раздачи который был указан не существует, выберите номер раздачи и действие повторно"); }
                        if (Convert.ToInt32(num_6) <= NUM)
                            game.GetRound(num_6).FullGame();
                        break;
                    case "7":
                        Console.WriteLine("Выберите номер раздачи");
                        int num_7 = Convert.ToInt32(Console.ReadLine());
                        if (Convert.ToInt32(num_7) > NUM) { Console.WriteLine("номер раздачи который был указан не существует, выберите номер раздачи и действие повторно"); }
                        if (Convert.ToInt32(num_7) <= NUM)
                            game.GetRound(num_7).getCurrentScore();
                        break;
                    case "8":
                        Console.WriteLine("Выберите номер раздачи");
                        int num_8 = Convert.ToInt32(Console.ReadLine());
                        if (Convert.ToInt32(num_8) > NUM) { Console.WriteLine("номер раздачи который был указан не существует, выберите номер раздачи и действие повторно"); }
                        if (Convert.ToInt32(num_8) <= NUM)
                            game.GetRound(num_8).average(num_8, game.rounds);

                        break;


                    case "0":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("повторите ввод корректно");
                        break;
                }
            
                
             }


        }

        static public void WriteToFile()
        {




        }
    }
}