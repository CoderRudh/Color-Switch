using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Button playButton;

    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject startGameAnimator;

    [SerializeField]
    GameObject endGameAnimator;

    [SerializeField]
    GameObject PauseButton;
    //, ResumeButton, HomeButton;

    [SerializeField]
    GameObject PauseScreen;

    [SerializeField]
    GameObject ResumeButton;

    [SerializeField]
    GameObject HomeButton;

    [SerializeField]
    GameObject BackButton;

    [SerializeField]
    GameObject RestartButton;

    [SerializeField]
    GameObject InGamePanel;

    [SerializeField]
    GameObject ScoreScreen;

    [SerializeField]
    GameObject HomeAfterGamePanel;

    [SerializeField]
    GameObject smallBall;
    List<GameObject> smallBalls;

    [SerializeField]
    GameObject smallStar;

    [SerializeField]
    GameObject plusOne;

    [SerializeField]
    GameObject SettingsButton;

    [SerializeField]
    GameObject SettingsPanel;

    List<GameObject> starClones;

    GameObject SettingsBack, PlayOrStopMusic, PlayOrStopSounds, MusicButton, SoundButton;
    GameObject play, pause, home;

    GameObject audioManager, colorManager;

    Color pauseScreenColor, playColor, pauseColor, homeColor;
    GameObject Color, Switch, Replica, Circles, Score, ScoreText, BestScore, BestScoreText;

    Button playOrStopMusic, playOrStopSounds;

    //Animator settingsAnimation;
    
    int numOfSmallBalls, numOfStars, bestScore;
    float musicAlpha, soundAlpha;
    bool soundOn, musicOn;

    void Start()
    {
        bestScore = 0;
        soundOn = true;
        musicOn = true;
        musicAlpha = 1;
        soundAlpha = 1;
        audioManager = FindObjectOfType<AudioManager>().gameObject;
        colorManager = FindObjectOfType<Colors>().gameObject;
        Button startButton = playButton.GetComponent<Button>();
        startButton.onClick.AddListener(StartGame);
        Button pauseButton = PauseButton.GetComponent<Button>();
        pauseButton.onClick.AddListener(PauseGame);
        Button resumeButton = ResumeButton.GetComponent<Button>();
        resumeButton.onClick.AddListener(ResumeGame);
        Button homeButton = HomeButton.GetComponent<Button>();
        homeButton.onClick.AddListener(GoHome);
        Button backButton = BackButton.GetComponent<Button>();
        backButton.onClick.AddListener(HomeAfterGameOver);
        Button restartButton = RestartButton.GetComponent<Button>();
        restartButton.onClick.AddListener(RestartGame);
        Init();
        SettingsInit();
        Button settingsButton = SettingsButton.GetComponent<Button>();
        settingsButton.onClick.AddListener(ToSettingsScreen);
        Button settingsBack = SettingsBack.GetComponent<Button>();
        settingsBack.onClick.AddListener(SettingsToMain);
        playOrStopMusic = PlayOrStopMusic.GetComponent<Button>();
        playOrStopMusic.onClick.AddListener(ChangeMusicSettings);
        playOrStopSounds = PlayOrStopSounds.GetComponent<Button>();
        playOrStopSounds.onClick.AddListener(ChangeSoundSettings);
        
        
        //smallBall.gameObject.GetComponent<Rigidbody2D>().velocity = direction * 10f;
        // Time.timeScale = 0f;
    }

    void Init()
    {
        numOfStars = 14;
        starClones = new List<GameObject>();
        smallStar.SetActive(false);
        for (int i = 0; i < numOfStars; ++i)
        {
            GameObject temp = Instantiate(smallStar, Vector3.zero, Quaternion.identity);
            starClones.Add(temp);
        }
        smallBalls = new List<GameObject>();
        numOfSmallBalls = 30;
        smallBall.SetActive(false);
        for(int i=0; i<numOfSmallBalls; ++i)
        {
            GameObject t = Instantiate(smallBall, Vector3.zero, Quaternion.identity);
            smallBalls.Add(t);
        }
        play = PauseScreen.transform.GetChild(0).gameObject;
        pause = PauseScreen.transform.GetChild(1).gameObject;
        home = PauseScreen.transform.GetChild(2).gameObject;
        pauseScreenColor = PauseScreen.GetComponent<Image>().color;
        playColor = play.GetComponent<Image>().color;
        pauseColor = pause.GetComponent<TMPro.TextMeshProUGUI>().color;
        homeColor = home.GetComponent<Image>().color;
    }

    void RestartGame()
    {
        audioManager.GetComponent<AudioManager>().Play("button");
        StartCoroutine(GameRestart());
    }

    void HomeAfterGameOver()
    {
        audioManager.GetComponent<AudioManager>().Play("button");
        StartCoroutine(HomeAfterGame());
    }

    void GoHome()
    {
        audioManager.GetComponent<AudioManager>().Play("button");
        StartCoroutine(GoToHome());
    }

    void StartGame()
    {
        audioManager.GetComponent<AudioManager>().Play("button");
        StartCoroutine(GameStart());
        
        //Time.timeScale = 1f;
    }

    void PauseGame()
    {
        audioManager.GetComponent<AudioManager>().Play("button");
        StartCoroutine(GamePause());
    }

    void ResumeGame()
    {
        audioManager.GetComponent<AudioManager>().Play("button");
        StartCoroutine(GameResume());
    }

    void ToSettingsScreen()
    {
        audioManager.GetComponent<AudioManager>().Play("button");
        StartCoroutine(GoToSettings());
        //settingsAnimation.SetBool("goBack", false);
        //SettingsPanel.SetActive(true);
        //settingsAnimation.Play("SettingsAnimation");
    }

    void SettingsToMain()
    {
        audioManager.GetComponent<AudioManager>().Play("button");
        StartCoroutine(BackToMain());
        //settingsAnimation.SetBool("goBack", true);
        //settingsAnimation.Play("BackToMain");
    }

    void ChangeMusicSettings()
    {
        audioManager.GetComponent<AudioManager>().Play("button");
        Color musicColor = MusicButton.GetComponent<Image>().color;
        if (musicOn)
        {
            musicOn = false;
            audioManager.GetComponent<AudioManager>().StopMusic();
            playOrStopMusic.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "PLAY MUSIC";
            musicColor = new Color(musicColor.r, musicColor.g, musicColor.b, 0.33f);
            musicAlpha = 0.33f;
            MusicButton.GetComponent<Image>().color = musicColor;
        }
        else
        {
            musicOn = true;
            audioManager.GetComponent<AudioManager>().PlayMusic();
            playOrStopMusic.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "STOP MUSIC";
            musicColor = new Color(musicColor.r, musicColor.g, musicColor.b, 1f);
            musicAlpha = 1f;
            MusicButton.GetComponent<Image>().color = musicColor;
        }
    }

    void ChangeSoundSettings()
    {
        audioManager.GetComponent<AudioManager>().Play("button");
        Color soundColor = SoundButton.GetComponent<Image>().color;
        if (soundOn)
        {
            soundOn = false;
            audioManager.GetComponent<AudioManager>().StopSounds();
            playOrStopSounds.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "PLAY SOUNDS";
            soundAlpha = 0.33f;
            soundColor = new Color(soundColor.r, soundColor.g, soundColor.b, 0.33f);
        }
        else
        {
            soundOn = true;
            audioManager.GetComponent<AudioManager>().PlaySounds();
            playOrStopSounds.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "STOP SOUNDS";
            soundAlpha = 1f;
            soundColor = new Color(soundColor.r, soundColor.g, soundColor.b, 1f);
        }
        SoundButton.GetComponent<Image>().color = soundColor;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
        }
    }

    void SettingsInit()
    {
        SettingsBack = SettingsPanel.transform.GetChild(1).gameObject;
        PlayOrStopMusic = SettingsPanel.transform.GetChild(2).gameObject;
        PlayOrStopSounds = SettingsPanel.transform.GetChild(3).gameObject;
        MusicButton = SettingsPanel.transform.GetChild(4).gameObject;
        Debug.Log(MusicButton.name);
        SoundButton = SettingsPanel.transform.GetChild(5).gameObject;
        //settingsAnimation = SettingsPanel.GetComponent<Animator>();
    }

    public void ExplodePlayer(Transform player)
    {
        //Camera.main.transform.GetComponent<EdgeCollider2D>();
        ColorSet colorSet;
        Colors colors = colorManager.GetComponent<Colors>();
        int randColor;
        float rand;
        foreach (GameObject t in smallBalls)
        {
            rand = Random.Range(0.4f, 0.9f);
            randColor = Random.Range(0, 4);
            t.transform.localScale = new Vector3(rand, rand, 0);
            t.SetActive(true);
            t.transform.position = player.position;
            colorSet = (ColorSet)randColor;
            switch(colorSet)
            {
                case ColorSet.Blue:
                    t.GetComponent<SpriteRenderer>().color = colors.colorBlue;
                    break;
                case ColorSet.Pink:
                    t.GetComponent<SpriteRenderer>().color = colors.colorPink;
                    break;
                case ColorSet.Purple:
                    t.GetComponent<SpriteRenderer>().color = colors.colorPurple;
                    break;
                case ColorSet.Yellow:
                    t.GetComponent<SpriteRenderer>().color = colors.colorYellow;
                    break;
            }
            float theta = Random.Range(0, 360);
            Vector2 direction = new Vector2(Mathf.Cos(theta * Mathf.Deg2Rad), Mathf.Sin(theta * Mathf.Deg2Rad));
            t.GetComponent<Rigidbody2D>().velocity = direction * 10f;
        }
        StartCoroutine(SetScoreScreen());
        
    }
    
    IEnumerator GoToSettings()
    {

        Component[] images = SettingsPanel.GetComponentsInChildren<Image>();
        Component[] texts = SettingsPanel.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
        float t = 0;
        //InGamePanel.SetActive(true);
        SettingsPanel.SetActive(true);
        while (t < 1)
        {
            t += Time.unscaledDeltaTime * 4f;
            foreach (Image image in images)
            {
                if(image.name == "Music")
                    image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0, musicAlpha, t));
                else if (image.name == "Sound")
                    image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0, soundAlpha, t));
                else
                    image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0, 1, t));
            }
            foreach (TMPro.TextMeshProUGUI text in texts)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(0, 1, t));
            }
            SettingsPanel.GetComponent<Image>().color = new Color(SettingsPanel.GetComponent<Image>().color.r,
                                                                SettingsPanel.GetComponent<Image>().color.g,
                                                                SettingsPanel.GetComponent<Image>().color.b, Mathf.Lerp(0, 1, t));
            //play.GetComponent<Image>().color = new Color(playColor.r, playColor.g, playColor.b, Mathf.Lerp(0, 1, t));
            //pause.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(pauseColor.r, pauseColor.g, pauseColor.b, Mathf.Lerp(0, 1, t));
            //home.GetComponent<Image>().color = new Color(homeColor.r, homeColor.g, homeColor.b, Mathf.Lerp(0, 1, t));
            yield return null;
        }
        //SettingsPanel.SetActive(false);
        //yield return new WaitForSecondsRealtime(0.5f);
        //player.GetComponent<Player>().canJump = true;
    }

    IEnumerator BackToMain()
    {

        Component[] images = SettingsPanel.GetComponentsInChildren<Image>();
        Component[] texts = SettingsPanel.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
        float t = 1;
        //InGamePanel.SetActive(true);
        //SettingsPanel.SetActive(true);
        while (t > 0)
        {
            t -= Time.unscaledDeltaTime * 4f;
            foreach (Image image in images)
            {
                if (image.name == "Music")
                    image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0, musicAlpha, t));
                else if (image.name == "Sound")
                    image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0, soundAlpha, t));
                else
                    image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0, 1, t));
            }
            foreach (TMPro.TextMeshProUGUI text in texts)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(0, 1, t));
            }
            SettingsPanel.GetComponent<Image>().color = new Color(SettingsPanel.GetComponent<Image>().color.r,
                                                                SettingsPanel.GetComponent<Image>().color.g,
                                                                SettingsPanel.GetComponent<Image>().color.b, Mathf.Lerp(0, 1, t));
            //play.GetComponent<Image>().color = new Color(playColor.r, playColor.g, playColor.b, Mathf.Lerp(0, 1, t));
            //pause.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(pauseColor.r, pauseColor.g, pauseColor.b, Mathf.Lerp(0, 1, t));
            //home.GetComponent<Image>().color = new Color(homeColor.r, homeColor.g, homeColor.b, Mathf.Lerp(0, 1, t));
            yield return null;
        }
        SettingsPanel.SetActive(false);
        //yield return new WaitForSecondsRealtime(0.5f);
        //player.GetComponent<Player>().canJump = true;
    }

    IEnumerator SetScoreScreen()
    {
        TMPro.TextMeshProUGUI scoreText, inGameText;
        scoreText = ScoreScreen.transform.Find("ScoreText").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        inGameText = InGamePanel.transform.Find("Score").gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        
        //SETTING SCORE TEXT
        ScoreScreen.transform.Find("ScoreText").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = inGameText.text;

        //SETTING BEST SCORE TEXT
        int score = int.Parse(scoreText.text);
        bestScore = Mathf.Max(bestScore, score);
        ScoreScreen.transform.Find("BestScoreText").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = bestScore.ToString();

        Component[] images = ScoreScreen.GetComponentsInChildren<Image>();
        Component[] texts = ScoreScreen.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
        foreach (Image image in images)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        }
        foreach (TMPro.TextMeshProUGUI text in texts)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        }
        ScoreScreen.GetComponent<Image>().color = new Color(ScoreScreen.GetComponent<Image>().color.r,
                                                                ScoreScreen.GetComponent<Image>().color.g,
                                                                ScoreScreen.GetComponent<Image>().color.b, 1);
        yield return new WaitForSeconds(2);
        ScoreScreen.SetActive(true);
    }

    IEnumerator GameRestart()
    {
        player.GetComponent<Player>().Reset();
        Component[] images = ScoreScreen.GetComponentsInChildren<Image>();
        Component[] texts = ScoreScreen.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
        float t = 1;
        InGamePanel.SetActive(true);
        while (t > 0)
        {
            t -= Time.unscaledDeltaTime * 4f;
            foreach (Image image in images)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0, 1, t));
            }
            foreach (TMPro.TextMeshProUGUI text in texts)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(0, 1, t));
            }
            ScoreScreen.GetComponent<Image>().color = new Color(ScoreScreen.GetComponent<Image>().color.r, 
                                                                ScoreScreen.GetComponent<Image>().color.g, 
                                                                ScoreScreen.GetComponent<Image>().color.b, Mathf.Lerp(0, 1, t));
            //play.GetComponent<Image>().color = new Color(playColor.r, playColor.g, playColor.b, Mathf.Lerp(0, 1, t));
            //pause.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(pauseColor.r, pauseColor.g, pauseColor.b, Mathf.Lerp(0, 1, t));
            //home.GetComponent<Image>().color = new Color(homeColor.r, homeColor.g, homeColor.b, Mathf.Lerp(0, 1, t));
            yield return null;
        }
        ScoreScreen.SetActive(false);
        yield return new WaitForSecondsRealtime(0.5f);
        player.GetComponent<Player>().canJump = true;
        //Time.timeScale = 1f;

    }

    IEnumerator HomeAfterGame()
    {
        float t = 0;
        HomeAfterGamePanel.SetActive(true);
        Time.timeScale = 1f;
        Color color = HomeAfterGamePanel.GetComponent<Image>().color;
        while (t < 1)
        {
            t += Time.unscaledDeltaTime * 10f;
            //alpha = Mathf.Lerp(0, 1, t);
            HomeAfterGamePanel.GetComponent<Image>().color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, 1, t));
            yield return null;
        }
        player.GetComponent<Player>().canJump = true;
        mainMenu.SetActive(true);
        InGamePanel.SetActive(false);
        ScoreScreen.SetActive(false);
        //PauseScreen.SetActive(false);
        t = 0;
        while (t < 1)
        {
            t += Time.unscaledDeltaTime * 1f;
            yield return null;
        }
        t = 1;
        while (t > 0)
        {
            t -= Time.unscaledDeltaTime * 5f;
            HomeAfterGamePanel.GetComponent<Image>().color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, 1, t));
            yield return null;
        }
        player.GetComponent<Player>().Reset();
        HomeAfterGamePanel.SetActive(false);
    }

    IEnumerator GoToHome()
    {
        float t = 0;
        endGameAnimator.SetActive(true);
        Time.timeScale = 1f;
        Color color = endGameAnimator.GetComponent<Image>().color;
        while (t < 1)
        {
            t += Time.unscaledDeltaTime * 10f;
            //alpha = Mathf.Lerp(0, 1, t);
            endGameAnimator.GetComponent<Image>().color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, 1, t));
            yield return null;
        }
        player.GetComponent<Player>().canJump = true;
        mainMenu.SetActive(true);
        InGamePanel.SetActive(false);
        PauseScreen.SetActive(false);
        t = 0;
        while (t < 1)
        {
            t += Time.unscaledDeltaTime * 1f;
            yield return null;
        }
        t = 1;
        while (t > 0)
        {
            t -= Time.unscaledDeltaTime * 5f;
            endGameAnimator.GetComponent<Image>().color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, 1, t));
            yield return null;
        }
        endGameAnimator.SetActive(false);
        //player.SetActive(true);
    }

    IEnumerator GameResume()
    {        
        float t = 3f/4;
        while (t > 0)
        {
            t -= Time.unscaledDeltaTime * 8f;
            PauseScreen.GetComponent<Image>().color = new Color(pauseScreenColor.r, pauseScreenColor.g, pauseScreenColor.b, Mathf.Lerp(0, 1, t));
            play.GetComponent<Image>().color = new Color(playColor.r, playColor.g, playColor.b, Mathf.Lerp(0, 1, t * 4 / 3f));
            pause.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(pauseColor.r, pauseColor.g, pauseColor.b, Mathf.Lerp(0, 1, t * 4 / 3f));
            home.GetComponent<Image>().color = new Color(homeColor.r, homeColor.g, homeColor.b, Mathf.Lerp(0, 1, t * 4 / 3f));
            yield return null;
        }
        PauseScreen.SetActive(false);
        yield return new WaitForSecondsRealtime(0.5f);
        player.GetComponent<Player>().canJump = true;
        Time.timeScale = 1f;
    }

    IEnumerator GamePause()
    {
        //yield return new WaitForSecondsRealtime(1);
        player.GetComponent<Player>().canJump = false;
        PauseScreen.SetActive(true);
        float t = 0;
        Time.timeScale = 0f;
        while (t < 3/4f)
        {
            t += Time.unscaledDeltaTime * 8f;
            PauseScreen.GetComponent<Image>().color = new Color(pauseScreenColor.r, pauseScreenColor.g, pauseScreenColor.b, Mathf.Lerp(0, 1, t));
            play.GetComponent<Image>().color = new Color(playColor.r, playColor.g, playColor.b, Mathf.Lerp(0, 1, t*4/3f));
            pause.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(pauseColor.r, pauseColor.g, pauseColor.b, Mathf.Lerp(0, 1, t*4/3f));
            home.GetComponent<Image>().color = new Color(homeColor.r, homeColor.g, homeColor.b, Mathf.Lerp(0, 1, t*4/3f));
            yield return null;
        }
        //yield return WaitForSeconds(1f);
        //play = 
    }

    IEnumerator GameStart()
    {
        float t = 0;
        player.GetComponent<Player>().Reset();
        player.SetActive(true);
        startGameAnimator.SetActive(true);
        Color color = startGameAnimator.GetComponent<Image>().color;
        while (t < 1)
        {
            t += Time.unscaledDeltaTime * 10f;
            //alpha = Mathf.Lerp(0, 1, t);
            startGameAnimator.GetComponent<Image>().color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, 1, t));
            yield return null;
        }
        mainMenu.SetActive(false);
        t = 0;
        while(t<1)
        {
            t += Time.unscaledDeltaTime * 1f;
            yield return null;
        }
        InGamePanel.SetActive(true);
        t = 1;
        while (t > 0)
        {
            t -= Time.unscaledDeltaTime * 5f;
            startGameAnimator.GetComponent<Image>().color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, 1, t));
            yield return null;
        }
        startGameAnimator.SetActive(false);
        player.GetComponent<Player>().canJump = true;
    }

    public IEnumerator AnimatePlusOne()
    {
        float t = 0, transformSpeed = 10f;
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

    public IEnumerator SpawnPlusOne(GameObject star)
    {
        float alpha;// = plusOne.GetComponent<SpriteRenderer>().color.a;
        float t = 0, y0, y1;
        y0 = star.transform.position.y;
        y1 = star.transform.position.y + 0.8f;
        plusOne.transform.position = star.transform.position;
        Color newColor;
        while (t < 1)
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
        while (t > 0)
        {
            t -= Time.deltaTime * 1.5f;
            alpha = plusOne.GetComponent<TMPro.TextMeshProUGUI>().color.a;
            newColor = new Color(1, 1, 1, Mathf.Lerp(0, 1f, t));
            plusOne.GetComponent<TMPro.TextMeshProUGUI>().color = newColor;
            plusOne.transform.position = new Vector3(plusOne.transform.position.x, Mathf.Lerp(y0, y1, 1 - t), plusOne.transform.position.z);
            yield return null;
        }
    }

    public IEnumerator SpawnStars(Transform bigStar)
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
            foreach (GameObject star in starClones)
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
}
