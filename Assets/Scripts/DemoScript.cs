using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToAdd;

    public void addToInventory(int id){
        inventoryManager.addItem(itemsToAdd[id]);
    }
}
