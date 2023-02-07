using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GunManager : MonoBehaviour
{
    private RaycastHit hit;
    private int _munitionStock = 10;
    private int _headScore = 0;
    private int _bodyScore = 0;
    private bool _lightIsOn = false;
    private int _currentLightIndex = 0;

    [SerializeField] private TMP_Text _munitionsText;
    [SerializeField] private TMP_Text _headScoreText;
    [SerializeField] private TMP_Text _bodyScoreText;

    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPosition;
    [SerializeField] private float _bulletForce = 100f;

    [SerializeField] private InputActionReference _shootAction;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClipMunition0;
    [SerializeField] private AudioClip _audioClipShoot;
    [SerializeField] private AudioClip _audioClipReloadMunition;

    [SerializeField] private Light[] _lights = new Light[10];

    private void Start()
    {
        _shootAction.action.Enable();
        _shootAction.action.performed += ShootBullet;
    }

    private void ShootBullet(InputAction.CallbackContext obj)
    {
        if (_munitionStock != 0)
        {
            if (Physics.Raycast(_bulletSpawnPosition.position, _bulletSpawnPosition.forward, out hit, Mathf.Infinity))
            {
                GameObject bulletToSpawn = Instantiate(_bulletPrefab, _bulletSpawnPosition.position, Quaternion.identity);
                Rigidbody bulletRigidbody = bulletToSpawn.GetComponent<Rigidbody>();

                PlayAudioClip(_audioClipShoot);

                bulletRigidbody.AddForce(_bulletSpawnPosition.forward * _bulletForce, ForceMode.Impulse);
                _munitionStock--;
            }
        }
        else
        {
            PlayAudioClip(_audioClipMunition0);
        }

        DisplayMunition();
        Score();
    }

    private void reloadBullet()
    {
        // trigger clip + gun
        _audioSource.clip = _audioClipReloadMunition;
        _audioSource.Play();
    }

    private void PlayAudioClip(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
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

    private void DisplayMunition()
    {
        _munitionsText.text = _munitionStock.ToString();
    }

    private void Score()
    {
        if (hit.collider.gameObject.CompareTag("BodyTarget"))
        {
            LightOn();
            Debug.Log("Body score : " + _bodyScore);
            _bodyScore++;
            _bodyScoreText.text = _bodyScore.ToString();
        }

        if (hit.collider.gameObject.CompareTag("HeadTarget"))
        {
            LightOn();
            Debug.Log("Head score : " + _headScore);
            _headScore++;
            _headScoreText.text = _headScore.ToString();
        }
    }

    private void RaycastEngine()
    {

    }
}