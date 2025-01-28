public class TelegramMessageModel
{
    public long updateId;
    public long charId;
    public string text;
    public string firstName;

    public TelegramMessageModel(long charId, string firstName, string messageText, long updateId)
    {
        this.charId = charId;
        this.firstName = firstName;
        this.text = messageText;
        this.updateId = updateId;
    }

    public override string ToString()
    {
        return $"{firstName} {text} {charId} {updateId}";
    }
}