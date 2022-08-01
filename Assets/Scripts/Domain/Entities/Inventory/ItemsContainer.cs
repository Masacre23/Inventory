using Domain.Entities.Inventory.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domain.Entities.Inventory {
    public interface ItemsContainer {
        public List<Item> items { get; }

        public void Add(Item item) {
            items.Add(item);
        }

        public bool Remove(Item item) {
            return items.Remove(item);
        }

        public abstract Dictionary<Item, DeteriorationState> checkDeteriorationState();
    }
}