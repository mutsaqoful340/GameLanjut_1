using System;
using UnityEngine;

[CreateAssetMenu(fileName = "InputPlayModule", menuName = "AddOn/Gameplay", order = 0)]
public class InputPlayModule : ScriptableObject
{
    public Action<ActionState> OnAction { get; set; }

    public Vector3 MoveHandler
    {
        get
        {
            var axisX = input.GamePlay.Movement.ReadValue<Vector2>().x;
            var axisZ = input.GamePlay.Movement.ReadValue<Vector2>().y;
            return new Vector3(axisX, 0, axisZ);
        }
    }

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