using System;
using System.Numerics;

class RSA
{
    static void Main(string[] args)
    {
        int p = 13;
        int q = 11;

        int n = p * q;

        int phi = (p - 1) * (q - 1);

        int e = 7;

        int d = ModInverse(e, phi);

        Console.WriteLine($"Public key: (e={e}, n={n})");
        Console.WriteLine($"Private key: (d={d}, n={n})");

        string message = "Hello";
        BigInteger[] encryptedMessage = Encrypt(message, e, n);

        Console.WriteLine("Encrypted message:");
        foreach (var c in encryptedMessage)
        {
            Console.Write(c + " ");
        }
        Console.WriteLine();

        string decryptedMessage = Decrypt(encryptedMessage, d, n);
        Console.WriteLine($"Decrypted message: {decryptedMessage}");
    }

    static BigInteger[] Encrypt(string message, int e, int n)
    {
        BigInteger[] encrypted = new BigInteger[message.Length];

        for (int i = 0; i < message.Length; i++)
        {
            BigInteger m = message[i];
            encrypted[i] = BigInteger.ModPow(m, e, n);
        }

        return encrypted;
    }

    static string Decrypt(BigInteger[] encryptedMessage, int d, int n)
    {
        char[] decrypted = new char[encryptedMessage.Length];

        for (int i = 0; i < encryptedMessage.Length; i++)
        {
            BigInteger c = encryptedMessage[i];
            decrypted[i] = (char)BigInteger.ModPow(c, d, n);
        }

        return new string(decrypted);
    }

    static int ModInverse(int a, int m)
    {
        (int g, int x, int y) = ExtendedGcd(a, m);
        if (g != 1)
        {
            throw new ArgumentException("a and m must be coprime");
        }
        return (x % m + m) % m;
    }

    static (int, int, int) ExtendedGcd(int a, int b)
    {
        if (a == 0)
        {
            return (b, 0, 1);
        }

        (int g, int x1, int y1) = ExtendedGcd(b % a, a);
        int x = y1 - (b / a) * x1;
        int y = x1;
        return (g, x, y);
    }
}
