using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;

namespace Site.Helpers
{
    public class Response<T>
    {
        public Response()
        {
        }

        public Response(int code, T content, string message)
        {
            Code = code;
            Content = content;
            Message = message;
        }

        public int Code { get; set; }

        public T Content { get; set; }

        public string Message { get; set; }
    }

    public class SimpleResponse
    {
        public SimpleResponse()
        {
        }

        public SimpleResponse(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public int Code { get; set; }
        public string Message { get; set; }
    }

    public static class TempDataExtensions
    {
        public static void Set(this ITempDataDictionary tempData, string key, SimpleResponse value)
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }
        public static SimpleResponse Get(this ITempDataDictionary tempData, string key)
        {
            return JsonConvert.DeserializeObject<SimpleResponse>((string)tempData[key]);
        }
    }
}
