using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "obs")
        {
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }

        else if (collision.gameObject.tag == "spike")
        {
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }

        else if (collision.gameObject.tag == "breakable")
        {
            collision.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

}
