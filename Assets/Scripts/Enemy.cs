using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Enemy variables
    public int maxHealth = 100;
    public int currentHealth;

    //Movement and aggro range values
    public int MoveSpeed = 4;
    public int MaxDist = 10;
    public int MinDist = 5;

    //The target to attack
    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Player);
        if (Vector3.Distance(transform.position, Player.position) <= MaxDist)//not MinDist
        {
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;
            if (Vector3.Distance(transform.position, Player.position) <= MinDist)//not MaxDist
            {
                //Here Call any function you want, like Shoot or something
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
