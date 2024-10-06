namespace WordReviser.Components.Services
{
    public class UtilsService
    {
        public static bool JudgeIsInline(string sentence)
        {
            if(sentence.Length < 4)
            {
                return false;
            }

            string tag = "$$";
            string head = sentence.Substring(0, 2);
            string tail = sentence.Substring(sentence.Length-2, 2);

            return head == tag && tail == tag;
        }
    }
}
