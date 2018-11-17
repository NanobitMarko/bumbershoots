using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private PlayerController character;

    private bool hasContinued;

    private static SceneController instance;

    public static SceneController Instance
    {
        get { return instance; }
    }

    public int GetCurrentScore()
    {
        return character.GetScore();
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DisplayMainScreen();
    }

    public void DisplayMainScreen()
    {
        MenuController.Instance.ShowMenu(MainMenu.Create());
        character.SetMovementEnabled(false);
        character.SetAnimation("Idle");
        CameraManager.Instance.offset = Vector3.zero;
        SoundManager.Instance.PlayBGM(SoundManager.Music.Menu);
    }

    public void BeginGame()
    {
        MenuController.Instance.ShowMenu(GameHud.Create(character));
        character.SetMovementEnabled(true);
        character.SetAnimation("FallFast");
        CameraManager.Instance.offset = Vector3.down * 3.5f;
        SoundManager.Instance.PlayBGM(SoundManager.Music.Uplifting);
    }

    public void OnCharacterDeath()
    {
        MenuController.Instance.ShowMenu(ContinueMenu.Create());
        character.SetMovementEnabled(false);
        character.SetAnimation("Death", false);
        
        SoundManager.Instance.PlaySFX(SoundManager.Effects.Death);
    }

    public void EndGame()
    {
        ReloadScene();
    }

    public void ContinueGame()
    {
        character.ContinueGame();
        hasContinued = true;
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool HasContinued()
    {
        return hasContinued;
    }
}
