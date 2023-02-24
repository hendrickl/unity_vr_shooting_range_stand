using UnityEngine;

public class TargetManager : MonoBehaviour
{
    private bool _switch; // to enable/disable movement to see the impact on the target
    private bool _activateMovement; // to enable/disable movement of the target

    // * * *
    // Variables related to target position
    private Vector3 _currentTarget;
    [SerializeField] private GameObject _fixedTarget;
    [SerializeField] private Transform _movingTargetPositionA;
    [SerializeField] private Transform _movingTargetPositionB;
    [SerializeField] private Transform _fixedTargetTowardPlayerPosition;
    [SerializeField] private Transform _fixedTargetTowardBgPosition;

    // * * *
    // Variables related to target movement speed
    [SerializeField] private float _speed;
    [SerializeField] private float _speedOnSwitch;

    // * * *
    // Variables related to audio
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _impactSound;

    private void Start()
    {
        _currentTarget = _movingTargetPositionA.transform.position;
    }

    private void Update()
    {
        // Activate moving target if movement is enabled and the target isn't switched
        if (_activateMovement && !_switch)
        {
            ActivateMovingTarget(_speed);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        PlayAudioClip(_impactSound);
    }

    public void ActivateMovingTarget(float speed)
    {
        // Enable target movement and set the speed
        _activateMovement = true;
        speed = _speed;

        // Calculate the distance between the target object and its current target
        float _distance = Vector3.Distance(transform.position, _currentTarget);

        if (_distance <= _speed * Time.deltaTime)
        {
            // If the target object is close enough to the target, set it to the target position
            transform.position = _currentTarget;

            // Switch to the other target position
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
            // Move the target object towards the current target
            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);
        }
    }

    public void DeactivateMovingTarget(float speed)
    {
        // Disable target movement and set the speed to 0, Set the target object position to the fixed target's background position
        _activateMovement = false;
        speed = 0f;
        gameObject.transform.position = _fixedTargetTowardBgPosition.position;
    }

    public void MoveFixedTargetTowardPlayer()
    {
        // Move the fixed target towards the player position if it isn't already switched or moving
        if (_switch == false && !_activateMovement)
        {
            _switch = true;
            iTween.MoveTo(_fixedTarget, iTween.Hash("position", _fixedTargetTowardPlayerPosition, "speed", _speedOnSwitch, "easetype", "linear"));
        }
    }

    public void MoveFixedTargetTowardBg()
    {
        // Move the fixed target towards the background position if it is already switched
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
            throw new UnityException("The Audiosource is not initialized");
        }

        _audioSource.clip = clip;
        _audioSource.volume = 1f;
        _audioSource.Play();
    }

    public void StopAudio()
    {
        if (!_audioSource)
        {
            throw new UnityException("The Audiosource is not initialized");
        }

        _audioSource.Stop();
    }
}
