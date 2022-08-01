using Domain.Entities.Inventory;
using Domain.Entities.Inventory.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domain.UseCases.Inventory {
    public interface IDeteriorator {
        public delegate void EventReplaceItemByGarbage(int index, Item garbage);
        public event EventReplaceItemByGarbage onReplacedItemByGarbage;
        public void Deteriorate();
    }

    public class Deteriorator: IDeteriorator {

        ItemsContainer container;
        public Deteriorator(ItemsContainer container) {
            this.container = container;
        }

        public event IDeteriorator.EventReplaceItemByGarbage onReplacedItemByGarbage;

        public void Deteriorate() {
            List<Item> destroyedConsumables = new List<Item>();
            container.items.ForEach(item => {
                if (item.deteriorationRate != 0 && item.deterioration < (int)DeteriorationState.DESTROYED) {
                    item.deterioration += item.deteriorationRate;
                    item.price--;
                    if (item.price < 0) item.price = 0;

                    if (item is Consumable && item.deterioration >= (int)DeteriorationState.DESTROYED) {
                        Debug.Log("Destroyed " + item.name);
                        destroyedConsumables.Add(item);
                    }
                }
            });

            destroyedConsumables.ForEach(item => {
                var index = container.items.IndexOf(item);
                container.Remove(item);
                var garbage = new Garbage(item.id, "Garbage", item.weight);
                container.items.Insert(index, garbage);
                if(onReplacedItemByGarbage != null)
                    onReplacedItemByGarbage(index, garbage);
            });
        }
    }

    public class DummyDeteriorator : IDeteriorator {
        public event IDeteriorator.EventReplaceItemByGarbage onReplacedItemByGarbage;

        public void Deteriorate() {}
    }
}