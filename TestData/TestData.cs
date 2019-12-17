using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class TestData
    {
        static Dictionary<string, object> Data = new Dictionary<string, object>();

        public static readonly string UserData = "UserData";
        public static readonly string PostDataResponse = "PostDataResponse";
        public static readonly string ExpectedPostData = "ExpectedPostData";
        public static readonly string UpdatedPost = "UpdatedPost";
        public static readonly string PhotoData = "PhotoData";

        public static void Add(string key, object obj)
        {
            try
            {
                Data.Add(key, obj);
            }
            catch (ArgumentException)
            {
                Data.Remove(key);
                Data.Add(key, obj);
            }
        }

        public static void Remove(string key)
        {
            try
            {
                Data.Remove(key);
            }
            catch (ArgumentException)
            {
                Data.Remove(key);
            }
        }

        public static void RemoveAll()
        {
            Data.Clear();
        }

        public static T Get<T>(string key)
        {
            try
            {
                return (T)Convert.ChangeType(Data[key], typeof(T));
            }
            catch (KeyNotFoundException exception)
            {
                return (T)Convert.ChangeType(default(T), typeof(T));
            }
        }

    }
}
