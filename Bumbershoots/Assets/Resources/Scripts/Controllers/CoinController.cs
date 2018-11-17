using UnityEngine;

public class CoinController : MonoBehaviour {

    public int amountOfCoins;
    private CircleCollider2D circleColl;
    private SpriteRenderer spriteRend;

    private void Start() {
        if(amountOfCoins == 0){
            amountOfCoins = 5;
        }
        circleColl = GetComponent<CircleCollider2D>();
        if(circleColl == null) {
            this.gameObject.AddComponent<CircleCollider2D>();
            circleColl = GetComponent<CircleCollider2D>();
            circleColl.isTrigger = true;
        }

        spriteRend = GetComponent<SpriteRenderer>();
        if (spriteRend == null)
        {
            this.gameObject.AddComponent<SpriteRenderer>();
            spriteRend = GetComponent<SpriteRenderer>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.AddCoins(amountOfCoins);
            //Debug.Log("Dodano 5 coinsa!!!" + this.gameObject.name );
            //Save coin position here for particle effect instance position
            //Destroy(gameObject);
            circleColl.enabled = false;
            spriteRend.enabled = false;
        }
    }
}
