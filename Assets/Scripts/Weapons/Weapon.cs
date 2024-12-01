using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f;

    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;
    public Transform parentTransform;

    void Awake()
    {
        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, collectionCheck, defaultCapacity, maxSize);
        timer = 0f;
        if (bulletSpawnPoint == null)
        {
            bulletSpawnPoint = transform;
        }
    }

    void Start()
    {
        timer = Time.time + shootIntervalInSeconds;
    }

    Bullet CreateBullet()
    {
        if (bullet == null)
        {
            Debug.LogError("Bullet prefab is not set.");
            return null;
        }
        Bullet bulletInstance = Instantiate(bullet); 
        bulletInstance.ObjectPool = objectPool;
        return bulletInstance;
    }

    void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject); 
    }

    void FixedUpdate()
    {
        if (Time.time >= timer && bullet != null)
        {
            Bullet bulletObject = objectPool.Get();
            if (bulletObject != null)
            {
                bulletObject.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                timer = Time.time + shootIntervalInSeconds; // Set next time to shoot
            }
        }
    }
}
