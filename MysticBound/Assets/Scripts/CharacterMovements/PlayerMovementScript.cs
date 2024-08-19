using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
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
}
