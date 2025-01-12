using Game;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AZone _triggerZone;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;

    public float Speed;
    public float HorizontalMove;
    public Transform graundCheck;
    public float jumpForce;
    private bool _onGround;
    public float Radius;
    public LayerMask Graund;

    public Rigidbody2D rb;
    private Lever _lever;
    public HealthBarPlayers healthBar;

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
        if (lever) 
        {
            _lever = lever;
        }
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
        _onGround = Physics2D.OverlapCircle(graundCheck.position, Radius, Graund);

        if (_onGround)
        {
            _animator.SetBool("JUMP", false);
            
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (_onGround) rb.velocity = Vector2.up * jumpForce;
            _animator.SetBool("JUMP", true);
            
        }

        //if (rb.velocity.y < 0)
        //{
        //    _animator.SetBool("JUMP", true);

        //    _onGround = false;
        //}


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
        if (Input.GetKeyDown(KeyCode.U)) {
            _animator.SetBool("ATTACK", true);
            healthBar.damage();

        }
        if (Input.GetKeyUp(KeyCode.U)){
            _animator.SetBool("ATTACK", false);

        }

        if (Input.GetButtonDown("e"))
        {
            _lever?.SpawnBlock();
            healthBar.hill();
        }
    }
}
