using UnityEngine;

public class MainMenu : MonoBehaviour {

    public static Transform Create()
    {
        return Instantiate(Resources.Load<Transform>("Menus/MainMenu"));
    }

    public void OnStartClicked()
    {
        Destroy(gameObject);
        SoundManager.Instance.PlaySFX(SoundManager.Effects.Select);
        SceneController.Instance.BeginGame();
    }
}
