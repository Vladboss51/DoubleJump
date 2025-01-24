using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    [SerializeField] private GameObject _spike;
    [SerializeField] private Animator _animator;
    public float attackRate = 3f;
    private float _nextAttackTime = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= _nextAttackTime)
        {
            _animator.SetBool("ATTACK", true);

            _nextAttackTime = Time.time + attackRate;
        }
    }

    private void Attack()
    {
        Instantiate(_spike);

    }

    private void AttackFalse()
    {
        _animator.SetBool("ATTACK", false);
    }
}
