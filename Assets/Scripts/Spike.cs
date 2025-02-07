using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    public float speedSpike = 250f;
    private float _moveDirection = -1;


    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _rb.velocity = transform.right * speedSpike * _moveDirection * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayersController>().Damage();
            Destroy(gameObject);
        }

        if (collision.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
