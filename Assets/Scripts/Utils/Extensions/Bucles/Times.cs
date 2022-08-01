using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils.Extensions.Bucles {

    public static class TimesExtensions {
        public static void Times(this int num, Action<int> action) {
            for (int i = 0; i < num; i++) {
                action(i);
            }
        }
    }
}
