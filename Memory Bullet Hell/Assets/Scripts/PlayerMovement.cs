using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    private Rigidbody2D rb;
    private Vector2 movement;

    private void OnEnable()
    {
        rb = this.GetComponent<Rigidbody2D>();
        UpdateHandler.FixedUpdateOccurred += PlayerMove;
        UpdateHandler.UpdateOccurred += GetMovementInput;
    }

    private void OnDisable()
    {
        UpdateHandler.FixedUpdateOccurred -= PlayerMove;
        UpdateHandler.UpdateOccurred -= GetMovementInput;
    }

    private void PlayerMove() {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    private void GetMovementInput() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Bullet")) {
            collision.gameObject.SetActive(false);
        }
    }
}
