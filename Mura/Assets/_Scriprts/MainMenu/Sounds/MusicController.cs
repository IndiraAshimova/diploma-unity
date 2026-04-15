using UnityEngine;

public class MusicController : MonoBehaviour
{
    [Header("Scene Music")]
    [SerializeField] private AudioClip sceneMusic;

    private void Start()
    {
        if (sceneMusic != null)
        {
            SoundManager.Instance
                .PlayMusic(sceneMusic);
        }
    }
}