using Game;
using UnityEngine;

public class PlayerSecond : MonoBehaviour
{
    [SerializeField] private AZone _triggerZone;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;

    public float Speed;
    public float HorizontalMove;

    [SerializeField] private Transform _graundCheck;
    public float jumpForce;
    private bool _onGround;
    public float jumpRadius;
    [SerializeField] private LayerMask _graund;

    public Rigidbody2D rb;
    private Lever _lever;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
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

        if (_onGround) _animator.SetBool("JUMP", false);

        if (Input.GetButtonDown("up"))
        {
            if (_onGround) rb.velocity = Vector2.up * jumpForce;
            _animator.SetBool("JUMP", true);
        }

        //if (rb.velocity.y < -0.05)
        //{
        //    _animator.SetBool("JUMP", true);

        //    _onGround = false;
        //}


        HorizontalMove = Input.GetAxis("Vertical");
        transform.position += transform.right *HorizontalMove * Speed * Time.deltaTime;
        
        if (HorizontalMove > 0){
            _animator.SetBool("RUN", true) ;
            _sprite.flipX = false;
        }
        if (HorizontalMove < 0){
            _animator.SetBool("RUN", true);
            _sprite.flipX = true;
        }
        if (HorizontalMove == 0)
        {
            _animator.SetBool("RUN", false);
        }


        if (Input.GetButtonDown("ctrl"))
        {

            _lever?.SpawnBlock();
   
        }
    }
}
