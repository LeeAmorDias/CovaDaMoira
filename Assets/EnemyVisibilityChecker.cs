using UnityEngine;

public class EnemyVisibilityChecker : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Transform enemy;

    [Header("Settings")]
    [SerializeField] private float maxVisibleDistance = 50f;
    [SerializeField] private bool checkLineOfSight = true;

    public bool IsEnemyVisible()
    {
        Vector3 viewportPoint = playerCamera.WorldToViewportPoint(enemy.position);

        // Check if enemy is in front of camera and within screen bounds
        bool inViewport = viewportPoint.z > 0 &&
                          viewportPoint.x > 0 && viewportPoint.x < 1 &&
                          viewportPoint.y > 0 && viewportPoint.y < 1;

        if (!inViewport) return false;

        // Check if within max distance
        float distance = Vector3.Distance(playerCamera.transform.position, enemy.position);
        if (distance > maxVisibleDistance) return false;

        // Optional line of sight check
        if (checkLineOfSight)
        {
            Vector3 direction = (enemy.position - playerCamera.transform.position).normalized;
            if (Physics.Raycast(playerCamera.transform.position, direction, out RaycastHit hit, maxVisibleDistance))
            {
                if (hit.transform != enemy)
                {
                    return false; // Something is blocking view
                }
            }
        }

        return true;
    }

}
