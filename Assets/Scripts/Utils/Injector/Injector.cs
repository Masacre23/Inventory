using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Utils.Injector {
    public class Injector {
        public static Injector Instance => _instance ?? (_instance = new Injector());
        private static Injector _instance;
        private readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

        public void RegisterService<T>(T service) {
            var type = typeof(T);

            Assert.IsFalse(services.ContainsKey(type), $"Service {type} already registered");

            services.Add(type, service);
        }

        public T GetService<T>() {
            var type = typeof(T);
            if (!services.TryGetValue(type, out var service)) {
                throw new Exception($"Service {type} not found");
            }

            return (T)service;
        }
    }
}