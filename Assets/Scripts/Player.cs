using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

//public enum Colors { Blue, Yellow, Pink, Purple };

public class Player : MonoBehaviour
{
    private string colorChanger = "ColorChanger";
    private string manualChanger = "ManualColorChanger";

    [SerializeField]
    GameObject colorManager;

    [SerializeField]
    GameObject smallStar;

    [SerializeField]
    GameObject plusOne;

    [SerializeField]
    GameObject Score;

    [SerializeField]
    List<GameObject> baseStars;

    [SerializeField]
    GameObject generatePatterns;

    [SerializeField]
    GameObject gameManager;

    List<GameObject> starClones;
    Colors colors;

    [System.NonSerialized]
    public ColorSet colorSet;

    SpriteRenderer spriteRenderer;
    Vector3 snapPosition = new Vector3(0f, -4f, 0f);
    Vector3 initialPos, cameraPos;
    GameObject currentStar, audioManager;

    [System.NonSerialized]
    public bool canJump;

    float jumpForce = 8f, firstJumpForce = 12f;
    bool firstJump;
    string tempColor;
    string currentColor;
    int colorIndex;
    int starSpeed;
    int score;
    private int numOfStars;
    Rigidbody2D player;
    
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>().gameObject;
        initialPos = transform.position;
        cameraPos = Camera.main.transform.position;
        canJump = false;
        starSpeed = 50;
        starClones = new List<GameObject>();
        firstJump = true;
        colorIndex = UnityEngine.Random.Range(0, 4);
        player = GetComponent<Rigidbody2D>();
        colors = colorManager.GetComponent<Colors>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        numOfStars = 14;
        //StarCloneInit();
        colors.Init();
        SetRandomColor();
    }
    
    void Update()
    {
        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Check if finger is over a UI element
            if (!(EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) && EventSystem.current.currentSelectedGameObject != null &&
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null))
            {
                audioManager.GetComponent<AudioManager>().Play("jump");
                if (firstJump)
                {
                    player.velocity = Vector2.up * firstJumpForce;
                    firstJump = false;
                }
                else
                    player.velocity = Vector2.up * jumpForce;
            }
        }
        
        /*if (canJump && Input.GetMouseButtonDown(0))// || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (!(EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject != null &&
            EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null))
            {
                audioManager.GetComponent<AudioManager>().Play("jump");
                if (firstJump)
                {
                    player.velocity = Vector2.up * firstJumpForce;
                    firstJump = false;
                }
                else
                    player.velocity = Vector2.up * jumpForce;
            }
        }*/
        if(transform.position.y < -4)
        {
            transform.position = snapPosition;
        }
        if(Camera.main.transform.position.y - gameObject.transform.position.y > 5)
        {
            gameManager.GetComponent<GameManager>().ExplodePlayer(gameObject.transform);
            //Reset();
            gameObject.SetActive(false);
        }
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Reset()
    {
        
        gameObject.SetActive(true);
        transform.position = initialPos;
        Camera.main.transform.position = cameraPos;
        //StopCoroutine(SpawnStars(currentStar.transform));
        foreach (GameObject s in baseStars)
            s.SetActive(true);
        SetRandomColor();
        //GeneratePatterns gp = new GeneratePatterns();
        //generatePatterns.GetComponent<GeneratePatterns>().spawnedObjects.Clear();
        //gp.spawnedObjects.Clear();
        Score.GetComponent<TMPro.TextMeshProUGUI>().text = "0";
        generatePatterns.GetComponent<GeneratePatterns>().Reset();
    }

    void StarCloneInit()
    {
        smallStar.SetActive(false);
        for(int i=0; i<numOfStars; ++i)
        {
            GameObject temp = Instantiate(smallStar, Vector3.zero, Quaternion.identity);
            starClones.Add(temp);
        }
        //Debug.Log(starClones.Count);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        /*if(col.tag == manualChanger)
        {
            spriteRenderer.color = colors.manualColor;
            Destroy(col.gameObject);
            return;
        }*/
        if(col.tag == colorChanger)
        {
            audioManager.GetComponent<AudioManager>().Play("colorswitch");
            SetRandomColor();
            Destroy(col.gameObject);
            return;
        }
        if(col.tag != manualChanger && col.tag != "Star" && col.tag != colorSet.ToString())
        {
            //Debug.LogFormat("Current color: {0}", currentColor);
            //Debug.LogFormat("ColorSet value: {0}", colorSet);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            //Reset();
            gameManager.GetComponent<GameManager>().ExplodePlayer(gameObject.transform);
            gameObject.SetActive(false);
            return;
        }
        if(col.tag == "Star")
        {
            audioManager.GetComponent<AudioManager>().Play("star");
            currentStar = col.gameObject;
            //Vector2 pos = UnityEngine.Random.insideUnitCircle;
            //Debug.Log(int.Parse(GetComponent<TMPro.TextMeshProUGUI>().text));
            score = int.Parse(Score.GetComponent<TMPro.TextMeshProUGUI>().text) + 1;
            Score.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();
            GameManager obj = gameManager.GetComponent<GameManager>();
            obj.starsCollected++;
            obj.StartCoroutine(obj.SpawnStars(col.transform));
            obj.StartCoroutine(obj.SpawnPlusOne(col.gameObject));
            obj.StartCoroutine(obj.AnimatePlusOne());
            if (col.gameObject.name == "BaseStar1" || col.gameObject.name == "BaseStar2")
                col.gameObject.SetActive(false);
            else
                Destroy(col.gameObject);
            //Instantiate(smallStar, UnityEngine.Random.insideUnitCircle * 1/2f, Quaternion.identity);
        }
    }
    /*
    IEnumerator AnimatePlusOne()
    {
        float t = 0, transformSpeed = 10f ;
        float downScale, upScale;
        downScale = plusOne.transform.localScale.x;
        upScale = plusOne.transform.localScale.x + 0.01f;
        while (t < 1f)
        {
            t += Time.deltaTime * transformSpeed;

            plusOne.transform.localScale = new Vector3(Mathf.Lerp(downScale, upScale, t), Mathf.Lerp(downScale, upScale, t), 1);
            yield return null;
        }
        t = 1;
        while (t > 0)
        {
            t -= Time.deltaTime * transformSpeed;
            plusOne.transform.localScale = new Vector3(Mathf.Lerp(downScale, upScale, t), Mathf.Lerp(downScale, upScale, t), 1);
            yield return null;
        }
    }

    IEnumerator SpawnPlusOne(GameObject star)
    {
        float alpha;// = plusOne.GetComponent<SpriteRenderer>().color.a;
        float t = 0, y0, y1;
        y0 = star.transform.position.y;
        y1 = star.transform.position.y + 0.8f;
        plusOne.transform.position = star.transform.position;
        Color newColor;
        while(t<1)
        {
            t += Time.deltaTime * 2f;
            alpha = plusOne.GetComponent<TMPro.TextMeshProUGUI>().color.a;
            newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1f, t));
            plusOne.GetComponent<TMPro.TextMeshProUGUI>().color = newColor;
            plusOne.transform.position = new Vector3(plusOne.transform.position.x, Mathf.SmoothStep(y0, y1, t), plusOne.transform.position.z);
            yield return null;
        }
        t = 1;
        y0 = y1;
        y1 = y0 + 0.3f;
        while (t>0)
        {
            t -= Time.deltaTime * 1.5f;
            alpha = plusOne.GetComponent<TMPro.TextMeshProUGUI>().color.a;
            newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1f, t));
            plusOne.GetComponent<TMPro.TextMeshProUGUI>().color = newColor;
            plusOne.transform.position = new Vector3(plusOne.transform.position.x, Mathf.Lerp(y0, y1, 1-t), plusOne.transform.position.z);
            yield return null;
        }
    }

    IEnumerator SpawnStars(Transform bigStar)
    {
        float aTime = 1f;
        float alpha;
        Color newColor;
        GameObject temp;
        foreach (GameObject star in starClones)
        {
            temp = star;//Instantiate(smallStar, UnityEngine.Random.insideUnitCircle * 1 / 2f, Quaternion.identity);
            temp.transform.position = UnityEngine.Random.insideUnitCircle / 2;
            temp.transform.position += bigStar.transform.position;
            //Debug.Log(temp.GetComponent<SpriteRenderer>().color.a);
            temp.SetActive(true);
        }
        for (float t = 0.0f; t < 0.5f; t += Time.deltaTime / aTime)
        {
            foreach(GameObject star in starClones)
            {
                alpha = star.GetComponent<SpriteRenderer>().color.a;
                newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, t, aTime));
                star.GetComponent<SpriteRenderer>().color = newColor;
            }
            yield return null;
        }
        for (float t = 0.5f; t > 0.0f; t -= Time.deltaTime)
        {
            foreach (GameObject star in starClones)
            {
                alpha = star.GetComponent<SpriteRenderer>().color.a;
                newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, t, aTime));
                star.GetComponent<SpriteRenderer>().color = newColor;
            }
            yield return null;
        }
        //yield return new WaitForEndOfFrame();
    }
    */
    void SetRandomColor()
    {
        //Debug.LogFormat("Size of ColorChoice: {0}", colors.colorChoice.Count);
        //Debug.LogFormat("Index picked: {0}: ", colorIndex);
        tempColor = colors.colorChoice[colorIndex];
        //Debug.LogFormat("Current color: {0}", tempColor);
        colorSet = colors.colorDict[tempColor];
        switch (colorSet)
        {
            case ColorSet.Blue:
                spriteRenderer.color = colors.colorBlue;
                break;
            case ColorSet.Yellow:
                spriteRenderer.color = colors.colorYellow;
                break;
            case ColorSet.Pink:
                spriteRenderer.color = colors.colorPink;
                break;
            case ColorSet.Purple:
                spriteRenderer.color = colors.colorPurple;
                break;
        }
        colors.colorChoice.RemoveAt(colorIndex);
        colorIndex = UnityEngine.Random.Range(0, colors.colorChoice.Count);
        colors.colorChoice.Add(tempColor);
    }
}
