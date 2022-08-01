using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domain.Entities.Inventory.Items {
    [System.Serializable]
    public class Resource : Item {
        public enum Type { AMMO }
        public Type type;
        public Resource(string id, string name, int weight, int price, int deteriorationRate, Type type) : base(id, name, weight, price, deteriorationRate) {
            this.type = type;
        }

        public override Item CopyAsNew() => new Resource(id, name, weight, price, deteriorationRate, type);
    }
}
