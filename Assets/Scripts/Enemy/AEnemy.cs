using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(EnemyHPManager))]
[RequireComponent(typeof(Animator))]


public abstract class AEnemy : MonoBehaviour
{
    private IStateEnemy stateEnemy;
    private AttackStateEnemy attackStateEnemy;
    private DeathStateEnemy deathStateEnemy;
    private IdleStateEnemy idleStateEnemy;
    private RunStateEnemy runStateEnemy;
    private TakeDamageStateEnemy takeDamageStateEnemy;


    public CircleCollider2D _collider2D;
    private Animator _animator;
    public EnemyHPManager enemyHPManager;
    public DropItem dropItem;
    private ABoidMovement movement;
    private Vector3 movementEnemy;

    float time = 0;



    public Transform target;
    public EnemyStats enemyStats;
    public float moveSpeed;

    public virtual void Awake()
    {
        attackStateEnemy = new AttackStateEnemy();
        deathStateEnemy = new DeathStateEnemy();
        idleStateEnemy = new IdleStateEnemy();
        runStateEnemy = new RunStateEnemy();
        takeDamageStateEnemy = new TakeDamageStateEnemy();

        _collider2D = GetComponent<CircleCollider2D>();
        _animator = GetComponent<Animator>();
        enemyHPManager = GetComponent<EnemyHPManager>();
        dropItem = GetComponent<DropItem>();

        movement = GetComponent<BoidNormal>();
        enemyStats= GetComponent<EnemyStats>();



    }
    void Start()
    {
        BoidManager.Instance.ResisterMembers(movement);
        ChangeToIdleState();


        enemyStats.CaculateStats();
    }
    public virtual void Update()
    {
        stateEnemy.Update(this);
        // Debug.Log(stateEnemy);
    }
    public virtual void FixedUpdate()
    {
        stateEnemy.FixedUpdate(this);
    }

    public virtual void DropItem()
    {

    }
    public void GetDamage(float damage)
    {
        GameObject damagebox = PoolManager.Instance.GetFromPool(EPool.Damage, transform.position, Quaternion.identity, setpos: true);
        TextMeshPro text = damagebox.GetComponentInChildren<TextMeshPro>();
        text.text = damage.ToString();

        

        StartCoroutine(PopupDamageBox(damagebox));
    }

    public void DealDame()
    {
        PlayerHpManger playerHpManger = target.gameObject.GetComponent<PlayerHpManger>();
        playerHpManger.TakeDMG(enemyStats.CalculateDamage());
        
    }
    public virtual bool IsTargetInAttackThreshold()

    {
        
        if ((transform.position - target.position).magnitude <= enemyStats.GetAttack_Thredhold()) return true;
        return false;
    }
    public virtual void ChaseTarget()
    {
        movementEnemy = (target.position - transform.position).normalized;

        transform.position += movementEnemy * moveSpeed * Time.deltaTime;

        if (movementEnemy != Vector3.zero)
            {
                
                transform.rotation = Quaternion.Euler(0, movementEnemy.x >=0 ? 180 : 0 , 0);
            }
    }

    public virtual void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void AddMoveBehaviour(ABoidMovement aBoidMovement)
    {
        movement = aBoidMovement;
        BoidManager.Instance.ResisterMembers(movement);
    }



    // AttackState
    public virtual void ChangeToAttackState()
    {
        // Debug.Log("Change to Attack");
        stateEnemy = attackStateEnemy;
        stateEnemy.Enter(this);   
    }
    public virtual void EnterAttackState()
    {
        _animator.SetTrigger(EState.Attack.ToString());
    }
    public virtual void ExitAttackState()
    {


    }
    public virtual void UpdateAttackState()
    {
        if (!target)
        {
            ChangeToIdleState();
            return;
            
        }
        else
        {
            if (!IsTargetInAttackThreshold())
            {
                ChangeToRunState();
                ChaseTarget();
                return;
            }

        }
    }
    public virtual void FixedUpdateAttackState() { }



    // DeathState
    public virtual void ChangeToDeathState()
    {
        stateEnemy = deathStateEnemy;
        stateEnemy.Enter(this);   
    }
    public virtual void EnterDeathState()
    {
        _animator.SetTrigger(EState.Death.ToString());
        StartCoroutine(DeathState());
    }
    public virtual void ExitDeathState() { }
    public virtual void UpdateDeathState() { }
    public virtual void FixedUpdateDeathState() { }

    // IdleState
    public virtual void ChangeToIdleState()
    {
        stateEnemy = idleStateEnemy;
        stateEnemy.Enter(this);   
    }
    public virtual void EnterIdleState()
    { 
        _animator.SetTrigger(EState.Idle.ToString());
    }
    public virtual void ExitIdleState() { }
    public virtual void UpdateIdleState()
    {

        if (target)
        {
            if (IsTargetInAttackThreshold())
            {
                ChangeToAttackState();
                return;
            }
            else
            {
                ChangeToRunState();
                ChaseTarget();
                return;
            }
        }

    }
    public virtual void FixedUpdateIdleState()
    {

    }


    // RunState
    public virtual void ChangeToRunState()
    {
        // Debug.Log("ChangeToRun");
        stateEnemy = runStateEnemy;
        stateEnemy.Enter(this);   
    }
    public virtual void EnterRunState()
    {

        _animator.SetTrigger(EState.Run.ToString());

    }
    public virtual void ExitRunState()
    {

    }
    public virtual void UpdateRunState()
    {
        if (target)
        {
            if (IsTargetInAttackThreshold())
            {
                ChangeToAttackState();
                
            }
            else
            {
                ChaseTarget();
                
            }
            
        }
        else
        {
            ChangeToIdleState();
        }
    }
    public virtual void FixedUpdateRunState()
    {

    }

    // TakedamageState
    public virtual void ChangeToTakedamageState()
    {

    }
    public virtual void EnterTakedamageState() { }
    public virtual void ExitTakedamageState() { }
    public virtual void UpdateTakedamageState() { }
    public virtual void FixedUpdateTakedamageState() { }


    public IEnumerator DeathState()
    {

        yield return new WaitForSeconds(2f);
        BoidManager.Instance.UnResisterMembers(movement);
        dropItem.DropAt(transform.position);
        Destroy(gameObject);
    }
    public IEnumerator PopupDamageBox(GameObject dmgBox)
    {
        dmgBox.transform.localScale = dmgBox.transform.localScale * 0.1f;
        dmgBox.transform.position += new Vector3(0, 0.2f, 0);

        while (dmgBox.transform.localScale.x < 1)
        {
            dmgBox.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            dmgBox.transform.position += new Vector3(0, 0.01f, 0);
            yield return new WaitForSeconds(0.035f);

        }

        yield return new WaitForSeconds(0.2f);

        PoolManager.Instance.AddToPool(dmgBox, EPool.Damage);



    }
}
