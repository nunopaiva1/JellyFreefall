using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawner : MonoBehaviour
{
    public GameObject[] obstacle;
    private List<GameObject> obstacles;

    public Sprite breakableSprite;
    public Sprite spikeSprite;

    public static float delaySpawn = 1.3f;

    public GameObject bullet, enemySpike1, tree1, tree2, tree3;

    public GameObject player1, playerPrefab;

    private void Start()
    {

        Sprite hair = GetSprite("SpritesPlayer/", SelectedSkin.hairSkin);
        Sprite body = GetSprite("SpritesPlayer/", SelectedSkin.bodySkin);
        Sprite eyes = GetSprite("SpritesPlayer/", SelectedSkin.eyesSkin);


        player1.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = hair;
        player1.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = body;
        player1.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite = eyes;

        /*player1.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite =
        playerPrefab.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;

        player1.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite =
        playerPrefab.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite;

        player1.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite =
        playerPrefab.transform.GetChild(2).gameObject.GetComponent<SpriteRenderer>().sprite;*/

        //INSTANCIA 10 OBJECTOS QUE VÃO TROCANDO ENTRE SI AO LONGO DO JOGO

        obstacles = new List<GameObject>();
        for(int i = 0; i < 10; i++)
        {
            var go = Instantiate(obstacle[Random.Range(0, obstacle.Length)], Vector3.zero, Quaternion.identity);
            go.SetActive(false);
            obstacles.Add(go);
        }

        //InvokeRepeating("SpawnObstacles", 5, 1);

        StartCoroutine(Spawn());
    }

    public static Sprite GetSprite(string path, string spr)
    {

        Sprite[] sp = Resources.LoadAll<Sprite>(path);

        foreach (Sprite x in sp)
        {
            if (x.name.Contains(spr))
            {
                return x;
            }
        }

        return null;

    }


    IEnumerator Spawn()
    {

        for (int i = 0; i < obstacles.Count; i++)
        {
            if (obstacles[i].activeInHierarchy == false)
            {
                var pos = new Vector3(0, -7);

                obstacles[i].transform.position = pos;


                obstacles[i].SetActive(true);

                for (int j= 0; j< obstacles[i].transform.childCount; j++)
                {
                        obstacles[i].transform.GetChild(j).tag = "obs";
                    //print(obstacles[i].transform.GetChild(j));
                }

                var spikeId = Random.Range(0, obstacles[i].transform.childCount - 1);
                var spike = obstacles[i].transform.GetChild(spikeId);

                var sr = spike.GetComponent<SpriteRenderer>();
                if (sr)
                {
                    //sr.color = Color.red;
                    spike.gameObject.tag = "spike";
                    sr.sprite = spikeSprite;

                }

                var nDestructible = Random.Range(0, 3);
                var listID = new List<int>();

                for (int a = 0; a < nDestructible; a++)
                {
                    listID.Add(Random.Range(0, obstacles[i].transform.childCount - 1));
                    var breakable = obstacles[i].transform.GetChild(Random.Range(0, obstacles[i].transform.childCount - 1));

                    var sr1 = breakable.GetComponent<SpriteRenderer>();
                    if (sr1)
                    {
                        //sr1.color = Color.green;
                        sr1.sprite = breakableSprite;
                        breakable.tag = "breakable";
                    }
                }

                var nCheckpoint = Random.Range(3, 5);
                var checkpointID = new List<int>();

                for (int a = 0; a < nCheckpoint; a++)
                {
                    checkpointID.Add(Random.Range(0, obstacles[i].transform.childCount - 1));
                    var checkpoint = obstacles[i].transform.GetChild(Random.Range(0, obstacles[i].transform.childCount - 1));

                    var sr2 = checkpoint.GetComponent<SpriteRenderer>();
                    if (sr2)
                    {
                        sr2.color = Color.clear;
                        checkpoint.tag = "checkpoint";
                    }
                }

                for (int j = 0; j < obstacles[i].transform.childCount; j++)
                {
                    if (obstacles[i].transform.GetChild(j).tag == "checkpoint")
                    {
                        obstacles[i].transform.GetChild(j).GetComponent<BoxCollider2D>().isTrigger = true;
                        obstacles[i].transform.GetChild(j).GetComponent<BoxCollider2D>().size = new Vector2(4.95f, 2.30f);


                        if (j > 0 && j + 1 < obstacles[i].transform.childCount)
                        {
                            if (obstacles[i].transform.GetChild(j - 1).tag != "checkpoint" && obstacles[i].transform.GetChild(j + 1).tag != "checkpoint")
                            {
                                obstacles[i].transform.GetChild(j).tag = "checkpoint2";
                            }

                        }

                        else if (j == 0 && obstacles[i].transform.GetChild(j + 1).tag != "checkpoint") {

                            obstacles[i].transform.GetChild(j).tag = "checkpoint2";

                        }

                    }

                    else if (obstacles[i].transform.GetChild(j).tag == "spike")
                    {
                        var enemySpike = Instantiate(enemySpike1, obstacles[i].transform.GetChild(j).position + new Vector3(0f, 0.80f, 0f), Quaternion.identity);
                        enemySpike.transform.parent = obstacles[i].transform.GetChild(j);
                    }

                    else if (obstacles[i].transform.GetChild(j).tag == "obs")
                    {
                        var nTree = Random.Range(0, 10);

                        if (nTree == 0)
                        {
                            var tree = Instantiate(tree1, obstacles[i].transform.GetChild(j).position + new Vector3(0f, 1f, 0f), Quaternion.identity);
                            tree.transform.parent = obstacles[i].transform.GetChild(j);
                        }
                        else if (nTree == 1)
                        {
                            var tree = Instantiate(tree2, obstacles[i].transform.GetChild(j).position + new Vector3(0f, 1f, 0f), Quaternion.identity);
                            tree.transform.parent = obstacles[i].transform.GetChild(j);
                        }

                        else if (nTree == 2)
                        {
                            var tree = Instantiate(tree3, obstacles[i].transform.GetChild(j).position + new Vector3(0f, 0.65f, 0f), Quaternion.identity);
                            tree.transform.parent = obstacles[i].transform.GetChild(j);
                        }

                    }

                }

                break;
              //  active += 1;
               //     if (active >= n) break;
            }

        }

        /* if (player.GetComponent<Playerv2>().s1)
         {
             //STAGE 1
             yield return new WaitForSeconds(1.3f);
         }

         else if(player.GetComponent<Playerv2>().s2)
         {
             //STAGE 2
             yield return new WaitForSeconds(1f);
         }
         else if (player.GetComponent<Playerv2>().s3)
         {
             //STAGE 3
             yield return new WaitForSeconds(0.70f);
         }*/

        yield return new WaitForSeconds(delaySpawn);


        StartCoroutine(Spawn());
    }

    public List<GameObject> getObs()
    {
        return obstacles;
    }

}
