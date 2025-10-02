using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour, IUseable, IHoldable, IReloadable
{
    [SerializeField] private GameObject _barrel;
    [SerializeField] private Bullet _bulletPrefab;

    [Header("Stats")]
    [SerializeField] private float _damage = 2.5f;
    [SerializeField] private float _fireRate = 1.0f;
    [SerializeField] private float _reloadRate = 3.0f;

    [Header("Magazine")]
    [SerializeField] private int _maxMagazine = 12;
    [SerializeField] private int _ammoCapacity = 120;

    [Header("Customisations")]
    [SerializeField] private bool _canAim = false;

    #region Working Variables

    private bool _hasShot = false;
    private bool _isReloading = false;
    private int _ammo = 0;
    private int _ammoReserves = 0;
    private PlayerManager _playerManager;
    private Interactable _interactable;

    #endregion

    private void Awake()
    {
        _interactable = GetComponent<Interactable>();
        _interactable.PickedUpE += AttachPlayer;
        _ammo = _maxMagazine;
        _ammoReserves = _ammoCapacity;
    }

    public void PrimaryUse()
    {
        if (!CanShoot()) return;

        Shoot();
    }

    public void SecondaryUse()
    {
        if (!_canAim) return;
    }

    private bool CanShoot()
    {
        if (_hasShot) return false;
        if (_ammo <= 0) return false;
        if (_isReloading) return false;
        return true;
    }

    private void Shoot()
    {
        _hasShot = true;
        _ammo--;

        Bullet bullet = ObjectPoolManager.Instance.GetPrefab("bullet").GetComponent<Bullet>();
        bullet.Spawn(_barrel.transform, _playerManager.GetPlayerComponent<PlayerCameraBase>().Camera.transform, _damage);
        bullet.Move();

        StartCoroutine(ShootTimer());
        if (_ammo <= 0 && CanReload())
            Reload();
    }

    private bool CanReload()
    {
        if (_ammoReserves <= 0) return false;
        if (_ammo >= _maxMagazine) return false;
        if (_isReloading) return false;
        return true;
    }

    private void Reload()
    {
        _isReloading = true;

        if (_ammoReserves < _maxMagazine)
        {
            _ammo = _ammoReserves;
            _ammoReserves = 0;
        }
        else
        {
            int ammoDiff = _maxMagazine - _ammo;
            _ammo = _maxMagazine;
            _ammoReserves-= ammoDiff;
        }
        StartCoroutine(ReloadTimer());
    }

    private IEnumerator ReloadTimer()
    {
        yield return new WaitForSeconds(_reloadRate);
        _isReloading = false;
    }

    private IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(_fireRate);
        _hasShot = false;
    }

    private void AttachPlayer()
    {
        _playerManager = _interactable.AttachedPlayer;
    }

    private void OnDestroy()
    {
        _interactable.PickedUpE -= AttachPlayer;
    }

    public string GetInformation()
    {
        if (_isReloading)
            return "Reloading...";
        return $"{_ammo}/{_ammoReserves}";
    }

    public string GetName()
    {
        return GetComponent<Interactable>().ItemName;
    }

    void IReloadable.Reload()
    {
        Reload();
    }
}
