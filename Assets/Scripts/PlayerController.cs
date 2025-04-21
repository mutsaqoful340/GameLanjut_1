
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public InputPlayModule InputModule;
    public CharacterController Body;
    public float Speed;

    //Ts instantiate Singleton
    public static PlayerController Instance { get; private set; }

    private void Fall()
    {

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
        var IsIdle = (vector.x, vector.z) == (0, 0);
        // If there's no input,
        if (IsIdle)
        {
            // Character idles.
            Idle();
        }
        // If there's input,
        else
        {
            // Character move.
            Run();
            Body.Move(vector * Speed * Time.deltaTime);
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
        Body = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Locomotion();
    }
}
