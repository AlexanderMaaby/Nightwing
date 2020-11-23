using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXProjectile : MonoBehaviour
{

    private bool collided;

    public GameObject impactVFX;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player" && collision.gameObject.tag != "MainCamera" && collided != true)
        {
            collided = true;

            var impact = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.identity) as GameObject;

            Destroy(impact, 2);

            Destroy(gameObject);
            Debug.Log(collision.gameObject.name);
        }
    }
}
