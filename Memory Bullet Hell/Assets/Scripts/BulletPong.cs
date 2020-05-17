using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPong : BulletBouncy
{
    [SerializeField] List<Vector2> newPongDirections;
    private int numOfBouncesLeft;
    private SpriteRenderer sprt;
    private float transparancyDecrement;
    private float transparancy;

    protected override void OnEnable()
    {
        sprt = this.gameObject.GetComponent<SpriteRenderer>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        numOfBouncesLeft = newPongDirections.Count;
        transparancyDecrement = 1.0f / (float)numOfBouncesLeft;
        transparancy = 1;
        BouncyBulletThrust(initialThrustX, initialThrustY, rb);
    }

    public override int interactWithPlayer()
    {
        int earnablePoint = 0;
        if (numOfBouncesLeft <= 0)
        {
            earnablePoint = pointValue;
            gameObject.SetActive(false);
        }
        else {
            transparancy = transparancy - transparancyDecrement;
            sprt.color = new Color(sprt.color.r, sprt.color.g, sprt.color.b, transparancy);
            BouncyBulletThrust(newPongDirections[newPongDirections.Count - numOfBouncesLeft].x, newPongDirections[newPongDirections.Count - numOfBouncesLeft].y, rb);
            numOfBouncesLeft--;
        }
        return earnablePoint;
    }

    public void SetNewPongDirections(List<Vector2> d) {
        newPongDirections.Clear();
        foreach (Vector2 v in d) {
            newPongDirections.Add(v);
        }
        numOfBouncesLeft = newPongDirections.Count;
    }
}
