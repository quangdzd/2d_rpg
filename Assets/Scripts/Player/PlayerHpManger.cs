using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpManger : AHpManager
{

    public Slider healthBarSlider;
    public void Start()
    {
        GetHealthBar();
        healthBarSlider = healthBar.GetComponent<Slider>();
    }
    public override void TakeDMG(float dmg)
    {

        PlayerController PlayerController = GetComponent<PlayerController>();
        if (currentHp <= 0)
        {
            // PlayerController.ChangeToDeathState();
        }

        currentHp = Mathf.Max(0, currentHp - dmg);
        PlayerController.GetDamage(dmg);

        healthBarSlider.value = currentHp / MaxHp;
        

    }
}
