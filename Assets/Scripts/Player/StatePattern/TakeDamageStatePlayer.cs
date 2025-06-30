using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageStatePlayer : IStatePlayer
{
    public void Enter(PlayerController player)
    {
        player.EnterTakedamageState();
    }

    public void Exit(PlayerController player)
    {
        player.ExitTakedamageState();
    }

    public void FixedUpdate(PlayerController player)
    {
        player.FixedUpdateTakedamageState();
    }

    public void Update(PlayerController player)
    {
        player.UpdateTakedamageState();
    }
}
