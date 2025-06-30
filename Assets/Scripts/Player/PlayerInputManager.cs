using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerInputManager
{
    public Dictionary<Func<bool>, Action> _actions = new Dictionary<Func<bool>, Action>();

    public void Update()
    {
        // Duyệt qua tất cả các điều kiện và hành động
        foreach (var pair in _actions)
        {
            // Nếu điều kiện (Func<bool>) trả về true, thì thực thi hành động (Action)
            if (pair.Key.Invoke())
            {
                pair.Value.Invoke();
            }
        }
    }

    public void RegisterAction(Func<bool> condition, Action action)
    {
        if (!_actions.ContainsKey(condition))
        {
            _actions.Add(condition, action);
        }
    }
    public void UnregisterAction(Func<bool> condition)
    {
        if (_actions.ContainsKey(condition))
        {
            _actions.Remove(condition);
        }
    }
    
}


