using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private CharacterController cc;
    private Vector3 move;

    [SerializeField] private Camera cam;
    [SerializeField] private float fov;
    [SerializeField] private float wallFov;
    [SerializeField] private float wallJumpFov;
    [SerializeField] private float wallFovTime;

    [SerializeField] private float speed;
    [SerializeField] private float gravity;

    [SerializeField] private float jumpHeight;

    [SerializeField] private float wallJumpHeight;
    [SerializeField] private float wallJumpSpeed;
    [SerializeField] private bool canBounce;
    

    [SerializeField] Vector3 velocity;
    [SerializeField] private bool isGrounded;

    void Update()
    {
        //Character Movement
        if(cc.isGrounded && velocity.y < 0)
        {
            velocity.y = -20f;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        move = (transform.right * x + transform.forward * z);

        cc.Move(move * speed * Time.deltaTime);

        //Jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);

        if(isGrounded)
        {
            canBounce = true;
        }

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallFovTime * Time.deltaTime);
    }

    //Wall Jump
    void OnControllerColliderHit (ControllerColliderHit hit)
    {
        if(!isGrounded)
        {
            if(hit.transform.tag == "WallJump")
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallFov, wallFovTime * Time.deltaTime);
                if(canBounce)
                {
                    if(Input.GetButtonDown("Jump"))
                    {
                        velocity.y = wallJumpHeight;
                        move = hit.normal * wallJumpSpeed;
                        canBounce = false;
                    }
                }
            }
        }
    }
}