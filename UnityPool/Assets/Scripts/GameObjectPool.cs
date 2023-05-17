using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] private int _poolSize = 10;
    [SerializeField] private int _maxPoolSize = 10;

    [Tooltip("Object derived from PooledObject class")]
    [SerializeField] private GameObject _pooledObject;

    private IObjectPool<GameObject> _pool;

    private void Awake()
    {
        // Creating pool
        _pool = new ObjectPool<GameObject>(CreatePoolObject,PooledObjectOnGet,PooledObjectOnRelease,PooledObjectOnDestroy,true,_poolSize,_maxPoolSize);
    }

    private GameObject CreatePoolObject()
    {
        GameObject pooledObject = Instantiate(_pooledObject);

        // Assign pool to pooled object
        pooledObject.GetComponent<Explosion>().SetPool(_pool);

        return pooledObject;
    }

    // When object spawn
    private void PooledObjectOnGet(GameObject pooledObject)
    {
        pooledObject.SetActive(true);
        SetRandomPosition(pooledObject);
    }

    // When object destroy
    private void PooledObjectOnRelease(GameObject pooledObject)
    {
        pooledObject.SetActive(false);
    }

    private void PooledObjectOnDestroy(GameObject pooledObject)
    {
        Destroy(pooledObject);
    }

    // Random position after spawn
    private void SetRandomPosition(GameObject pooledObject)
    {
        pooledObject.transform.position = Random.insideUnitSphere * 5f;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 70, 150, 50), "SpawnExplosion"))
        {
            this._pool.Get();
        }
    }
}
