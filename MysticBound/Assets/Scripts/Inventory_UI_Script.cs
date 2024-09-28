using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory_UI_Script : MonoBehaviour
{
    InventoryScript inventoryScript;
    private Image[] imageChilderens;
    private Button[] buttonChilderens;
    private bool inventoryOn;


    public Transform inventoryParent;


    Inventory_Slot_Scripts [] inventorySlots;

    public delegate void OnToggleInventoryOn();
    public OnToggleInventoryOn toggleInventoryOnCallBack;

    public delegate void OnToggleInventoryOff();
    public OnToggleInventoryOff toggleInventoryOffCallBack;


    

    private void Awake()
    {
        inventoryScript = InventoryScript.instance;
        inventoryScript.OnItemCallBack += UpdateInventoryUI;
        imageChilderens = GetComponentsInChildren<Image>();
        buttonChilderens = GetComponentsInChildren<Button>();
        inventorySlots = inventoryParent.GetComponentsInChildren<Inventory_Slot_Scripts>();

        if (inventoryScript.OnItemCallBack != null)
        {
            Debug.Log("this is subscribed");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inventoryOn = false;

        foreach (Image itemImage in imageChilderens)
        {
            Color alpha = itemImage.color;
            alpha.a = 0;
            itemImage.color = alpha;
        }

        foreach (Button buttons in buttonChilderens)
        {
            buttons.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInventoryUI()
    {
        #region /*WEAPONS_INVENTORY*/
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < inventoryScript.weaponList.Count)
            {
                inventorySlots[i].AddWeaponInventory(
                inventoryScript.weaponList[i].WeaponDataGameObject,
                inventoryScript.weaponList[i].WeaponDataSprite,
                inventoryScript.weaponList[i].weaponScripts);
            }
            else 
            {
                inventorySlots[i].RemoveWeaponInventory();
            }
        }
      
        #endregion

        #region /*ARMORS_INVENTORY*/
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < inventoryScript.armorList.Count)
            {
                inventorySlots[i].AddArmorInventory(
                inventoryScript.armorList[i].ArmorDataGameObject,
                inventoryScript.armorList[i].ArmorDataSprite,
                inventoryScript.armorList[i].armorScripts);
            }
            else
            {
                inventorySlots[i].RemoveArmorInventory();
            }
        }


        #endregion
    }
    public void ChangeWeaponUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].weaponItems != null)
            {
                inventorySlots[i].weaponIcon.enabled = true;
                inventorySlots[i].armorIcon.enabled = false;
            }
        
        }
    }

    public void ChangeArmorUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].armorItems != null) 
            {
                inventorySlots[i].weaponIcon.enabled = false;
                inventorySlots[i].armorIcon.enabled = true;
            }
            
        }
    }
    #region /*INVENTORY_TOGGLE*/
    public void OnInventoryToggle(InputAction.CallbackContext ctx)
    {
        if (!inventoryOn) 
        {
            inventoryOn = true;
            toggleInventoryOnCallBack?.Invoke();

            foreach (Image itemImage in imageChilderens)
            {
                Color alpha = itemImage.color;
                alpha.a = 1;
                itemImage.color = alpha;
            }

            foreach (Button buttons in buttonChilderens)
            {
                buttons.interactable = true;
            }
        }
        
        else if (inventoryOn) 
        {
            inventoryOn = false;
            toggleInventoryOffCallBack?.Invoke();

            foreach (Image itemImage in imageChilderens)
            {
                Color alpha = itemImage.color;
                alpha.a = 0;
                itemImage.color = alpha;
            }

            foreach (Button buttons in buttonChilderens)
            {
                buttons.interactable = false;
            }
        }
    }
    #endregion
}
