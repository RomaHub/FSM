using UnityEngine;
using System.Collections.Generic;

public class BulletPool : Singleton<BulletPool>
{

    public GameObject bulletPrefab;
    public int amount = 20;

    List<GameObject> pooledBullets;

    public void InitializePool()
    {
        pooledBullets = new List<GameObject>();
        for (int i = 0; i < amount; i++)
        {
            GameObject go = Instantiate(bulletPrefab);
            go.SetActive(false);
            pooledBullets.Add(go);
        }
    }

    public GameObject GetBullet()
    {

        GameObject bullet = pooledBullets.Find((go) => !go.activeInHierarchy);

        if (bullet != null)
        {
            return bullet;
        }

        GameObject newBullet = Instantiate(bulletPrefab);
        pooledBullets.Add(newBullet);
        amount++;
        return newBullet;

    }
}
