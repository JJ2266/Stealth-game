using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class player : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField] public float Movespeed = 10;
    [SerializeField] Transform cam;

    Vector3 scale;

    private float? lastGroundedTime;
    private float? jumpbuttonPressedTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightShift))
        {
            EnterCrouch();
        }
        if (Input.GetKeyUp(KeyCode.RightShift))
        {
            ExitCrouch();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal") * Movespeed;
        float verticalinput = Input.GetAxisRaw("Vertical") * Movespeed;

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = 0;


        Vector3 ForwardRelative = verticalinput * camForward;
        Vector3 RightRelative = horizontalInput * camRight;

        Vector3 moveDir = ForwardRelative + RightRelative;

   
            rb.velocity = new Vector3(moveDir.x, rb.velocity.y, moveDir.z);

            var movementVector = new Vector3(horizontalInput, 0, verticalinput);
            
 
        if (moveDir.sqrMagnitude == 0) return;
        transform.forward = new Vector3(rb.velocity.x, 0, rb.velocity.z);

    }
    void EnterCrouch()
    {
         var Crouchscale = new Vector3(1f, 0.5f, 1f);
        transform.localScale = Crouchscale;
        Movespeed = 3;
    }
    void ExitCrouch()
    {
        var Normalscale = new Vector3(1f, 1f, 1f);
        transform.localScale = Normalscale;
        Movespeed = 10;
    }
}
