using UnityEngine;
using TMPro;

public class GunManager : MonoBehaviour
{
    private RaycastHit hit;
    private bool _lightIsOn = false;
    private int _currentLightIndex = 0;
    private bool _hasReload = false;
    // * * *
    [SerializeField] private int _munitionStock;
    [SerializeField] private int _munitionReload;
    [SerializeField] private int _headScore;
    [SerializeField] private int _bodyScore;
    // * * *
    [SerializeField] private TMP_Text _munitionsText;
    [SerializeField] private TMP_Text _headScoreText;
    [SerializeField] private TMP_Text _bodyScoreText;
    // * * *
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _impactPrefab;
    [SerializeField] private GameObject _targetFixedContainer;
    [SerializeField] private Transform _bulletSpawnPosition;
    [SerializeField] private float _bulletForce = 100f;
    // * * *
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClipMunition0;
    [SerializeField] private AudioClip _audioClipShoot;
    [SerializeField] private AudioClip _audioClipReload;
    // * * *
    [SerializeField] private Light[] _lights = new Light[10];

    private void OnTriggerEnter(Collider other)
    {
        if (_munitionStock == 0 && !_hasReload)
        {
            ReloadBullet(other);
            DisplayMunitionAmount();
            LightOff();
        }
    }

    public void ShootBullet()
    {
        if (_munitionStock > 0)
        {
            RaycastShooter();
            PlayAudioClip(_audioClipShoot);
            Score();
            _munitionStock--;
            _hasReload = false;
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

        impactToSpawn.transform.SetParent(_targetFixedContainer.transform);
    }

    private void ReloadBullet(Collider other)
    {
        if (other.gameObject.CompareTag("Reload"))
        {
            _hasReload = true;
            PlayAudioClip(_audioClipReload);
            other.gameObject.SetActive(false);
            _munitionStock = _munitionReload;
        }
    }

    private void PlayAudioClip(AudioClip clip)
    {
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

    private void LightOff()
    {
        for (int i = 0; i < _lights.Length; i++)
        {
            _currentLightIndex = 0;
            _lights[i].gameObject.SetActive(false);
        }
    }

    private void DisplayMunitionAmount()
    {
        _munitionsText.text = _munitionStock.ToString();
    }

    private void Score()
    {
        if (hit.collider.gameObject.CompareTag("BodyTarget"))
        {
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