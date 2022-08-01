using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domain.Entities.Inventory.Items {
    public class Garbage : Item {
        public Garbage(string id, string name, int weight) : base(id, name, weight) {
            deterioration = 10000;
        }

        public override Item CopyAsNew() => new Garbage(id, name, weight);
    }
}