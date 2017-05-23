﻿using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public PlayerHealth player1Health;
    public PlayerHealth player2Health;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    void Start () {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn () {
		if(player1Health.currentHealth <= 0f || player2Health.currentHealth <= 0f) {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
