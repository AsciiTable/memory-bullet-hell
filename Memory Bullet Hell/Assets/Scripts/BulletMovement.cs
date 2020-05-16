﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] protected float speed = 5.0f;
    [SerializeField] protected int pointValue = 1;
    protected bool destroyOnTouch = true;
    //[SerializeField] private Transform startpoint;
    //[SerializeField] private float endpointX;
    protected virtual void OnEnable()
    {
        //this.transform.position = startpoint.transform.position;
        UpdateHandler.UpdateOccurred += bulletMoveLeft;
    }

    protected virtual void OnDisable()
    {
        UpdateHandler.UpdateOccurred -= bulletMoveLeft;
    }

    protected void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    protected void bulletMoveLeft()
    {
        this.transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
    }

/*    public int getPoints()
    {
        return pointValue;
    }*/

    public virtual int interactWithPlayer() {
        gameObject.SetActive(false);
        return pointValue;
    }

    public bool getDestroyOnTouch()
    {
        return destroyOnTouch;
    }
}
