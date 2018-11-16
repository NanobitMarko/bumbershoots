using UnityEngine;
using UnityEngine.UI;

public class GameHud : MonoBehaviour
{
	[SerializeField] private Text scoreLabel;
	[SerializeField] private ControlPanel controlPanel;
	private PlayerController character;

	public static Transform Create(PlayerController character)
	{
		var menu = Instantiate(Resources.Load<GameHud>("Menus/GameHud"));
		menu.character = character;
		return menu.transform;
	}

	public void Start()
	{
		SetScore(0);
		ConnectControls(character);
	}

	private void ConnectControls(PlayerController controller)
	{
		controlPanel.FingerDown += controller.OnFingerDown;
		controlPanel.FingerUp += controller.OnFingerUp;
		controller.ScoreChanged += SetScore;
	}

	private void SetScore(int score)
	{
		scoreLabel.text = FormattedScore(score);
	}

	private static string FormattedScore(int score)
	{
		return score.ToString();
	}
}
