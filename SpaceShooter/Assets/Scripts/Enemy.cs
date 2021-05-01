using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] int killScore = 150;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] GameObject destroyVFX;
    [SerializeField] float vfxDuration = 1f;
    [SerializeField] AudioClip firingSound;
    [SerializeField] AudioClip deathSound;

    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        gameSession = FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        GameObject enemyProjectile = Instantiate(
            projectile, 
            transform.position, 
            Quaternion.identity) as GameObject;
        enemyProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position);
    }

    private void OnTriggerEnter2D(Collider2D playerProjectile)
    {
        DamageDealer damageDealer = playerProjectile.gameObject.GetComponent<DamageDealer>();
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        gameSession.AddToScore(killScore);
        AudioSource.PlayClipAtPoint(firingSound, Camera.main.transform.position);
        GameObject sparkles = Instantiate(
            destroyVFX,
            transform.position,
            transform.rotation) as GameObject;
        Destroy(sparkles, vfxDuration);
    }
}
