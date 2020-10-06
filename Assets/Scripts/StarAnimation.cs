using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAnimation : MonoBehaviour
{
    float upScale, downScale;
    float t, transformSpeed;
    bool coroutineAllowed;
    // Start is called before the first frame update
    void Start()
    {
        coroutineAllowed = true;
        transformSpeed = 1.25f;
        //transform.localScale = new Vector3(transform.localScale.x * 1.2f, transform.localScale.y * 1.2f, transform.localScale.z);
    }

    void Update()
    {
        if(coroutineAllowed)
        {
            coroutineAllowed = false;
            StartCoroutine(AnimateStar());
        }
    }

    IEnumerator AnimateStar()
    {
        t = 0;
        upScale = 0.22f;
        downScale = 0.18f;
        Transform starTransform = transform;
        while(t < 1f)
        {
            t += Time.deltaTime * transformSpeed;
            
            transform.localScale = new Vector3(Mathf.Lerp(downScale, upScale, t), Mathf.Lerp(downScale, upScale, t), 1);
            yield return null;
        }
        t = 1;
        while(t > 0)
        {
            t -= Time.deltaTime * transformSpeed;
            transform.localScale = new Vector3(Mathf.Lerp(downScale, upScale, t), Mathf.Lerp(downScale, upScale, t), 1);
            yield return null;
        }
        coroutineAllowed = true;
    }

}
