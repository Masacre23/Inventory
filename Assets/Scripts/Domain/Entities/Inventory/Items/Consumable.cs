using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domain.Entities.Inventory.Items {
    [System.Serializable]
    public class Consumable : Item {
        public enum Type { FOOD, POISON }
        public Type type;

        [HideInInspector]
        public bool hidePrice;

        public Consumable(string id, string name, int weight, int deteriorationRate, Type type) : base(id, name, weight, 0, deteriorationRate) { 
            this.type = type;
        }

        public override Item CopyAsNew() => new Consumable(id, name, weight, deteriorationRate, type);
    }
}
