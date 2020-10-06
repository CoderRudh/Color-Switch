using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierGizmosTest : MonoBehaviour
{
    List<Transform> P;
    Vector3 gizmosPosition;

    void Start()
    {
    }

    void Init()
    {
        P = new List<Transform>();
        foreach (Transform child in transform)
            P.Add(child.transform);
    }

    int Fact(int n)
    {
        if (n == 0)
            return 1;
        return n * Fact(n - 1);
    }

    int nCr(int n, int r)
    {
        return Fact(n) / (Fact(r) * Fact(n - r));
    }

    private void OnDrawGizmos()
    {
        Init();
        int n = P.Count;
        for (float t = 0; t <= 1; t += 0.05f)
        {
            for (int i = 0; i < n; ++i)
            {
                if(i==0)
                {
                    gizmosPosition = nCr(n, i) * Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i) * P[i].position;
                }
                else
                    gizmosPosition += nCr(n, i) * Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i) * P[i].position;
            }
            Gizmos.DrawSphere(gizmosPosition, 0.1f);
        }
       
    }
}
