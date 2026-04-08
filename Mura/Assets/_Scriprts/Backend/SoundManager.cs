using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip clickSound;

    private void Awake()
    {
        // ≈сли уже есть SoundManager Ч удалить новый
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // ¬ј∆Ќќ Ч не уничтожать при смене сцены
        DontDestroyOnLoad(gameObject);
    }

    public void PlayClick()
    {
        audioSource.PlayOneShot(clickSound);
    }
}