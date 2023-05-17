using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : PooledObject
{
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private AudioSource _audioSource;
    
    private IEnumerator BeginExplosion()
    {
        _audioSource.Play();
        _particles.Play();

        while (_particles.isPlaying || _audioSource.isPlaying)
        {
            yield return new WaitForSeconds(0.1f);
        }

        ReleasePooledObject();
        yield return null;
    }

    private void OnEnable()
    {
        StartCoroutine(BeginExplosion());
    }
        
}
