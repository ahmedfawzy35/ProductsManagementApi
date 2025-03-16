using System.Text.Json.Serialization;

namespace Products_Management_API.Enums
{

    [JsonConverter(typeof(JsonStringEnumConverter))] // لجعل قيم ال أينم  تظهر كأسماء بدلاً من أرقام
    public enum OrderDirection
    {
        Ascending,  // تصاعدي
        Descending  // تنازلي
    }
}
