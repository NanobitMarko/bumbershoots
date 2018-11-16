using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour {

    public BoxCollider2D obsCollider;

	// Use this for initialization
	void Start () {
        obsCollider = GetComponent<BoxCollider2D>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<PlayerController>().AddDamage();
        Destroy(other.gameObject);
        Debug.Log("Uništen je player!");
    }
}
