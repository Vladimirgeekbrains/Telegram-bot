string token = File.ReadAllText("token.config");

TelegramBot bot = new TelegramBot(token);

void Updates(TelegramMessageModel msg)
{
    bot.SendMessage(msg.charId, $"{msg.text} : получено");
}

bot.action = Updates;

bot.Start();

System.Console.WriteLine("+++++");