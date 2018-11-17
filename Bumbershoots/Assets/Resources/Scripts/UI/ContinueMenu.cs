using UnityEngine;
using UnityEngine.UI;

public class ContinueMenu : MonoBehaviour
{
	[SerializeField] private Button continueButton;
	[SerializeField] private Text scoreLabel;
	
	public static Transform Create()
	{
		return Instantiate(Resources.Load<Transform>("Menus/ContinueMenu"));
	}

	public void Start()
	{
		scoreLabel.text = SceneController.Instance.GetCurrentScore().ToString();
		continueButton.interactable = !SceneController.Instance.HasContinued();
	}
	
	public void OnRestartClicked()
	{
		SceneController.Instance.EndGame();
	}

	public void OnContinueClicked()
	{
		Destroy(gameObject);
		SceneController.Instance.ContinueGame();
	}
}
