using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Extensions.Collections {
    public static class BasicExtensions {
        public static bool IsEmpty<T>(this List<T> self) {
            return self.Count == 0;
        }
        public static bool IsNotEmpty<T>(this List<T> self) {
            return !self.IsEmpty();
        }

        public static T First<T>(this List<T> self) { return self[0]; }

        public static T Random<T>(this List<T> self) { return self[UnityEngine.Random.Range(0, self.Count)]; }

        public static void ForeachIndexed<T>(this List<T> self, Action<int, T> action) {
            int index = 0;
            foreach (T element in self) {
                action(index, element);
                index++;
            }
        }
    }
}