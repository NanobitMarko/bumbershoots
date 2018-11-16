using UnityEngine;

public class MainMenu : MonoBehaviour {

    public static Transform Create()
    {
        return Instantiate(Resources.Load<Transform>("Menus/MainMenu"));
    }

    public void OnStartClicked()
    {
        Destroy(gameObject);
        SceneController.Instance.BeginGame();
    }
}
