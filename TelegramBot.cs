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
        hc.BaseAddress = new Uri($"https://api.telegram.org/bot{token}/");

        thread = new Thread(GetUpdates);
    }

    private void GetUpdates()
    {
        long offset = 0;
        while (true)
        {
            string content = hc.GetStringAsync($"getupdates?offset={offset}").Result;

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
        string answer = "залупа";
        Random random = new Random(); 
        switch (text.ToLower())
        {
            case "анекдот":
                answer = "Нету у меня, отъебись";
                break;

            case "загадка":
                answer = "Неа";
                break;

            case "паста про говно":
                answer = "Govno";
                break;

            case "/gayness":
                int randomNumber = random.Next (1, 101);
                answer = $"Ты пидор на {randomNumber}%, поздравляю";
                break;
            case "/howbigmydick":
                int randomN = random.Next (-2, 15);
                if (randomN < 1)
                {
                    answer = $"Длина твоего члена -   {randomN} см, он у тебя в жопу врос";
                } 
                else
                {
                    answer = $"Длина твоего члена -   {randomN} см, коротыш";
                }
                break;

            default:
                answer = "Че ты спизданул?";
                break;
        }
        string url = $"https://api.telegram.org/bot{token}/sendmessage?chat_id={userId}&text={answer}";
       // var sent = hc.SendAsync(url);
        //var req = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
        string content = hc.GetStringAsync(url).Result;
    }

    public void Start()
    {
        thread.Start();
    }
}