using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domain.Entities.Inventory.Items {

    [System.Serializable]
    public class Weapon : Item {
        public int dps;
        public enum Type { MELEE, RANGED }
        public Type type;

        [HideInInspector]
        public bool hideDeteriorationRate;
        public Weapon(string id, string name, int weight, int price, int dps, Type type) : base(id, name, weight, price) { 
            this.dps = dps;
            this.type = type;
        }

        public override Item CopyAsNew() => new Weapon(id, name, weight, price, dps, type);
    }
}