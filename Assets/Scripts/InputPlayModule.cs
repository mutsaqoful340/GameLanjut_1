using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InputPlayModule", menuName = "AddOn/Gameplay", order = 0)]
public class InputPlayModule : ScriptableObject
{
    public bool JumpInput => Input.GetButtonDown("Jump"); // Replace with your actual input check logic
    public Vector3 MoveHandler => new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    public Action<ActionState> OnAction { get; set; }


    //public bool RightHoldHandler => input.GamePlay.CamRight.ReadValue<float>() > 0;
    //public bool LeftHoldHandler => input.GamePlay.CamLeft.ReadValue<float>() > 0;
    public bool CastHoldHandler => input.GamePlay.Skill.ReadValue<float>() > 0;

    private InputController input;

    private void ActionAwake()
    {

        input.GamePlay.Interaction.performed += (e) => {
            OnAction?.Invoke(ActionState.Interaction);
        };

        input.GamePlay.Cancel.performed += (e) => {
            OnAction?.Invoke(ActionState.Cancel);
        };

        input.GamePlay.Attack.performed += (e) => {
            OnAction?.Invoke(ActionState.Attack);
        };

        input.GamePlay.Skill.canceled += (e) => {
            OnAction?.Invoke(ActionState.Skill);
        };
    }

    private void OnEnable()
    {
        input = new();
        input.Enable();
        ActionAwake();
        //InputPocketCallback?.Invoke();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}

public enum ActionState
{
    Movement, Interaction, Cancel, Attack, Skill,
}