using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using UnityEditor;

public class SelectedSkin : MonoBehaviour
{

    public AudioSource backgroundMusic;

    public string scene;
    public InputField inputText;

    public static string player_name = "";

    public SpriteRenderer plHair, plBody, plEye;

    public List<Sprite> Hairs = new List<Sprite>();
    public List<Sprite> Bodies = new List<Sprite>();
    public List<Sprite> Eyes = new List<Sprite>();
    public List<string> attacks = new List<string>();

    private int skin1, skin2, skin3, skinAtk;

    public static string ultimate;
    public Text ultiText, txtHair, txtBody, txtEyes;

    public GameObject player;

    public static string hairSkin, bodySkin, eyesSkin;

    public static int savedIdHair, savedIdBody, savedIdEyes;

    public Button atk1, atk2, atk3;

    void Start()
    {

        backgroundMusic.volume = PlayerPrefs.GetFloat("playerVolume");

        //SKINS E ATAQUE POR DEFEITO

        /*if (PlayerPrefs.GetString("playerHair") == "")
        {

            plHair.sprite = Hairs[0];
            plBody.sprite = Bodies[0];
            plEye.sprite = Eyes[0];

            //ultimate = attacks[0];
            //ultiText.text = ultimate;
        }*/

            //RECUPERA PLAYER PREFS

            inputText.text = player_name;

            //Sprite hair = Spawner.GetSprite("SpritesPlayer/", hairSkin);
            //Sprite body = Spawner.GetSprite("SpritesPlayer/", bodySkin);
            //Sprite eyes = Spawner.GetSprite("SpritesPlayer/", eyesSkin);

            Sprite hair = Spawner.GetSprite("SpritesPlayer/", PlayerPrefs.GetString("playerHair"));
            Sprite body = Spawner.GetSprite("SpritesPlayer/", PlayerPrefs.GetString("playerBody"));
            Sprite eyes = Spawner.GetSprite("SpritesPlayer/", PlayerPrefs.GetString("playerEyes"));

            hairSkin = PlayerPrefs.GetString("playerHair");
            bodySkin = PlayerPrefs.GetString("playerBody");
            eyesSkin = PlayerPrefs.GetString("playerEyes");

            plHair.sprite = Hairs[PlayerPrefs.GetInt("hairSkinID")];
            plBody.sprite = Bodies[PlayerPrefs.GetInt("bodySkinID")];
            plEye.sprite = Eyes[PlayerPrefs.GetInt("eyesSkinID")];

            skin1 = PlayerPrefs.GetInt("hairSkinID");
            skin2 = PlayerPrefs.GetInt("bodySkinID");
            skin3 = PlayerPrefs.GetInt("eyesSkinID");

            txtHair.text = "Hair: " + skin1;
            txtBody.text = "Body: " + skin2;
            txtEyes.text = "Eyes: " + skin3;

            ultiText.text = ultimate;

            if (PlayerPrefs.GetString("playerUltimate") == "Zen")
            {
                ultimate = attacks[0];
                resetColors();
                atk1.GetComponent<Image>().color = new Color32(179, 255, 153, 255);
                PlayerPrefs.SetString("playerUltimate", attacks[0]);
            }
            else if (PlayerPrefs.GetString("playerUltimate") == "Slowbro")
            {
                ultimate = attacks[1];
                resetColors();
                atk2.GetComponent<Image>().color = new Color32(179, 255, 153, 255);
                PlayerPrefs.SetString("playerUltimate", attacks[1]);
            }
        else if (PlayerPrefs.GetString("playerUltimate") == "Rifler")
            {
                ultimate = attacks[2];
                resetColors();
                atk3.GetComponent<Image>().color = new Color32(179, 255, 153, 255);
                PlayerPrefs.SetString("playerUltimate", attacks[2]);
          }
        else if (PlayerPrefs.GetString("playerUltimate") == "")
            {
                ultimate = attacks[0];
                resetColors();
                atk1.GetComponent<Image>().color = new Color32(179, 255, 153, 255);
                PlayerPrefs.SetString("playerUltimate", attacks[0]);
        }


    }

    public void changeScene()
    {

        //PrefabUtility.SaveAsPrefabAsset(player, "Assets/Prefabs/Player 1.prefab");

        player_name = inputText.text;

        if (inputText.text == "")
            player_name = "";

        hairSkin = plHair.sprite.name;
        bodySkin = plBody.sprite.name;
        eyesSkin = plEye.sprite.name;

        PlayerPrefs.SetString("playerUltimate", ultimate);

        PlayerPrefs.Save();

        SceneManager.LoadScene(scene);
    }

    public void nextAttack()
    {

        skinAtk += 1;

        if (skinAtk == attacks.Count)
        {

            skinAtk = 0;

        }

        ultimate = attacks[skinAtk];
        ultiText.text = ultimate;

    }

    public void previousAttack()
    {

        skinAtk -= 1;

        if (skinAtk < 0)
        {

            skinAtk = attacks.Count - 1;

        }

        ultimate = attacks[skinAtk];
        ultiText.text = ultimate;

    }

    public void nextHair() {

        skin1 += 1;

        if (skin1 == Hairs.Count) {

            skin1 = 0;

        }

        savedIdHair = skin1;
        plHair.sprite = Hairs[skin1];

        PlayerPrefs.SetString("playerHair", hairSkin);
        PlayerPrefs.SetInt("hairSkinID", savedIdHair);

        GameObject.Find("txt_hair").GetComponent<Text>().text = "Hair: " + skin1;

    }

    public void nextBody() {

        skin2 += 1;

        if (skin2 == Bodies.Count)
        {

            skin2 = 0;

        }

        savedIdBody = skin2;
        plBody.sprite = Bodies[skin2];

        PlayerPrefs.SetString("playerBody", bodySkin);
        PlayerPrefs.SetInt("bodySkinID", savedIdBody);

        GameObject.Find("txt_body").GetComponent<Text>().text = "Body: " + skin2;


    }

    public void nextEye() {

        skin3 += 1;

        if (skin3 == Eyes.Count)
        {

            skin3 = 0;

        }

        savedIdEyes = skin3;
        plEye.sprite = Eyes[skin3];

        PlayerPrefs.SetString("playerEyes", eyesSkin);
        PlayerPrefs.SetInt("eyesSkinID", savedIdEyes);

        GameObject.Find("txt_Eyes").GetComponent<Text>().text = "Eyes: " + skin3;

    }

    public void previousHair()
    {

        skin1 -= 1;

        if (skin1 < 0)
        {

            skin1 = Hairs.Count - 1;

        }

        savedIdHair = skin1;
        plHair.sprite = Hairs[skin1];

        PlayerPrefs.SetString("playerHair", hairSkin);
        PlayerPrefs.SetInt("hairSkinID", savedIdHair);

        GameObject.Find("txt_hair").GetComponent<Text>().text = "Hair: " + skin1;

    }

    public void previousBody()
    {

        skin2 -= 1;

        if (skin2 < 0)
        {

            skin2 = Bodies.Count - 1;

        }

        savedIdBody = skin2;
        plBody.sprite = Bodies[skin2];

        PlayerPrefs.SetString("playerBody", bodySkin);
        PlayerPrefs.SetInt("bodySkinID", savedIdBody);

        GameObject.Find("txt_body").GetComponent<Text>().text = "Body: " + skin2;


    }

    public void previousEye()
    {

        skin3 -= 1;

        if (skin3 < 0)
        {

            skin3 = Eyes.Count - 1;

        }

        savedIdEyes = skin3;
        plEye.sprite = Eyes[skin3];

        PlayerPrefs.SetString("playerEyes", eyesSkin);
        PlayerPrefs.SetInt("eyesSkinID", savedIdEyes);

        GameObject.Find("txt_Eyes").GetComponent<Text>().text = "Eyes: " + skin3;


    }

    public void selectedZen() {

        ultimate = attacks[0];
        ultiText.text = ultimate;

        resetColors();
        atk1.GetComponent<Image>().color = new Color32(179, 255, 153, 255);

    }

    public void selectedSlowbro()
    {

        ultimate = attacks[1];
        ultiText.text = ultimate;

        resetColors();
        atk2.GetComponent<Image>().color = new Color32(179, 255, 153, 255);

    }
    public void selectedRifler()
    {

        ultimate = attacks[2];
        ultiText.text = ultimate;

        resetColors();
        atk3.GetComponent<Image>().color = new Color32(179, 255, 153, 255);

    }

    private void resetColors() {

        atk1.GetComponent<Image>().color = Color.white;
        atk2.GetComponent<Image>().color = Color.white;
        atk3.GetComponent<Image>().color = Color.white;


    }


}
