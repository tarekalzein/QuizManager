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
        /// Serializer Method that takes an instance of ManagerContainer and serialize its content to a given fileName
        /// </summary>
        /// <param name="c">CourseManager instance to serialize</param>
        /// <param name="q">QuizesManager instance to serialize</param>
        /// <param name="targetFile"> the desired bin file name and location</param>
        /// <returns>true on success, false on failure</returns>
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
        /// Method to Deserialize a given file name into application data..
        /// </summary>
        /// <param name="targetFile">The data file</param>
        /// <param name="errorMessage">Deserialization error message</param>
        /// <returns></returns>
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
