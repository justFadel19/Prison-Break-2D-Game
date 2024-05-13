using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D enemyRigidbody;
    
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        enemyRigidbody.velocity = new Vector2(moveSpeed, 0);
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(enemyRigidbody.velocity.x)), 1f);
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }
}
