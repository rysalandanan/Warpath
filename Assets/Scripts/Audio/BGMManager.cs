using System.Collections;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;
    [SerializeField] AudioSource[] audioSources;
    private AudioSource currentAudioSource;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //audioSources = GetComponents<AudioSource>(); // Get all AudioSources attached
            PlayRandomBGM(); // Start playing music
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void PlayRandomBGM()
    {
        if (audioSources.Length == 0) return; // No audio sources found

        // Pick a random audio source
        int randomIndex = Random.Range(0, audioSources.Length);
        currentAudioSource = audioSources[randomIndex];

        // Play it and schedule the next song
        currentAudioSource.Play();
        StartCoroutine(WaitForMusicToEnd(currentAudioSource));
    }

    private IEnumerator WaitForMusicToEnd(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        PlayRandomBGM(); // Play another random BGM after current one ends
    }
}
