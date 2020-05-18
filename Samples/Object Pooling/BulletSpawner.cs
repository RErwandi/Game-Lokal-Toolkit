using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public BulletPool pool;
    public float spawnInterval;

    private void Start()
    {
        InvokeRepeating("SpawnBullet", 0f, spawnInterval);
    }

    private void SpawnBullet()
    {
        var bullet = pool.Rent();
        bullet.pool = pool;
    }
}
