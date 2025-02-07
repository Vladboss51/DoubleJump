using UnityEngine;


public class Cactus : MonoBehaviour
{
    [SerializeField] private GameObject _spike;
    [SerializeField] private Animator _animator;
    public float attackRate = 3f;
    private float _nextAttackTime = 0f;
    private Vector3 _position;
    private Quaternion _rotation;


    // Start is called before the first frame update
    void Start()
    {
        _position = transform.position;
        _rotation = transform.rotation;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time >= _nextAttackTime)
        {
            _animator.SetTrigger("ATTACK");

            _nextAttackTime = Time.time + attackRate;
        }
    }

    private void Attack()
    {
        Instantiate(_spike, _position, _rotation);

    }
}
