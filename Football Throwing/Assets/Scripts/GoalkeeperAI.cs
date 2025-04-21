using UnityEngine;

public class GoalkeeperAI : MonoBehaviour
{
    public Transform ball;
    public float moveSpeed = 3f;
    public float trackingRange = 5f;

    private void Update()
    {
        if (Vector2.Distance(ball.position, transform.position) < trackingRange)
        {
            Vector3 targetPos = new Vector3(ball.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
    }
}
