using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private CharacterController character;
    
    private static SceneController instance;

    public static SceneController Instance
    {
        get { return instance; }
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
    }

    public void BeginGame()
    {
        MenuController.Instance.ShowMenu(GameHud.Create(character));
        character.SetMovementEnabled(true);
        Invoke("OnCharacterDeath", 3f);
    }

    public void OnCharacterDeath()
    {
        MenuController.Instance.ShowMenu(ContinueMenu.Create());
        character.SetMovementEnabled(false);
    }

    public void EndGame()
    {
        ReloadScene();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene("GameScene"); // this doesn't work yet for some reason
    }
}
