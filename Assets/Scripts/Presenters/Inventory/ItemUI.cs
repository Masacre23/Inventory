using Domain.UseCases.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Injector;
using TMPro;
using Controllers;
using Domain.Entities.Inventory.Items;
using Domain.Entities.Inventory;

namespace Presenters.Inventory {
    public class ItemUI : MonoBehaviour {
        public delegate void EventActivate(string itemName);
        public event EventActivate onActivate;

        [SerializeField]
        TextMeshProUGUI name, price, weight, deterioration;
        [SerializeField]
        Button button;

        public void Activate() {
            if(onActivate != null) onActivate(name.text);
        }

        public void UpdateInfo(Item item) {
            name.text = item.name;
            price.text = "Price: " + item.price;
            weight.text = "Weight: " + item.weight;
            deterioration.text = item.deteriorationState.ToString();
            var colors = button.colors;
            switch (item.deteriorationState) {
                case DeteriorationState.NORMAL:
                    colors.normalColor = Color.green;
                    break;
                case DeteriorationState.DIRTY:
                    colors.normalColor = Color.yellow;
                    break;
                case DeteriorationState.FIXABLE:
                    colors.normalColor = Color.red;
                    break;
                case DeteriorationState.DESTROYED:
                    colors.normalColor = Color.black;
                    name.color = Color.white;
                    price.color = Color.white;
                    weight.color = Color.white;
                    deterioration.color = Color.white;
                    break;
            }
            button.colors = colors;
        }
    }
}