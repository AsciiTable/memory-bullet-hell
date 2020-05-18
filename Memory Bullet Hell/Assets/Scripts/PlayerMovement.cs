using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private AudioSource audio;
    //[SerializeField] private TextMeshProUGUI levelText;
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer sprtrd;

    private void OnEnable()
    {
        sprtrd = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
/*        if(levelText != null)
            levelText.SetText("Level: " + SceneManager.GetActiveScene().name);*/
        if(scoreText != null)
            scoreText.SetText("Score: 0");
        UpdateHandler.FixedUpdateOccurred += PlayerMove;
        UpdateHandler.UpdateOccurred += GetMovementInput;
    }

    private void OnDisable()
    {
        Debug.Log("Score: " + LevelManager.instance.GetScore()) ;
        UpdateHandler.FixedUpdateOccurred -= PlayerMove;
        UpdateHandler.UpdateOccurred -= GetMovementInput;
    }

    private void PlayerMove() {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    private void GetMovementInput() {
        movement.x = Input.GetAxisRaw("Horizontal");
        if (movement.x < 0 && !sprtrd.flipX)
            sprtrd.flipX = true;
        else if (movement.x > 0 && sprtrd.flipX)
            sprtrd.flipX = false;
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Bullet") && collision.gameObject.GetComponent<BulletMovement>().getDestroyOnTouch()) {
            int add = collision.gameObject.GetComponent<BulletMovement>().interactWithPlayer();
            if (add > 0) {
                audio.Play();
                LevelManager.instance.AddScore(add);
                Grader.GraderInstance.AddToPlayerScore(add);
            }
            
            if(scoreText != null)
                scoreText.SetText("Score: " + LevelManager.instance.GetScore());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Bullet") && !collision.gameObject.GetComponent<BulletMovement>().getDestroyOnTouch())
        {
            int add = collision.gameObject.GetComponent<BulletMovement>().interactWithPlayer();
            if (add > 0)
            {
                audio.Play();
                LevelManager.instance.AddScore(add);
                Grader.GraderInstance.AddToPlayerScore(add);
            }
            if (scoreText != null)
                scoreText.SetText("Score: " + LevelManager.instance.GetScore());
        }
    }
}
