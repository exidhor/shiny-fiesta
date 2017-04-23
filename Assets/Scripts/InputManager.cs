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

    private int _maxFrameLife  = 5;

    private int _currentFrameLife;

    void FixedUpdate()
    {
        _currentFrameLife++;

        if (_currentFrameLife > _maxFrameLife)
        {
            _action = false;
            _currentFrameLife = 0;
        }

        if (!_action)
            _action = Input.GetButtonDown("Action");
    }
}
