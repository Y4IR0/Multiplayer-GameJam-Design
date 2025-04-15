using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Transform movementBoundary;
    public static GameManager instance;

    public Transform NPCPrefab;
    public float npcSpawnAmount = 100;
    
    public Transform player1;
    public Transform player2;
    
    public Health player1Health;
    public Health player2Health;

    public UnityEvent<int> roundEnd;
    public UnityEvent roundStart;
    
    
    
    public bool isRoundOver = false;


    
    Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(this.movementBoundary.position.x + -(this.movementBoundary.localScale.x / 2), this.movementBoundary.position.x + this.movementBoundary.localScale.x / 2);
        float y = Random.Range(this.movementBoundary.position.y + -(this.movementBoundary.localScale.y / 2), this.movementBoundary.position.y + this.movementBoundary.localScale.y / 2);
        Vector3 spawnPosition = new Vector3(x, y, 0);
        return spawnPosition;
    }

    private void TryGetPlayers()
    {
        //if (!player1 || !player2) return;
        
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in players)
        {
            if (player.TryGetComponent<PlayerInput>(out PlayerInput input))
            {
                if (input.user.id == 1)
                {
                    player1 = player.transform;
                    player1Health = player.GetComponent<Health>();
                }
                else if (input.user.id == 2)
                {
                    player2 = player.transform;
                    player2Health = player.GetComponent<Health>();
                }
            }
        }
    }
    
    private void Start()
    {
        instance = this;
        movementBoundary = GameObject.Find("MovementBoundary").transform;

        for (int i = 0; i < npcSpawnAmount; i++)
        {
            Transform newNPC = Instantiate(NPCPrefab);
            newNPC.position = GetRandomSpawnPosition();
        }

        TryGetPlayers();
        StartRound();
    }

    IEnumerator EndRound(int winnerID)
    {
        if (isRoundOver) yield break;
        
        isRoundOver = true;
        roundEnd.Invoke(winnerID);
        
        yield return new WaitForSeconds(3);
        
        StartRound();
        isRoundOver = false;
    }
    
    void StartRound()
    {
        roundStart.Invoke();
        
        if (player1 == null || player2 == null) return;

        player1.position = GetRandomSpawnPosition();
        player2.position = GetRandomSpawnPosition();

        player1Health.health = 2;
        player2Health.health = 2;

        player1Health.penalties = 0;
        player2Health.penalties = 0;
    }

    private void Update()
    {
        TryGetPlayers();

        if (player1 == null) return;
        if (player2 == null) return;
        if (player1Health == null) return;
        if (player2Health == null) return;
        
        if (player1Health.health <= 0 || player1Health.penalties >= 3)
            StartCoroutine(EndRound(2));

        if (player2Health.health <= 0 || player2Health.penalties >= 3)
            StartCoroutine(EndRound(1));
    }
}
