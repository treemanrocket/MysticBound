using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("HOLDER FOR ITEMS")]
    Pickup_Items_and_Gear weaponPickUp, armorPickUp, regularItemPickUp;

    [SerializeField] private CharacterController characterController;

    private GameObject playerGameObject;
    private void Awake()
    {
        playerGameObject = GameObject.FindWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        characterController = playerGameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
