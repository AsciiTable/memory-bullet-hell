using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;

    private void OnEnable()
    {
        UpdateHandler.UpdateOccurred += PlayerMove;
    }

    private void OnDisable()
    {
        UpdateHandler.UpdateOccurred -= PlayerMove;
    }

    private void PlayerMove() {
        if (Input.GetButton("Horizontal")) {
            this.transform.position += new Vector3(speed * Input.GetAxis("Horizontal") * Time.deltaTime, 0f, 0f);
        }

        if (Input.GetButton("Vertical")){
            this.transform.position += new Vector3(0f, speed * Input.GetAxis("Vertical") * Time.deltaTime, 0f);
        }
    }
}
