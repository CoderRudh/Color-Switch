using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierFollow : MonoBehaviour
{
    [SerializeField]
    public GameObject routeParent;
    
    [System.NonSerialized]
    public List<Transform> routes;

    Vector2 gameObjectPosition;

    [System.NonSerialized]
    public int numOfChildrenPerCurve;

    [System.NonSerialized]
    public float speedModifier;

    [System.NonSerialized]
    public bool init = true;

    int totalChildren;

    void Start()
    {
        Init();
    }
    
    public void Init()
    {
        routes = new List<Transform>();
        //InitializeChildren();
        speedModifier = 0.8f;
        numOfChildrenPerCurve = transform.childCount/4;
        foreach (Transform child in routeParent.transform)
        {
            routes.Add(child.transform);
        }
        init = true;
    }
    
}
