using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource SFXPlayer;
    [SerializeField] private AudioSource BGMPlayer;

    public enum Effects
    {
        Coin,
        Death,
        Idle,
        Jump,
        PowerUp,
        Slip,
        StoneCrack
    }

    private static readonly Dictionary<Effects, int> EffectVariants = new Dictionary<Effects, int>
    {
        { Effects.Coin, 1 },
        { Effects.Death, 2 },
        { Effects.Jump, 2 },
        { Effects.PowerUp, 2 },
        { Effects.Idle, 3 },
        { Effects.StoneCrack, 1 },
        { Effects.Slip, 1 }
    };

    public enum Music
    {
        Menu,
        Uplifting
    }

    private static SoundManager instance;
    public static SoundManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    public void PlaySFX(Effects track)
    {
        SFXPlayer.PlayOneShot(Resources.Load<AudioClip>("Sounds/SFX/" + track + " " + Random.Range(1, EffectVariants[track] + 1)));
    }

    public void PlayBGM(Music track)
    {
        BGMPlayer.clip = Resources.Load<AudioClip>("Sounds/BGM/" + track);
        BGMPlayer.Play();
    }
}
