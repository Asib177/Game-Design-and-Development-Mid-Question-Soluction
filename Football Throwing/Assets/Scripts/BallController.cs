using UnityEngine;

public class BallController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;           // Player position
    [SerializeField] private LineRenderer aimLine;       // Aiming line
    private Rigidbody2D ballRb;                          // Ball's Rigidbody2D

    [Header("Throw Settings")]
    [SerializeField] private float minPower = 5f;
    [SerializeField] private float maxPower = 20f;
    [SerializeField] private float chargeRate = 10f;

    private float _currentPower;
    private bool _isCharging;

    private void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();

        if (aimLine == null)
        {
            aimLine = GameObject.Find("AimLine").GetComponent<LineRenderer>();
        }
    }

    void Update()
    {
        // Get mouse position in world
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0; // Ensure it's 2D
        Vector2 direction = (mouseWorldPos - player.position).normalized;

        // Draw aim line
        aimLine.SetPosition(0, player.position);
        aimLine.SetPosition(1, player.position + (Vector3)direction * 2f);

        // Start charging throw
        if (Input.GetMouseButtonDown(0))
        {
            _isCharging = true;
            _currentPower = minPower;
        }

        // Charging power
        if (_isCharging)
        {
            _currentPower += chargeRate * Time.deltaTime;
            _currentPower = Mathf.Clamp(_currentPower, minPower, maxPower);
        }

        // Release throw
        if (Input.GetMouseButtonUp(0))
        {
            _isCharging = false;
            ThrowBall();
        }
    }

    private void ThrowBall()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPos - (Vector2)player.position).normalized;

        // Reset velocity if needed
        ballRb.linearVelocity = Vector2.zero;

        // Apply force
        ballRb.AddForce(direction * _currentPower, ForceMode2D.Impulse);
    }

    // Remove this block completely from BallController
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("GOAL!");
            Destroy(other.gameObject); // Example: Remove ball on goal
        }
    }
}
