/*
 Author: Ghercioglo Roman (Romeon0)


 Use example:
    @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
    [Serializable()]
    public class ItemsDatabase : AbstractDatabaseData
    {
        public int coins;
        public int diamonds;
    }

    public class GameDatabase : AbstractDatabase<GameDatabase, ItemsDatabase>
    {
        public new static ItemsDatabase data;

        public new bool Load()
        {
            data = base.Load();
            if (data == null)
            {
                //Something gone wrong...
            }
            return true;
        }
    }
    @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
 */


using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Assets.Scripts.Framework.Other;
using UnityEngine;

namespace Assets.Scripts.Framework.Database
{
    public abstract class AbstractDatabase<TSingletonType, T> : Singleton<TSingletonType> 
        where T: AbstractDatabaseData, new()
        where TSingletonType : MonoBehaviour 
        
    {
        protected string databaseFile;
        protected T data;
        protected bool debug = false;

        public void DebugLog(params System.Object[] p)
        {
            if(debug)
                Debug.Log(p);
        }




        public void SetDatabaseFile(string filePath)
        {
           // if (!File.Exists((filePath)))
           //     File.Create(filePath).Close();

            databaseFile = filePath;
        }

        public void SetDatabaseFileDefault()
        {
            string folderPath = Application.persistentDataPath + "/Saves";
            string filePath = folderPath + "/database.txt";

            if (!Directory.Exists((folderPath)))
                Directory.CreateDirectory(folderPath);

            SetDatabaseFile(filePath);
        }



        public bool Save()
        {
            if (databaseFile == null)
            {
                //Debug.LogWarning("Database: Database path set to default.");
                SetDatabaseFileDefault();
            }

            DebugLog("Database: Saving to file");

            //To JSON format
            string databaseData = data.ToString();

            //Encrypt
            byte[] key = Encoding.ASCII.GetBytes("9Um0u8uU90UM8aoqpIRKDLSOQI91Jqwz");
            byte[] IV = Encoding.ASCII.GetBytes("Poqlfitruqkamz88");
            byte[] encryptedBytes = null;
            try
            {
                encryptedBytes = Encrypt(databaseData, key, IV);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database: Error while encrypting file. Exception: {0}", ex);
                return false;
            }

            //Save to file
            try
            {
                using (var fs = new FileStream(databaseFile, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(encryptedBytes, 0, encryptedBytes.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database: Error while writing to file. Exception: {0}", ex);
                return false;
            }

            DebugLog("Database: Saved.");
            return true;
        }

        public T Load()
        {
            if (databaseFile == null)
            {
                //Debug.LogWarning("Database: Database path set to default.");
                SetDatabaseFileDefault();
            }

            DebugLog("Database: Loading from file...");
            DebugLog("Database: File path - " + databaseFile);

            //No saves
            if (!File.Exists(databaseFile))
            {
                data = new T();
                DebugLog("Database: Created.");
                return data;
            }

            //Read from file
            byte[] buffer = new byte[1024];
            try
            {
                using (var fs = new FileStream(databaseFile, FileMode.Open, FileAccess.Read))
                {
                    int count = fs.Read(buffer, 0, 1024);
                    Array.Resize(ref buffer, count);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database: Error while reading from file. Exception: {0}", ex);
                return null;
            }

            //Decrypt
            byte[] key = Encoding.ASCII.GetBytes("9Um0u8uU90UM8aoqpIRKDLSOQI91Jqwz");
            byte[] IV = Encoding.ASCII.GetBytes("Poqlfitruqkamz88");
            string jsonString = null;
            try
            {
                jsonString = Decrypt(buffer, key, IV);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database: Error while decrypting file. Exception: {0}", ex);
                return null;
            }

            //JSON to class
            data = AbstractDatabaseData.FromString<T>(jsonString);

            DebugLog("Database: Loaded.");
            return data;
        }


        #region Helpers
        private static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create a new AesManaged.    
            using (AesManaged aes = new AesManaged())
            {
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                // Create MemoryStream    
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data    
            return encrypted;
        }

        private static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext;
            // Create AesManaged    
            using (var aes = new AesManaged())
            {
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                // Create the streams used for decryption.    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            plaintext = reader.ReadToEnd();

                        }
                    }
                        
                }
            }
            return plaintext;
        }
        #endregion
    }
}
