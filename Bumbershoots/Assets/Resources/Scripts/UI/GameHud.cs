using UnityEngine;
using UnityEngine.UI;

public class GameHud : MonoBehaviour
{
	[SerializeField] private Text scoreLabel;

	public static Transform Create()
	{
		return Instantiate(Resources.Load<Transform>("Menus/GameHud"));
	}

	public void Start()
	{
		scoreLabel.text = FormattedScore(0);
	}

	private static string FormattedScore(int score)
	{
		return "Score: " + score;
	}
}
