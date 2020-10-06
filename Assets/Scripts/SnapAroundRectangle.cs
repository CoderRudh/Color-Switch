using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapAroundRectangle : MonoBehaviour
{
    BoxCollider2D rect;
    
    GameObject child1, child2, child3, child4;
    
    float moveSpeed = 3f;
    float rectangleWidth;
    float leftMostPosition;
    float rightMostPosition;
    // Start is called before the first frame update
    void Start()
    {
        child1 = gameObject.transform.GetChild(0).gameObject;
        child2 = gameObject.transform.GetChild(1).gameObject;
        child3 = gameObject.transform.GetChild(2).gameObject;
        child4 = gameObject.transform.GetChild(3).gameObject;
        Init();
        SetPositions(child1, rectangleWidth);
        SetPositions(child2, rectangleWidth * 3f);
        SetPositions(child3, rectangleWidth * 5f);
        SetPositions(child4, rectangleWidth * 7f);
    }

    void SetPositions(GameObject child, float offset)
    {
        Vector3 position = new Vector3(-GeneratePatterns.cameraWidth + offset, transform.position.y, transform.position.z);
        child.transform.position = position;
    }

    void Init()
    {
        
        rect = gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
        
        rectangleWidth = rect.bounds.extents.x;
        leftMostPosition = -GeneratePatterns.cameraWidth - rectangleWidth;
        rightMostPosition = -leftMostPosition;
    }

    void moveObject(GameObject child)
    {
        child.transform.Translate(-moveSpeed * Time.deltaTime, 0f, 0f);
        if (child.transform.position.x < leftMostPosition)
            child.transform.position = new Vector3(rightMostPosition, child.transform.position.y, child.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        moveObject(child1);
        moveObject(child2);
        moveObject(child3);
        moveObject(child4);
    }
}
