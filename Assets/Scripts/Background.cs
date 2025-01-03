using UnityEngine;

public class Back : MonoBehaviour
{
    [SerializeField] private Cam _cam;
    public float ytocam;
    public float xpos;

    private void FixedUpdate()
    {
        transform.position = new Vector2(xpos, _cam.transform.position.y + ytocam);
    }
}
