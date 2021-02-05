using Common.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace WebFramework.Session
{
    //public interface ISession
    //{
    //    bool TryGetValue(string key, out byte[] value);
    //    void Set(string key, byte[] value);
    //    void Remove(string key);
    //}

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, value.ToJson());
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : value.FromJson<T>();
        }
    }

}
