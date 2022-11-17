using System;
using System.Security.Cryptography;
using System.Text;

namespace Util.Utilities
{
    public class EncryptDecrypt
    {
        #region Property
        static string Key = "iTogether#key";
        #endregion

        #region Public Methods
        public EncryptDecrypt GetEncryptDecrypt()
        {
            return new EncryptDecrypt();
        }

        public EncryptDecrypt()
        {
            
        }
        public EncryptDecrypt(string key)
        {
            Key = key;
        }

        /// <summary>
        /// Encrypt a string
        /// </summary>
        /// <param name="toEncrypt">a string which will be encrypted</param>
        /// <returns>encrypted string</returns>
        public string Encrypt(string toEncrypt)
        {
            return DefaultEncrypt(toEncrypt, true);
        }

        /// <summary>
        /// Decrypt a string
        /// </summary>
        /// <param name="toDecrypt">a string which will be decrypted</param>
        /// <returns>decrypted string</returns>
        public string Decrypt(string toDecrypt)
        {
            return DefaultDecrypt(toDecrypt, true);
        }

        /// <summary>
        /// Encrypt a string
        /// </summary>
        /// <param name="toEncrypt">a string which will be encrypted</param>
        /// <returns>encrypted string</returns>
        public string SHA256Encrypt(string toEncrypt)
        {
            return SHA256Encrypt(toEncrypt, true);
        }

        /// <summary>
        /// Decrypt a string
        /// </summary>
        /// <param name="toDecrypt">a string which will be decrypted</param>
        /// <returns>decrypted string</returns>
        public string SHA256Decrypt(string toDecrypt)
        {
            return SHA256Decrypt(toDecrypt, true);
        } 
        #endregion

        #region Miscellaneous
        private static string DefaultEncrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);


            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(Key));
                hashmd5.Clear();
            }
            else
                keyArray = Encoding.UTF8.GetBytes(Key);

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var cTransform = tdes.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        private static string DefaultDecrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            var toEncryptArray = Convert.FromBase64String(cipherString);

            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(Key));
                hashmd5.Clear();
            }
            else
            {
                keyArray = Encoding.UTF8.GetBytes(Key);
            }

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var cTransform = tdes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Encoding.UTF8.GetString(resultArray);
        }

        private static string SHA256Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);


            if (useHashing)
            {
                var hashmd5 = new SHA256CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(Key));
                hashmd5.Clear();
            }
            else
                keyArray = Encoding.UTF8.GetBytes(Key);

            var tdes = new TripleDESCryptoServiceProvider();

            var cTransform = tdes.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        private static string SHA256Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            var toEncryptArray = Convert.FromBase64String(cipherString);

            if (useHashing)
            {
                var hashmd5 = new SHA256CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(Key));
                hashmd5.Clear();
            }
            else
            {
                keyArray = Encoding.UTF8.GetBytes(Key);
            }

            var tdes = new TripleDESCryptoServiceProvider
            {
                Key = keyArray,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            var cTransform = tdes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();
            return Encoding.UTF8.GetString(resultArray);
        } 
        #endregion
    }
}
