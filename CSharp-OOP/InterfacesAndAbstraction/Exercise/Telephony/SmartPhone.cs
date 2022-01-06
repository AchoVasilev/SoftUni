namespace Telephony
{
    public class SmartPhone : ICallable, IBrowsable
    {

        public string Browse(string website)
        {
            return $"Browsing: {website}!";
        }

        public string Call(string number)
        {
            return $"Calling... {number}";
        }
    }
}
