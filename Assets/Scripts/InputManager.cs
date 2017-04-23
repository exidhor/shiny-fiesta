using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class InputManager : MonoSingleton<InputManager>
{
    public bool Action
    {
        get
        {
            bool value = _action;
            _action = false;

            return value;
        }
    }

    private bool _action;

    void FixedUpdate()
    {
        if(!_action)
            _action = Input.GetButtonDown("Action");
    }
}
