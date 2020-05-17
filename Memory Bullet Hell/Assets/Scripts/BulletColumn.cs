using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletColumn : BulletMovement
{
    [SerializeField] private int fullPointValue = 10;
    [SerializeField] private float preColumnTime = 0.5f;
    [SerializeField] private float columnActiveTime = 10.0f;
    [SerializeField] private float postColumnTime = 0.2f;
    [SerializeField] private float timeElapsedToNextPoint = 2.0f;
    private Collider2D columnCol;
    private SpriteRenderer sprt;
    private float currTime;
    private int totalEarnedPoints;

    protected override void OnEnable()
    {
        totalEarnedPoints = 0;
        destroyOnTouch = false;
        columnCol = gameObject.GetComponent<Collider2D>();
        sprt = gameObject.GetComponent<SpriteRenderer>();
        columnCol.enabled = false;
        Debug.Log("Column Active Time: " + columnActiveTime);
        StartCoroutine(SetUpTakeDown());
        UpdateHandler.UpdateOccurred += bulletMoveLeft;
    }

    protected override void OnDisable()
    {
        UpdateHandler.UpdateOccurred -= bulletMoveLeft;
    }

    private IEnumerator SetUpTakeDown() {
        Debug.Log("Column Active Time: " + columnActiveTime);
        sprt.color = new Color(sprt.color.r, sprt.color.g, sprt.color.b, 0.5f);
        yield return new WaitForSeconds(preColumnTime);
        Debug.Log("Column Active Time: " + columnActiveTime);
        columnCol.enabled = true;
        sprt.color = new Color(sprt.color.r, sprt.color.g, sprt.color.b, 1f);
        currTime = Time.time;
        Debug.Log("Column Active Time: " + columnActiveTime);
        yield return new WaitForSeconds(columnActiveTime);
        Debug.Log("Column Active Time: " + columnActiveTime);
        sprt.color = new Color(sprt.color.r, sprt.color.g, sprt.color.b, 0.2f);
        columnCol.enabled = false;
        yield return new WaitForSeconds(postColumnTime);
        gameObject.SetActive(false);
    }

    public override int interactWithPlayer()
    {
        int earnablePoint = 0;
        if (totalEarnedPoints < fullPointValue && Time.time - currTime >= timeElapsedToNextPoint) {
            earnablePoint = pointValue;
            currTime = Time.time;
            totalEarnedPoints += pointValue;
            Debug.Log("Total Earned Points: " + totalEarnedPoints);
        }
        return earnablePoint;
    }

}
