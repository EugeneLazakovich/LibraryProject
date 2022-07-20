namespace Lesson1_BL.Auth
{
    public interface ITokenGenerator
    {
        public string GenerateToken(string username, string role);
    }
}
