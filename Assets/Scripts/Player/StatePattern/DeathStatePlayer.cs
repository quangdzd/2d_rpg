using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathStatePlayer : IStatePlayer
{
    public void Enter(PlayerController player)
    {
        player.EnterDeathState();
    }

    public void Exit(PlayerController player)
    {
        player.ExitDeathState();
    }

    public void FixedUpdate(PlayerController player)
    {
        player.FixedUpdateDeathState();
    }

    public void Update(PlayerController player)
    {
        player.UpdateDeathState();
    }
}
