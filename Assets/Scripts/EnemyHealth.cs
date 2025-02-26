using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float healthEnemy = 1f;
    private Animator _anim;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        healthEnemy -= 1;
        if (healthEnemy <= 0)
        {
            _anim.SetTrigger("DIED");
            _collider.enabled = false;
        }
        StartCoroutine(TakeDamageCoroutine());
    }

    private IEnumerator TakeDamageCoroutine()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        _spriteRenderer.color = Color.white;
    }

    private void Died()
    {
        gameObject.SetActive(false);
    }
}
