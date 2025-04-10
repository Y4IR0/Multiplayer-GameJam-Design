using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    
    [Header("NPC Specific")]
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private bool doesWait = true;
    
    bool isMoving = false;

    IEnumerator MoveToLocation(Vector2 position)
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        float distance = Vector3.Distance(currentPosition, position);
        isMoving = true;
        
        while (distance > 3f)
        {
            currentPosition = new Vector2(transform.position.x, transform.position.y);
            distance = Vector3.Distance(currentPosition, position);
            rigidbody2D.linearVelocity = (position - currentPosition).normalized * speed;
            yield return new WaitForEndOfFrame();
        }
        
        rigidbody2D.linearVelocity =  Vector2.zero;
        isMoving = false;
    }

    Vector2 GetRandomTargetPosition()
    {
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 targetPosition = currentPosition;
        
        float x = Random.Range(-5, 5);
        float y = Random.Range(-5, 5);
        
        targetPosition += new Vector2(x, y);
        
        
        Transform movementBoundary = GameManager.instance.movementBoundary;
        
        x = Math.Clamp(targetPosition.x, movementBoundary.position.x + -(movementBoundary.localScale.x / 2), movementBoundary.position.x + movementBoundary.localScale.x / 2);
        y = Math.Clamp(targetPosition.y, movementBoundary.position.y + -(movementBoundary.localScale.y / 2), movementBoundary.position.y + movementBoundary.localScale.y / 2);

        //x = math.clamp(targetPosition.x, -2, 2);
        //y = math.clamp(targetPosition.y, -2, 2);
        
        targetPosition = new Vector2(x, y);
        
        return targetPosition;
    }
    
    IEnumerator Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        
        speed = Random.Range(.5f, 2f);
        doesWait = !(Random.Range(0, 3) == 0);
        
        while (true)
        {
            StartCoroutine(MoveToLocation(GetRandomTargetPosition()));
            yield return new WaitUntil(() => !isMoving);
            
            if (doesWait && Random.Range(0, 100) <= 70)
                yield return new WaitForSeconds(Random.Range(0f, 2f));
        }
    }
}
