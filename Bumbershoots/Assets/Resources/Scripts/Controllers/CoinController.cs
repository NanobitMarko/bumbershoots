using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

    public float amountOfCoins;

    void Start(){
        if(amountOfCoins == 0f){
            amountOfCoins = 5f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<PlayerController>().AddCoins(amountOfCoins);
        Debug.Log("Dodano " + amountOfCoins + "!");
        //Spremiti coin position za eventualno kasnije instanciranje particle effecta (prije nego se uništi coin)!
        Destroy(gameObject);
    }
}
