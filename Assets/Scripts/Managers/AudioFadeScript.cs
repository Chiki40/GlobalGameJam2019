using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFadeScript : MonoBehaviour
{
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = 1;
        if (audioSource)
        {
            startVolume = audioSource.volume;
        }

        while (audioSource && audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
        if (audioSource)
        {
            audioSource.Stop();
            audioSource.volume = startVolume;
        }
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        float startVolume = 0.2f;
        if (audioSource)
        {
            audioSource.volume = 0;
            audioSource.Play();
        }

        while (audioSource && audioSource.volume < 1.0f)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
        if (audioSource)
        {
            audioSource.volume = 1f;
        }
    }
}
