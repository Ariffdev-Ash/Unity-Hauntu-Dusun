using UnityEngine;

public class ProjectileBoundaryCleaner : MonoBehaviour
{
    [Header("Map Boundaries")]
    public float minX = -20f;
    public float maxX = 20f;
    public float minY = -10f;
    public float maxY = 10f;

    void Update()
    {
        Vector3 pos = transform.position;

        if (pos.x < minX || pos.x > maxX || pos.y < minY || pos.y > maxY)
        {
            Destroy(gameObject);
        }
    }
}
