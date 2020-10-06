using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierPathForEachCircle : MonoBehaviour
{
    GameObject parent;

    BezierFollow parentClass;
    
    Vector2 gameObjectPosition, P0, P1, P2, P3;

    float tParam;
    bool coroutineAllowed;
    bool init = false;
    int currentRoute;

    // Start is called before the first frame update
    void Start()
    {
        parent = this.transform.parent.gameObject;
        parentClass = parent.GetComponent<BezierFollow>();
        //if (!parentClass.init)
        {
            //parentClass.Init();
            //parentClass.init = true;
        }
    }

    void Init()
    {
        tParam = transform.GetSiblingIndex();
        currentRoute = (int)tParam / parentClass.numOfChildrenPerCurve;
        tParam = tParam % parentClass.numOfChildrenPerCurve;
        tParam = (1f / parentClass.numOfChildrenPerCurve) * tParam;
        coroutineAllowed = true;
        init = true;
    }

    void Update()
    {
        if (!init && parentClass.init)
            Init();
        if (init && coroutineAllowed)
            StartCoroutine(FollowRoute(currentRoute));
    }

    private IEnumerator FollowRoute(int routeNumber)
    {
        coroutineAllowed = false;

        P0 = parentClass.routes[routeNumber].GetChild(0).position;
        P1 = parentClass.routes[routeNumber].GetChild(1).position;
        P2 = parentClass.routes[routeNumber].GetChild(2).position;
        P3 = parentClass.routes[routeNumber].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * parentClass.speedModifier;

            gameObjectPosition = Mathf.Pow(1 - tParam, 3) * P0                    // (1-t)^3 * P0
                             + 3 * Mathf.Pow(1 - tParam, 2) * tParam * P1         // 3 * (1-t)^2 * t * P1
                             + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * P2       // 3 * (1-t) * t^2 * P2
                             + Mathf.Pow(tParam, 3) * P3;                         // t^3 * P3

            transform.position = gameObjectPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        currentRoute += 1;
        if (currentRoute > parentClass.routes.Count - 1)
            currentRoute = 0;
        coroutineAllowed = true;
    }
}
