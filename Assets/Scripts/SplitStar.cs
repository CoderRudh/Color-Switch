using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitStar : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("LOL");
        if (col.gameObject.name == "Player")
            Destroy(this.gameObject);
    }
}
