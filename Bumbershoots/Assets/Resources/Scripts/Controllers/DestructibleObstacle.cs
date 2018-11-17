using UnityEngine;

public class DestructibleObstacle : MonoBehaviour
{
	[SerializeField] private Animator _obstacleDestructionAnimator;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		PlayerController playerController = other.GetComponent<PlayerController>();

		if (playerController == null)
		{
			DestroyObstacle();
			return;
		}

		if (playerController.IsGoingFast)
		{
			playerController.SetAnimation("PunchThrough");
			DestroyObstacle();
		}
		else
		{
			playerController.AddDamage();
		}
	}

	private void DestroyObstacle()
	{
		_obstacleDestructionAnimator.SetTrigger("StartDestructionAnimation");
	}
	
}
