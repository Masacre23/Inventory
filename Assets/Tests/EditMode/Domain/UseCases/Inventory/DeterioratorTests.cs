using System.Collections;
using System.Collections.Generic;
using Domain.Entities.Inventory;
using Domain.Entities.Inventory.Items;
using Utils.Extensions.Bucles;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Domain.UseCases.Inventory {
    public class DeterioratorTests {

        [Test]
        public void ConsumableDeteriorateTest() {
            ItemsContainer inventory = new InventoryData();
            var cake = new Consumable("cake", "Chocolate Cake", 10, 1, Consumable.Type.FOOD);
            inventory.Add(cake);
            var deteriorator = new Deteriorator(inventory);
            deteriorator.Deteriorate();
            Assert.AreEqual(DeteriorationState.NORMAL, inventory.checkDeteriorationState().GetValueOrDefault(cake));

            9.Times(index =>
                deteriorator.Deteriorate()
            );
            Assert.AreEqual(DeteriorationState.DIRTY, inventory.checkDeteriorationState().GetValueOrDefault(cake));

            10.Times(index =>
                deteriorator.Deteriorate()
            );
            Assert.AreEqual(DeteriorationState.FIXABLE, inventory.checkDeteriorationState().GetValueOrDefault(cake));

            9.Times(index =>
                deteriorator.Deteriorate()
            );
            Assert.AreEqual(DeteriorationState.FIXABLE, inventory.checkDeteriorationState().GetValueOrDefault(cake));

            deteriorator.Deteriorate();
            Assert.IsTrue(inventory.items[0] is Garbage);
        }

        [Test]
        public void WeaponDeteriorateTest() {
            ItemsContainer inventory = new InventoryData();
            var weapon = new Weapon("weapon", "Weapon", 10, 20, 30, Weapon.Type.MELEE);
            inventory.Add(weapon);
            var deteriorator = new Deteriorator(inventory);
            
            100.Times(index =>
                deteriorator.Deteriorate()
            );
            Assert.AreEqual(DeteriorationState.NORMAL, inventory.checkDeteriorationState().GetValueOrDefault(weapon));
        }

        [Test]
        public void ResourceDeteriorateTest() {
            ItemsContainer inventory = new InventoryData();
            var resource = new Resource("arrow", "Arrow", 2, 10, 1, Resource.Type.AMMO);
            inventory.Add(resource);
            var deteriorator = new Deteriorator(inventory);
            deteriorator.Deteriorate();
            Assert.AreEqual(DeteriorationState.NORMAL, inventory.checkDeteriorationState().GetValueOrDefault(resource));
            Assert.AreEqual(9, resource.price);

            9.Times(index =>
                deteriorator.Deteriorate()
            );
            Assert.AreEqual(DeteriorationState.DIRTY, inventory.checkDeteriorationState().GetValueOrDefault(resource));
            Assert.AreEqual(0, resource.price);

            10.Times(index =>
                deteriorator.Deteriorate()
            );
            Assert.AreEqual(DeteriorationState.FIXABLE, inventory.checkDeteriorationState().GetValueOrDefault(resource));

            9.Times(index =>
                deteriorator.Deteriorate()
            );
            Assert.AreEqual(DeteriorationState.FIXABLE, inventory.checkDeteriorationState().GetValueOrDefault(resource));
            Assert.AreEqual(0, resource.price);

            deteriorator.Deteriorate();
            Assert.IsTrue(inventory.items[0] is Resource);
        }
    }
}