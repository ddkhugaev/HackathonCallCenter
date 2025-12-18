namespace HackathonCallCenter.Models
{
    public class DialogueLine
    {
        public string Role { get; set; } // "Клиент" или "Оператор"
        public string Text { get; set; }

        public DialogueLine(string role, string text)
        {
            Role = role;
            Text = text;
        }
    }
}
