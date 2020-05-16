using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knowledge : MonoBehaviour
{
    [SerializeField] private GameObject extraBulletsPlease;
    [SerializeField] private bool bouncy;
    [SerializeField] private float bouncyX;
    [SerializeField] private float bouncyY;
    private float startTime;
    private float extraBulletCount;

    [Header("Turret Customization")]
    [SerializeField] private float timeInterval = 1.0f;
    [Tooltip("Toggle to have turret shoot Immediately (Initial to shoot is true)")]
    [SerializeField] private bool shoot = false;

    private bool shootState = false;

    private void OnEnable()
    {
        startTime = Time.time;
        extraBulletCount = 0;
        UpdateHandler.UpdateOccurred += ShootByIntervals;
    }

    private void OnDisable()
    {
        UpdateHandler.UpdateOccurred -= ShootByIntervals;
        Debug.Log(extraBulletCount + " extra bullet(s) instantiated");
    }

    private void ShootByIntervals() {
        if ((Time.time - startTime >= timeInterval) || (shoot != shootState)) {
            Shoot();
            startTime = Time.time;
            shootState = shoot;
        }
    }

    private void Shoot() {
        GameObject bullet;
        if (bouncy) {
            bullet = ObjectPooler.SharedInstanceBouncy.GetPooledObject();
            bullet.GetComponent<BulletBouncy>().SetDirection(bouncyX, bouncyY);
        } 
        else
            bullet = ObjectPooler.SharedInstanceBullet.GetPooledObject();
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
}
