using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; //get rid of this soon when we have a scene manager script

public class PlayerMovementScript : MonoBehaviour, IPickable
{
    [Header("Variables")]
    private Rigidbody rigidBody;
    [SerializeField] private float speed;
    private Vector2 moveInputs;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = new Vector2(moveInputs.x * speed, rigidBody.velocity.y);

        //quick dev tool this goes into a different scene to test the singleton and the inventory
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Albert_Dev 1");
        }

    }

    #region /*MOVEMENT*/
    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            moveInputs = ctx.ReadValue<Vector2>();

        }

        else if (ctx.canceled)
        {
            moveInputs = Vector2.zero;
        }
    }
    #endregion

    public void PickupArmor(GameObject armorGameObject, Sprite armorIcon, ArmorScripts armorBehavior)
    {
        InventoryScript.instance.AddArmor(armorGameObject, armorIcon, armorBehavior);
    }

    public void PickupWeapon(GameObject weaponGameObject, Sprite weaponIcon, WeaponScript weaponBehavior)
    {
        InventoryScript.instance.AddWeapon(weaponGameObject, weaponIcon, weaponBehavior);
    }
}
