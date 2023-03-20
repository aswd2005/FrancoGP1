using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPosition : MonoBehaviour
{
    public float attackRange;
    public float speed;
    //creat a box to store information (position, rotation, scale)
    Transform player;
    //store if our boss flip or not setting the value to false
    public bool isFlipped = false;
    PlayerManager playerManager;
    public int BossHealth= 10;
    public bool phase2 = false;
    public bool phase3 = false;
    public bool isDead = false;
    public Transform shotLocation;
    public GameObject projectile;
    public GameObject projectile2;
    public float timer;
    public float waitingTime;
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Update()
    {
        //creat seres of if else statments rhat will check too see
        //if the boss is below 2 and above 3, bwlowe 3 and above 1
        //ans is less or = to 0
        if (BossHealth < 7 && BossHealth > 3)
        {
            phase2 = true;
            Debug.Log("phase2");
        }
        else if (BossHealth < 4 && BossHealth >= 1)
        {
            phase2 = false;
            Debug.Log("phase3");
            phase3 = true;
        }
        else if (BossHealth <= 0)
        {
            phase3 = false;
            Debug.Log("death");
            isDead = true;

            timer += Time.deltaTime;
        }
    }
    public void ProjectileShoot()
    {
        if (timer > waitingTime)
        {
            if (phase2)
            {
                GameObject clone = Instantiate(projectile, shotLocation.position, Quaternion.identity);
                timer = 0f;
            }
            else if(phase3)
            {
                GameObject clone = Instantiate(projectile2, shotLocation.position, Quaternion.identity);
                timer = 0f;
            }
        }
    }
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z = -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0, 180, 0);
            isFlipped = true;
        }
        else if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0, 180, 0);
            isFlipped = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerManager.TakeDamage();
        }
    }
}
