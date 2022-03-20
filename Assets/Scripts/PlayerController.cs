using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] AudioSource audioSourceCollision;
    [SerializeField] AudioClip collisionClip;

    [SerializeField] AudioHighPassFilter audioHighPassFilter;

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

        float cutoffFrequency = 10.0f + (pos.y + 10f) * 10.0f;
        audioHighPassFilter.cutoffFrequency = Mathf.Clamp(cutoffFrequency, 10f, 22000f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            //audioSourceCollision.volume = 0.4f;
            //audioSourceCollision.clip = collisionClip;
            audioSourceCollision.Play();
            GameManager.instance.ResetPositionPlayer(tag);
        }
    }


}
