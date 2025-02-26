using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(0f,0f); 
    }
}
