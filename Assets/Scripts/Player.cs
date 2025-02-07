using Game;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private AZone _triggerZone;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private HealthBarPlayers _healthBar;
    [SerializeField] private ShieldSystem _shieldSystem;

    public float Speed;
    public float HorizontalMove;
    private bool _facingRight = true;

    [SerializeField] private Transform _graundCheck;
    public float jumpForce;
    private bool _onGround;
    public float jumpRadius;
    [SerializeField] private LayerMask _graund;

    [SerializeField] private Transform _attackZone;
    public float attackRadius;
    public float attackRate = 0.5f;
    private float nextAttackTime = 0f;
    [SerializeField] private LayerMask _enemy;

    public Rigidbody2D rb;
    private Lever _lever;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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
        if (HillBlock) {_healthBar.GetHill(); Destroy(arg0.gameObject);}
        
    }

    private void BindOnExit(Collider2D arg0)
    {
        Door Door = arg0.gameObject.GetComponent<Door>();
        if (Door)  Door.Close();
        Lever lever = arg0.gameObject.GetComponent<Lever>();
        if (lever) _lever = null;
    }

    private void Update()
    {
        _onGround = Physics2D.OverlapCircle(_graundCheck.position, jumpRadius, _graund);
        if (!_onGround)
        {
            _animator.SetBool("FALL", true);
        }
        if (_onGround)
        {
            _animator.SetBool("JUMP", false);
            _animator.SetBool("FALL", false);
            
        }

        if (Input.GetKey(KeyCode.W))
        {
            _animator.SetBool("JUMP", true);
            
        }
        if (Input.GetKeyUp(KeyCode.Space)){
            _animator.SetBool("ATTACK", false);

        }


        HorizontalMove = Input.GetAxis("Horizontal");
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _animator.SetBool("ATTACK", true);

                Collider2D[] Enemies = Physics2D.OverlapCircleAll(_attackZone.position, attackRadius, _enemy);
                foreach (Collider2D enemy in Enemies)
                {
                    enemy.GetComponent<EnemyHealth>().TakeDamage();

                }

                nextAttackTime = Time.time + attackRate;
            }
        }

//ShieldZone
        if (Input.GetKeyDown(KeyCode.LeftShift))
            _shieldSystem.ActivationShield();
//Lever
        if (Input.GetButtonDown("e"))
        {
            _lever?.SpawnBlock();
        }
    }

    private void Jumping()
    {
        if (_onGround) rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_attackZone.position, attackRadius);
        Gizmos.DrawWireSphere(_graundCheck.position, jumpRadius);
    }

}
