using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField] private GameObject _movingTarget;
    [SerializeField] private GameObject _fixedTarget;
    [SerializeField] private Transform _movingTargetPosition;
    [SerializeField] private Transform _fixedTargetPosition;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedOnSwitch;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _impactSound;

    private void Update()
    {
        if (!_movingTargetPosition)
        {
            throw new UnityException();
        }

        MoveMovingTarget();
    }

    private void OnCollisionEnter(Collision other)
    {
        PlayAudioClip(_impactSound);
    }

    public void MoveMovingTarget()
    {
        iTween.MoveTo(_movingTarget, iTween.Hash("position", _movingTargetPosition, "speed", _speed, "loopType", "pingPong", "easetype", "linear"));
    }

    public void MoveFixedTarget()
    {
        iTween.MoveTo(_fixedTarget, iTween.Hash("position", _fixedTargetPosition, "speed", _speedOnSwitch, "easetype", "linear"));
    }

    private void PlayAudioClip(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.volume = 1f;
        _audioSource.Play();
    }
}
