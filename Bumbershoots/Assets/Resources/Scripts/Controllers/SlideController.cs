using UnityEngine;

public class SlideController : MonoBehaviour
{
	public Vector3 MovementVector;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		var player = other.GetComponent<PlayerController>();
		if (player != null)
		{
			player.StartSlide(MovementVector);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		var player = other.GetComponent<PlayerController>();
		if (player != null)
		{
			player.EndSlide();
		}
	}
}
