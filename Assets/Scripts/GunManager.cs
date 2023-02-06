using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GunManager : MonoBehaviour
{
    private int _munitionStock = 10;
    [SerializeField] private TMP_Text _munitionsText;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPosition;
    [SerializeField] private float _bulletForce = 100f;
    [SerializeField] private InputActionReference _shootAction;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClipMunition0;

    private void Start()
    {
        _shootAction.action.Enable();
        _shootAction.action.performed += ShootBullet;
    }

    private void ShootBullet(InputAction.CallbackContext obj)
    {
        if (_munitionStock != 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(_bulletSpawnPosition.position, _bulletSpawnPosition.forward, out hit, Mathf.Infinity))
            {
                Debug.Log("Hit " + hit.collider.gameObject.name);

                GameObject bulletToSpawn = Instantiate(_bulletPrefab, _bulletSpawnPosition.position, Quaternion.identity);
                Rigidbody bulletRigidbody = bulletToSpawn.GetComponent<Rigidbody>();

                _audioSource.Play();

                bulletRigidbody.AddForce(_bulletSpawnPosition.forward * _bulletForce, ForceMode.Impulse);

                _munitionStock--;
            }
        }
        else
        {
            _audioSource.clip = _audioClipMunition0;
            _audioSource.Play();
        }

        _munitionsText.text = _munitionStock.ToString();
    }
}