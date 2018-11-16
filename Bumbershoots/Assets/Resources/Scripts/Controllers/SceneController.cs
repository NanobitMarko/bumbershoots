﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private PlayerController character;
    
    private static SceneController instance;

    public static SceneController Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DisplayMainScreen();
    }

    public void DisplayMainScreen()
    {
        MenuController.Instance.ShowMenu(MainMenu.Create());
        character.SetMovementEnabled(false);
    }

    public void BeginGame()
    {
        MenuController.Instance.ShowMenu(GameHud.Create(character));
        character.SetMovementEnabled(true);
    }

    public void OnCharacterDeath()
    {
        MenuController.Instance.ShowMenu(ContinueMenu.Create());
        character.SetMovementEnabled(false);
        
        SoundManager.Instance.PlaySFX(SoundManager.Effects.Death);
    }

    public void EndGame()
    {
        ReloadScene();
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
