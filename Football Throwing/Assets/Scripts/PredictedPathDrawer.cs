using UnityEngine;

public class PredictedPathDrawer : MonoBehaviour
{
    public Transform player;
    public Transform ball;
    public LineRenderer lineRenderer;
    public int segmentCount = 30;
    public float maxPower = 20f;
    public AnimationCurve arcCurve;

    void Update()
    {
        if (ball == null || player == null || lineRenderer == null) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector2 direction = (mousePos - player.position).normalized;

        DrawArc(direction);
    }

    void DrawArc(Vector2 direction)
    {
        lineRenderer.positionCount = segmentCount;

        for (int i = 0; i < segmentCount; i++)
        {
            float t = i / (float)(segmentCount - 1);
            float height = arcCurve.Evaluate(t);
            Vector3 point = player.position + (Vector3)(direction * t * 5f); // adjust 5f for distance
            point.y += height * 2f; // adjust 2f for arc height
            lineRenderer.SetPosition(i, point);
        }
    }
}
