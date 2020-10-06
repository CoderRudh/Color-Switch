using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePatterns : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject colorManager;

    //[System.NonSerialized]
    List<GameObject> spawnedObjects, spawnedColorChangers;

    [System.NonSerialized]
    Color playerColor;

    //int times = 1;
    int spawnOffset = 12, offsetParam = 0;
    int size, rand, colorChangerPos;
    float xOffset;
    bool coroutineAllowed, canDelete;

    BoxCollider2D cameraCollider;
    public static float cameraWidth;

    Object[] prefabs;
    GameObject temp;
    Colors colors;
    Player main;
    GameObject colorChanger;
    Vector3 spawnPosition;

    void Awake()
    {
        /*test = Resources.Load("SpinningWheel") as GameObject;
        Instantiate(test, new Vector3(0, 0, 0), Quaternion.identity);
        test.SetActive(true);
        Debug.Log(test);*/
        Init();
        spawnedObjects = new List<GameObject>();
        spawnedColorChangers = new List<GameObject>();
        coroutineAllowed = true;
        canDelete = true;
        prefabs = Resources.LoadAll("Prefabs", typeof(Object));
        colorChanger = Resources.Load("ColorChanger") as GameObject;
        size = prefabs.Length;
    }

    void Init()
    {
        main = player.GetComponent<Player>();
        colors = colorManager.GetComponent<Colors>();
        cameraCollider = Camera.main.GetComponent<BoxCollider2D>();
        cameraCollider.enabled = true;
        cameraWidth = cameraCollider.bounds.extents.x;
        cameraCollider.enabled = false;
    }

    public void Reset()
    {
        spawnOffset = 12;
        foreach (GameObject t in spawnedObjects)
            Destroy(t);
        if(spawnedColorChangers.Count > 0)
        {
            foreach (GameObject t in spawnedColorChangers)
            {
                if (t != null)
                    Destroy(t);
            }
        }
        spawnedColorChangers.Clear();
        spawnedObjects.Clear();
    }

    void CalculateOffsets(GameObject go)
    {
        xOffset = 0;
        if (go.tag == "SpinningWheel")
        {
            offsetParam = 3;
            xOffset = (Random.Range(0, 2) * 2 - 1) * 1.3f;
        }
        if (go.tag == "SmallCircle")
            offsetParam = 2;
        if (go.tag == "LargeCircle")
            offsetParam = 4;
        if (go.tag == "MediumCircle")
            offsetParam = 3;
        if (go.tag == "SquareLarge")
            offsetParam = 5;
        if (go.tag == "Joint")
        {
            if (go.transform.childCount == 3)
                offsetParam = 4;
            if (go.transform.childCount == 4)
            {
                if (go.name == "3JointNormalSmall")
                    offsetParam = 5;
                else
                    offsetParam = 7;
            }
        }
        if (go.tag == "JointHorizontal")
            offsetParam = 3;
        if (go.tag == "BezierRectangle")
            offsetParam = 4;
    }

    void Update()
    {
        if (player!=null && spawnOffset - player.transform.position.y < 10)
        {
            if (coroutineAllowed)
            {
                coroutineAllowed = false;
                StartCoroutine(SpawnObject());
            }
            //times--;
        }

        if (spawnedObjects.Count > 0 && Camera.main.transform.position.y - spawnedObjects[0].transform.position.y > 20)
        {
            //print(spawnedObjects.Count); 
            if (canDelete)
            {
                canDelete = false;
                StartCoroutine(DeleteObjects());
            }
        }
    }

    IEnumerator DeleteObjects()
    {
        float cameraPosition, objectPosition;
        cameraPosition = Camera.main.transform.position.y;
        objectPosition = spawnedObjects[0].transform.position.y;
        while (spawnedObjects.Count > 0 && (cameraPosition - objectPosition) > 10 && objectPosition<cameraPosition)
        {
            Debug.Log("BLIMEY");
            GameObject t = spawnedObjects[0];
            spawnedObjects.RemoveAt(0);
            Destroy(t);
            if (spawnedObjects.Count > 0)
                objectPosition = spawnedObjects[0].transform.position.y;
        }
        yield return new WaitForEndOfFrame();
        canDelete = true;
    }

    IEnumerator ConfigureColorForThree(GameObject go)
    {
        int random = Random.Range(0, 4);
        string col = "";
        switch(random)
        {
            case 0:
                playerColor = colors.colorPink;
                col = "Pink";
                break;
            case 1:
                playerColor = colors.colorBlue;
                col = "Blue";
                break;
            case 2:
                playerColor = colors.colorPurple;
                col = "Purple";
                break;
            case 3:
                playerColor = colors.colorYellow;
                col = "Yellow";
                break;
        }

        spawnPosition = new Vector3(0f, spawnOffset, 0f);
        GameObject t = Instantiate(go, spawnPosition, Quaternion.identity);
        Debug.Log(playerColor);
        Debug.Log(random);
        Debug.Log(col);
        t.transform.GetChild(3).GetComponent<ManualChange>().color = playerColor;
        t.transform.GetChild(3).GetComponent<ManualChange>().playerColor = col;
        t.transform.GetChild(0).transform.Rotate(0f, 0f, 90f * random);
        t.transform.GetChild(1).transform.Rotate(0f, 0f, 90f * random);
        t.transform.GetChild(2).transform.Rotate(0f, 0f, 90f * random);
        spawnedObjects.Add(t);
        spawnOffset += 6;
        yield return new WaitForEndOfFrame();
    }

    IEnumerator ConfigureColorForTwo (GameObject go)
    {
        int random = Random.Range(0, 4);
        string col = "";
        switch (random)
        {
            case 0:
                playerColor = colors.colorPink;
                col = "Pink";
                break;
            case 1:
                playerColor = colors.colorBlue;
                col = "Blue";
                break;
            case 2:
                playerColor = colors.colorPurple;
                col = "Purple";
                break;
            case 3:
                playerColor = colors.colorYellow;
                col = "Yellow";
                break;
        }

        spawnPosition = new Vector3(0f, spawnOffset, 0f);
        GameObject t = Instantiate(go, spawnPosition, Quaternion.identity);
        Debug.Log(playerColor);
        Debug.Log(random);
        Debug.Log(col);
        t.transform.GetChild(2).GetComponent<ManualChange>().color = playerColor;
        t.transform.GetChild(2).GetComponent<ManualChange>().playerColor = col;
        t.transform.GetChild(0).transform.Rotate(0f, 0f, 90f * random);
        t.transform.GetChild(1).transform.Rotate(0f, 0f, 90f * random);
        spawnedObjects.Add(t);
        spawnOffset += 6;
        yield return new WaitForEndOfFrame();
    }

    IEnumerator SpawnObject()
    {
        rand = Random.Range(0, size);
        temp = (GameObject)prefabs[rand];
        if(temp.tag == "ManualChange")
        {
            if (spawnOffset == 12)
                spawnOffset = 15;
            else
            {
                if (offsetParam == 2)
                   spawnOffset += 4;
                else
                    spawnOffset += 4;
            }
            if(temp.transform.childCount == 5)
                StartCoroutine(ConfigureColorForThree(temp));
            else
                StartCoroutine(ConfigureColorForTwo(temp));
            coroutineAllowed = true;
            yield break;
        }

        // Colour Changer spawn
        if (temp.tag == "SmallCircle")
            spawnOffset++;
        spawnPosition = new Vector3(0, spawnOffset, 0);
        GameObject t = Instantiate(colorChanger, spawnPosition, Quaternion.identity);
        spawnedColorChangers.Add(t);
        CalculateOffsets(temp);

        // Prefab Instantiate
        spawnOffset += offsetParam;
        spawnPosition = new Vector3(xOffset, spawnOffset, 0);
        GameObject clone = Instantiate(temp, spawnPosition, Quaternion.identity);
        spawnedObjects.Add(clone);
        if (offsetParam == 2)
            spawnOffset += offsetParam + 1;
        else
            spawnOffset += offsetParam;
        yield return new WaitForEndOfFrame();
        coroutineAllowed = true;
    }
}
