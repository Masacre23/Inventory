using Domain.Entities.Inventory;
using Domain.Entities.Inventory.Items;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils.Extensions.Collections;
using Utils.Injector;

namespace Domain.UseCases.Inventory {

    public interface IInventoryIncreaser {
        public bool Increase(Item item);
        public Weapon AddWeapon();
        public Resource AddResource();
        public Consumable AddConsumable();
        }

    public class InventoryIncreaser : IInventoryIncreaser {
        ItemsContainer inventory;
        private int maxWeight;
        private List<Weapon> availableWeapons;
        private List<Resource> availableResources;
        private List<Consumable> availableConsumables;

        public InventoryIncreaser(ItemsContainer inventory, int maxWeight, List<Weapon> availableWeapons, List<Resource> availableResources, List<Consumable> availableConsumables) {
            this.inventory = inventory;
            this.maxWeight = maxWeight;
            this.availableWeapons = availableWeapons;
            this.availableResources = availableResources;
            this.availableConsumables = availableConsumables;
        }

        public bool Increase(Item item) {
            var totalWeight = inventory.items.Sum(it => it.weight) + item.weight;
            if (totalWeight <= maxWeight) {
                inventory.Add(item.CopyAsNew());
                Debug.Log("Added " + item.name);
                Debug.Log("Current inventory weight: " + totalWeight);
                return true;
            } else {
                Debug.Log(item.name + " couldn't be added.");
                return false;
            }
        }

        public Weapon AddWeapon() {
            var ret = availableWeapons.Random();
            if (Increase(ret)) return ret;
            else return null;
        }

        public Resource AddResource() {
            var ret = availableResources.Random();
            if (Increase(ret)) return ret;
            else return null;
        }

        public Consumable AddConsumable() {
            var ret = availableConsumables.Random();
            if (Increase(ret)) return ret;
            else return null;
        }
    }

    public class DummyInventoryIncreaser : IInventoryIncreaser {
        public Weapon AddWeapon() {
            return new Weapon("weapon", "weapon", 1, 1, 1, Weapon.Type.MELEE);
        }

        public Resource AddResource() {
            return new Resource("resource", "resource", 1, 1, 1, Resource.Type.AMMO);
        }

        public Consumable AddConsumable() {
            return new Consumable("consumable", "consumable", 1, 1, Consumable.Type.FOOD);
        }

        public bool Increase(Item item) {
            return true;
        }
    }
}