namespace Lesson1_BL.Services.EncryptionService
{
    public interface IEncryptionService
    {
        string DecryptString(string cipherText);
        string EncryptString(string plainText);
    }
}