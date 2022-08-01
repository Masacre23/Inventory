using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domain.UseCases.Inventory.ScriptableObjects {
    [CreateAssetMenu(menuName = "Scriptable Objects/Inventory config")]
    public class InventoryConfig : ScriptableObject {
        public int maxWeight;
        public GameObject itemUI;
        public int deteriorationSeconds = 1;
    }
}