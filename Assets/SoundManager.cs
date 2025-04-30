using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // Singleton

    public AudioSource enemyDeathSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayEnemyDeathSound(AudioClip clip)
    {
        enemyDeathSource.PlayOneShot(clip); // Reproduce sin interrupciones
        Debug.Log("Enemy death sound played.");
    }
}