using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShieldSystem : MonoBehaviour
{
    [SerializeField] private GameObject _shieldZone;
    [SerializeField] private RectTransform _transform;
    [SerializeField] private SpriteRenderer _backBar;
    public float shieldRate = 5f;
    private float nextShieldTime = 0f;
    private float _scaleX;

    // Start is called before the first frame update
    void Start()
    {
        _scaleX = transform.localScale.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time < nextShieldTime && _transform.localScale.x < 70)
        {
            _transform.localScale = new Vector2(_transform.localScale.x + _scaleX / ((shieldRate-1) * 60), _transform.localScale.y);
        }

        else if (Time.time == nextShieldTime) _backBar.color = Color.green;

        if (_shieldZone.activeSelf)
        {
            if (_transform.localScale.x <= 0) DeactivationShield();
            else _transform.localScale = new Vector2(_transform.localScale.x - _scaleX / ((shieldRate - 1) * 60), _transform.localScale.y);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Bullet")
    //    {
    //        Destroy(collision.gameObject);
    //        DeactivationShield();
    //    }
    //}


    public void ActivationShield()
    {
        if (Time.time >= nextShieldTime)
        {
            _shieldZone.SetActive(true);
            _backBar.color = Color.cyan;

        }
    }

    public void DeactivationShield()
    {
        nextShieldTime = Time.time + shieldRate;
        _shieldZone.SetActive(false);
        _transform.localScale = new Vector2(0f, _transform.localScale.y);
        _backBar.color = Color.black;
    }
}
