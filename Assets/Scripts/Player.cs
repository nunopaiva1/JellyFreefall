using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    private void Update()
    {
        if (Input.GetKey(KeyCode.D)) transform.position += Vector3.right * 4 * Time.deltaTime;
        if (Input.GetKey(KeyCode.A)) transform.position += Vector3.left * 4 * Time.deltaTime;

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Animator anim = gameObject.GetComponent<Animator>();

        anim.SetBool("isFalling", false);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Animator anim = gameObject.GetComponent<Animator>();

        anim.SetBool("isFalling", true);


    }

}
