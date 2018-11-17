using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameHud : MonoBehaviour
{
    public static GameHud instance;

	[SerializeField] private Text scoreLabel;
	[SerializeField] private ControlPanel controlPanel;
	private PlayerController character;
	private int previousScore;
	private const int majorScoreBounceTreshold = 1000;
	private const int scoreBounceTreshold = 100;
	private const int minorScoreBounceTreshold = 10;
    
    public GameObject AddCoinsLabelHolder;
    public GameObject AddCoinsLabel;

    private void Awake()
    {
        instance = this;
    }

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
        character.CharacterContinuing += ConnectControls;
    }

	private void ConnectControls()
	{
		controlPanel.FingerDown += character.OnFingerDown;
		controlPanel.FingerUp += character.OnFingerUp;
		character.ScoreChanged += SetScore;
		character.CharacterDeath += DisconnectControls;
	}

	private void DisconnectControls()
	{
		controlPanel.FingerDown -= character.OnFingerDown;
		controlPanel.FingerUp -= character.OnFingerUp;
		character.ScoreChanged -= SetScore;
		character.CharacterDeath -= DisconnectControls;
	}

	private void SetScore(int score)
	{
		if (previousScore / majorScoreBounceTreshold < score / majorScoreBounceTreshold) {
			scoreLabel.rectTransform.localScale = Vector3.one * 1.8f;
			scoreLabel.rectTransform.DOScale(Vector3.one, 0.3f);
			scoreLabel.color = new Color32(255, 224, 117, 255);
		} else if (previousScore / scoreBounceTreshold < score / scoreBounceTreshold) {
			scoreLabel.rectTransform.localScale = Vector3.one * 1.4f;
			scoreLabel.rectTransform.DOScale(Vector3.one, 0.3f);
		} else if (previousScore / minorScoreBounceTreshold < score / minorScoreBounceTreshold) {
			scoreLabel.rectTransform.localScale = Vector3.one * 1.15f;
			scoreLabel.rectTransform.DOScale(Vector3.one, 0.1f);
		}
		scoreLabel.text = FormattedScore(score);
		previousScore = score;
	}

	private static string FormattedScore(int score)
	{
		return score.ToString();
	}

    public void SpawnAddCoinsLabel(int amount){
        if (AddCoinsLabel == null) return;
        GameObject newAddCoinsLabel = Instantiate(AddCoinsLabel);
        newAddCoinsLabel.transform.localScale = new Vector3(1, 1, 1);
        newAddCoinsLabel.transform.SetParent(AddCoinsLabelHolder.transform, false);
        newAddCoinsLabel.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        newAddCoinsLabel.GetComponent<Text>().text = "+" + amount.ToString();
    }
}
