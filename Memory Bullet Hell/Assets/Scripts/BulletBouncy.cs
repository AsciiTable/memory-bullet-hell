using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBouncy : BulletMovement
{
    [SerializeField] private float initialThrustX = 1.0f;
    [SerializeField] private float initialThrustY = 1.0f;
    protected override void OnEnable()
    {
        //this.transform.position = startpoint.transform.position;
        BouncyBulletThrust();
    }

    protected override void OnDisable()
    {
        // nothing pls thanks
    }

    protected void BouncyBulletThrust() {
        this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(initialThrustX, initialThrustY));
    }

    public void SetDirection(float x, float y) {
        initialThrustX = x;
        initialThrustY = y;
    }
}
