using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierPathForRectangle : MonoBehaviour
{
    [SerializeField]
    GameObject routeParent;

    Vector2 gameObjectPosition;

    List<Transform> children; 

    bool coroutineAllowed;
    float speedModifier;
    float tParam;
    bool initialized;
    int childCount;
    // Start is called before the first frame update
    void Start()
    {
        initialized = false;
        coroutineAllowed = true;
        speedModifier = 0.2f;
        children = new List<Transform>();
        Init();
    }

    public static float AngleInRad(Vector3 vec1, Vector3 vec2)
    {
        return Mathf.Atan2(vec2.y - vec1.y, vec2.x - vec1.x);
    }

    public static float AngleInDeg(Vector3 vec1, Vector3 vec2)
    {
        return AngleInRad(vec1, vec2) * 180 / Mathf.PI;
    }

    void Init()
    {
        Vector2 P0 = routeParent.transform.GetChild(0).position;
        Vector2 P1 = routeParent.transform.GetChild(1).position;
        float angle = AngleInDeg(P0, P1);
        foreach (Transform child in transform)
        {
            children.Add(child.transform);
            child.transform.Rotate(0f, 0f, angle);
        }
        childCount = children.Count;
        initialized = true;
    }

    void Update()
    {
        if (initialized && coroutineAllowed)
            StartCoroutine(FollowRoute());
    }

    private IEnumerator FollowRoute()
    {
        coroutineAllowed = false;

        Vector2 P0 = routeParent.transform.GetChild(0).position;
        Vector2 P1 = routeParent.transform.GetChild(1).position;
        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;
            for(int i=0; i<childCount; ++i)
            {
                float t = tParam + (float)i/childCount;
                t = t % 1;
                gameObjectPosition = (1 - t) * P0 + t * P1;
                children[i].transform.position = gameObjectPosition;
            }

            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        coroutineAllowed = true;
    }
}
