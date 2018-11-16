using Spine;
using Spine.Unity;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float SpeedFast = 8f;
    public float SpeedSlow = 2f;
    
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
    private float timeIntervalAtWhichWeGainPoints = 0.05f;
    private float lastTimeStampWhenWeGainedPoints;
    private int pointsGainedEveryTimeInterval = 1;
    private bool shouldGainPointsFromTime;

    private void Start()
    {
        mesh.AnimationState.Complete += OnAnimationComplete;
    }    

    private void Update()
    {
        if (shouldMove)
            transform.localPosition = transform.localPosition + speed * speedFactor * Time.deltaTime;
        
        if (shouldGainPointsFromTime)
            if (lastTimeStampWhenWeGainedPoints + timeIntervalAtWhichWeGainPoints < Time.realtimeSinceStartup) {
                lastTimeStampWhenWeGainedPoints = Time.realtimeSinceStartup;
                points += pointsGainedEveryTimeInterval; // possibly multiply for missed time?
                if (ScoreChanged != null)
                {
                    ScoreChanged(score);
                }
            }
    }

    public void SetMovementEnabled(bool enabled)
    {
        shouldMove = enabled;
        shouldGainPointsFromTime = enabled;
    }

    public void OnFingerDown()
    {
        speedFactor = SpeedSlow;
        SetAnimation("SlowDown");
    }

    private void OnAnimationComplete(TrackEntry entry)
    {
        switch (mesh.AnimationName)
        {
            case "SlowDown":
            {
                SetAnimation("FallSlow");
                break;
            }
            case "SpeedUp":
            {
                SetAnimation("FallFast");
                break;
            }
            default:
            {
                break;
            }
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
        if (ScoreChanged != null) {
            ScoreChanged(score);
        }
        // TODO display some fancy text with the amount of coins collected! leave this to Marko, he's amazing at this. or don't.
    }
}
