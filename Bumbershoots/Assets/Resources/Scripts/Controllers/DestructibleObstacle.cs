using UnityEngine;

public class DestructibleObstacle : MonoBehaviour
{
	[SerializeField] private Animator _obstacleDestructionAnimator;
    public GameObject deathCollider;
	
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
			playerController.SetAnimation("PunchThrough", false);
			DestroyObstacle();
		}
		else
		{
			//playerController.AddDamage();
		}
	}

	private void DestroyObstacle()
    {
        _obstacleDestructionAnimator.SetTrigger("StartDestructionAnimation");
        deathCollider.SetActive(false);
    }
	
}
