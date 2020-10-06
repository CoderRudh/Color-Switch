using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonCircles : MonoBehaviour
{
    float rotateSpeed = 90f;

    void Update()
    {
        transform.GetChild(0).transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
        transform.GetChild(1).transform.Rotate(0f, 0f, -(rotateSpeed-10) * Time.deltaTime);
        transform.GetChild(2).transform.Rotate(0f, 0f, (rotateSpeed-20) * Time.deltaTime);

    }
}
