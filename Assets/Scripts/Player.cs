using Game;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AZone _triggerZone;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private HealthBarPlayers _healthBar;

    public float Speed;
    public float HorizontalMove;

    [SerializeField] private Transform _graundCheck;
    public float jumpForce;
    private bool _onGround;
    public float jumpRadius;
    [SerializeField] private LayerMask _graund;

    [SerializeField] private Transform _attackZone;
    public float attackRadius;
    private float x;
    private float y;
    private float z;
    private bool right = true;
    private bool left = false;
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
        if (HillBlock) HillBlock.hill();
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


        HorizontalMove = Input.GetAxis("Horizontal");
        transform.position += transform.right * HorizontalMove * Speed * Time.deltaTime;
        
        if (HorizontalMove > 0){
            _animator.SetBool("RUN", true) ;
            _sprite.flipX = false;
        }
        else if (HorizontalMove < 0){
            _animator.SetBool("RUN", true);
            _sprite.flipX = true;
        }
        else
        {
            _animator.SetBool("RUN", false);
        }

        //attack
        if (Input.GetKeyDown(KeyCode.V)) {
            //x = _attackZone.localPosition.x;
            //y = _attackZone.localPosition.y;
            //z = _attackZone.localPosition.z;
            _animator.SetBool("ATTACK", true);
            if (_sprite.flipX == true)
            {
                if (left == false) x -= 1.7f;
                left = true;
                right = false;
                _attackZone.localPosition = new Vector3(0, 0, 0);
            }
            else if (_sprite.flipX == false)
            {
                if (right == false) x += 1.7f;
                right = true;
                left = false;
                _attackZone.localPosition = new Vector3(0, 0, 0);
            }

            Collider2D[] Enemies = Physics2D.OverlapCircleAll(_attackZone.localPosition, attackRadius, _enemy);
            foreach (Collider2D enemy in Enemies)
            {
                _healthBar.damage();

            }
        }
        if (Input.GetKeyUp(KeyCode.V)){
            _animator.SetBool("ATTACK", false);

        }

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
        Gizmos.DrawWireSphere(_attackZone.localPosition, attackRadius);
    }
}
