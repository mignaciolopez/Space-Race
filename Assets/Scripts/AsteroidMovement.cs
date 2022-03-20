using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [SerializeField] float speed = 4.0f;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        velocity.x = transform.position.x < 0f ? 1f : -1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += velocity * speed * Time.deltaTime;
    }
}
