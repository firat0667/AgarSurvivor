using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ParticleType
{
    Circle,
    Square
}


public class ParticleManager : MonoBehaviour
{
    public static ParticleManager Instance;

    [SerializeField] private ParticleSystem _particleSystem;


    [Header("Pool")]
    [SerializeField] private ObjectPool _particlePool;


    [Header("Particles")]

    [SerializeField] private Mesh _squareParticle;
    [SerializeField] private Mesh _circleParticle;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Update()
    {

    }

    public void CreateParticle(Transform particleTransform,EnemyType enemyType)
    {
           
        GameObject go = _particlePool.GetObject();
        go.transform.position = particleTransform.position;
        ParticleSystem particle =go.GetComponent<ParticleSystem>();
        ParticleSystemRenderer particleRenderer = particle.GetComponent<ParticleSystemRenderer>();
        particleRenderer.mesh= chouseMesh(chouseParticleType(enemyType));
        particle.Play();
        StartCoroutine(ReturnParticleToPool(go, particle.main.duration));
       
    }
    ParticleType chouseParticleType(EnemyType enemyType) => enemyType switch
    {
        EnemyType.Normal => ParticleType.Circle,  
        EnemyType.Square => ParticleType.Square,   
        _ => ParticleType.Circle                    
    };

    Mesh chouseMesh(ParticleType particleType) => particleType switch
    {
        ParticleType.Circle => _circleParticle,  
        ParticleType.Square => _squareParticle,  
        _ => _circleParticle                   
    };


    private IEnumerator ReturnParticleToPool(GameObject particle, float delay)
    {
        yield return new WaitForSeconds(delay);  
        _particlePool.ReturnObject(particle);  
    }
}
