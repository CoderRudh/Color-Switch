using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotator : MonoBehaviour
{
    float rotateSpeed = 90f;
    float logoSpeed = 100f;
    float rhombusSpeed = 80f;
    int rand;
    // Start is called before the first frame update
    void Start()
    {
        rand = Random.Range(0, 2) * 2 - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "Rhombus")
            transform.Rotate(0f, 0f, rand * rhombusSpeed * Time.deltaTime);
        else if (gameObject.tag == "BaseRotator")
            transform.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
        else if (gameObject.tag == "LogoLeft")
            transform.Rotate(0f, 0f, logoSpeed * Time.deltaTime);
        else if (gameObject.tag == "LogoRight")
            transform.Rotate(0f, 0f, -logoSpeed * Time.deltaTime);
        else if (gameObject.tag == "Joint")
        {
            if (gameObject.transform.childCount == 2)
            {
                gameObject.transform.GetChild(0).transform.Rotate(0f, 0f, rand * rotateSpeed * Time.deltaTime);
                gameObject.transform.GetChild(1).transform.Rotate(0f, 0f, rand * rotateSpeed * Time.deltaTime);
            }
            else if (gameObject.transform.childCount == 3)
            {
                gameObject.transform.GetChild(0).transform.Rotate(0f, 0f, rand * rotateSpeed * Time.deltaTime);
                gameObject.transform.GetChild(1).transform.Rotate(0f, 0f, rand * rotateSpeed * Time.deltaTime);
                gameObject.transform.GetChild(2).transform.Rotate(0f, 0f, rand * rotateSpeed * Time.deltaTime);
            }
        }
        else if (gameObject.tag == "JointHorizontal")
        {
            gameObject.transform.GetChild(0).transform.Rotate(0f, 0f, rand * rotateSpeed * Time.deltaTime);
            gameObject.transform.GetChild(1).transform.Rotate(0f, 0f, rand * rotateSpeed * Time.deltaTime);
        }
        else if (gameObject.tag == "SmallStar")
            gameObject.transform.Rotate(0f, 0f, 1.5f * rotateSpeed * Time.deltaTime);
        else
            transform.Rotate(0f, 0f, rand * rotateSpeed * Time.deltaTime);
    }
}
