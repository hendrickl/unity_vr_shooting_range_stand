using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetContainerManager : MonoBehaviour
{
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private float _speed;
    [SerializeField] private AudioSource _audioSource;

    private void Update()
    {
        if (!_targetPosition)
        {
            throw new UnityException();
        }

        MoveTo();
    }

    private void MoveTo()
    {
        iTween.MoveTo(gameObject, iTween.Hash("position", _targetPosition, "speed", _speed, "loopType", "pingPong", "easetype", "linear"));
    }

    private void OnCollisionEnter(Collision other)
    {
        _audioSource.volume = 1f;
        _audioSource.Play();
    }
}
