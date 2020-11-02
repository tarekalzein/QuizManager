using BusinessLayer;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataAccessLayer
{
    public class SerializationHelper
    {        
        //private static string targetFile = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory) + "/data.bin";

        /// <summary>
        /// Serializer Method that takes an instance of album manager and serialize its content to a file data.bin
        /// </summary>
        /// <param name="albumManager">Instance of AlbumManager that contains the data to save</param>
        /// <returns>returns bool (true on success)</returns>
        public static bool Serialize(CourseManager c, QuizesManager q, string targetFile)
        {
            ManagersContainer container = new ManagersContainer(c, q);
            FileStream fileStream = null;
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                fileStream = new FileStream(targetFile, FileMode.Create);
                binaryFormatter.Serialize(fileStream, container);
            }
            catch (SerializationException ex)
            {
                System.Console.WriteLine(ex.Message); 
                return false;
            }
            finally
            {
                fileStream.Close();
            }
            return true;
        }

        /// <summary>
        /// Deserializer method that reads the data.bin file in root (if exists) and retrieve its data.
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns>instance of retrieved ablum manager</returns>
        public static ManagersContainer Deserialize(string targetFile,out string errorMessage)
        {
            FileStream fileStream = null;
            errorMessage = null;
            ManagersContainer container = new ManagersContainer();
            try
            {
                if (File.Exists(targetFile))
                {
                    fileStream = new FileStream(targetFile, FileMode.Open);
                    BinaryFormatter b = new BinaryFormatter();
                    if (new FileInfo(targetFile).Length > 0)
                    {
                        container = (ManagersContainer)b.Deserialize(fileStream);
                    }
                }
            }
            catch (SerializationException e)
            {
                errorMessage = "Error loading saved data. \n" + e.Message;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
            }
            return container;
        }
    }
}
