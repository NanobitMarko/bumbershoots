using UnityEngine;

public class CoinController : MonoBehaviour {

    public int amountOfCoins;

    private void Start() {
        if(amountOfCoins == 0){
            amountOfCoins = 5;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.AddCoins(amountOfCoins);
            //Spremiti coin position za eventualno kasnije instanciranje particle effecta (prije nego se uništi coin)!
            Destroy(gameObject);
        }
    }
}
