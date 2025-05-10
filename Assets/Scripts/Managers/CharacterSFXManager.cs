using UnityEngine;

public class CharacterSFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip[] characterSounds;

    public void PlaySound(int index)
    {
        sfxSource.PlayOneShot(characterSounds[index]);
    }
    
}
