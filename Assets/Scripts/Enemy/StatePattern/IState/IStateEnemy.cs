using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateEnemy
{
    public void Enter(AEnemy enemy);
    public void Exit(AEnemy enemy);
    public void Update(AEnemy enemy);
    public void  FixedUpdate(AEnemy enemy);
}
