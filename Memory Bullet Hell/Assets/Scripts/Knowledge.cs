﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knowledge : MonoBehaviour
{
    private enum BulletType { 
        Regular,
        Bouncy,
        Pong
    }
    [SerializeField] BulletType bulletType;
    [SerializeField] private GameObject extraBulletsPlease;
    [SerializeField] private float bulletSpeed = 5.0f;
    [SerializeField] private float bouncyX;
    [SerializeField] private float bouncyY;
    [SerializeField] private List<Vector2> newPongDirections;
    private float startTime;
    private float extraBulletCount;
    private int bulletcount = 0;
    [SerializeField] private bool disableAll;

    [Header("Turret Customization")]
    [SerializeField] private float timeInterval = 1.0f;
    [Tooltip("Toggle to have turret shoot Immediately (Initial to shoot is true)")]
    [SerializeField] private bool shoot = false;

    private bool shootState = false;

    private void OnEnable()
    {
        disableAll = false;
        startTime = Time.time;
        extraBulletCount = 0;
        UpdateHandler.UpdateOccurred += ShootByIntervals;
    }

    private void OnDisable()
    {
        UpdateHandler.UpdateOccurred -= ShootByIntervals;
        Debug.Log(extraBulletCount + " extra " + bulletType.ToString() + "bullet(s) instantiated");
        shootState = false;
    }

    private void ShootByIntervals() {
        if (disableAll) {
            if (bulletType.Equals(BulletType.Bouncy))
            {
                ObjectPooler.SharedInstanceBouncy.DisableAllBullets();
            }
            else if (bulletType.Equals(BulletType.Pong))
            {
                ObjectPooler.SharedInstanceBullet.DisableAllBullets();
            }
            else
                ObjectPooler.SharedInstancePong.DisableAllBullets();
        }
            
        if ((Time.time - startTime >= timeInterval && timeInterval != 0) || (shoot != shootState)) {
            Shoot();
            startTime = Time.time;
            shootState = shoot;
        }
    }

    private void Shoot() {
        GameObject bullet;
        if (bulletType.Equals(BulletType.Bouncy)) {
            bullet = ObjectPooler.SharedInstanceBouncy.GetPooledObject();
        }
        else if (bulletType.Equals(BulletType.Pong)) {
            bullet = ObjectPooler.SharedInstancePong.GetPooledObject();
        } 
        else
            bullet = ObjectPooler.SharedInstanceBullet.GetPooledObject();

        if (bullet != null){
            if (bulletType.Equals(BulletType.Bouncy))
            {
                bullet.GetComponent<BulletBouncy>().SetDirection(bouncyX, bouncyY);
            }
            else if (bulletType.Equals(BulletType.Pong))
            {
                bullet.GetComponent<BulletPong>().SetDirection(bouncyX, bouncyY);
                bullet.GetComponent<BulletPong>().SetNewPongDirections(newPongDirections);
            }
            bullet.GetComponent<BulletMovement>().SetSprite(bulletcount);
            bulletcount++;
            bullet.GetComponent<BulletMovement>().SetBulletSpeed(bulletSpeed);
            bullet.transform.position = this.transform.position;
            bullet.transform.rotation = this.transform.rotation;
            
            bullet.SetActive(true);
        }
        else {
            if (bulletType.Equals(BulletType.Bouncy))
            {
                bullet = Instantiate(extraBulletsPlease, this.transform.position, this.transform.rotation);
                ObjectPooler.SharedInstanceBouncy.AddNewPooledObject(bullet);
            }
            else if (bulletType.Equals(BulletType.Pong))
            {
                bullet = Instantiate(extraBulletsPlease, this.transform.position, this.transform.rotation);
                ObjectPooler.SharedInstancePong.AddNewPooledObject(bullet);
            }
            else {
                bullet = Instantiate(extraBulletsPlease, this.transform.position, this.transform.rotation);
                ObjectPooler.SharedInstanceBullet.AddNewPooledObject(bullet);
            }
            extraBulletCount++;
        }
        if(Grader.GraderInstance != null)
            Grader.GraderInstance.AddToMaxScore(bullet.GetComponent<BulletMovement>().pointValue);
    }
}
