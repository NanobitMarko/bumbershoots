using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Vector3 speed = Vector3.down;
    private float speedFactor = 1.0f;
    private bool shouldMove = true;

    private void Update()
    {
        if (shouldMove)
            transform.localPosition = transform.localPosition + speed * speedFactor * Time.deltaTime;
    }

    public void SetMovementEnabled(bool enabled)
    {
        shouldMove = enabled;
    }
}
