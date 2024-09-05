using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Light directionalLight;
    private ParticleSystem _particleSystem;
    private bool _isRain = false;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        StartCoroutine(Weather());
    }

    private void Update()
    {
        if (_isRain && directionalLight.intensity > 0.25f)
            LightIntensity(-1);
        else if (!_isRain && directionalLight.intensity < 0.5f)
            LightIntensity(1);
    }

    private void LightIntensity(int mult)
    {
        directionalLight.intensity += 0.1f * Time.deltaTime * mult;
    }

    IEnumerator Weather()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(20f, 35f));

            if (_isRain)
                _particleSystem.Stop();
            else
                _particleSystem.Play();
            

            _isRain = !_isRain;
        }
    }

}
