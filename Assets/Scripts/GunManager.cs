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
    [SerializeField] private GameObject _impactPrefab;
    [SerializeField] private GameObject _targetContainer;
    [SerializeField] private Transform _bulletSpawnPosition;
    [SerializeField] private float _bulletForce = 100f;

    [SerializeField] private InputActionReference _shootAction;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClipMunition0;
    [SerializeField] private AudioClip _audioClipShoot;
    [SerializeField] private AudioClip _audioClipReload;
    [SerializeField] private AudioClip _audioClipReloadMunition;

    [SerializeField] private Light[] _lights = new Light[10];

    private void Start()
    {
        _shootAction.action.Enable();
        _shootAction.action.performed += ShootBullet;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_munitionStock == 0)
        {
            ReloadBullet(other);
            DisplayMunitionAmount();
        }
    }

    private void ShootBullet(InputAction.CallbackContext obj)
    {
        if (_munitionStock > 0)
        {
            RaycastShooter();
            PlayAudioClip(_audioClipShoot);
            Score();
            _munitionStock--;
        }
        else
        {
            PlayAudioClip(_audioClipMunition0);
        }

        DisplayMunitionAmount();
    }

    private void RaycastShooter()
    {
        if (!Physics.Raycast(_bulletSpawnPosition.position, _bulletSpawnPosition.forward, out hit, Mathf.Infinity))
        {
            throw new UnityException();
        }
        else
        {
            GameObject bulletToSpawn = Instantiate(_bulletPrefab, _bulletSpawnPosition.position, Quaternion.identity);
            Rigidbody bulletRigidbody = bulletToSpawn.GetComponent<Rigidbody>();
            bulletRigidbody.AddForce(_bulletSpawnPosition.forward * _bulletForce, ForceMode.Impulse);

            RaycastImpactMarker();
        }
    }

    private void RaycastImpactMarker()
    {
        Vector3 hitPosition = hit.point;
        GameObject impactToSpawn = Instantiate(_impactPrefab, hitPosition, Quaternion.identity);
        impactToSpawn.transform.SetParent(_targetContainer.transform);
    }

    private void ReloadBullet(Collider other)
    {
        if (other.gameObject.CompareTag("Reload"))
        {
            PlayAudioClip(_audioClipReload);
            other.gameObject.SetActive(false);
            _munitionStock = 10;
        }
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

    private void DisplayMunitionAmount()
    {
        _munitionsText.text = _munitionStock.ToString();
    }

    private void Score()
    {
        if (hit.collider.gameObject.CompareTag("BodyTarget"))
        {
            LightOn();
            _bodyScore++;
            _bodyScoreText.text = _bodyScore.ToString();
        }

        if (hit.collider.gameObject.CompareTag("HeadTarget"))
        {
            LightOn();
            _headScore++;
            _headScoreText.text = _headScore.ToString();
        }
    }
}