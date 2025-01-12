using UnityEngine;
using System;

public class Cam : MonoBehaviour
{
    [SerializeField] private Player _player1;
    [SerializeField] private PlayerSecond _player2;

    private void FixedUpdate()
    {
        transform.position = new Vector3(0f, Math.Max((_player1.transform.position.y + _player2.transform.position.y) / 2 + 5, 0), -21f);

    }
}
