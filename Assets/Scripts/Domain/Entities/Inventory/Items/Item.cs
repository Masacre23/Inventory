using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Domain.Entities.Inventory.Items {


    [System.Serializable]
    public abstract class  Item {
        public string id;
        public string name;
        public int weight;
        [ConditionalHide("hidePrice")]
        public int price;

        [HideInInspector]
        public int deterioration;

        public DeteriorationState deteriorationState { get => 
                deterioration < (int)DeteriorationState.DIRTY ? DeteriorationState.NORMAL :
                deterioration < (int)DeteriorationState.FIXABLE ? DeteriorationState.DIRTY :
                deterioration < (int)DeteriorationState.DESTROYED ? DeteriorationState.FIXABLE :
                DeteriorationState.DESTROYED;
        }

        [ConditionalHide("hideDeteriorationRate")]
        public int deteriorationRate;

        public Item(string id, string name, int weight, int price = 0, int deteriorationRate = 0) {
            this.id = id;
            this.name = name;
            this.weight = weight;
            this.price = price;
            this.deteriorationRate = deteriorationRate;
        }

        public abstract Item CopyAsNew();
    }
}