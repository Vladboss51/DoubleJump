using UnityEngine;
using System;

public class Cam : MonoBehaviour
{
    [SerializeField] private Player _player1;
    [SerializeField] private PlayerSecond _player2;

    private void Awake()
    {
        Application.targetFrameRate = 100;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(0f, Math.Max((_player1.transform.position.y + _player2.transform.position.y) / 2, 0), -6.5f);

    }
}
