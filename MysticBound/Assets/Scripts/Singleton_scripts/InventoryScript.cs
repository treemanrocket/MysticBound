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
    [Tooltip("limit for how many weapons you can have in the weapon list")][SerializeField] private int WeaponLimit;
    [System.Serializable]
    public struct WeaponData
    {
        public Sprite WeaponDataSprite;
        public GameObject WeaponDataGameObject;
        public WeaponScript weaponScripts;
    }
    [Tooltip("a list of how many weapons")] public List<WeaponData> WeaponList = new List<WeaponData>();


    [Header("ArmorList")]
    [Tooltip("limit for how many armors you can have in the armor list")][SerializeField] private int ArmorLimit;
    [System.Serializable]
    public struct ArmorData
    {
        public Sprite ArmorDataSprite;
        public GameObject ArmorDataGameObject;
        public ArmorScripts armorScripts;
    }
    [Tooltip("a list of how many armos")] public List<ArmorData> ArmorList = new List<ArmorData>();
    #endregion

}
