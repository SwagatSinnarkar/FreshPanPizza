using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text.Json;

namespace FreshPanPizza.Helpers
{
    //TemData we can commanly use for storing the Cart, Entire Cart along with Items. So, when we try to assign the entire cartalong with item so we`ll get the error
    //of serialization or De-serialization.
    public static class TempDataExtension
    {
        //Set method for setting the object(store the thing as a string). So, 1st time with Set method whatever the data we`ll get we`re just going to Serialize.
        public static void Set<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonSerializer.Serialize(value);
        }

        //Get method for getting the object(stored thing get deserialized). And, the time of retreival we`fre just going to Deserialize.
        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            tempData.TryGetValue(key, out object o);
            return o == null ? null : JsonSerializer.Deserialize<T>((string)o);
        }

        //Peek method will help us to read the data from TempData again & again. It`ll not mark it for the deletion.
        public static T Peek<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o = tempData.Peek(key);
            return o == null ? null : JsonSerializer.Deserialize<T>((string)o);
        }

        //Get 1 will mark the data to be deleted so after 1 time lead using the Get method, you`ll not able to read it again. But if you want to read it again use Peek 
        //method of the TempData.
    }
}
