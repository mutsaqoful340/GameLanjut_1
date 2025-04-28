
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public InputPlayModule InputModule;
    public CharacterController Body;
    public Animator Anim;
    public Vector3 MoveUpdate;
    public float Speed;


    // State Locomotion
    public bool IsIdle;
    public bool IsFall;
    public bool IsJump;


    //Ts instantiate Singleton
    public static PlayerController Instance { get; private set; }

    private IEnumerator JumpCoroutine()
    {
        if (!IsJump)
        {
            // Matikan efek gravitasi
            IsFall = false;
            IsJump = true;

            if (!IsIdle)
            {
                //Tinggikan nilai axis y
                MoveUpdate.y++;
                // Gerakkan karakter
                Body.Move(MoveUpdate * Time.deltaTime);
                yield return new WaitForSeconds(0.3f);

            }
            else
            {
                // Untuk loncat diam
                MoveUpdate.y += 9.8f;
                Body.Move(MoveUpdate * Time.deltaTime);
                yield return new WaitForSeconds(0.3f);
            }
            // Aktifkan efek gravitasi;
            IsFall = true;  
        }
    }
    
    private void Action(ActionState state)
    {
        switch(state)
        {
            case ActionState.Skill:
                StartCoroutine(JumpCoroutine());
                break;
        }
    }
    private void Fall()
    {
        if (IsFall)
        {
            MoveUpdate.y += -9.8f * Time.deltaTime;

            //Gerakkan karakter agar jatoh ke bawah
            Body.Move(MoveUpdate * Time.deltaTime);
            if (Body.isGrounded)
            {
                // Reset pergerakan ke nilai 0
                MoveUpdate = Vector3.zero;
                IsJump = false;
            }
            else
            {
                IsJump = true;
            }
        }
    }

    private void Idle()
    {

    }

    private void Run()
    {

    }

    private void Death()
    {

    }

    private void Jump()
    {

    }

    private void Dash()
    {

    }

    private void Walk()
    {

    }
    private void Locomotion()
    {
        // Default position is Fall
        Fall();
        // ?"Normalized" is used for things doesn't need acceleration.
        // ?Value of "Normalized" is 1. And the kind that bettween 0-1 is called "Fuzzy". It's used for things need acceleration.

        // Waiting input
        var vector = InputModule ? InputModule.MoveHandler.normalized : Vector3.zero;
        IsIdle = (vector.x, vector.z) == (0, 0);
        
        if (IsIdle)
        {
            // Character idles.
            Anim.SetFloat("Move", 0f);
            Idle();
        }
        // If there's input,
        else
        {
            // Character Animation
            Run();
            Anim.SetFloat("Move", 1f);

            if (!IsJump)
            {
                // Update movement
                MoveUpdate = new Vector3(vector.x, MoveUpdate.y, vector.z);
                var rotate = Quaternion.LookRotation(vector);

                // Character Rotation
                transform.rotation = Quaternion.Slerp(rotate, transform.rotation, Time.deltaTime);
            }

            // Character Move
            Body.Move(Speed * Time.deltaTime * MoveUpdate);
        }
        // Character ded.
        Death();

        Jump();
        Walk();
        Dash();
    }

    private void Start()
    {
        // Singleton(Lazy) > Hanya mengizinkan object untuk exist cuma satu kali (ex: Player. Multiplayer? That's a guest!)
        Instance = this;
        // Initialize component
        Body = GetComponent<CharacterController>();
        Anim = GetComponent<Animator>();

        // Initilize Action
        InputModule.OnAction = Action;
    }

    private void Update()
    {
        Locomotion();
    }
}
