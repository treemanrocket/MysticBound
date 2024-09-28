using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{


    #region /*SINGLETON*/
    public static InventoryScript instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged OnItemCallBack;

    #region/*STATE*/
    public enum InventoryState
    {
        ITEMS,
        WEAPONS,
        ARMOR
    }

    [Header("InventoryStates")]
    [Tooltip("This will be the inventory state where it will show if the inventory is on items, weapons, or armor category")]
    public InventoryState inventoryStates;

    #endregion

    #region /*INVENTORY*/
    [Header("INVENTORY")]
    [Tooltip("limit for how many items you can have in the inventory")][SerializeField] private int inventoryLimit;
    [System.Serializable]
    public struct inventoryData
    {
        public Sprite inventoryDataSprite;
        public GameObject inventoryDataGameObject;
    }
    [Tooltip("a list of how many items")] public List<inventoryData> itemList = new List<inventoryData>();


    [Header ("WeaponList")]
    [Tooltip("limit for how many weapons you can have in the weapon list")][SerializeField] private int weaponLimit;
    [System.Serializable]
    public struct WeaponData
    {
        public Sprite WeaponDataSprite;
        public GameObject WeaponDataGameObject;
        public WeaponScript weaponScripts;
    }
    [Tooltip("a list of how many weapons")] public List<WeaponData> weaponList = new List<WeaponData>();


    [Header("ArmorList")]
    [Tooltip("limit for how many armors you can have in the armor list")][SerializeField] private int armorLimit;
    [System.Serializable]
    public struct ArmorData
    {
        public Sprite ArmorDataSprite;
        public GameObject ArmorDataGameObject;
        public ArmorScripts armorScripts;
    }
    [Tooltip("a list of how many armos")] public List<ArmorData> armorList = new List<ArmorData>();
    #endregion

    #region/*CHANGE STATES*/

    public void ChangeToItems()
    {
        inventoryStates = InventoryState.ITEMS;
    }

    public void ChangeToWeapons()
    {
        inventoryStates = InventoryState.WEAPONS;
    }

    public void ChangeToArmor()
    {
        inventoryStates = InventoryState.ARMOR;
    }

    #endregion

    #region /*ARMOR INVENTORY*/

    public void AddArmor(GameObject gameArmor, Sprite armorIcons, ArmorScripts armorScriptsObject)
    {
        if (armorList.Count < armorLimit)
        {
            armorList.Add(new ArmorData { 
                ArmorDataSprite = armorIcons, 
                ArmorDataGameObject = gameArmor, 
                armorScripts = armorScriptsObject });

               OnItemCallBack?.Invoke();
        }

        else if (armorList.Count >= armorLimit)
        {
            Debug.Log("this is exceeding the limit");
        }
    }

    public void RemoveArmor(GameObject gameArmor, Sprite armorIcons, ArmorScripts armorScriptsObject)
    {
        armorList.Remove(new ArmorData { 
            ArmorDataSprite = armorIcons, 
            ArmorDataGameObject = gameArmor, 
            armorScripts = armorScriptsObject });

            OnItemCallBack?.Invoke();
    }

    #endregion
    #region /*WEAPON INVENTORY*/

    public void AddWeapon(GameObject gameWeapon, Sprite weaponIcons, WeaponScript weaponScriptsObject)
    {
        if (weaponList.Count < weaponLimit)
        {
            weaponList.Add(new WeaponData { 
                WeaponDataSprite = weaponIcons,
                WeaponDataGameObject = gameWeapon, 
                weaponScripts = weaponScriptsObject });

              OnItemCallBack?.Invoke();
        }

        else if (weaponList.Count >= weaponLimit)
        {
            Debug.Log("this is exceeding the limit");
        }
    }

    public void RemoveWeapon(GameObject gameWeapon, Sprite weaponIcons, WeaponScript weaponScriptsObject)
    {
        weaponList.Remove(new WeaponData {
            WeaponDataSprite = weaponIcons,
            WeaponDataGameObject = gameWeapon,
            weaponScripts = weaponScriptsObject });

          OnItemCallBack?.Invoke();
    }

    #endregion

}
