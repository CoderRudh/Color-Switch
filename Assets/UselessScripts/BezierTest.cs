using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierTest : MonoBehaviour
{
    [SerializeField]
    GameObject routeParent;

    List<Transform> controlPoints;
    Vector3 gameObjectPosition;

    bool initialized, coroutineAllowed;
    float t;

    void Start()
    {
        initialized = false;
        Init();
    }

    void Init()
    {
        t = 0;
        controlPoints = new List<Transform>();
        foreach(Transform child in routeParent.transform)
        {
            controlPoints.Add(child);
        }
        coroutineAllowed = true;
        initialized = true;
    }

    void Update()
    {
        if(initialized && coroutineAllowed)
        {
            coroutineAllowed = false;
            StartCoroutine(CreatePath());
        }
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

    IEnumerator CreatePath()
    {
        int n = controlPoints.Count;
        while(t < 1)
        {
            gameObjectPosition = Vector3.zero;
            t += Time.deltaTime * 0.2f;
            for(int i=0; i<n; ++i)
            {
                gameObjectPosition += nCr(n, i) * Mathf.Pow(1 - t, n - i) * Mathf.Pow(t, i) * controlPoints[i].position;
            }
            transform.position = gameObjectPosition;
            yield return new WaitForEndOfFrame();
        }
        t = 0f;
        coroutineAllowed = true;
    }

}
