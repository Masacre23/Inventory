using Domain.Entities.Inventory.Items;
using Domain.Entities.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domain.UseCases.Inventory {

    public class InventoryData: ItemsContainer {
 
        private List<Item> _items = new List<Item>();
        public List<Item> items => _items;

        public Dictionary<Item, DeteriorationState> checkDeteriorationState() {
            Dictionary<Item, DeteriorationState> ret = new Dictionary<Item, DeteriorationState>();
            
            items.ForEach(item => {
                DeteriorationState state =
                    item.deterioration < (int)DeteriorationState.DIRTY ? DeteriorationState.NORMAL :
                    item.deterioration < (int)DeteriorationState.FIXABLE ? DeteriorationState.DIRTY :
                    item.deterioration < (int)DeteriorationState.DESTROYED ? DeteriorationState.FIXABLE : DeteriorationState.DESTROYED;

                ret.Add(item, state);
            });

            return ret;
        }
    }
}