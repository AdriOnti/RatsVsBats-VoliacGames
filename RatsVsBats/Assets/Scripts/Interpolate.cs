using UnityEngine;

public class Interpolate : MonoBehaviour
{
    public enum Axis
    {
        x,
        y,
        z
    }
    public Axis axis;
    public float delta; 
    public float speed;
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = startPos;
        if (axis == Axis.x)
        {
            v.x += delta * Mathf.Sin(Time.time * speed);
        }
        else if (axis == Axis.y)
        {
            v.y += delta * Mathf.Sin(Time.time * speed);
        }
        else if (axis == Axis.z)
        {
            v.z += delta * Mathf.Sin(Time.time * speed);
        }
        transform.position = v;
    }
}
