using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace BowlingScoreCalculator.Utilities
{
    public class InjectorProvider
    {
        public static ServiceProvider ServiceProvider { get; private set; }
        public static List<Type> Instantiated { get; } = new List<Type>();
        public static T Get<T>()
        {
            try
            {
                var result = ServiceProvider.GetService<T>();
                if (!Instantiated.Contains(typeof(T)))
                {
                    Instantiated.Add(typeof(T));
                }
                return result;
            }
            catch
            {
                return default;
            }
        }

        public static object Get(Type t)
        {
            try
            {
                var result = ServiceProvider.GetService(t);
                if (!Instantiated.Contains(t))
                {
                    Instantiated.Add(t);
                }
                return result;
            }
            catch
            {
                return default;
            }
        }

        public static void SetServiceProvider(ServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
