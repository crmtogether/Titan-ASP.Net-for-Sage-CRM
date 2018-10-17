using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Security.Cryptography;
using System.IO;
namespace SageCRM.AspNet
{
    class converter
    {
        public converter()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static string Decrypt(string TextToBeDecrypted, bool AcceleratorLicense, bool DistribLicense, bool newLicense = true, bool CrystalLicense = false, bool MobileXLicense = false, bool Customer365License = false)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            string Password = "SageCRMTogether";
            if (CrystalLicense)
            {
                Password = "CRMTogetherCM2015";
            }
            else
                if (AcceleratorLicense)
                {
                    Password = "SageCRMTogetherxlrator";
                }
                else
                    if (MobileXLicense)
                    {
                        Password = "CRMTogetherJan2017";
                    }
                    else
                        if (Customer365License)
                        {
                            Password = "Customer365Feb2017";
                        }
                        else
                            if (DistribLicense)
                            {
                                Password = "SageCRMTogetherDistrib";
                            }



            string DecryptedData;

            try
            {
                byte[] EncryptedData = Convert.FromBase64String(TextToBeDecrypted);

                byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
                //Making of the key for decryption
                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
                //Creates a symmetric Rijndael decryptor object.
                ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

                MemoryStream memoryStream = new MemoryStream(EncryptedData);
                //Defines the cryptographics stream for decryption.THe stream contains decrpted data
                CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

                byte[] PlainText = new byte[EncryptedData.Length];
                int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
                memoryStream.Close();
                cryptoStream.Close();

                //Converting to string
                DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
            }
            catch
            {
                DecryptedData = TextToBeDecrypted;
            }
            return DecryptedData;
        }

        public static string Encrypt(string TextToBeEncrypted, bool AcceleratorLicense, bool DistribLicense, bool newlicense = true, bool CrystalLicense = false, bool MobileXLicense = false, bool Customer365License = false)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            string Password = "SageCRMTogether";
            if (CrystalLicense)
            {
                Password = "CRMTogetherCM2015";
            }
            else
                if (AcceleratorLicense)
                {
                    if (newlicense)
                    {
                        Password = "CRMTogetherApril2015";
                    }
                    else
                    {
                        Password = "SageCRMTogetherxlrator";
                    }
                }
                else
                    if (MobileXLicense)
                    {
                        Password = "CRMTogetherJan2017";
                    }
                    else
                        if (Customer365License)
                        {
                            Password = "Customer365Feb2017";
                        }
                        else
                            if (DistribLicense)
                            {
                                if (newlicense)
                                {
                                    Password = "CRMTogetherApril2015Dist";
                                }
                                else
                                {
                                    Password = "SageCRMTogetherDistrib";
                                }
                            }
            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(TextToBeEncrypted);
            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
            //Creates a symmetric encryptor object. 
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream();
            //Defines a stream that links data streams to cryptographic transformations
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(PlainText, 0, PlainText.Length);
            //Writes the final state and clears the buffer
            cryptoStream.FlushFinalBlock();
            byte[] CipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string EncryptedData = Convert.ToBase64String(CipherBytes);

            return EncryptedData;
        }
    }




    public class converterPass
    {
        public converterPass()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static string Decrypt(string TextToBeDecrypted)
        {
            RijndaelManaged RijndaelCipher = new RijndaelManaged();

            string Password = "Accelerate101";
            string DecryptedData;
            try
            {
                byte[] EncryptedData = Convert.FromBase64String(TextToBeDecrypted);

                byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
                //Making of the key for decryption
                PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
                //Creates a symmetric Rijndael decryptor object.
                ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));

                MemoryStream memoryStream = new MemoryStream(EncryptedData);
                //Defines the cryptographics stream for decryption.THe stream contains decrpted data
                CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);

                byte[] PlainText = new byte[EncryptedData.Length];
                int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);

                memoryStream.Close();
                cryptoStream.Close();

                //Converting to string
                DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
            }
            catch
            {

                DecryptedData = TextToBeDecrypted;
            }
            return DecryptedData;
        }

    }

}
