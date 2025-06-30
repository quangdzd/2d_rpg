using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public abstract class AHpManager : MonoBehaviour
{
    [SerializeField] protected float MaxHp;

    [SerializeField] protected float currentHp;

    public GameObject healthBar = null;
    protected Coroutine healthBarCoroutine;



    protected virtual void Awake()
    {
        MaxHp = Mathf.Max(1, MaxHp);
        currentHp = MaxHp;

    }
    public virtual void TakeDMG(float dmg)
    {
        
        
    }
    public virtual void TakeDMG(float dmg, Vector2 sourceDMG)
    {
        if (currentHp == 0)
        {
            return;
        }

        currentHp = Mathf.Max(0, currentHp - dmg);
    }

    public void SetMaxHp(float _value)
    {
        MaxHp = Mathf.Max(_value, 1);
        currentHp = MaxHp;
    }


    public bool IsFullHp()
    {
        return (currentHp == MaxHp) ? true : false;
    }

    public void FlatRestoreHP(float _value)
    {

    }
    public void PercentRestoreHP(float _value)
    {
        currentHp = Mathf.Min(currentHp += MaxHp * _value / 100, MaxHp);
        RefreshHealthBar();
            }
    public void FlatRestoreMP(float _value)
    {

    }
    public void PercentRestoreMP(float _value)
    {

    }

    public void RefreshHealthBar()
    {
        Slider slider = healthBar.GetComponent<Slider>();
        slider.value = currentHp / MaxHp;
    }

    public void GetHealthBar()
    {
        if (healthBar == null)
        {
            healthBar = PoolManager.Instance.GetFromPool(EPool.HealthBar, transform.position, Quaternion.identity);
            healthBar.transform.SetParent(UIManager.Instance.healthBarParent, false);


            var follow = healthBar.GetComponent<HealthBarFollow>();
            if (!follow)
            {
                follow = healthBar.AddComponent<HealthBarFollow>();
            }
            follow.target = transform;
            follow.offset = UIManager.Instance.healthBarOffset;

            Slider slider = healthBar.GetComponent<Slider>();
            slider.value = currentHp / MaxHp;

        }
    }

    public IEnumerator HealthBarChange()
    {
        float oldHP = currentHp;

        Slider slider = healthBar.GetComponent<Slider>();
        slider.value = currentHp / MaxHp;

        yield return new WaitForSeconds(2f);

        float newHP = currentHp;

        if (newHP == oldHP)
        {
            if (healthBar != null)
            {
                PoolManager.Instance.AddToPool(healthBar, EPool.HealthBar);
                healthBar = null;
            }
        }
    }

}