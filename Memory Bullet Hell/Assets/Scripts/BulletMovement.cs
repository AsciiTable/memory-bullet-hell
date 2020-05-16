using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private int pointValue = 1;
    //[SerializeField] private Transform startpoint;
    //[SerializeField] private float endpointX;
    private void OnEnable()
    {
        //this.transform.position = startpoint.transform.position;
        UpdateHandler.UpdateOccurred += bulletMoveLeft;
    }

    private void OnDisable()
    {
        UpdateHandler.UpdateOccurred -= bulletMoveLeft;
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void bulletMoveLeft()
    {
        this.transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
    }

    public int getPoints()
    {
        return pointValue;
    }
}
