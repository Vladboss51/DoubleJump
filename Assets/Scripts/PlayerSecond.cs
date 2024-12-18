using Game;
using UnityEngine;

public class PlayerSecond : MonoBehaviour
{
    [SerializeField] private AZone _triggerZone;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;

    public float Speed;
    public float RotationSpeed;
    public bool Comebacking = false;
    private bool _onGround = true;

    public float HorizontalMove;
    public float VerticalMove;

    public Rigidbody2D rb;
    private Vector2 _jumpForce = new Vector2(0, 800);
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Block block = collision.gameObject.GetComponent<Block>();
        if (block)
        {
            if (block.transform.position.y <= transform.position.y)
            {
                _animator.SetBool("JUMP", false);

                _onGround = true;
            }
        }
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
        if (rb.velocity.y < -0.05)
        {
            _animator.SetBool("JUMP", true);

            _onGround = false;
        }
        if (Input.GetButtonDown("up"))
        {
            if (_onGround) rb.AddForce(_jumpForce);
            _animator.SetBool("JUMP", true);
            _onGround = false;
        }
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
