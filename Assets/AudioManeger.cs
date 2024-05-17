using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManeger : MonoBehaviour
{
    [SerializeField] AudioSource musicSorce;
    [SerializeField] AudioSource SFXSorce;

    [Header("SFX")]
    public AudioClip background;
    public AudioClip spawn;
    public AudioClip Undead_dead;
    public AudioClip Human_dead;
    public AudioClip Undead_atk;
    public AudioClip Human_atk;
    public AudioClip arrow;
    public AudioClip fireball;
    public AudioClip heal;
    public AudioClip merge;
    public AudioClip ice_spell;

    private void Start()
    {
        musicSorce.clip = background;
        musicSorce.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSorce.PlayOneShot(clip);
    }

}
