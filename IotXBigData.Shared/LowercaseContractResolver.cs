using Newtonsoft.Json.Serialization;

namespace IotXBigData.Shared;

public class LowercaseContractResolver : DefaultContractResolver
{
    protected override string ResolvePropertyName(string propertyName)
    {
        return propertyName.ToLower();
    }
}