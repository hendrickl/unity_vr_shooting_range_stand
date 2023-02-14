using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    private bool _switch;
    private bool _activateMovement;

    private Vector3 _currentTarget;
    [SerializeField] private GameObject _fixedTarget;
    [SerializeField] private Transform _movingTargetPositionA;
    [SerializeField] private Transform _movingTargetPositionB;
    [SerializeField] private Transform _fixedTargetTowardPlayerPosition;
    [SerializeField] private Transform _fixedTargetTowardBgPosition;

    [SerializeField] private float _speed;
    [SerializeField] private float _speedOnSwitch;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _impactSound;

    private void Start()
    {
        _currentTarget = _movingTargetPositionA.transform.position;
    }

    private void Update()
    {
        if (_activateMovement && !_switch)
        {
            ActivateMovingTarget();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        PlayAudioClip(_impactSound);
    }

    public void ActivateMovingTarget()
    {
        _activateMovement = true;
        _speed = 1.5f;

        float _distance = Vector3.Distance(transform.position, _currentTarget);

        if (_distance <= _speed * Time.deltaTime)
        {
            transform.position = _currentTarget;

            if (_currentTarget == _movingTargetPositionB.position)
            {
                _currentTarget = _movingTargetPositionA.position;
            }
            else
            {
                _currentTarget = _movingTargetPositionB.position;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);
        }
    }

    public void DeactivateMovingTarget()
    {
        _activateMovement = false;
        _speed = 0f;
        gameObject.transform.position = _fixedTargetTowardBgPosition.position;
    }

    public void MoveFixedTargetTowardPlayer()
    {
        if (_switch == false && !_activateMovement)
        {
            _switch = true;
            iTween.MoveTo(_fixedTarget, iTween.Hash("position", _fixedTargetTowardPlayerPosition, "speed", _speedOnSwitch, "easetype", "linear"));
        }
    }

    public void MoveFixedTargetTowardBg()
    {
        if (_switch)
        {
            _switch = false;
            iTween.MoveTo(_fixedTarget, iTween.Hash("position", _fixedTargetTowardBgPosition, "speed", _speedOnSwitch, "easetype", "linear"));
        }
    }

    private void PlayAudioClip(AudioClip clip)
    {
        if (!_audioSource)
        {
            throw new UnityException();
        }

        _audioSource.clip = clip;
        _audioSource.volume = 1f;
        _audioSource.Play();
    }

    public void StopAudio()
    {
        if (!_audioSource)
        {
            Debug.LogWarning("Audio source is not defined");
            throw new UnityException();
        }

        print("Stop audio");
        _audioSource.Stop();
        // _audioSource.volume = 0f;
    }
}
