using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private int damage;
    public float speed = 1f; // Speed of the up and down movement
    public float height = 1f; // Maximum height of the movement
    private float startTime; // Time at which the movement started
    // Start is called before the first frame update
    void Start()
    {
        damage = 10;
        startTime = Time.time; // Record the start time
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the movement using sine function
        float yOffset = Mathf.Sin((Time.time - startTime) * speed) * height;

        // Update the object's position
        transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
    }
}
