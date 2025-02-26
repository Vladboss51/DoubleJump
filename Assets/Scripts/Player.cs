using Game;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Character
{
    //[SerializeField] private ShieldSystem _shieldSystem;
    [SerializeField] private Transform _attackZone;
    public float attackRadius;
    [SerializeField] protected LayerMask _enemy;

    private void Start()
    {
        _moveButton = "Horizontal1";
        _jumpButton = "up1";
        _interactionButton = "e";
        _attackButton = "Fire1";
    }

    protected override void Attack()
    {
    base.Attack();
        Collider2D[] Enemies = Physics2D.OverlapCircleAll(_attackZone.position, attackRadius, _enemy);
        foreach (Collider2D enemy in Enemies)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage();

        }
        
    }
}
