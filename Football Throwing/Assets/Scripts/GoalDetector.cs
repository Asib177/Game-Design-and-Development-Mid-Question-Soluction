using UnityEngine;

public class GoalDetector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("GOAL!");
            Destroy(other.gameObject); // Optional: remove the ball after scoring
        }
    }
}
