using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    private enum BulletType
    {
        Regular,
        Bouncy,
        Pong
    }
    [SerializeField] BulletType bulletType;
    public static ObjectPooler SharedInstanceBullet;
    public static ObjectPooler SharedInstanceBouncy;
    public static ObjectPooler SharedInstancePong;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    private void Awake()
    {
        if (bulletType.Equals(BulletType.Bouncy))
            SharedInstanceBouncy = this;
        else if (bulletType.Equals(BulletType.Pong))
            SharedInstancePong = this;
        else
            SharedInstanceBullet = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++) {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            obj.transform.parent = this.transform;
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject() {
        for (int i = 0; i < pooledObjects.Count; i++) {
            if (!pooledObjects[i].activeInHierarchy)
                return pooledObjects[i];
        }
        return null;
    }

    public void AddNewPooledObject(GameObject n) {
        pooledObjects.Add(n);
    }

    public void DisableAllBullets() {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            pooledObjects[i].SetActive(false);
        }
    }
}
