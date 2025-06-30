using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(PlayerStats))]

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerHpManger))]
[RequireComponent(typeof(CircleCollider2D))]

public class PlayerController : MonoBehaviour
{


    private PlayerInputManager _playerInputManager;
    private PlayerStats _playerStats;


    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    public PlayerHpManger _playerHpManger;
    public CircleCollider2D _playerCollider;


    private IStatePlayer statePlayer;
    private AttackStatePlayer attackStatePlayer;
    private DashStatePlayer dashStatePlayer;
    private DeathStatePlayer deathStatePlayer;
    private IdleStatePlayer idleStatePlayer;
    private RunStatePlayer runStatePlayer;
    private TakeDamageStatePlayer takeDamageStatePlayer;


    //StatPlayer
    [SerializeField]

    private float _speed;

    private bool _isAttack;

    private Vector2 _movement;
    float moveThreshold = 0.33f;
    private Vector2 lastCheckedPos;


    [Header("Effect")]
    public Collider2D[] colliderAttacks;
    private ContactFilter2D filterCollider = new ContactFilter2D();

    public GameObject[] effectsAttack;

    [Header("InventoryManager")] public InventoryManager inventoryManager;


    ///Find object around player
    private List<ABoidMovement> boidsArroudPlayer = new List<ABoidMovement>();
    private List<Item> itemsAroundPlayer = new List<Item>();


    void Awake()
    {


        attackStatePlayer = new AttackStatePlayer();
        dashStatePlayer = new DashStatePlayer();
        deathStatePlayer = new DeathStatePlayer();
        idleStatePlayer = new IdleStatePlayer();
        runStatePlayer = new RunStatePlayer();
        takeDamageStatePlayer = new TakeDamageStatePlayer();

        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerHpManger = GetComponent<PlayerHpManger>();
        _playerCollider = GetComponent<CircleCollider2D>();
        filterCollider = new ContactFilter2D();
        filterCollider.useTriggers = true;


        _playerInputManager = new PlayerInputManager();
        _playerStats = GetComponent<PlayerStats>();

        _movement = Vector2.zero;
        _speed = 2f;


    }

    void Start()
    {
        RegisterAction();
        ChangeToIdleState();

        _playerStats.CaculateStats();
    }

    void Update()
    {
        _playerInputManager.Update();
        statePlayer.Update(this);

        foreach (var boid in boidsArroudPlayer)
        {
            AEnemy aEnemy = boid.GetComponent<AEnemy>();
            if (aEnemy != null && (aEnemy.target == null || !aEnemy.target ))
            {
                aEnemy.SetTarget(transform);
            }
        }



    }
    void FixedUpdate()
    {

        statePlayer.FixedUpdate(this);

    }

    public void GetDamage(float damage)
    {
        GameObject damagebox = PoolManager.Instance.GetFromPool(EPool.Damage, transform.position, Quaternion.identity, setpos: true);
        TextMeshPro text = damagebox.GetComponentInChildren<TextMeshPro>();
        text.text = damage.ToString();

        StartCoroutine(PopupDamageBox(damagebox));
    }


    // AttackState
    public void ChangeToAttackState()
    {
        statePlayer = attackStatePlayer;
        statePlayer.Enter(this);
    }
    public void EnterAttackState()
    {
        _animator.SetBool("isAttack", true);
    }
    public void ExitAttackState()
    {
        _animator.SetBool("isAttack", false);
        if (_animator.GetBool("isRun"))
        {
            ChangeToRunState();

        }
        else
        {
            ChangeToIdleState();
        }

    }
    public void UpdateAttackState()
    {
        if (!_isAttack) ExitAttackState();




    }

    public void NormalAttackDealDamage(int type)
    {
        foreach (var boid in boidsArroudPlayer)
        {
            if (colliderAttacks[type].IsTouching(boid.transform.gameObject.GetComponent<AEnemy>()._collider2D, filterCollider))
            {

                EnemyHPManager enemyHPManager = boid.transform.gameObject.GetComponent<EnemyHPManager>();
                enemyHPManager.TakeDMG(_playerStats.CalculateDamageToEnemy());


                // AEnemy enemy = boid.transform.gameObject.GetComponent<AEnemy>();
                // enemy.GetDamage(_playerStats.CalculateDamageToEnemy());

            }
        }
        Debug.Log(boidsArroudPlayer.Count);
    }


    public void FixedUpdateAttackState() { }

    // DashState
    public void ChangeToDashState()
    {
        statePlayer = dashStatePlayer;
        statePlayer.Enter(this);
    }
    public void EnterDashState() { }
    public void ExitDashState() { }
    public void UpdateDashState() { }
    public void FixedUpdateDashState() { }

    // DeathState
    public void ChangeToDeathState()
    {
        statePlayer = deathStatePlayer;
        statePlayer.Enter(this);
    }
    public void EnterDeathState() { }
    public void ExitDeathState() { }
    public void UpdateDeathState() { }
    public void FixedUpdateDeathState() { }

    // IdleState
    public void ChangeToIdleState()
    {
        statePlayer = idleStatePlayer;
        statePlayer.Enter(this);
    }
    public void EnterIdleState() { }
    public void ExitIdleState() { }
    public void UpdateIdleState()
    {

        if (_isAttack)
        {
            ChangeToAttackState();
        }
        else if (!_isAttack && _movement.magnitude != 0)
        {
            ChangeToRunState();
        }



    }
    public void FixedUpdateIdleState()
    {

    }


    // RunState
    public void ChangeToRunState()
    {
        statePlayer = runStatePlayer;
        statePlayer.Enter(this);
    }
    public void EnterRunState()
    {
        _animator.SetBool("isRun", true);

    }
    public void ExitRunState()
    {
        _animator.SetBool("isRun", false);
        _rigidbody2D.velocity = Vector2.zero;
    }
    public void UpdateRunState()
    {
        if (_movement.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (_movement.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (_movement.magnitude == 0 && !_isAttack)
        {
            _rigidbody2D.velocity = _movement.normalized * _speed;
            ExitRunState();
            ChangeToIdleState();
        }
        else if (_isAttack)
        {
            ExitRunState();
            ChangeToAttackState();
        }
    }
    public void FixedUpdateRunState()
    {

        _rigidbody2D.velocity = _movement.normalized * _speed;

        if (Vector2.Distance(transform.position, lastCheckedPos) > moveThreshold)
        {
            boidsArroudPlayer = BoidManager.Instance.GetEnemiesNerest(transform.position);


            // List<AEnemy> aEnemies = GameManager.Instance.GetEnemiesNerest(transform.position, 1);
            // foreach (AEnemy enemy in aEnemies)
            // {
            //     Debug.DrawLine(transform.position, enemy.transform.position, Color.red);
            // }
            lastCheckedPos = transform.position;
        }


    }

    // TakedamageState
    public void ChangeToTakedamageState()
    {
        statePlayer = takeDamageStatePlayer;
        statePlayer.Enter(this);
    }
    public void EnterTakedamageState() { }
    public void ExitTakedamageState() { }
    public void UpdateTakedamageState() { }
    public void FixedUpdateTakedamageState() { }


    public void RegisterAction()
    {
        _playerInputManager._actions.Add(GetKeyDown_Down, () => { _movement += Vector2.down; });
        _playerInputManager._actions.Add(GetKeyUp_Down, () => { _movement -= Vector2.down; });

        _playerInputManager._actions.Add(GetKeyDown_Up, () => { _movement += Vector2.up; });
        _playerInputManager._actions.Add(GetKeyUp_Up, () => { _movement -= Vector2.up; });

        _playerInputManager._actions.Add(GetKeyDown_Right, () => { _movement += Vector2.right; });
        _playerInputManager._actions.Add(GetKeyUp_Right, () => { _movement -= Vector2.right; });

        _playerInputManager._actions.Add(GetKeyDown_Left, () => { _movement += Vector2.left; });
        _playerInputManager._actions.Add(GetKeyUp_Left, () => { _movement -= Vector2.left; });

        _playerInputManager._actions.Add(GetKeyDown_Attack, () => { _isAttack = true; });
        _playerInputManager._actions.Add(GetKeyUp_Attack, () => { _isAttack = false; });


        _playerInputManager._actions.Add(GetKeyDown_Pickup, () =>
        {
            try
            {
                Item item = HUD.Instance.PickupManager_Pickup();
                inventoryManager.AddItem(item._Item);
                Destroy(item.gameObject);
                Debug.Log("pickup");
            }
            catch
            {

            }
        });
    }

    public bool GetKeyDown_Attack()
    {
        return Input.GetKeyDown(KeyCode.K);
    }
    public bool GetKeyUp_Attack()
    {
        return Input.GetKeyUp(KeyCode.K);
    }
    public bool GetKeyDown_Pickup()
    {
        return Input.GetKeyDown(KeyCode.F);
    }

    public bool GetKeyDown_Up()
    {
        return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
    }
    public bool GetKeyDown_Down()
    {
        return Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);
    }
    public bool GetKeyDown_Right()
    {
        return Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);
    }
    public bool GetKeyDown_Left()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
    }

    public bool GetKeyUp_Up()
    {
        return Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W);
    }
    public bool GetKeyUp_Down()
    {
        return Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S);
    }
    public bool GetKeyUp_Right()
    {
        return Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D);
    }
    public bool GetKeyUp_Left()
    {
        return Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A);
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
