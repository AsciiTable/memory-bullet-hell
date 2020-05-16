using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletColumn : BulletMovement
{
    //[Tooltip("Max amount of points user can earn if they stayed in for the whole time")]
    //[SerializeField] private int fullPointValue = 10;
    [SerializeField] private float preColumnTime = 0.5f;
    [SerializeField] private float columnActiveTime = 10.0f;
    [SerializeField] private float postColumnTime = 0.2f;
    [SerializeField] private float timeElapsedToNextPoint = 2.0f;
    private Collider2D columnCol;
    private SpriteRenderer sprt;
    private float currTime;

    protected override void OnEnable()
    {
        destroyOnTouch = false;
        columnCol = gameObject.GetComponent<Collider2D>();
        sprt = gameObject.GetComponent<SpriteRenderer>();
        columnCol.enabled = false;
        StartCoroutine(SetUpTakeDown());
        UpdateHandler.UpdateOccurred += bulletMoveLeft;
    }

    protected override void OnDisable()
    {
        UpdateHandler.UpdateOccurred -= bulletMoveLeft;
    }

    private IEnumerator SetUpTakeDown() {
        sprt.color = new Color(sprt.color.r, sprt.color.g, sprt.color.b, 0.5f);
        yield return new WaitForSeconds(preColumnTime);
        columnCol.enabled = true;
        sprt.color = new Color(sprt.color.r, sprt.color.g, sprt.color.b, 1f);
        currTime = Time.time;
        yield return new WaitForSeconds(columnActiveTime);
        sprt.color = new Color(sprt.color.r, sprt.color.g, sprt.color.b, 0.2f);
        columnCol.enabled = false;
        yield return new WaitForSeconds(postColumnTime);
        gameObject.SetActive(false);
    }

    public override int interactWithPlayer()
    {
        int earnablePoint = 0;
        if (Time.time - currTime >= timeElapsedToNextPoint) {
            earnablePoint = pointValue;
            currTime = Time.time;
        }
        return earnablePoint;
    }

}
