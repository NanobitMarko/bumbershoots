﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 speed = Vector3.down;
    private float speedFactor = 1.0f;
    private bool shouldMove = true;

    private void Update()
    {
        if (shouldMove)
            transform.localPosition = transform.localPosition + speed * speedFactor * Time.deltaTime;
    }

    public void SetMovementEnabled(bool enabled)
    {
        shouldMove = enabled;
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

    public void AddCoins(float amount){
        //napraviti coinse
    }
}
