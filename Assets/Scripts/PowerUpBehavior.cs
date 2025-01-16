using UnityEngine;

public class PowerUpBehavior : MonoBehaviour
{
    [Header("Power-Up Settings")]
    [Tooltip("The amount by which the rocket count increases when the power-up is collected")]
    [SerializeField] private int rocketCountIncrease = 1;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collided with the power-up
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                playerMovement.IncreaseRocketCount(rocketCountIncrease);
                if (playerMovement.RocketCount < 8 || playerMovement.RocketCount == 8)
                {
                    Destroy(gameObject);  // Destroy power-up if the rocket count is not maxed
                }
            }
        }
    }
}
