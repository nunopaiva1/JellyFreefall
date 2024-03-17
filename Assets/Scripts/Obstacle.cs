using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public Sprite defSprite;
    public static float obsSpeed = 3.0f;

    void Update()
    {
        transform.position += Vector3.up * obsSpeed * Time.deltaTime;

        bool sayGoodbye = Playerv2.slowbro;

        if (transform.localPosition.y > 5.3f || (transform.localPosition.y < -6 && sayGoodbye) )
        {
            for(int i=0; i<transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);


                transform.GetChild(i).gameObject.GetComponent<BoxCollider2D>().enabled = true;


                transform.GetChild(i).gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

                transform.GetChild(i).GetComponent<BoxCollider2D>().size = new Vector2(4.95f, 4.60f);


                var sr = transform.GetChild(i).GetComponent<SpriteRenderer>();
                if (sr)
                {
                    sr.color = Color.white;
                    sr.sprite = defSprite;

                    if (transform.GetChild(i).childCount > 0)
                    {
                        for (int j = 0; j < transform.GetChild(i).childCount; j++)

                            Destroy(transform.GetChild(i).GetChild(j).gameObject);
                    }

                }
            }
            gameObject.SetActive(false);
        }
    }
}
