using System;
using System.Text;

public class VerificationCodeGenerator
{
    public string GenerateVerificationCode(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        StringBuilder codeBuilder = new StringBuilder();
        Random random = new Random();

        for (int i = 0; i < length; i++)
        {
            int index = random.Next(chars.Length);
            codeBuilder.Append(chars[index]);
        }

        return codeBuilder.ToString();
    }
}
