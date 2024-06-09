using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voiceline : MonoBehaviour
{
    public AudioClip infantryClip;
    public AudioClip archerClip;
    public AudioClip strongClip;
    public AudioClip collectorClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayUnitVoiceline(UnitType unitType)
    {
        switch (unitType)
        {
            case UnitType.Infantry:
                audioSource.PlayOneShot(infantryClip);
                break;
            case UnitType.Archer:
                audioSource.PlayOneShot(archerClip);
                break;
            case UnitType.Strong:
                audioSource.PlayOneShot(strongClip);
                break;
            case UnitType.Collector:
                audioSource.PlayOneShot(collectorClip);
                break;
            default:
                Debug.LogWarning("Unknown unit type");
                break;
        }
    }
}
