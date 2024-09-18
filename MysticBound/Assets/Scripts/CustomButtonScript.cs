using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class CustomButtonScript : MonoBehaviour, IPointerClickHandler
{

    public bool inventoryActive;

    public UnityEvent leftClick;
    public UnityEvent middleClick;
    public UnityEvent rightClick;
   
    private InventoryScript inventoryScript;

    //will need to make a delegate for the inventory script so that it can call on the two functions, MakeButtonsActive and
    //MakeButtonsInactive
    void Awake()
    {
        inventoryActive = false;
    }

    void MakeButtonsActive()
    {
        inventoryActive = true;
    }

    void MakeButtonsInactive()
    {
        inventoryActive = false;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            leftClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Middle)
            middleClick.Invoke();
        else if (eventData.button == PointerEventData.InputButton.Right)
            rightClick.Invoke();
    }

    public void EquipItem()
    {
        if (inventoryActive) 
        {

            switch (InventoryScript.instance.inventoryStates)
            {
                case InventoryScript.InventoryState.ITEMS:
                    Debug.Log("use button was clicked " + this);
                    break;
                case InventoryScript.InventoryState.WEAPONS:
                    Debug.Log("equip button for weapon was clicked " + this);
                    break;
                case InventoryScript.InventoryState.ARMOR:
                    Debug.Log("equip button for armor was clicked " + this);
                    break;
            }
        }

        
    }

    public void RemoveItem()
    {
        if (inventoryActive)
        {
            switch (InventoryScript.instance.inventoryStates)
            {
                case InventoryScript.InventoryState.ITEMS:
                    Debug.Log("remove button was clicked " + this);
                    break;
                case InventoryScript.InventoryState.WEAPONS:
                    Debug.Log("remove button for weapon was clicked " + this);
                    break;
                case InventoryScript.InventoryState.ARMOR:
                    Debug.Log("remove button for armor was clicked " + this);
                    break;
            }
        }
        
    }
   
}
