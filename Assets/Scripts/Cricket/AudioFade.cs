using System.Collections;
using UnityEngine;

public class AudioFader : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayAndFadeOut(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.volume = 1f;
        audioSource.Play();
        StartCoroutine(FadeOutCoroutine(clip.length));
    }

    private IEnumerator FadeOutCoroutine(float clipLength)
    {
        float fadeDuration = 4f;
        float startVolume = audioSource.volume;

        yield return new WaitForSeconds(clipLength - fadeDuration);

        float time = 0;
        while (time < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop();
    }
}
