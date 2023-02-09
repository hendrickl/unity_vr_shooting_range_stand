using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public bool Switch;
    [SerializeField] private GameObject _movingTarget;
    [SerializeField] private GameObject _fixedTarget;
    [SerializeField] private Transform _movingTargetPosition;
    [SerializeField] private Transform _fixedTargetTowardPlayerPosition;
    [SerializeField] private Transform _fixedTargetTowardBgPosition;
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
        _audioSource.clip = clip;
        _audioSource.volume = 1f;
        _audioSource.Play();
    }
}
