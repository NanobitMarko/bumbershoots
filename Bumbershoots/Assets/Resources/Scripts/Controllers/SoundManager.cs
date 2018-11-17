using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource SFXPlayer;
    [SerializeField] private AudioSource BGMPlayer;

    public enum Effects
    {
        Coin,
        Death,
        Jump,
        PowerUp,
        Slip,
        StoneCrack,
        Umbrella,
        Wind
    }

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
        SFXPlayer.PlayOneShot(Resources.Load<AudioClip>("Sounds/SFX/" + track + " SFX"));
    }

    public void PlayBGM(Music track)
    {
        BGMPlayer.clip = Resources.Load<AudioClip>("Sounds/BGM/" + track + " BGM");
        BGMPlayer.Play();
    }
}
