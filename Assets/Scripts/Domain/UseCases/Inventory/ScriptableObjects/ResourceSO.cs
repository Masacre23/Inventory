using Domain.Entities.Inventory.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domain.UseCases.Inventory.ScriptableObjects {

    [CreateAssetMenu(menuName = "Scriptable Objects/Create Resource")]
    public class ResourceSO : ScriptableObject {
        public Resource resource;
    }
}
