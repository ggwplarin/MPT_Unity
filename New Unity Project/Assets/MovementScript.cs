using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MovementScript : MonoBehaviour
{
    public CharacterController controller;
    //public VisualEffect vFX;
    public Transform cam;
    public float v = 10f;
    public float turnRate = 0.1f;
    public float gravity = -9f;
    public float gravytyMp = 15f;
    public float jumpPower = 35f;
    public float shiftV = 25f;
    public float maxStamina = 100f;
    public float stamina = 0f;
    Vector3 velocity;
    float turnV;

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        bool shiftpress = Input.GetKey(KeyCode.LeftShift);
        

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        velocity.y = !controller.isGrounded ? velocity.y + gravity * Time.deltaTime*gravytyMp:0;
        stamina += shiftpress && stamina > 1 &&direction.magnitude >= 0.1f ? -1 : stamina == maxStamina ? 0 : 0.25f;
        if (Input.GetButton("Jump")&&controller.isGrounded)
        {
            //vFX.Play();
            velocity.y += jumpPower;
        }

        if (direction.magnitude >= 0.1f)
        {
            float target = Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg+cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target, ref turnV, turnRate);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
       
            Vector3 moveDir = Quaternion.Euler(0f, target, 0f) * Vector3.forward;
            
            controller.Move(moveDir.normalized*(shiftpress&&stamina>10?shiftV:v) *Time.deltaTime);
        }
        controller.Move(velocity * Time.deltaTime);
    }
}
