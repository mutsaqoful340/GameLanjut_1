using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Animations;
using UnityEditor.Experimental.GraphView;
using UnityEngine.InputSystem.LowLevel;

public class EnemyActive : MonoBehaviour
{
    public enum EnemyState
    {
        Idle, Busy, Moving, Attack, Patrol, Death
    }

    public EnemyState State;
    public CharacterController enemyBody;
    public Animator enemyAnimator;

    public Transform enemyTargetting;

    [Header("Enemy Data")]
    public float MaxHealth;
    public float CurrHealth;
    public float Speed;
    public float AttackSpeed;
    public float DeathSpeed;
    public float Distance;

    public float AttackDelay;
    public float DeathDelay;

    private void Start()
    {
        enemyBody = GetComponent<CharacterController>();
        enemyAnimator = GetComponent<Animator>();
        enemyTargetting = GameObject.FindWithTag("Player").transform; 
        //enemyTargetting = PlayerController.Instance.transform; // Find player with singleton

    }

    private void Update()
    {
        if (AttackDelay > 0)
        {
            AttackDelay -= Time.deltaTime;
        }

        if (AttackDelay < 0)
        {
            AttackDelay = 0;
        }

        Distance = Vector3.Distance(enemyTargetting.position, transform.position);
        if (Distance < 15f)
        {
            
            var lookPos = (enemyTargetting.position - transform.position);
            lookPos.y = 0;

            var rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookPos), Time.deltaTime * 5f);
            transform.rotation = rotation;

            var moveDirection = transform.TransformDirection(Vector3.forward);
            if (Distance < 1.5f)
            {
                enemyAnimator.SetFloat("Move", 0);
                if (AttackDelay == 0)
                {
                    enemyAnimator.SetTrigger("Attack");
                    State = EnemyState.Attack;
                    AttackDelay = AttackSpeed;
                }


            }
            else
            {
                enemyBody.Move(moveDirection * Time.deltaTime * Speed);
                enemyAnimator.SetFloat("Move", 1f);
                State = EnemyState.Moving;
            }
        }
        else
        {
            enemyAnimator.SetFloat("Move", 0);
            State = EnemyState.Idle; 
        }

        // Fungsi Death

    }
}
