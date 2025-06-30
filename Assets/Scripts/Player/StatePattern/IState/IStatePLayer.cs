using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatePlayer
{
    public void Enter(PlayerController player);
    public void Exit(PlayerController player);
    public void Update(PlayerController player);
    public void  FixedUpdate(PlayerController player);
}
