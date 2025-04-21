using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InputUIModule", menuName = "AddOn/InputUI", order = 1)]
public class InputUIModule : ScriptableObject
{
    private InputController input;

    private void OnEnable()
    {
        input = new();
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}