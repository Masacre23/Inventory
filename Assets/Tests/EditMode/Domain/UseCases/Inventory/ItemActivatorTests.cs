using Domain.Entities.Inventory;
using Domain.Entities.Inventory.Items;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using Utils.Extensions.Bucles;

namespace Domain.UseCases.Inventory {
    public class ItemActivatorTests {

        [Test]
        public void Activate() {
            ItemsContainer inventory = new InventoryData();
            var cake = new Consumable("cake", "Chocolate Cake", 10, 1, Consumable.Type.FOOD);
            var pizza = new Consumable("pizza", "Pineapple pizza", 10, 1, Consumable.Type.POISON);
            var weapon = new Weapon("sword", "Sword", 10, 1, 23, Weapon.Type.MELEE);
            inventory.Add(cake);
            inventory.Add(pizza);

            var activator = new ItemActivator(inventory);
            activator.Activate(cake.name);

            Assert.AreEqual(1, inventory.items.Count);

            inventory.Add(weapon);
            activator.Activate(pizza.name);
            Assert.AreEqual(1, inventory.items.Count);

            activator.Activate(weapon.name);
            Assert.AreEqual(1, inventory.items.Count);
        }
    }
}