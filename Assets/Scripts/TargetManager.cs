using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public bool Switch;

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
        ActivateMovingTarget();
    }

    private void OnCollisionEnter(Collision other)
    {
        PlayAudioClip(_impactSound);
    }

    public void ActivateMovingTarget()
    {
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

    public void MoveFixedTargetTowardPlayer()
    {
        if (Switch == false)
        {
            print("Target moves toward player");
            iTween.MoveTo(_fixedTarget, iTween.Hash("position", _fixedTargetTowardPlayerPosition, "speed", _speedOnSwitch, "easetype", "linear"));
            Switch = true;
            print("Switch = " + Switch);
        }
    }

    public void MoveFixedTargetTowardBg()
    {
        if (Switch)
        {
            print("Target moves toward Bg");
            iTween.MoveTo(_fixedTarget, iTween.Hash("position", _fixedTargetTowardBgPosition, "speed", _speedOnSwitch, "easetype", "linear"));
            Switch = false;
            print("Switch = " + Switch);
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
}
