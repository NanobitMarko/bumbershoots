using Spine;
using Spine.Unity;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float SpeedFast = 8f;
    public float SpeedSlow = 2f;

    public int MultiplierFast = 2;
    public int MultiplierSlow = 2;
    
    private Vector3 speed = Vector3.down;
    private float speedFactor = 8f;
    private bool shouldMove = true;
    [SerializeField] private SkeletonAnimation mesh;
    
    public delegate void ScoreChangedHandler(int score); 
    
    public event ScoreChangedHandler ScoreChanged;

    public delegate void CharacterDeathHandler();

    public event CharacterDeathHandler CharacterDeath;

    private int coins;
    private int points;

    private int score
    {
        get { return coins + points; }
    }
    
    private int scoreMultiplier = 1;
    private float timeIntervalAtWhichWeGainPoints = 0.1f;
    private float lastTimeStampWhenWeGainedPoints;
    private float pointsGainedForDistancePassed = 0.3f;
    private bool shouldGainPointsFromProgress;
    private float lastDistanceWhenWeGainedPoints;
    private float minimalScoringDistance = 0.1f;

    private void Start()
    {
        mesh.AnimationState.Complete += OnAnimationComplete;
        lastDistanceWhenWeGainedPoints = transform.localPosition.y;
    }    

    private void Update()
    {
        if (shouldMove)
            transform.localPosition = transform.localPosition + speed * speedFactor * Time.deltaTime;
        
        if (shouldGainPointsFromProgress)
            if (lastDistanceWhenWeGainedPoints - minimalScoringDistance > transform.localPosition.y)
            {
                // y is dropping!
                int pointsGained = Mathf.RoundToInt(Mathf.Abs(transform.localPosition.y - lastDistanceWhenWeGainedPoints) /
                                                    minimalScoringDistance * pointsGainedForDistancePassed) * scoreMultiplier;
                if (pointsGained > 0) {
                    points += pointsGained;
                    lastDistanceWhenWeGainedPoints = transform.localPosition.y;
                    if (ScoreChanged != null) {
                        ScoreChanged(score);
                    }
            }
        }
    }

    public void SetMovementEnabled(bool enabled)
    {
        shouldMove = enabled;
        shouldGainPointsFromProgress = enabled;
    }

    public void OnFingerDown()
    {
        speedFactor = SpeedSlow;
        scoreMultiplier = MultiplierSlow;
        SetAnimation("SlowDown");
    }

    private void OnAnimationComplete(TrackEntry entry)
    {
        string newAnimationName = null;
        switch (mesh.AnimationName)
        {
            case "SlowDown":
            {
                newAnimationName ="FallSlow";
                break;
            }
            case "SpeedUp":
            {
                newAnimationName = "FallFast";
                break;
            }
            case "PunchThrough":
            {
                newAnimationName = IsGoingFast ? "FallFast" : "FallSlow";
                break;
            }
            default:
            {
                break;
            }
        }

        if (!string.IsNullOrEmpty(newAnimationName))
        {
            SetAnimation(newAnimationName);
        }
    }

    public void SetAnimation(string animationName)
    {
        //mesh.skeleton.SetToSetupPose();
        mesh.AnimationName = animationName;
    }

    public void OnFingerUp()
    {
        speedFactor = SpeedFast;
        scoreMultiplier = MultiplierFast;
        SetAnimation("SpeedUp");
    }

    public void AddDamage(){
        Debug.Log("Pocinjena je steta!");
        SceneController.Instance.OnCharacterDeath();

        if (CharacterDeath != null)
            CharacterDeath();
    }

    public void AddCoins(int amount)
    {
        coins += amount * scoreMultiplier;
        
        SoundManager.Instance.PlaySFX(SoundManager.Effects.Coin);
        
        if (ScoreChanged != null) {
            ScoreChanged(score);
        }
        // TODO display some fancy text with the amount of coins collected! leave this to Marko, he's amazing at this. or don't.
    }

    public bool IsGoingFast
    {
        get { return speedFactor > 0.8f * SpeedFast; }
    }
}
