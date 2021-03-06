using System;
using System.Collections;
using Spine;
using Spine.Unity;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float InvincibleSeconds = 1;
    public float InvincibleFlashDurationSeconds = 0.08f;
    
    public float SpeedFast = 8f;
    public float SpeedSlow = 2f;

    public int MultiplierFast = 2;
    public int MultiplierSlow = 2;

    private bool _isInvincible;
    private Vector3 speed = Vector3.down;
    private float speedFactor = 8f;
    private bool shouldMove = true;
    [SerializeField] private SkeletonAnimation mesh;

    public delegate void ScoreChangedHandler(int score);

    public event ScoreChangedHandler ScoreChanged;

    public delegate void CharacterDeathHandler();

    public event CharacterDeathHandler CharacterDeath;
    public Action CharacterContinuing;

    private int coins;
    private int points;

    private int score
    {
        get { return coins + points; }
    }

    private int scoreMultiplier = 1;
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
                if (pointsGained > 0)
                {
                    points += pointsGained;
                    lastDistanceWhenWeGainedPoints = transform.localPosition.y;
                    if (ScoreChanged != null)
                    {
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
        if (mesh.AnimationName == "Slide")
            return;
        SetAnimation("SlowDown", false);
    }

    private void OnAnimationComplete(TrackEntry entry)
    {
        string newAnimationName = null;
        switch (mesh.AnimationName)
        {
            case "SlowDown":
            {
                CancelInvoke("PlayBoredSound");
                InvokeRepeating("PlayBoredSound", 3f, 5f);
                newAnimationName = "FallSlow";
                break;
            }
            case "SpeedUp":
            {
                CancelInvoke("PlayBoredSound");
                newAnimationName = "FallFast";
                break;
            }
            case "PunchThrough":
            {
                newAnimationName = IsGoingFast ? "FallFast" : "FallSlow";
                break;
            }
        }

        if (!string.IsNullOrEmpty(newAnimationName))
        {
            SetAnimation(newAnimationName);
        }
    }

    private void PlayBoredSound()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Effects.Idle);
    }

    public void SetAnimation(string animationName, bool loop = true)
    {
        mesh.loop = loop;
        mesh.AnimationName = animationName;
    }

    public void OnFingerUp()
    {
        speedFactor = SpeedFast;
        scoreMultiplier = MultiplierFast;
        if (mesh.AnimationName == "Slide")
            return;
        SetAnimation("SpeedUp", false);
    }

    public void AddDamage()
    {
        if (_isInvincible)
        {
            return;
        }
        
        SceneController.Instance.OnCharacterDeath();
        CancelInvoke("PlayBoredSound");

        if (CharacterDeath != null)
            CharacterDeath();
        _isInvincible = true;
    }

    public void ContinueGame()
    {
        StartCoroutine(StartInvincibility());
        SetMovementEnabled(true);
        SetAnimation("FallFast");
        speedFactor = SpeedFast;
        if (CharacterContinuing != null)
        {
            CharacterContinuing();
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount * scoreMultiplier;

        SoundManager.Instance.PlaySFX(SoundManager.Effects.Coin);

        if (ScoreChanged != null)
        {
            ScoreChanged(score);
        }

        // TODO display some fancy text with the amount of coins collected! leave this to Marko, he's amazing at this. or don't.
        GameHud.instance.SpawnAddCoinsLabel(amount);
    }

    public bool IsGoingFast
    {
        get { return speedFactor > SpeedSlow; }
    }

    public void StartSlide(Vector3 slideSpeed)
    {
        speed = slideSpeed;
        SetAnimation("Slide");
        
        SoundManager.Instance.PlaySFX(SoundManager.Effects.Slip);
    }

    public void EndSlide()
    {
        speed = Vector3.down;
        SetAnimation(IsGoingFast ? "FallFast" : "FallSlow");
    }

    private IEnumerator StartInvincibility()
    {
        _isInvincible = true;
        MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
        float invincibilityDuration = 0;
        float visibilityDuration = 0;

        while (invincibilityDuration < InvincibleSeconds)
        {
            visibilityDuration += Time.deltaTime;
            invincibilityDuration += Time.deltaTime;

            if (visibilityDuration > InvincibleFlashDurationSeconds)
            {
                visibilityDuration = 0;
                meshRenderer.enabled = !meshRenderer.enabled;        
            }
            
            yield return null;
        }

        meshRenderer.enabled = true;
        _isInvincible = false;
    }

    public int GetScore()
    {
        return score;
    }
}
