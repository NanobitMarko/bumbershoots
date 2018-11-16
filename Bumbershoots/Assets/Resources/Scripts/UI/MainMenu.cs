using UnityEngine;

public class MainMenu : MonoBehaviour {

    public static Transform Create()
    {
        return Instantiate(Resources.Load<Transform>("Menus/MainMenu"));
    }

    public void OnStartClicked()
    {
        // TODO game controller -> start game
        Destroy(gameObject);
        MenuController.Instance.ShowMenu(GameHud.Create());
        Debug.LogWarning("Start button clicked! Not fully implemented yet!");
    }
}
