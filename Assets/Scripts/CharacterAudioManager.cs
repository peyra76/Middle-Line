using UnityEngine;

public class CharacterAudioManager : MonoBehaviour
{
    private AudioSource audioSource;

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
    }

    public void AE_AttackSwing()
    {
        if (attackSound != null)
        {
            audioSource.PlayOneShot(attackSound, combatVolume);
        }
    }
}