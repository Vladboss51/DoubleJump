using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] public Block _block;
    public void SpawnBlock()
    {
        _block.gameObject.SetActive(true);
    }
}
