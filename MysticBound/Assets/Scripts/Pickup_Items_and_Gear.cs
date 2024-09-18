using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Items_and_Gear : MonoBehaviour
{
    [Header("Behaviour script")]
    public ArmorScripts armorScripts;
    public WeaponScript weaponScripts;
    private bool pickedUp;

    [Header("Variables")]
    [Tooltip("This is for how many seconds it takes for the gun to be picked up again")]
    [Range(0.1f, 10f)]
    public float waitSeconds;

    void Awake()
    {
        pickedUp = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IPickable characterGameObject))
        {
            this.gameObject.SetActive(false);
            pickedUp = true;
            this.gameObject.transform.parent = other.transform;


            //if the game object is an armor piece
            if (armorScripts != null && weaponScripts == null) 
            {
                characterGameObject.PickupArmor(this.gameObject, armorScripts.icon, armorScripts);
            }

            //if the game object is an weapon piece
            if (armorScripts == null && weaponScripts != null)
            {
                characterGameObject.PickupWeapon(this.gameObject, weaponScripts.icon, weaponScripts);
            }
        }
    }
}
