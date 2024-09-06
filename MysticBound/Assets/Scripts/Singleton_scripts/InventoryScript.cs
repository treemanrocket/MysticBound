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

    #region /*INVENTORY*/
    [Header("INVENTORY")]
    [Tooltip("limit for how many items you can have in the inventory")][SerializeField] private int inventoryLimit;
    [System.Serializable]
    public struct inventoryData
    {
        public Sprite inventoryDataSprite;
        public GameObject inventoryDataGameObject;
        //public GunBehavior armGunBehavior;
    }
    [Tooltip("a list of how many items")] public List<inventoryData> itemList = new List<inventoryData>();
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
