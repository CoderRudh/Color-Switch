using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualChange : MonoBehaviour
{
    [System.NonSerialized]
    public Color color;

    [System.NonSerialized]
    public string playerColor;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            Debug.Log(playerColor);
            col.gameObject.GetComponent<SpriteRenderer>().color = color;
            if(playerColor == "Blue")
                col.gameObject.GetComponent<Player>().colorSet = ColorSet.Blue;
            if (playerColor == "Purple")
                col.gameObject.GetComponent<Player>().colorSet = ColorSet.Purple;
            if (playerColor == "Yellow")
                col.gameObject.GetComponent<Player>().colorSet = ColorSet.Yellow;
            if (playerColor == "Pink")
                col.gameObject.GetComponent<Player>().colorSet = ColorSet.Pink;
        }
        FindObjectOfType<AudioManager>().Play("colorswitch");
        Destroy(gameObject);
    }

}
