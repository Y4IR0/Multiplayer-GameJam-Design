using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            collision.GetComponent<Health>().coins += 1;
            GameManager.instance.currentCoins.Remove(transform);
            Destroy(gameObject);
        }
    }
}
