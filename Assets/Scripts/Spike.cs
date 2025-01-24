using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spike : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    public float speedSpike = 100f;
    private Vector3 _oldPosition;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        //Vector3 _oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _oldPosition = transform.position;
        Debug.Log(transform.position);
        _rb.velocity = Vector2.left * speedSpike * Time.deltaTime;

    }

    private void LateUpdate()
    {
        if (transform.position == _oldPosition)
        {
            Destroy(gameObject);
            Debug.Log("---" + transform.position);
        }
        
    }
}
