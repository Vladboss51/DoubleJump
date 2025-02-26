using Game;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSecond : Character
{
    [SerializeField] private GameObject _arrow;
    private Vector3 _position;
    private Quaternion _rotation;

    private void Start()
    {
        _moveButton = "Horizontal2";
        _jumpButton = "up2";
        _interactionButton = "ctrl";
        _attackButton = "Fire2";
    }

    protected override void Attack()
    {
        base.Attack();
        _position = transform.position + new Vector3(0.2f, 0f, 0f);
        _rotation = transform.rotation;
        Instantiate(_arrow, _position, _rotation);

    }
}
