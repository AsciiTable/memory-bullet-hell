using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knowledge : MonoBehaviour
{
    [SerializeField] private float timeInterval = 1.0f;
    [SerializeField] private GameObject extraBulletsPlease;
    private float startTime;
    private float extraBulletCount;

    [Header("Testing Y Position")]
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float yMax = 0f;
    [SerializeField] private float yMin = -10f;

    private void OnEnable()
    {
        startTime = Time.time;
        extraBulletCount = 0;
        UpdateHandler.UpdateOccurred += ShootByIntervals;
        UpdateHandler.UpdateOccurred += MoveUpDown;
    }

    private void OnDisable()
    {
        UpdateHandler.UpdateOccurred -= ShootByIntervals;
        UpdateHandler.UpdateOccurred -= MoveUpDown;
        Debug.Log(extraBulletCount + " extra bullet(s) instantiated");
    }

    private void ShootByIntervals() {
        if (Time.time - startTime >= timeInterval) {
            Shoot();
            startTime = Time.time;
        }
    }

    private void Shoot() {
        GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();
        if (bullet != null){
            bullet.transform.position = this.transform.position;
            bullet.transform.rotation = this.transform.rotation;
            bullet.SetActive(true);
        }
        else {
            Instantiate(extraBulletsPlease, this.transform.position, this.transform.rotation);
            extraBulletCount++;
        }
    }

    //Turret Moving Up and Down
    private void MoveUpDown()
    {

        if((transform.position.y >= yMax && moveSpeed > 0) || (transform.position.y <= yMin && moveSpeed < 0))
        {
            moveSpeed *= -1;
        }

        transform.position += new Vector3(0, moveSpeed, 0) * Time.deltaTime;
    }
}
