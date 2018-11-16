using UnityEngine;
using UnityEngine.UI;

public class GameHud : MonoBehaviour
{
	[SerializeField] private Text scoreLabel;
	[SerializeField] private ControlPanel controlPanel;
	private CharacterController character;

	public static Transform Create(CharacterController character)
	{
		var menu = Instantiate(Resources.Load<GameHud>("Menus/GameHud"));
		menu.character = character;
		return menu.transform;
	}

	public void Start()
	{
		scoreLabel.text = FormattedScore(0);
		ConnectControls(character);
	}

	public void ConnectControls(CharacterController controller)
	{
		controlPanel.FingerDown += controller.OnFingerDown;
		controlPanel.FingerUp += controller.OnFingerUp;
	}

	private static string FormattedScore(int score)
	{
		return "Score: " + score;
	}
}
