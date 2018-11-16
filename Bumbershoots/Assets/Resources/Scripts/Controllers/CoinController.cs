using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour {

    public int amountOfCoins;

    void Start(){
        if(amountOfCoins == 0){
            amountOfCoins = 5;
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
