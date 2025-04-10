using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Transform movementBoundary;
    public static GameManager instance;

    public Transform NPCPrefab;
    public float npcSpawnAmount = 100;

    private void Start()
    {
        instance = this;
        
        movementBoundary = GameObject.Find("MovementBoundary").transform;

        for (int i = 0; i < npcSpawnAmount; i++)
        {
            Transform newNPC = Instantiate(NPCPrefab);
            
            float x = Random.Range(this.movementBoundary.position.x + -(this.movementBoundary.localScale.x / 2), this.movementBoundary.position.x + this.movementBoundary.localScale.x / 2);
            float y = Random.Range(this.movementBoundary.position.y + -(this.movementBoundary.localScale.y / 2), this.movementBoundary.position.y + this.movementBoundary.localScale.y / 2);
            Vector3 spawnPosition = new Vector3(x, y, 0);
            
            newNPC.position = spawnPosition;
        }
    }
}
