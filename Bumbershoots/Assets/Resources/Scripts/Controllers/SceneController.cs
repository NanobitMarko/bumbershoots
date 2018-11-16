using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
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
        MenuController.Instance.ShowMenu(MainMenu.Create());
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
