using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] PlatformPosition = new Transform[2];
    int direction = 1;
    public float speed;
    Vector2 target;
    // Update is called once per frame
    void Update()
    {
        target = currentMovementTarget();

        float distance = (target - (Vector2)PlatformPosition[0].position).magnitude;

        if (distance <= .5f)
        {
            direction *= -1;
        }
    }

    private void FixedUpdate()
    {
        PlatformPosition[0].position = Vector2.Lerp(PlatformPosition[0].position, target, speed * Time.deltaTime);
    }

    Vector2 currentMovementTarget()
    {
        if (direction == 1)
        {
            return PlatformPosition[1].position;
        }
        else
        {
            return PlatformPosition[2].position;
        }
    }

    private void OnDrawGizmos()
    {
        if (PlatformPosition[0] != null && PlatformPosition[1] != null && PlatformPosition[2] != null)
        {
            Gizmos.DrawLine(PlatformPosition[0].transform.position, PlatformPosition[1].transform.position);
            Gizmos.DrawLine(PlatformPosition[0].transform.position, PlatformPosition[2].transform.position);

        }
    }
}
