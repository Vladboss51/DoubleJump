using UnityEngine;

public class MovingBlock : Block
{
    [SerializeField] private Rigidbody2D _rb;


    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(0f,0f); 
    }
}
