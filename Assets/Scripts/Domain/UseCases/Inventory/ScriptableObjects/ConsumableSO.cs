using Domain.Entities.Inventory.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domain.UseCases.Inventory.ScriptableObjects {

    [CreateAssetMenu(menuName = "Scriptable Objects/Create Consumable")]
    public class ConsumableSO : ScriptableObject {
        public Consumable consumable;
    }
}
