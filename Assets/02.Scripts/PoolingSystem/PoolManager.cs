using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoBehaviour
{
    [Header("적 풀링 개수")] [SerializeField] private int _enemyPoolSize;

    [Header("적 종류")] [SerializeField] private List<Enemy> _enemyLists;

    [Header("총알 풀링 개수")] [SerializeField] private int _bulletPoolSize;

    [Header("총알 종류")] [SerializeField] private List<Bullet> _bulletLists;

    [Header("아이템 풀링 개수")] [SerializeField] private int _itemPoolSize;

    [Header("아이템 종류")] [SerializeField] private List<Item> _itemLists;


    private Dictionary<Enemy, Stack<Enemy>> _enemyPool
        = new Dictionary<Enemy, Stack<Enemy>>();

    private Dictionary<Bullet, Stack<Bullet>> _bulletPool
        = new Dictionary<Bullet, Stack<Bullet>>();

    private Dictionary<Item, Stack<Item>> _itemPool
        = new Dictionary<Item, Stack<Item>>();


    public static PoolManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        InitPool();
    }


    public void InputToPool(GameObject poolingObject)
    {
        if (poolingObject == null)
            return;

        if (poolingObject.TryGetComponent(out Enemy enemy))
        {
            PutEnemyToPool(enemy);
        }
        else if (poolingObject.TryGetComponent(out Bullet bullet))
        {
            PutBulletToPool(bullet);
        }
        else if (poolingObject.TryGetComponent(out Item item))
        {
            PutItemToPool(item);
        }
    }


    public PoolingObject GetFromPool(GameObject poolingObject)
    {
        if (poolingObject == null)
            return null;

        if (poolingObject.TryGetComponent(out Enemy enemy))
        {
            Enemy result = GetEnemyFromPool(enemy);

            if (result != null)
            {
                return result.GetComponent<PoolingObject>();
            }
        }
        else if (poolingObject.TryGetComponent(out Bullet bullet))
        {
            Bullet result = GetBulletFromPool(bullet);

            if (result != null)
            {
                return result.GetComponent<PoolingObject>();
            }
        }
        else if (poolingObject.TryGetComponent(out Item item))
        {
            Item result = GetItemFromPool(item);

            if (result != null)
            {
                return result.GetComponent<PoolingObject>();
            }
        }

        return null;
    }


    private void InitPool()
    {
        // Enemy
        foreach (Enemy enemyPrefab in _enemyLists)
        {
            Stack<Enemy> pool = new Stack<Enemy>();

            for (int i = 0; i < _enemyPoolSize; i++)
            {
                Enemy spawnedEnemy = Instantiate(
                    enemyPrefab,
                    Vector3.zero,
                    Quaternion.identity
                );

                PoolingObject spawnedPoolObject =
                    spawnedEnemy.GetComponent<PoolingObject>();

                spawnedPoolObject.InitPoolObject(enemyPrefab.gameObject);
                spawnedPoolObject.Deactivate();

                pool.Push(spawnedEnemy);
            }

            _enemyPool.Add(enemyPrefab, pool);
        }


        // Bullet
        foreach (Bullet bulletPrefab in _bulletLists)
        {
            Stack<Bullet> pool = new Stack<Bullet>();

            for (int i = 0; i < _bulletPoolSize; i++)
            {
                Bullet spawnedBullet = Instantiate(
                    bulletPrefab,
                    Vector3.zero,
                    Quaternion.identity
                );

                PoolingObject spawnedPoolObject =
                    spawnedBullet.GetComponent<PoolingObject>();

                spawnedPoolObject.InitPoolObject(bulletPrefab.gameObject);
                spawnedPoolObject.Deactivate();

                pool.Push(spawnedBullet);
            }

            _bulletPool.Add(bulletPrefab, pool);
        }


        // Item
        foreach (Item itemPrefab in _itemLists)
        {
            Stack<Item> pool = new Stack<Item>();

            for (int i = 0; i < _itemPoolSize; i++)
            {
                Item spawnedItem = Instantiate(
                    itemPrefab,
                    Vector3.zero,
                    Quaternion.identity
                );

                PoolingObject spawnedPoolObject =
                    spawnedItem.GetComponent<PoolingObject>();

                spawnedPoolObject.InitPoolObject(itemPrefab.gameObject);
                spawnedPoolObject.Deactivate();

                pool.Push(spawnedItem);
            }

            _itemPool.Add(itemPrefab, pool);
        }
    }


    private Enemy GetEnemyFromPool(Enemy enemyPrefab)
    {
        if (!_enemyPool.TryGetValue(enemyPrefab, out Stack<Enemy> pool))
        {
            return null;
        }

        if (pool.Count == 0)
        {
            return null;
        }

        Enemy enemy = pool.Pop();

        enemy.GetComponent<PoolingObject>().Activate();

        return enemy;
    }


    private Bullet GetBulletFromPool(Bullet bulletPrefab)
    {
        if (!_bulletPool.TryGetValue(bulletPrefab, out Stack<Bullet> pool))
        {
            return null;
        }

        if (pool.Count == 0)
        {
            return null;
        }

        Bullet bullet = pool.Pop();

        bullet.GetComponent<PoolingObject>().Activate();

        return bullet;
    }


    private Item GetItemFromPool(Item itemPrefab)
    {
        if (!_itemPool.TryGetValue(itemPrefab, out Stack<Item> pool))
        {
            return null;
        }

        if (pool.Count == 0)
        {
            return null;
        }

        Item item = pool.Pop();

        item.GetComponent<PoolingObject>().Activate();

        return item;
    }


    private void PutEnemyToPool(Enemy inputEnemy)
    {
        PoolingObject poolingObject =
            inputEnemy.GetComponent<PoolingObject>();

        GameObject originPrefab =
            poolingObject.GetPoolKey();

        Enemy enemyPrefab =
            originPrefab.GetComponent<Enemy>();

        if (_enemyPool.TryGetValue(enemyPrefab, out Stack<Enemy> pool))
        {
            poolingObject.Deactivate();
            pool.Push(inputEnemy);
        }
    }


    private void PutBulletToPool(Bullet inputBullet)
    {
        PoolingObject poolingObject =
            inputBullet.GetComponent<PoolingObject>();

        GameObject originPrefab =
            poolingObject.GetPoolKey();

        Bullet bulletPrefab =
            originPrefab.GetComponent<Bullet>();

        if (_bulletPool.TryGetValue(bulletPrefab, out Stack<Bullet> pool))
        {
            poolingObject.Deactivate();
            pool.Push(inputBullet);
        }
    }


    private void PutItemToPool(Item inputItem)
    {
        PoolingObject poolingObject =
            inputItem.GetComponent<PoolingObject>();

        GameObject originPrefab =
            poolingObject.GetPoolKey();

        Item itemPrefab =
            originPrefab.GetComponent<Item>();

        if (_itemPool.TryGetValue(itemPrefab, out Stack<Item> pool))
        {
            poolingObject.Deactivate();
            pool.Push(inputItem);
        }
    }
}
