using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] private Player _player1;
    [SerializeField] private PlayerSecond _player2;

    private void FixedUpdate()
    {
        transform.position = new Vector3(0, (_player1.transform.position.y + _player2.transform.position.y) / 2 + 5, -20f);

    }
}
