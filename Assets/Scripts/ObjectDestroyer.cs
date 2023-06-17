using SpaceShooter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    #region Singleton
    public static ObjectDestroyer Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public void DestroyGameObject(GameObject gameObject, float interval, ParticleSystem particleSystem, Action action)
    {
        StartCoroutine(DestroyGameObjectCoroutine(gameObject, interval, particleSystem, action));
    }

    IEnumerator DestroyGameObjectCoroutine(GameObject gameObject, float interval, ParticleSystem particleSystem, Action action)
    {
        Debug.Log("CoroutineStarted");
        
        if (particleSystem != null)
        {
            particleSystem.transform.position = gameObject.transform.position;
            particleSystem.Play();
        }
        
        Destroy(gameObject);        
        
        yield return new WaitForSeconds(interval);        
        action?.Invoke();
        Debug.Log("After coroutine");
    }

}
