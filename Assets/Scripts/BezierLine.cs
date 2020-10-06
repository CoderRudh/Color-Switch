using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierLine : MonoBehaviour
{
    List<Transform> P;
    Vector2 gizmosPosition;

    void Start()
    {
    }

    void Init()
    {
        P = new List<Transform>();
        foreach (Transform child in transform)
            P.Add(child.transform);
    }

    private void OnDrawGizmos()
    {
        Init();
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmosPosition = (1 - t) * P[0].position + t * P[1].position;                 
            Gizmos.DrawSphere(gizmosPosition, 0.1f);
        }

        Vector2 point1 = new Vector2(P[0].position.x, P[0].position.y);
        Vector2 point2 = new Vector2(P[1].position.x, P[1].position.y);

        Gizmos.DrawLine(point1, point2);
    }
}
