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
        Debug.Log("the dokizeme");
    }
    #endregion
}
