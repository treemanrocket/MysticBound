using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static InventoryScript;

public class Inventory_Slot_Scripts : MonoBehaviour
{
    [Header("HOLDER FOR IMAGES")]
    public Image weaponIcon;
    public Image armorIcon;
    public Image regularItemIcon;

    [Header("HOLDER FOR ITEMS")]
    public GameObject weaponItems;
    public GameObject armorItems;
    public GameObject regularItems;

    //this is for holding the different pickup script
    Pickup_Items_and_Gear weaponPickUp, armorPickUp, regularItemPickUp;

    //this is for holding the SO
    WeaponScript weaponScript;
    ArmorScripts armorScript;

    [SerializeField] private PlayerMovementScript playermovementScript;

    private GameObject playerGameObject;
    private void Awake()
    {
        playerGameObject = GameObject.FindWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        playermovementScript = playerGameObject.GetComponent<PlayerMovementScript>();
    }

    #region/*WEAPONS*/
    public void AddWeaponInventory(GameObject weaponRef, Sprite weaponSpriteRef, WeaponScript weapon_SO)
    {
        weaponPickUp = weaponRef.GetComponent<Pickup_Items_and_Gear>();
        weaponScript = weapon_SO;
        //weaponIcon.enabled = true;
        weaponIcon.sprite = weaponSpriteRef;
        weaponItems = weaponRef;

        if (InventoryScript.instance.inventoryStates == InventoryState.WEAPONS && weaponItems != null)
        {
            weaponIcon.enabled = true;
        }
    }

    public void RemoveWeaponInventory()
    {
        weaponScript = null;
        weaponIcon.enabled = false;
        weaponIcon.sprite = null;
        weaponItems = null;
    }
    #endregion

    #region/*ARMOR*/
    public void AddArmorInventory(GameObject armorRef, Sprite armorSpriteRef, ArmorScripts armor_SO)
    {
        armorPickUp = armorRef.GetComponent<Pickup_Items_and_Gear>();
       armorScript = armor_SO;
        //armorIcon.enabled = true;
        armorIcon.sprite = armorSpriteRef;
        armorItems = armorRef;

        if (InventoryScript.instance.inventoryStates == InventoryState.ARMOR && armorItems !=null)
        {
            armorIcon.enabled = true;
        }
    }

    public void RemoveArmorInventory()
    {
        armorScript = null;
        armorIcon.enabled = false;
       armorIcon.sprite = null;
       armorItems = null;
    }
    #endregion
}
