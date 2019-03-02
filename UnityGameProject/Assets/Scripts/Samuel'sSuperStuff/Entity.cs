using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{    
    public float MovementSpeed;
    public float WalkSpeed = 2;
    public float SneekSpeed = 1;
    public float SprintSpeed = 4;
    public float startColliderHeight = 0;
    private float Stamina = 5.0f;

    public float JumpStrength;
   
    private Rigidbody Rigid;

    public Event_Start Boolon;
    public Larder_Event_Start Boolon2;

    Animator anim; //Assigns the animator

    private bool StartRotate = true;
    private CapsuleCollider col;

    private bool Moving;
    private bool isCrouching;

    //set to one for no effect and only changed in traps;
    public float SpeedModifier = 1;       
    
    protected virtual void Start ()
    {
        MovementSpeed = WalkSpeed;
        anim = GetComponent<Animator>(); //Calls animator
        Rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        startColliderHeight = col.height;
	}

    public void Move(Vector3 MoveDir)
    {
        
        if (Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetBool("IsCrouching", true);
            MovementSpeed = SneekSpeed;
        
            AnimatorStateInfo stateinfo = anim.GetCurrentAnimatorStateInfo(0);
            if (isCrouching)
            {
                if (stateinfo.IsName("Crouch_Idle") || stateinfo.IsName("Crouch_Walk"))
                {
                    float colliderHeight = anim.GetFloat("ColliderHeight");
                    col.height = startColliderHeight * colliderHeight;
                    float centery = col.height / 2;
                    col.center = new Vector3(col.center.x, centery, col.center.z);
                }
            }
            else
            {
                MovementSpeed = WalkSpeed;
            }
            if (Input.GetKey(KeyCode.W))
            {
                anim.SetBool("IsCrouchWalk", true);
            }
            else
            {
                anim.SetBool("IsCrouchWalk", false);
            }
        }
        else
        {            
            anim.SetBool("IsCrouching", false);
            col.height = startColliderHeight;
            float centery = col.height / 2;
            col.center = new Vector3(col.center.x, centery, col.center.z);
        }
        if (Input.GetKey(KeyCode.W))
        {
            Moving = true;

            anim.SetBool("IsWalking", true);

            Vector3 Vec;
            Vec = new Vector3(MoveDir.x * MovementSpeed, Rigid.velocity.y, MoveDir.z * MovementSpeed);

            Rigid.velocity = Vec * SpeedModifier;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                Debug.Log("running");
                Debug.Log(MovementSpeed);
                MovementSpeed = SprintSpeed;

                anim.SetBool("IsRunning", true);               
            }
            else
            {
                anim.SetBool("IsRunning", false);
                MovementSpeed = WalkSpeed;
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                
                isCrouching = true;
                MovementSpeed = SneekSpeed;
            
                anim.SetBool("IsCrouchWalk", true);
            
                AnimatorStateInfo stateinfo = anim.GetCurrentAnimatorStateInfo(0);
                if (isCrouching)
                {
                    if (stateinfo.IsName("Crouch_Idle") || stateinfo.IsName("Crouch_Walk"))
                    {
                        float colliderHeight = anim.GetFloat("ColliderHeight");
                        col.height = startColliderHeight * colliderHeight;
                        float centery = col.height / 2;
                        col.center = new Vector3(col.center.x, centery, col.center.z);
                    }
                }
                else
                {
                    MovementSpeed = WalkSpeed;
                    
                }
            }
            else
            {
                anim.SetBool("IsCrouchWalk", false);
                col.height = startColliderHeight;
                float centery = col.height / 2;
                col.center = new Vector3(col.center.x, centery, col.center.z);
            }
            

            //if (Input.GetKey(KeyCode.A))
            //{
            //    MovementSpeed = WalkSpeed;
            //
            //    anim.SetBool("IsWalking", false);
            //    anim.SetBool("IsWalkingL", true);
            //
            //
            //}
            //else
            //{
            //    anim.SetBool("IsWalking", true);
            //    anim.SetBool("IsWalkingL", false);
            //}
            //
            //if (Input.GetKey(KeyCode.D))
            //{
            //    MovementSpeed = WalkSpeed;
            //
            //    anim.SetBool("IsWalking", false);
            //    anim.SetBool("IsWalkingR", true);
            //
            //
            //}
            //else
            //{
            //    anim.SetBool("IsWalking", true);
            //    anim.SetBool("IsWalkingR", false);
            //}
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Moving = true;
        
            anim.SetBool("IsWalkingL", true);
        
            Vector3 Vec;
            Vec = new Vector3(MoveDir.x * MovementSpeed, Rigid.velocity.y, MoveDir.z * MovementSpeed);
        
            Rigid.velocity = Vec * SpeedModifier;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Moving = true;
        
            anim.SetBool("IsWalkingR", true);
        
            Vector3 Vec;
            Vec = new Vector3(MoveDir.x * MovementSpeed, Rigid.velocity.y, MoveDir.z * MovementSpeed);
        
            Rigid.velocity = Vec * SpeedModifier;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Moving = true;

            anim.SetBool("IsWalkingR", true);

            Vector3 Vec;
            Vec = new Vector3(MoveDir.x * MovementSpeed, Rigid.velocity.y, MoveDir.z * MovementSpeed);

            Rigid.velocity = Vec * SpeedModifier;
        }


        //if(Input.GetKey(KeyCode.Space))
        // {
        //     anim.SetBool("IsJumping", true);
        //     Jump();
        // }
        else
        {
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsWalkingL", false);
            anim.SetBool("IsWalkingR", false);
            anim.SetBool("IsRunning", false);
            
        }       

    }

    public void Jump()
    {
       if (OnGround())
       {
            Rigid.AddForce(Vector3.up * JumpStrength);
       }
    }    

    public bool OnGround()
    {
        RaycastHit HitInfo;
        return Physics.Raycast(transform.position, Vector3.down, out HitInfo, 1.2f);

    }

    public float GetSpeed()
    {
        return WalkSpeed;
    }

    public void AnimStop()
    {
        if (Boolon.Event == true)
        {
            Debug.Log("working");
            anim.SetBool("IsWalking", false);
            anim.SetBool("IsWalkingL", false);
            anim.SetBool("IsWalkingR", false);
            anim.SetBool("IsRunning", false);
        }
    }
}
