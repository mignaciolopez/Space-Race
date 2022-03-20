using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string axisName = "Vertical";
    public float speed = 4.0f;

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        pos.y += Input.GetAxisRaw(axisName) * speed * Time.deltaTime;

        if (Input.GetAxisRaw(axisName) < 0f && pos.y < -9f)
            pos = transform.position;

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
            GameManager.instance.ResetPositionPlayer(tag);
    }
}
