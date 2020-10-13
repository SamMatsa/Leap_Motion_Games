using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    public Object enemyRef;
    public string enemyResource = "enemy1";
    int currentHealth;
    public int respawnTimeMin;
    public int respawnTimeMax;

    void Start()
    {
        enemyRef = Resources.Load(enemyResource);
        currentHealth = maxHealth;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0) currentHealth = 0;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public float GetHealthPercent()
    {
        return (float)currentHealth / maxHealth;
    }


    void Update()
    {
    }

    public void Die()
    {
        SessionData.incrementEnemiesKilled();
        KillAndRespawn();
    }

    public void KillAndRespawn()
    {
        Invoke("Respawn", randomRespawnTime());
        gameObject.SetActive(false);
    }

    public int randomRespawnTime()
    {
        return Random.Range(respawnTimeMin, respawnTimeMax);
    }

    void Respawn()
    {
        GameObject clone = (GameObject)Instantiate(enemyRef);
        clone.transform.position = transform.position;
        clone.transform.rotation = transform.rotation;
    }


}
