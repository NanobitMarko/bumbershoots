using UnityEngine;

public class ObstacleController : MonoBehaviour {

    public BoxCollider2D obsCollider;

	// Use this for initialization
	private void Start () {
        obsCollider = GetComponent<BoxCollider2D>();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        SceneController.Instance.OnCharacterDeath();
        other.GetComponent<PlayerController>().AddDamage();
        Debug.Log("Uništen je player!");
    }
}
