using UnityEngine;

public class Bobbing : MonoBehaviour
{
    public float floatSpeed = 1f;      // Speed of the up and down motion
    public float floatHeight = 0.5f;   // Maximum height from the original position

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
