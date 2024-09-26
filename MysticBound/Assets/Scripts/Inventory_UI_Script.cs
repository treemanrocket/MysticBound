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


    Inventory_Slot_Scripts inventorySlots;

    public delegate void OnToggleInventoryOn();
    public OnToggleInventoryOn toggleInventoryOnCallBack;

    public delegate void OnToggleInventoryOff();
    public OnToggleInventoryOff toggleInventoryOffCallBack;

    
    //InventorySlot will need to have the inventory slot script

    private void Awake()
    {
       


    }

    // Start is called before the first frame update
    void Start()
    {
        inventoryOn = false;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region /*INVENTORY_TOGGLE*/
    public void OnInventoryToggle(InputAction.CallbackContext ctx)
    {
        if (!inventoryOn) 
        {
            inventoryOn = true;
            toggleInventoryOnCallBack?.Invoke();
            
        }
        
        else if (inventoryOn) 
        {
            inventoryOn = false;
            toggleInventoryOffCallBack?.Invoke();
        }
    }
    #endregion
}
