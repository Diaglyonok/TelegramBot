using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.InlineKeyboardButtons;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleApplication7
{
    class Program
    {
        private static TelegramBotClient bot;

        static void Main(string[] args)
        {
            bot = new TelegramBotClient(Secret.key);

            bot.OnMessage += Bot_OnMessage;
            bot.OnCallbackQuery += Bot_OnCallbackQuery;

            var me = bot.GetMeAsync().Result;

            Console.WriteLine($"{me.FirstName} {me.Username} ");

            bot.StartReceiving();
            
            Console.ReadLine();
            bot.StopReceiving();
        }

        private static void Bot_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            return;
        }

        private static async void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = e.Message;

            Console.WriteLine($"{message.From.FirstName} {message.From.LastName} отправил сообщение: \"{message.Text}\"");

            String text = "";

            switch (message.Text)
            {
                case "/start":
                    text = @"Привет
Ты можешь мне написать вот что:
/Hello
/timeTable";    //*/
                    break;

                case "/Hello":
                    text = $"Hello, {message.From.FirstName} {message.From.LastName}!";
                    var inlineKeyBoard = new InlineKeyboardMarkup( new[]
                        {
                            InlineKeyboardButton.WithUrl("VK", "https://vk.com/diaglyonok"),
                            InlineKeyboardButton.WithUrl("github", "https://github.com/Diaglyonok")
                        });

                    await bot.SendTextMessageAsync(message.From.Id, "Потыкайте", replyMarkup: inlineKeyBoard);
                    break;

                case "/timeTable":
                    text = "No timetable implemented yet";
                    break;

            }

            if (text != null && text != "")
                await bot.SendTextMessageAsync(message.From.Id, text);
        }
    }
}
