using Domain.UseCases.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Injector;
using TMPro;
using Presenters.Inventory;
using Domain.Entities.Inventory.Items;
using Domain.UseCases.Inventory.ScriptableObjects;
using Domain.Entities.Inventory;
using Utils.Extensions.Collections;

namespace Controllers {
    public class InventoryController : MonoBehaviour {
        
        ItemsContainer container;
        IItemActivator activator;
        IInventoryIncreaser increaser;
        IDeteriorator deteriorator;
        InventoryConfig inventoryConfig;

        [SerializeField]
        Transform inventoryUI;

        List<ItemUI> itemsUI = new List<ItemUI>();

        void Start() {
            container = Injector.Instance.GetService<ItemsContainer>();
            activator = Injector.Instance.GetService<IItemActivator>();
            increaser = Injector.Instance.GetService<IInventoryIncreaser>();
            deteriorator = Injector.Instance.GetService<IDeteriorator>();
            inventoryConfig = Injector.Instance.GetService<InventoryConfig>();

            activator.onRemovedItem += RemoveItemAt;
            deteriorator.onReplacedItemByGarbage += OnReplacedByGarbage;
        }

        float period = 0.0f;
        void Update() {
            if (period > inventoryConfig.deteriorationSeconds) {
                deteriorator.Deteriorate();

                container.items.ForeachIndexed((index, item) =>
                    itemsUI[index].UpdateInfo(item)
                );

                period = 0;
            }
            period += Time.deltaTime;
        }

        private void OnReplacedByGarbage(int index, Item garbage) {
            var itemUI = itemsUI[index];
            RemoveItemAt(index);
            AddItem(garbage, index);
        }

        private void OnItemActivated(string itemName) {
            activator.Activate(itemName);
        }

        private void AddItem(Item item, int index = -1) {
            if (item == null) return;

            var newItem = GameObject.Instantiate(inventoryConfig.itemUI);
            newItem.transform.SetParent(inventoryUI);
            var itemUI = newItem.GetComponent<ItemUI>();
            itemUI.UpdateInfo(item);
            itemUI.onActivate += OnItemActivated;
            if (index == -1) {
                itemsUI.Add(itemUI);
            } else {
                newItem.transform.SetSiblingIndex(index);
                itemsUI.Insert(index, itemUI);
            }
        }

        public void AddWeapon() {
            AddItem(increaser.AddWeapon());
        }

        public void AddResource() {
            AddItem(increaser.AddResource());
        }

        public void AddConsumible() {
            AddItem(increaser.AddConsumable());
        }

        private void RemoveItemAt(int index) {
            var itemUI = itemsUI[index];
            itemUI.onActivate -= OnItemActivated;
            itemsUI.RemoveAt(index);
            GameObject.Destroy(itemUI.gameObject);
        }

        public void RemoveLast() {
            if (itemsUI.Count == 0) {
                Debug.Log("The inventory is empty");
                return;
            }
            container.items.RemoveAt(itemsUI.Count - 1);
            RemoveItemAt(itemsUI.Count - 1);
        }
    }
}