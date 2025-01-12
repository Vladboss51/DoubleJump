using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] public GameObject _block;
    [SerializeField] private SpriteRenderer _sprite;
    public void SpawnBlock()
    {
        if (!_block.gameObject.activeInHierarchy)
        {
            _block.gameObject.SetActive(true);
            _sprite.flipX = true;
        }
        else
        {
            _block.gameObject.SetActive(false);
            _sprite.flipX = false;
        }
    }
}
