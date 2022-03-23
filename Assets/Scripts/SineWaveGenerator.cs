using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveGenerator : MonoBehaviour
{
    [Range(1f, 20000f)]  //Creates a slider in the inspector
    public float frequency = 1500f;

    public float sampleRate = 256000f;
    public float waveLengthInSeconds = 1f;

    [SerializeField] float offOn = 0.15f;

    [SerializeField] AudioSource audioSource;
    float phase = 0;

    float elapsedTime = 0f;
    bool playing = true;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        offOn =  0.15f - (transform.position.y + 9f) / 300f;

        frequency = 1499f + (transform.position.y + 9f) * 150f;

        if (elapsedTime >= offOn)
        {
            elapsedTime = 0f;

            audioSource.volume = (playing) ? 0f : 0.1f;
            playing = (playing) ? false : true;
        }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        for (int i = 0; i < data.Length; i += channels)
        {
            phase += 2 * Mathf.PI * frequency / sampleRate;

            data[i] = Mathf.Sin(phase);
            if (channels == 2)
                data[i+1] = Mathf.Sin(phase);

            if (phase >= 2 * Mathf.PI)
            {
                phase -= 2 * Mathf.PI;
            }
        }
    }
}
