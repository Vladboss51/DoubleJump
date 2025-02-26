using Game;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private AZone _triggerZone;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private ShieldSystem _shieldSystem;
    [SerializeField] private HealthBarPlayers _healthPlayers;
    [SerializeField] private GameObject _shieldZone;
    public float distance = 0.6f;

    public float Speed;
    public float HorizontalMove;
    private bool _facingRight = true;

    [SerializeField] private Transform _graundCheck;
    public float jumpForce;
    private bool _onGround;
    public float jumpRadius;
    [SerializeField] private LayerMask _graund;

//    [SerializeField] protected Transform _attackZone;
//    public float attackRadius;
    public float attackRate = 0.5f;
    private float nextAttackTime = 0f;
//    [SerializeField] protected LayerMask _enemy;

    private Lever _lever;

    protected string _moveButton;
    protected string _jumpButton;
    protected string _interactionButton;
    protected string _attackButton;



    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        
    }

    private void OnEnable()
    {
        _triggerZone.OnEnter.AddListener(BindOnEnter);
        _triggerZone.OnExit.AddListener(BindOnExit);

    }

    private void OnDisable()
    {
        _triggerZone.OnEnter.RemoveListener(BindOnEnter);
        _triggerZone.OnExit.RemoveListener(BindOnExit);
    }

    private void BindOnEnter(Collider2D arg0)
    {
        Door Door = arg0.gameObject.GetComponent<Door>();
        if (Door) Door.Open();
        Lever lever = arg0.gameObject.GetComponent<Lever>();
        if (lever) _lever = lever;
        HillBlock HillBlock = arg0.gameObject.GetComponent<HillBlock>();
        if (HillBlock) { _healthPlayers.GetHill(); Destroy(arg0.gameObject); }
        Spike spike = arg0.gameObject.GetComponent<Spike>();
        if (spike) { GetHit(); Destroy(arg0.gameObject); }

    }

    private void BindOnExit(Collider2D arg0)
    {
        Door Door = arg0.gameObject.GetComponent<Door>();
        if (Door) Door.Close();
        Lever lever = arg0.gameObject.GetComponent<Lever>();
        if (lever) _lever = null;
    }

    private void Update()
    {
//jump
        _onGround = Physics2D.OverlapCircle(_graundCheck.position, jumpRadius, _graund);
        if (!_onGround)
        {
            _animator.SetBool("FALL", true);
        }
        if (_onGround)
        {
            _animator.SetBool("FALL", false);

        }

        if (Input.GetButtonDown(_jumpButton))
        {
            _animator.SetTrigger("JUMP");

        }

//move
        HorizontalMove = Input.GetAxis(_moveButton);
        transform.position += transform.right * HorizontalMove * Speed * Time.deltaTime;

        if (HorizontalMove != 0)
        {
            _animator.SetBool("RUN", true);
            if (HorizontalMove > 0 && !_facingRight)
            {
                transform.Rotate(0f, 180f, 0f);
                Speed *= -1;
                _facingRight = !_facingRight;
            }
            else if (HorizontalMove < 0 && _facingRight)
            {
                transform.Rotate(0f, 180f, 0f);
                Speed *= -1;
                _facingRight = !_facingRight;
            }
        }
        else
        {
            _animator.SetBool("RUN", false);
        }

//attack
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown(_attackButton))
            {
                _animator.SetTrigger("ATTACK");
                nextAttackTime = Time.time + attackRate;
            }
        }

//ShieldZone
        if (Input.GetKeyDown(KeyCode.LeftShift))
            _shieldSystem.ActivationShield();
//Lever
        if (Input.GetButtonDown(_interactionButton))
        {
            _lever?.SpawnBlock();
        }
    }

    protected virtual void Attack()
    {
        
    }

    private void Jumping()
    {
        if (_onGround) _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere(_attackZone.position, attackRadius);
        Gizmos.DrawWireSphere(_graundCheck.position, jumpRadius);
    }


    private void GetHit()
    {
        float _distance = Vector2.Distance(transform.position, _shieldZone.transform.position);
        if (_distance < distance && _shieldZone.activeSelf)
        {
            _shieldSystem.DeactivationShield();
        }

        else
            _healthPlayers.GetDamage();
    }

}
