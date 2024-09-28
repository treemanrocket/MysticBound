using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventoryScript;

public class Change_Inventory_StateScript : MonoBehaviour
{
   
    public void ChangeToWeapons()
    {
        InventoryScript.instance.inventoryStates = InventoryState.WEAPONS;
    }

    public void ChangeToItems()
    {
        InventoryScript.instance.inventoryStates = InventoryState.ITEMS;
    }  

    public void ChangeToArmor()
    {
        InventoryScript.instance.inventoryStates = InventoryState.ARMOR;
    }
}
