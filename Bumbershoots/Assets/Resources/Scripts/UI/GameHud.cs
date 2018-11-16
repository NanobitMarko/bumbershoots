using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameHud : MonoBehaviour
{
	[SerializeField] private Text scoreLabel;
	[SerializeField] private ControlPanel controlPanel;
	private PlayerController character;
	private int previousScore;
	private const int scoreBounceTreshold = 100;

	public static Transform Create(PlayerController character)
	{
		var menu = Instantiate(Resources.Load<GameHud>("Menus/GameHud"));
		menu.character = character;
		return menu.transform;
	}

	public void Start()
	{
		SetScore(0);
		ConnectControls();
	}

	private void ConnectControls()
	{
		controlPanel.FingerDown += character.OnFingerDown;
		controlPanel.FingerUp += character.OnFingerUp;
		character.ScoreChanged += SetScore;
		character.CharacterDeath += DisconnectControls;
	}

	public void DisconnectControls()
	{
		controlPanel.FingerDown -= character.OnFingerDown;
		controlPanel.FingerUp -= character.OnFingerUp;
		character.ScoreChanged -= SetScore;
		character.CharacterDeath -= DisconnectControls;
	}

	private void SetScore(int score)
	{
		if (previousScore / scoreBounceTreshold < score / scoreBounceTreshold)
		{
			scoreLabel.rectTransform.localScale = Vector3.one * 1.2f;
			scoreLabel.rectTransform.DOScale(Vector3.one, 0.5f);
		}
		scoreLabel.text = FormattedScore(score);
		previousScore = score;
	}

	private static string FormattedScore(int score)
	{
		return score.ToString();
	}
}
