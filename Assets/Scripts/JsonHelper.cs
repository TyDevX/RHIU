using System.Collections.Generic;
using UnityEngine;

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            string newJson = "{\"array\":" + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.array;
        }

        private class Wrapper<T>
        {
            public T[] array;
        }
    }
