using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private PlayerController character;

    private bool hasContinued;

    private Transform _continueMenu;
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
        
        SoundManager.Instance.PlayBGM(SoundManager.Music.Menu);
    }

    public void BeginGame()
    {
        MenuController.Instance.ShowMenu(GameHud.Create(character));
        character.SetMovementEnabled(true);
        
        SoundManager.Instance.PlayBGM(SoundManager.Music.Uplifting);
    }

    public void OnCharacterDeath()
    {
        _continueMenu = ContinueMenu.Create();
        MenuController.Instance.ShowMenu(_continueMenu);
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
        Destroy(_continueMenu.gameObject);
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
