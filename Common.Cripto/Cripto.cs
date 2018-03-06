using Common.Domain.Enums;
using Common.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Common.Cripto
{

    public class Cripto : ICripto
    {
        public string Encrypt(string value, TypeCripto type, string salt)
        {
            if (type == TypeCripto.Hash128)
                return ComputeHash128(value, salt);

            if (type == TypeCripto.Hash512)
                return ComputeHash512(value, salt);

            if (type == TypeCripto.MD5Hash)
                return EncryptMD5HashString(value, salt);

            return string.Empty;
        }

        private string ComputeHash128(string value, string salt)
        {
            var encrypt = true;
            byte[] toEncryptorDecryptArray = null;
            ICryptoTransform cTransform = null;

            if (salt.IsNullOrEmpaty())
                throw new InvalidOperationException("Salt not found");

            byte[] keyArrays = MD5Hash(salt);

            var resultsArray = TripleDESCrypto(value, encrypt, toEncryptorDecryptArray, cTransform, keyArrays);
            if (encrypt)
                return Convert.ToBase64String(resultsArray, 0, resultsArray.Length);

            return UTF8Encoding.UTF8.GetString(resultsArray);
        }

        private static byte[] CreateHash(string unHashed)
        {
            var md5Hasing = new System.Security.Cryptography.HMACMD5();
            var data = UTF8Encoding.UTF8.GetBytes(unHashed);
            data = md5Hasing.ComputeHash(data);
            return data;
        }

        private static byte[] MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(input));

                //Fix para key 24
                if (result.Length < 24)
                {
                    List<byte> byteList = new List<byte>(result);
                    for (int i = result.Length - 1; i < 24; i++)
                    {
                        byteList.Add(0);
                    }
                    result = byteList.Take(24).ToArray();
                }

                return result;
            }
        }

        private byte[] TripleDESCrypto(string value, bool encrypt, byte[] toEncryptorDecryptArray, ICryptoTransform cTransform, byte[] keyArrays)
        {
            using (var tdes = TripleDES.Create())
            {
                tdes.Key = keyArrays;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;

                if (encrypt == true)
                {
                    toEncryptorDecryptArray = UTF8Encoding.UTF8.GetBytes(value);
                    cTransform = tdes.CreateEncryptor();
                }
                else
                {
                    toEncryptorDecryptArray = Convert.FromBase64String(value.Replace(' ', '+'));
                    cTransform = tdes.CreateDecryptor();
                }

                byte[] resultsArray = cTransform.TransformFinalBlock(toEncryptorDecryptArray, 0, toEncryptorDecryptArray.Length);

                return resultsArray;
            }
        }

        private string ComputeHash512(string value, string salt)
        {
            if (salt.IsNullOrEmpaty())
                throw new InvalidOperationException("Salt not found");

            using (var SHA512 = System.Security.Cryptography.SHA512.Create())
            {
                var result = SHA512.ComputeHash(UTF8Encoding.UTF8.GetBytes(value));
                return Convert.ToBase64String(result);
            }
        }

        public static string EncryptMD5HashString(string Message, string Passphrase)
        {
            if (string.IsNullOrEmpty(Message))
            {
                return string.Empty;
            }
            else
            {
                using (var tripleDES = TripleDES.Create())
                {
                    byte[] Results;
                    UTF8Encoding UTF8 = new UTF8Encoding();
                    MD5 MD5 = MD5.Create();

                    byte[] TDESKey = MD5.ComputeHash(UTF8.GetBytes(Passphrase));

                    if (TDESKey.Length == 16)
                    {
                        byte[] keyTemp = new byte[24];
                        Buffer.BlockCopy(TDESKey, 0, keyTemp, 0, TDESKey.Length);
                        Buffer.BlockCopy(TDESKey, 0, keyTemp, TDESKey.Length, 8);
                        TDESKey = keyTemp;
                    }

                    tripleDES.Key = TDESKey;
                    tripleDES.Mode = CipherMode.ECB;
                    tripleDES.Padding = PaddingMode.PKCS7;

                    byte[] DataToEncrypt = UTF8.GetBytes(Message);

                    ICryptoTransform Encryptor = tripleDES.CreateEncryptor();
                    Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);

                    return Convert.ToBase64String(Results);
                }
            }
        }
    }

}
