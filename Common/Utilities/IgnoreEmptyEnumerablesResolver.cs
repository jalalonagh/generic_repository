using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Common.Utilities
{
    public class IgnoreEmptyEnumerablesResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (property.DeclaringType is IEnumerable)
            {
                property.ShouldSerialize = instance =>
                {
                    var enumer = instance
                        .GetType()
                        .GetProperty(property.PropertyName)
                        .GetValue(instance, null) as IEnumerable;

                    if (enumer != null)
                    {
                        // check to see if there is at least one item in the Enumerable
                        return enumer.GetEnumerator().MoveNext();
                    }
                    else
                    {
                        // if the enumerable is null, we defer the decision to NullValueHandling
                        return true;
                    }
                };
            }

            return property;
        }
    }

    public class IgnoreUnexpectedArraysConverter<T> : IgnoreUnexpectedArraysConverterBase
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }
    }

    public class IgnoreUnexpectedArraysConverter : IgnoreUnexpectedArraysConverterBase
    {
        readonly IContractResolver resolver;

        public IgnoreUnexpectedArraysConverter(IContractResolver resolver)
        {
            if (resolver == null)
                throw new ArgumentNullException();
            this.resolver = resolver;
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType.IsPrimitive || objectType == typeof(string))
                return false;
            return resolver.ResolveContract(objectType) is JsonObjectContract;
        }
    }

    public abstract class IgnoreUnexpectedArraysConverterBase : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var contract = serializer.ContractResolver.ResolveContract(objectType);
            if (!(contract is JsonObjectContract))
            {
                throw new JsonSerializationException(string.Format("{0} is not a JSON object", objectType));
            }

            do
            {
                if (reader.TokenType == JsonToken.Null)
                    return null;
                else if (reader.TokenType == JsonToken.Comment)
                    continue;
                else if (reader.TokenType == JsonToken.StartArray)
                {
                    var array = JArray.Load(reader);
                    if (array.Count > 0)
                        throw new JsonSerializationException(string.Format("Array was not empty."));
                    return null;
                }
                else if (reader.TokenType == JsonToken.StartObject)
                {
                    // Prevent infinite recursion by using Populate()
                    existingValue = existingValue ?? contract.DefaultCreator();
                    serializer.Populate(reader, existingValue);
                    return existingValue;
                }
                else
                {
                    throw new JsonSerializationException(string.Format("Unexpected token {0}", reader.TokenType));
                }
            }
            while (reader.Read());
            throw new JsonSerializationException("Unexpected end of JSON.");
        }

        public override bool CanWrite { get { return false; } }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
