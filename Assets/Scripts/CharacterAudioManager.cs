using UnityEngine;

public class CharacterAudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Movement Sounds")]
    public AudioClip[] footstepSounds;
    public float footstepVolume = 0.5f; 

    [Header("Combat Sounds")]
    public AudioClip attackSound;
    public AudioClip hitSound;
    public float combatVolume = 0.8f;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.spatialBlend = 1.0f;
        audioSource.minDistance = 1f;
        audioSource.maxDistance = 20f;
    }

    public void AE_Footstep()
    {
        if (footstepSounds != null && footstepSounds.Length > 0)
        {
            AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
            audioSource.PlayOneShot(clip, footstepVolume);
        }
    }

    public void AE_AttackSwing()
    {
        if (attackSound != null)
        {
            audioSource.PlayOneShot(attackSound, combatVolume);
        }
    }
}