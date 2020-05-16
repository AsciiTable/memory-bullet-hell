using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBouncy : BulletMovement
{
    [SerializeField] protected float initialThrustX = 1.0f;
    [SerializeField] protected float initialThrustY = 1.0f;
    private Rigidbody2D rb;
    protected override void OnEnable()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        //this.transform.position = startpoint.transform.position;
        BouncyBulletThrust(initialThrustX, initialThrustY, rb);
    }

    protected override void OnDisable()
    {
        // nothing pls thanks
    }

    protected void BouncyBulletThrust(float x, float y, Rigidbody2D r) {
        r.velocity = new Vector2(0f,0f);
        r.AddForce(new Vector2(x, y));
    }

    public void SetDirection(float x, float y) {
        initialThrustX = x;
        initialThrustY = y;
    }
}
