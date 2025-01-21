using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Playerv2 : MonoBehaviour
{
    public static int playerHighScore = 0;

    public static int adsCount = 0;

    private const int STAGE1 = 300, STAGE2 = 600, STAGE3 = 900;
    private const string attack1 = "Zen", attack2 = "Slowbro", attack3 = "Rifler";

    public Sprite hero1, hero2, hero3;

    private bool notChild = true;
    float deltax, deltay;

    public Animator cam;

    public Text scoreText, scoreInfo, onFireText, streakText;
    public GameObject gameOver, ui_controls;

    public Button btn_ulti;

    private bool canUlt = false;

    public ParticleSystem blood, tranquility, boom;

    public int score = 0;

        private float comboFire = 0, percent = 0, maxPercent = 100;

    bool left = false, right = false, isOnFire = false, isOverheat = false;

    static bool dead = false;

    public AudioSource stage1, stage2, stage3;
    public bool s1 = true, s2 = false, s3 = false;

    public GameObject background1, background2, background3;

    public Transform bulletPoint;
    public GameObject bulletPrefab;

    public LineRenderer line;

    public static bool slowbro = false;

    Rigidbody2D rb;

    private bool canScore = true;

    public Image ultiCount, ultiIcon;
    public Sprite icon1, icon2, icon3;

    public Text txtScoreDead;

    private bool isUlting = false;

    private int onFireValue = 5, overheatValue = 10;

    private List<string> listaOnFire = new List<string> { "YOU'RE ON \n FIRE!", "IMPRESSIVE!", "EASY THERE!", "GO GO GOO!!", "CAN'T STOP \n WON'T STOP!"};
    private List<string> listaOverheat = new List<string> { "JELLY GOD!!", "FALL JELLY, FALL!!", "BONUS BONUS BONUS!!", "WILL YOU EVER \n GIVE UP??", "GODLIKE!!", "I'M JELLYOUS" };
    private List<string> listaFinalMessageNormal = new List<string> { "You tried so hard and (...)", "Well well well, it's you again", "You'll get there... eventually", "Clearly a bug... noobie devs", "You're better than this... right?", "Just use your powers 4Head" };
    private List<string> listaFinalMessageHigh = new List<string> { "Wow, not bad!", "New highscore... still low", "Now that was a lucky run", "Now go even further beyond!!", "Nice one! It's been 85 years..." };


    public Text highscore, superFunny;

    public ParticleSystem starParticles;

    public Image star;

    void Start()
    {

        stage1.volume = PlayerPrefs.GetFloat("playerVolume");
        stage2.volume = PlayerPrefs.GetFloat("playerVolume");
        stage3.volume = PlayerPrefs.GetFloat("playerVolume");

        rb = GetComponent<Rigidbody2D>();

        Application.targetFrameRate = 60;

        btn_ulti.enabled = false;
        btn_ulti.GetComponent<Image>().color = Color.white;
        //btn_ulti.GetComponentInChildren<Text>().text = "0%";

        if (SelectedSkin.ultimate == attack1) {

            ultiIcon.sprite = icon1;

        }
        else if (SelectedSkin.ultimate == attack2)
        {

            ultiIcon.sprite = icon2;

        }
        else if (SelectedSkin.ultimate == attack3)
        {

            ultiIcon.sprite = icon3;

        }
        //Debug.Log(SceneManagement.player_name);

        /*if (SelectedSkin.ultimate == "Tranquility")
        {

            gameObject.GetComponent<SpriteRenderer>().sprite = hero1;

        }

        else if (SelectedSkin.ultimate == "Slowbro")
        {

            gameObject.GetComponent<SpriteRenderer>().sprite = hero2;

        }

        else if (SelectedSkin.ultimate == "Machine Gun")
        {

            gameObject.GetComponent<SpriteRenderer>().sprite = hero3;

        }*/
        transform.GetChild(10).gameObject.GetComponent<TextMesh>().text = SelectedSkin.player_name;

    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D) || right == true)
        {
            rb.AddForce(new Vector2(PlayerPrefs.GetFloat("playerSensitivity"), 0));
            //transform.position += Vector3.right * SceneManagement.heroSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || left == true)
        {
            rb.AddForce(new Vector2(-PlayerPrefs.GetFloat("playerSensitivity"), 0));
            //transform.position += Vector3.left * SceneManagement.heroSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) && canUlt )
        {
            ultimate();
            //transform.position += Vector3.left * SceneManagement.heroSpeed * Time.deltaTime;
        }


        if (Input.touchCount > 0)
        {

            var touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)
            {
                rb.AddForce(new Vector2(-PlayerPrefs.GetFloat("playerSensitivity"), 0));

                //transform.position += Vector3.left * SceneManagement.heroSpeed * Time.deltaTime;
            }
            else if (touch.position.x > Screen.width / 2)
            {
                rb.AddForce(new Vector2(PlayerPrefs.GetFloat("playerSensitivity"), 0));

                //transform.position += Vector3.right * SceneManagement.heroSpeed * Time.deltaTime;
            }
        }

        if (notChild)
        {
            transform.position = Vector2.Lerp(transform.localPosition, new Vector2(transform.localPosition.x, 2), 2 * Time.deltaTime);
        }

    }

    private void Update()
    {

        if (!dead)
        {
            if (Input.GetKey(KeyCode.D) || right == true)
            {
                //rb.AddForce(new Vector2(SceneManagement.heroSpeed, 0));
                //transform.position += Vector3.right * SceneManagement.heroSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A) || left == true)
            {
                //rb.AddForce(new Vector2(SceneManagement.heroSpeed, 0));
                //transform.position += Vector3.left * SceneManagement.heroSpeed * Time.deltaTime;
            }

        }

        /*
        if (Input.touchCount > 0)
        {
            
            var touch = Input.GetTouch(0);
            if (touch.position.x < Screen.width / 2)
            {
                transform.position += Vector3.left * SceneManagement.heroSpeed * Time.deltaTime;
            }
            else if (touch.position.x > Screen.width / 2)
            {
                transform.position += Vector3.right * SceneManagement.heroSpeed * Time.deltaTime;
            }

            Touch touch = Input.GetTouch(0);

            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // if(GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
                    //   {
                    deltax = touchPos.x - transform.position.x;
                   // deltay = touchPos.y - transform.position.y;

                    rb.freezeRotation = true;
                    rb.velocity = new Vector2(0, 0);
                    GetComponent<CircleCollider2D>().sharedMaterial = null;
                    //    }
                    break;

                case TouchPhase.Moved:
                    //if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos) && moveAllowed)
                   // rb.MovePosition(new Vector2(touchPos.x - deltax, touchPos.y - deltay));
                    transform.position += new Vector3(touchPos.x - deltax, 0) * 2 * Time.deltaTime;
                    break;

                case TouchPhase.Ended:
                    //rb.freezeRotation = false;
                    PhysicsMaterial2D mat = new PhysicsMaterial2D();
                    GetComponent<CircleCollider2D>().sharedMaterial = mat;
                    break;
            }

        }*/
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "checkpoint")
        {
            if (canScore)
            {
                /* if (comboFire == 0)
                {
                    var audioClip1 = Resources.Load<AudioClip>("Audio/sound_1") as AudioClip;

                    gameObject.GetComponent<AudioSource>().PlayOneShot(audioClip1);

                }*/
                //collision.gameObject.SetActive(false);

                // ---------------------------------
                // SCORE + ULTIMATE
                // ---------------------------------

                if (isOnFire)
                {
                    if (isOverheat)
                    {
                        score += 100;
                        scoreInfo.text = "+100 points";
                        StartCoroutine(InfoText());
                    }
                    else if(!isOverheat)
                    {
                        score += 50;
                        scoreInfo.text = "+50 points";
                        StartCoroutine(InfoText());

                    }

                    checkUltimate(8);

                    if (percent < maxPercent)
                    {
                        //btn_ulti.GetComponentInChildren<Text>().text = percent + "%";
                        ultiCount.GetComponent<Image>().fillAmount = (percent / 100);
                    }
                    else
                    {
                        btn_ulti.enabled = true;
                        canUlt = true;
                        //btn_ulti.GetComponentInChildren<Text>().text = "100%";
                        //btn_ulti.GetComponent<Image>().color = Color.cyan;
                        tranquility.gameObject.SetActive(true);

                        ultiCount.GetComponent<Image>().color = Color.green;
                        ultiCount.GetComponent<Image>().fillAmount = 1;

                    }
                }
                else
                {
                    score += 10;
                    scoreInfo.text = "+10 points";
                    StartCoroutine(InfoText());

                    checkUltimate(4);

                    if (percent < maxPercent)
                    {
                        //btn_ulti.GetComponentInChildren<Text>().text = percent + "%";
                        ultiCount.GetComponent<Image>().fillAmount = (percent / 100);
                    }
                    else
                    {
                        btn_ulti.enabled = true;
                        canUlt = true;
                        //btn_ulti.GetComponentInChildren<Text>().text = "100%";
                        //btn_ulti.GetComponent<Image>().color = Color.cyan;
                        tranquility.gameObject.SetActive(true);

                        ultiCount.GetComponent<Image>().color = Color.green;
                        ultiCount.GetComponent<Image>().fillAmount = 1;

                    }
                }

                comboFire += 1;
                scoreText.text = "Score \n" + score;
                streakText.text = "Streak \n" + comboFire + "x";

                if (comboFire >= onFireValue && comboFire < overheatValue)
                {
                    if (!isOnFire)
                    {
                        transform.GetChild(7).gameObject.GetComponent<AudioSource>().Play();

                        int index = Random.Range(0, listaOnFire.Count);
                        onFireText.gameObject.GetComponent<Text>().text = listaOnFire[index];
                    }

                    isOnFire = true;
                    transform.GetChild(3).gameObject.SetActive(true);
                    onFire();
                }

                else if (comboFire >= overheatValue)
                {

                    if (!isOverheat)
                    {
                        int index = Random.Range(0, listaOverheat.Count);
                        onFireText.gameObject.GetComponent<Text>().text = listaOverheat[index];
                    }

                    isOverheat = true;

                    ParticleSystem.MainModule ps = transform.GetChild(3).gameObject.GetComponent<ParticleSystem>().main;
                    ps.startColor = new ParticleSystem.MinMaxGradient(Color.red);
                    ps.gravityModifier = -5f;

                    print("OMG" + comboFire);

                }
                else if (comboFire < onFireValue)
                {
                    isOnFire = false;
                    transform.GetChild(3).gameObject.SetActive(false);
                    onFireText.gameObject.SetActive(false);

                    ParticleSystem.MainModule ps = transform.GetChild(3).gameObject.GetComponent<ParticleSystem>().main;
                    ps.startColor = new ParticleSystem.MinMaxGradient(Color.cyan, Color.white);
                    ps.gravityModifier = Random.Range(-1f, -1.75f);

                    isOverheat = false;
                }

                checkStage();
            }

            canScore = false;
        }

        else if (collision.gameObject.tag == "checkpoint2")
        {
            if (canScore)
            {
                //collision.gameObject.SetActive(false);

                // ---------------------------------
                // SCORE + ULTIMATE
                // ---------------------------------

                if (isOnFire)
                {
                    if (isOverheat)
                    {
                        score += 200;
                        scoreInfo.text = "+200 points";
                        StartCoroutine(InfoText());
                    }
                    else if (!isOverheat)
                    {
                        score += 100;
                        scoreInfo.text = "+100 points";
                        StartCoroutine(InfoText());

                    }

                    checkUltimate(8);

                    if (percent < maxPercent)
                    {
                        //btn_ulti.GetComponentInChildren<Text>().text = percent + "%";
                        ultiCount.GetComponent<Image>().fillAmount = (percent / 100);
                    }
                    else
                    {
                        btn_ulti.enabled = true;
                        canUlt = true;
                        //btn_ulti.GetComponentInChildren<Text>().text = "100%";
                        //btn_ulti.GetComponent<Image>().color = Color.cyan;
                        tranquility.gameObject.SetActive(true);

                        ultiCount.GetComponent<Image>().color = Color.green;
                        ultiCount.GetComponent<Image>().fillAmount = 1;

                    }
                }
                else
                {
                    score += 20;
                    scoreInfo.text = "+20 points";
                    StartCoroutine(InfoText());

                    checkUltimate(4);

                    if (percent < maxPercent)
                    {
                        //btn_ulti.GetComponentInChildren<Text>().text = percent + "%";
                        ultiCount.GetComponent<Image>().fillAmount = (percent / 100);
                    }
                    else
                    {
                        btn_ulti.enabled = true;
                        canUlt = true;
                        //btn_ulti.GetComponentInChildren<Text>().text = "100%";
                        //btn_ulti.GetComponent<Image>().color = Color.cyan;
                        tranquility.gameObject.SetActive(true);

                        ultiCount.GetComponent<Image>().color = Color.green;
                        ultiCount.GetComponent<Image>().fillAmount = 1;

                    }
                }

                comboFire += 1;
                scoreText.text = "Score \n" + score;
                streakText.text = "Streak \n" + comboFire + "x";

                if (comboFire >= onFireValue && comboFire < overheatValue)
                {
                    if(!isOnFire)
                    {
                        transform.GetChild(7).gameObject.GetComponent<AudioSource>().Play();

                        int index = Random.Range(0, listaOnFire.Count);
                        onFireText.gameObject.GetComponent<Text>().text = listaOnFire[index];
                    }

                    isOnFire = true;
                    transform.GetChild(3).gameObject.SetActive(true);
                    onFire();
                }

                else if (comboFire >= overheatValue)
                {

                    if (!isOverheat)
                    {
                        int index = Random.Range(0, listaOverheat.Count);
                        onFireText.gameObject.GetComponent<Text>().text = listaOverheat[index];
                    }

                    isOverheat = true;

                    ParticleSystem.MainModule ps = transform.GetChild(3).gameObject.GetComponent<ParticleSystem>().main;
                    ps.startColor = new ParticleSystem.MinMaxGradient(Color.red);
                    ps.gravityModifier = -5f;

                    print("OMG" + comboFire);

                }
                else if (comboFire < onFireValue)
                {
                    isOnFire = false;
                    transform.GetChild(3).gameObject.SetActive(false);
                    onFireText.gameObject.SetActive(false);

                    ParticleSystem.MainModule ps = transform.GetChild(3).gameObject.GetComponent<ParticleSystem>().main;
                    ps.startColor = new ParticleSystem.MinMaxGradient(Color.cyan, Color.white);
                    ps.gravityModifier = Random.Range(-1f, -1.75f);

                    isOverheat = false;

                }

                checkStage();
            }

            canScore = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "checkpoint")
        {

            canScore = true;

        }

        else if (collision.gameObject.tag == "checkpoint2")
        {

            canScore = true;

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.tag == "spike")
            {
            blood.transform.position = gameObject.transform.position;

            adsCount += 1;

            if (adsCount >= 4)
            { 
                //AdsManager.ShowInterstitialAd();
                adsCount = 0;
            }

            dead = true;
            tranquility.gameObject.SetActive(false);

            gameOver.SetActive(true);
            txtScoreDead.text = score + " Points";
            ui_controls.SetActive(false);

            gameObject.SetActive(false);
            blood.gameObject.SetActive(true);
            scoreInfo.enabled = false;
            onFireText.enabled = false;

            highscore.text = "Highscore: " + PlayerPrefs.GetInt("highscore");

            if (score > PlayerPrefs.GetInt("highscore"))
            {

                PlayerPrefs.SetInt("highscore", score);

                //playerHighScore = score;

                //PlayGames.AddScoreToLeaderboard();

                starParticles.gameObject.SetActive(true);
                star.gameObject.SetActive(true);

                print("NEW HIGHSCORE!");

                highscore.text = "NEW Highscore: " + PlayerPrefs.GetInt("highscore");

                int index = Random.Range(0, listaOverheat.Count);
                superFunny.text = listaFinalMessageHigh[index];

            }

            else
            {
                int index = Random.Range(0, listaOverheat.Count);
                superFunny.text = listaFinalMessageNormal[index];
            }


        }
            else if (collision.gameObject.tag == "breakable")
            {

            if (comboFire >= onFireValue)
            {
                isOnFire = false;
                isOverheat = false;
                transform.GetChild(3).gameObject.SetActive(false);
                onFireText.gameObject.SetActive(false);
            }

            comboFire = 0;
            streakText.text = "Streak \n" + comboFire + "x";

            boom.transform.position = collision.transform.position - new Vector3(0f, -0.25f);

                collision.gameObject.SetActive(false);
                checkUltimate(10);

            StartCoroutine(breakBlock());

            if (percent < maxPercent)
            {
                //btn_ulti.GetComponentInChildren<Text>().text = percent + "%";
                ultiCount.GetComponent<Image>().fillAmount = percent/100;
            }
            else
            {
                btn_ulti.enabled = true;
                canUlt = true;
                //btn_ulti.GetComponentInChildren<Text>().text = "100%";
                //btn_ulti.GetComponent<Image>().color = Color.cyan;
                tranquility.gameObject.SetActive(true);

                ultiCount.GetComponent<Image>().color = Color.green;
                ultiCount.GetComponent<Image>().fillAmount = 1;

            }

        }
            else if (collision.gameObject.tag == "checkpoint")
            {

            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

            }

            else if (collision.gameObject.tag == "checkpoint2")
            {

                collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

            }
            else if (collision.gameObject.tag == "obs")
            {

                if (isUlting)
                {

                    collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;

                }
                else
                {

                collision.gameObject.GetComponent<BoxCollider2D>().enabled = true;

                if (comboFire >= onFireValue)
                    {
                        isOnFire = false;
                        isOverheat = false;
                        transform.GetChild(3).gameObject.SetActive(false);
                        onFireText.gameObject.SetActive(false);
                    }

                    comboFire = 0;
                    streakText.text = "Streak \n" + comboFire + "x";

            }
        }

        //Debug.Log(comboFire);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "obs")
        {
            if (isUlting)
            {

                collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                notChild = true;
            }
            else
            {
                collision.gameObject.GetComponent<BoxCollider2D>().enabled = true;

            }

        }

        var dir = (transform.localPosition - collision.transform.localPosition).normalized;
        //print("Col dir: " + dir);

        if (dir.y >= .5f && !isUlting)
        {
            //print("DIR.Y : " + dir.y);
            //transform.parent = collision.gameObject.transform.parent;
            notChild = false;
        }
        else
        {
            transform.parent = null;
            notChild = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        transform.parent = null;
        notChild = true;
    }
    
    public void moveLeft()
    {
        left = true;
    }

    public void moveRight()
    {
        right = true;
    }

    public void stopMovement()
    {
        left = false;
        right = false;
    }

    public void ultimate()
    {
        StartCoroutine(activateUltimate());
        btn_ulti.enabled = false;
        canUlt = false;
        //btn_ulti.GetComponentInChildren<Text>().text = "0%";
        percent = 0;
        ultiCount.GetComponent<Image>().fillAmount = percent / 100;
        ultiCount.GetComponent<Image>().color = new Color(0x00, 0xF0, 0xFF);
        //btn_ulti.GetComponent<Image>().color = Color.white;
        tranquility.gameObject.SetActive(false);
    }

    IEnumerator breakBlock() {

        boom.gameObject.SetActive(true);
        cam.SetBool("isShaking", true);

        yield return new WaitForSeconds(0.3f);

        cam.SetBool("isShaking", false);
        boom.gameObject.SetActive(false);

    }

    IEnumerator activateUltimate()
    {

        //------------------------
        // ULTIS E COOLDOWNS
        //------------------------

        // TRANQUILITY

        if (SelectedSkin.ultimate == attack1)
        {
            transform.GetChild(8).gameObject.GetComponent<AudioSource>().Play();

            transform.GetChild(4).gameObject.SetActive(true);
            //gameObject.GetComponent<CircleCollider2D>().enabled = false;

            isUlting = true;

            yield return new WaitForSeconds(2);

            transform.GetChild(4).gameObject.SetActive(false);
            //gameObject.GetComponent<CircleCollider2D>().enabled = true;

            isUlting = false;

        }

        // SLOW MO

        else if (SelectedSkin.ultimate == attack2)
        {

            float rng = Random.Range(0.05f, 0.12f);

            Obstacle.obsSpeed = 1.0f;

            if (s1)
            {
                Spawner.delaySpawn = 2.5f;
                stage1.pitch -= rng;
            }
            else if (s2)
            {
                Spawner.delaySpawn = 3.0f;
                stage2.pitch -= rng;

            }
            else if (s3)
            {
                stage3.pitch -= rng;
                Spawner.delaySpawn = 3.5f;
            }


            yield return new WaitForSeconds(1.5f);

            slowbro = true;

            if (s1)
            {
                Spawner.delaySpawn = 1.3f;
                stage1.pitch += rng;
            }

            else if (s2)
            {
                Spawner.delaySpawn = 1.0f;
                stage2.pitch += rng;
            }
            else if (s3)
            {
                Spawner.delaySpawn = 0.7f;
                stage3.pitch += rng;
            }


            yield return new WaitForSeconds(1.5f);

            stage1.pitch = 1f;
            stage2.pitch = 1f;
            stage3.pitch = 1f;


            slowbro = false;

            Obstacle.obsSpeed = 3.0f;


        }

        // KAMEHAMEHA
        else if (SelectedSkin.ultimate == attack3)
        {



            for (int i = 0; i < 5; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletPoint.position, bulletPoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(-bulletPoint.up * 30f, ForceMode2D.Impulse);
                Destroy(bullet, 0.5f);

                transform.GetChild(9).gameObject.GetComponent<AudioSource>().Play();

                yield return new WaitForSeconds(0.2f);
            }


        }

    }

    public void restart()
    {
        dead = false;
        ui_controls.SetActive(true);
        gameOver.SetActive(false);
        score = 0;
        s1 = true; s2 = false; s3 = false;
        tranquility.gameObject.SetActive(false);
        background3.SetActive(false);
        background1.SetActive(true);
        isUlting = false;

        ultiCount.GetComponent<Image>().fillAmount = percent / 100;
        ultiCount.GetComponent<Image>().color = new Color(0x00,0xF0,0xFF);


        slowbro = false;
        Obstacle.obsSpeed = 3.0f;

        Spawner.delaySpawn = 1.3f;

        print(SelectedSkin.ultimate);

        starParticles.gameObject.SetActive(false);
        star.gameObject.SetActive(false);

        PlayerPrefs.Save();

        SceneManager.LoadScene(1);
    }

    public void backMenu()
    {
        dead = false;
        ui_controls.SetActive(true);
        gameOver.SetActive(false);
        score = 0;
        s1 = true; s2 = false; s3 = false;
        tranquility.gameObject.SetActive(false);
        background3.SetActive(false);
        background1.SetActive(true);

        slowbro = false;
        Obstacle.obsSpeed = 3.0f;

        Spawner.delaySpawn = 1.3f;

        starParticles.gameObject.SetActive(false);
        star.gameObject.SetActive(false);

        PlayerPrefs.Save();

        SceneManager.LoadScene("Menu");
    }

    void scale()
    {
        scoreInfo.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }
    void normalFlash()
    {
        scoreInfo.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    IEnumerator InfoText()
    {
        scale();
        yield return new WaitForSeconds(0.3f);
        normalFlash();
    }

    void onFire()
    {
        onFireText.gameObject.SetActive(true);
    }

    void checkStage()
    {
        if (score < STAGE1)
        {
            //STAGE 1
            s1 = true;
            Spawner.delaySpawn = 1.3f;
        }

        else if (score >= STAGE1 && score < STAGE3)
        {
            //STAGE 2

            if(s1)
                StartCoroutine(shakeLevel());

            s1 = false; s2 = true;

            stage1.Stop();
            if (!stage2.isPlaying)
                stage2.Play();

            background1.SetActive(false);
            background2.SetActive(true);
            Spawner.delaySpawn = 1.0f;


        }
        else if (score >= STAGE3)
        {
            //STAGE 3

            if(s2)
                StartCoroutine(shakeLevel());

            s2 = false; s3 = true;

            stage2.Stop();
            if (!stage3.isPlaying)
                stage3.Play();
            background2.SetActive(false);
            background3.SetActive(true);
            Spawner.delaySpawn = 0.7f;

        }
    }

    IEnumerator shakeLevel() {

        cam.SetBool("isChanging", true);
        yield return new WaitForSeconds(0.3f);
        cam.SetBool("isChanging", false);


    }

    void checkUltimate(int count) {

        //POR DEFEITO AQUI CHEGAM 5 ou 10 de ULT CHARGE)

        if (SelectedSkin.ultimate == attack1)
        {

            percent += count;

        }

        else if (SelectedSkin.ultimate == attack2)
        {

            percent += count;

        }

        else if (SelectedSkin.ultimate == attack3)
        {

            percent += count;

        }

    }
}
