using UnityEngine;

public class ObstacleController : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player entered");
        var player = other.GetComponent<PlayerController>();
        if (player != null)
            player.AddDamage();
    }
}
