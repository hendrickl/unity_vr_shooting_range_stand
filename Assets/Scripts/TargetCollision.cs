using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetCollision : MonoBehaviour
{
    private int _headScore = 0;
    private int _bodyScore = 0;
    private bool _lightIsOn = false;
    private int _currentLightIndex = 0;
    [SerializeField] private Light[] _lights = new Light[10];
    [SerializeField] private TMP_Text _headScoreText;
    [SerializeField] private TMP_Text _bodyScoreText;

    private void OnCollisionEnter(Collision other)
    {
        if (_lights != null)
        {
            LightOn();
        }
        else
        {
            throw new UnityException();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("BodyTarget"))
        {
            _bodyScore++;
            _bodyScoreText.text = _bodyScore.ToString();
        }

        if (gameObject.CompareTag("HeadTarget"))
        {
            _headScore++;
            _headScoreText.text = _headScore.ToString();
        }
    }

    private void LightOn()
    {
        if (_currentLightIndex < _lights.Length)
        {
            _lightIsOn = !_lightIsOn;
            _lights[_currentLightIndex].gameObject.SetActive(_lightIsOn);
            _currentLightIndex++;
        }
        _lightIsOn = false;
    }
}
