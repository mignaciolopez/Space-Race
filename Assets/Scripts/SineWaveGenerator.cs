using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineWaveGenerator : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [Range(1, 20000)]  //Creates a slider in the inspector
    [SerializeField] int frequency = 275;
    [SerializeField] int samplerate = 44100;
    [SerializeField] float toggleTime = 0.135f;

    [SerializeField, Min(1)] float pitchDelta = 15f;

    float elapsedTime = 0f;
    float pitch = 1f;

    float[] samples;

    private void Awake()
    {
        if (CompareTag("Player2"))
        {
            frequency += 5;
            toggleTime += 0.005f;
            pitchDelta += 1f;
        }

        audioSource.clip = AudioClip.Create("MySinusoid", samplerate * 2, 2, samplerate, false);
        samples = new float[audioSource.clip.samples * audioSource.clip.channels];
        SetAudioData();
        audioSource.Play();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        float toggleTimeTemp = toggleTime - (transform.position.y + 9f) / 250f;

        pitch = 1f + (transform.position.y + 9f) / pitchDelta;
        audioSource.pitch = Mathf.Clamp(pitch, 1f, 3f);

        if (elapsedTime >= toggleTimeTemp)
        {
            elapsedTime = 0f;

            audioSource.mute = (audioSource.mute) ? false : true;
        }
    }

    void SetAudioData()
    {
        float phase = 0f;

        for (int i = 0; i < samples.Length; i += audioSource.clip.channels)
        {
            phase = Mathf.Sin(Mathf.PI * 2 * i * frequency / samplerate);

            samples[i] = phase;
            samples[i + 1] = phase;
        }

        audioSource.clip.SetData(samples, 0);
    }
}
