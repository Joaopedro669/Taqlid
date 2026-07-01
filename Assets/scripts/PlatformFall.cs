using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour
{    Rigidbody2D rb;
    float fallDelay = 2f;
    public FixedJoint2D joint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy (joint);
        Destroy(gameObject, fallDelay);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("Fall");
        }
    }
}