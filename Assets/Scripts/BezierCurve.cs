using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
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
            gizmosPosition = Mathf.Pow(1 - t, 3) * P[0].position                // (1-t)^3 * P0
                             + 3 * Mathf.Pow(1 - t, 2) * t * P[1].position      // 3 * (1-t)^2 * t * P1
                             + 3 * (1 - t) * Mathf.Pow(t, 2) * P[2].position      // 3 * (1-t) * t^2 * P2
                             + Mathf.Pow(t, 3) * P[3].position;                 // t^3 * P3
            Gizmos.DrawSphere(gizmosPosition, 0.1f);
        }

        Vector2 point1 = new Vector2(P[0].position.x, P[0].position.y);
        Vector2 point2 = new Vector2(P[1].position.x, P[1].position.y);
        Vector2 point3 = new Vector2(P[2].position.x, P[2].position.y);
        Vector2 point4 = new Vector2(P[3].position.x, P[3].position.y);

        Gizmos.DrawLine(point1, point2);
        Gizmos.DrawLine(point3, point4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
