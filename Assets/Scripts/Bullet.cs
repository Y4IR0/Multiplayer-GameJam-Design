using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [DoNotSerialize] public GameObject shooter; // Will be set when instantiating

    public float speed = 10f;
    public float lifeTime;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("NPC"))
        {
            Health health = shooter.GetComponent<Health>();
            health.penalties += 1;

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Player")) 
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.health -= 1;
        }
    }
}
