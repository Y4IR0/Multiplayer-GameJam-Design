using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab1; // First player prefab
    public GameObject playerPrefab2; // Second player prefab
    public PlayerInputManager playerInputManager;

    private int currentPlayerCount = 0;

    void Start()
    {
        // Listen for players joining
        playerInputManager.onPlayerJoined += OnPlayerJoined;
    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("Player Joined: " + currentPlayerCount);

        // Ensure we don't go beyond the 2 players
        if (currentPlayerCount >= 2)
        {
            Debug.LogWarning("Maximum players reached!");
            return;
        }

        // Select prefab based on the current player count
        GameObject playerPrefabToSpawn = (currentPlayerCount == 0) ? playerPrefab1 : playerPrefab2;

        // Instantiate the selected player prefab
        GameObject player = Instantiate(playerPrefabToSpawn, Vector3.zero, Quaternion.identity);

        // Increment the player count
        currentPlayerCount++;
    }
}
