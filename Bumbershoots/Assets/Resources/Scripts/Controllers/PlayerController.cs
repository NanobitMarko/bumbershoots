using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 speed = Vector3.down;
    private float speedFactor = 1.0f;
    private bool shouldMove = true;
    
    public delegate void ScoreChangedHandler(int score); 
    
    public event ScoreChangedHandler ScoreChanged;

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
        speedFactor = 0.5f;
    }

    public void OnFingerUp()
    {
        speedFactor = 1.0f;
    }

    public void AddDamage(){
        Debug.Log("Pocinjena je steta!");
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
