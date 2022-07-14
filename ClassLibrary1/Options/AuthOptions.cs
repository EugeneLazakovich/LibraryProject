namespace Lesson1_BL.Options
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int LifetimeInSeconds { get; set; }        
    }
}
