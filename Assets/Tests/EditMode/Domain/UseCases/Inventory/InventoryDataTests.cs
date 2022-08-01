using Domain.Entities.Inventory;
using Domain.Entities.Inventory.Items;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using Utils.Extensions.Bucles;

namespace Domain.UseCases.Inventory {
    public class InventoryDataTests  {
        [Test]
        public void Increase() {
            ItemsContainer inventory = new InventoryData();
            var cake = new Consumable("cake", "Chocolate Cake", 10, 1, Consumable.Type.FOOD);
            inventory.Add(cake.CopyAsNew());
            inventory.Add(cake.CopyAsNew());
            inventory.Add(cake.CopyAsNew());

            Assert.AreEqual(3, inventory.items.Count);

            inventory.Remove(inventory.items[0]);
            Assert.AreEqual(2, inventory.items.Count);

            var expected = new Dictionary<Item, DeteriorationState>() { { cake.CopyAsNew(), DeteriorationState.NORMAL }, { cake.CopyAsNew(), DeteriorationState.NORMAL } };
            var actual = inventory.checkDeteriorationState();
            Assert.AreEqual(expected.Values, actual.Values);
        }
    }
}