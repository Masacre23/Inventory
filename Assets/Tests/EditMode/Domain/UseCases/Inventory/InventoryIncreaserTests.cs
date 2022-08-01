using Domain.Entities.Inventory;
using Domain.Entities.Inventory.Items;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using Utils.Extensions.Bucles;


namespace Domain.UseCases.Inventory {
    public class InventoryIncreaserTests {

        [Test]
        public void Increase() {
            ItemsContainer inventory = new InventoryData();
            var cake = new Consumable("cake", "Chocolate Cake", 10, 1, Consumable.Type.FOOD);
            var pizza = new Consumable("pizza", "Pineapple pizza", 10, 1, Consumable.Type.POISON);
            var arrow = new Resource("arrow", "Arrow", 10, 1, 1, Resource.Type.AMMO);
            var weapon = new Weapon("sword", "Sword", 10, 1, 23, Weapon.Type.MELEE);

            IInventoryIncreaser increaser = new InventoryIncreaser(
                inventory, 
                30,
                new List<Weapon>() { weapon },
                new List<Resource>() { arrow },
                new List<Consumable>() { cake, pizza }
            );


            increaser.Increase(new Weapon("yolo", "yolo", 0, 0, 0, Weapon.Type.MELEE));

            Assert.AreEqual(1, inventory.items.Count);

            increaser.AddWeapon();
            Assert.AreEqual(2, inventory.items.Count);
            Assert.IsTrue(inventory.items[0] is Weapon && inventory.items[1] is Weapon);

            increaser.AddResource();
            Assert.AreEqual(3, inventory.items.Count);
            Assert.IsTrue(inventory.items[0] is Weapon && inventory.items[1] is Weapon && inventory.items[2] is Resource);

            2.Times(index => {
                increaser.AddConsumable();
                Assert.AreEqual(4, inventory.items.Count);
                Assert.IsTrue(inventory.items[0] is Weapon && inventory.items[1] is Weapon && inventory.items[2] is Resource && inventory.items[3] is Consumable);
            });
        }
    }
}
