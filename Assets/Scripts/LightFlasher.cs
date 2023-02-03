using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlasher : MonoBehaviour
{
    private bool _lightIsOn = false;
    private int _currentIndex = 0;
    [SerializeField] private Light[] _lights = new Light[10];

    private void OnCollisionEnter(Collision other)
    {
        if (gameObject.CompareTag("BodyTarget"))
        {
            if (_currentIndex < _lights.Length)
            {
                _lightIsOn = !_lightIsOn;
                _lights[_currentIndex].color = Color.green;
                _lights[_currentIndex].gameObject.SetActive(_lightIsOn);
                _currentIndex++;
            }
            _lightIsOn = false;
        }
    }
}
