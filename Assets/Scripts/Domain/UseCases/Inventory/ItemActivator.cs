using Domain.Entities.Inventory;
using Domain.Entities.Inventory.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Extensions.Collections;

namespace Domain.UseCases.Inventory {

    public interface IItemActivator {
        public delegate void EventRemoveItem(int index);
        public event EventRemoveItem onRemovedItem;
        public void Activate(string itemName);
    }

    public class ItemActivator: IItemActivator {

        ItemsContainer container;
        public ItemActivator(ItemsContainer container) {
            this.container = container;
        }

        public event IItemActivator.EventRemoveItem onRemovedItem;

        public void Activate(string itemName) {
            var item = container.items.Find(item =>
                item.name == itemName
            );

            switch (item) {
                case Weapon:
                    var weapon = (Weapon)item;
                    if (weapon.type == Weapon.Type.RANGED) {
                        var arrows = container.items.FindAll (item => 
                            item is Resource && ((Resource)item).type == Resource.Type.AMMO
                        );
                        if (arrows.IsNotEmpty()) {
                            var indexArrow = container.items.IndexOf(arrows.First());
                            container.Remove(arrows.First());
                            onRemovedItem(indexArrow);
                            
                            Debug.Log("Used arrow");
                        }else {
                            Debug.Log("Out of arrows");
                        }
                    }
                    break;
                case Consumable:
                    var index = container.items.IndexOf(item);
                    container.Remove(item);
                    if(onRemovedItem != null)
                        onRemovedItem(index);
                    var consumable = (Consumable)item;
                    if (consumable.type == Consumable.Type.FOOD) {
                        Debug.Log("Yummy");
                    }else if(consumable.type == Consumable.Type.POISON) {
                        Debug.Log("It seems toxic, but you eat it anyway");
                    }
                    break;

            }
        }
    }

    public class DummyItemActivator : IItemActivator {
        public event IItemActivator.EventRemoveItem onRemovedItem;

        public void Activate(string itemName) {}
    }
}