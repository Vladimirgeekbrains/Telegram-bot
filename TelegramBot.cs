public class TelegramBot
{
    string token;
    public Action<TelegramMessageModel> action;
    HttpClient hc;
    Thread thread;

    public TelegramBot(string token)
    {
        this.token = token;
        hc = new HttpClient();
        hc.BaseAddress = new Uri($"https://api.telegram.org/bot{token}/getupdates?offset=526434289");

        thread = new Thread(GetUpdates);
    }

    private void GetUpdates()
    {
        long offset = 0;
        while (true)
        {
            string content = hc.GetStringAsync($"getupdates?offset={offset}");

            var ms = new Json_Parser().GetMessage(content);
            if (ms.Length != 0)
            {
                foreach (var item in ms)
                {
                    Console.WriteLine(item);
                    action(item);
                }
                offset = ms[ms.Length - 1].updateId + 1;
            }
            Thread.Sleep(1000);
        }
    }

    public void SendMessage(long userId, string text)
    {

    }

    public void Start()
    {
        thread.Start();
    }
}