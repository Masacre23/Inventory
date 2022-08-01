using Domain.Entities.Inventory;
using Domain.UseCases.Inventory.ScriptableObjects;
using Domain.UseCases;
using Domain.UseCases.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Injector;
using Domain.Entities.Inventory.Items;

public class DependenciesInitializer {
    public void Initialize() {
        var inventoryData = new InventoryData();
        var inventoryConfig = Resources.Load<InventoryConfig>("Data/InventoryConfig");
        var weapons = new List<Weapon>();
        var resources = new List<Resource>();
        var consumables = new List<Consumable>();

        foreach(WeaponSO weaponSO in Resources.LoadAll("Data", typeof(WeaponSO))){
            weapons.Add(weaponSO.weapon);
        }
        foreach (ResourceSO resourceSO in Resources.LoadAll("Data", typeof(ResourceSO))) {
            resources.Add(resourceSO.resource);
        }
        foreach (ConsumableSO consumableSO in Resources.LoadAll("Data", typeof(ConsumableSO))) {
            consumables.Add(consumableSO.consumable);
        }

        Injector.Instance.RegisterService<InventoryConfig>(inventoryConfig);
        Injector.Instance.RegisterService<ItemsContainer>(inventoryData);
        Injector.Instance.RegisterService<IDeteriorator>(new Deteriorator(inventoryData));
        Injector.Instance.RegisterService<IItemActivator>(new ItemActivator(inventoryData));
        Injector.Instance.RegisterService<IInventoryIncreaser>(new InventoryIncreaser(inventoryData, inventoryConfig.maxWeight, weapons, resources, consumables));
    }
}