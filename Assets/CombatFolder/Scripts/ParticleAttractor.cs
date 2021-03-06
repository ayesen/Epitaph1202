using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAttractor : MonoBehaviour
{

    public Transform attractorTransform;

    private ParticleSystem _particleSystem;
    private ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[1000];

    public int particleAmount;

    public void Start()
    {
        //_attractorTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _particleSystem = GetComponent<ParticleSystem>();
        _particles = new ParticleSystem.Particle[particleAmount];
    }

    public void LateUpdate()
    {
        if (_particleSystem.isPlaying && attractorTransform != null)
        {
            int length = _particleSystem.GetParticles(_particles);
            Vector3 attractorPosition = new Vector3(attractorTransform.position.x, attractorTransform.position.y + 2, attractorTransform.position.z);

            for (int i = 0; i < length; i++)
            {
                _particles[i].position = _particles[i].position + (attractorPosition - _particles[i].position) / (_particles[i].remainingLifetime) * Time.deltaTime;
            }
            _particleSystem.SetParticles(_particles, length);
        }

    }
}
