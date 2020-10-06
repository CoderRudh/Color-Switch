using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleCircles : MonoBehaviour
{
    GameObject child1;
    GameObject child2;
    float rotateSpeed = 80f;
    int rand;
    // Start is called before the first frame update
    void Start()
    {
        rand = Random.Range(0, 2) * 2 - 1;
        child1 = transform.GetChild(0).gameObject;
        child2 = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        child1.transform.Rotate(0f, 0f, rand * rotateSpeed * Time.deltaTime);
        child2.transform.Rotate(0f, 0f, rand * rotateSpeed * Time.deltaTime);
    }
}
