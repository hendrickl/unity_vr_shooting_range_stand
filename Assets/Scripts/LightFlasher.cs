using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlasher : MonoBehaviour
{
    private bool _lightIsOn = false;
    [SerializeField] private Light _light;

    private void OnCollisionEnter(Collision other)
    {
        if (gameObject.CompareTag("BodyTarget"))
        {
            _lightIsOn = !_lightIsOn;
            _light.color = Color.green;
            _light.gameObject.SetActive(_lightIsOn);
        }
        _lightIsOn = false;
    }
}
