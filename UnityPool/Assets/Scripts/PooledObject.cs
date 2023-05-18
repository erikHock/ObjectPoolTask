using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledObject : MonoBehaviour
{
    private IObjectPool<GameObject> _pool;

    public void SetPool(IObjectPool<GameObject> pool)
    {
        _pool = pool;
    }

    public void ReleasePooledObject()
    {
        _pool.Release(this.gameObject);
    }
}
