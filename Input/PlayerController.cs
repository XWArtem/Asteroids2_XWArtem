using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private MainHeroPositionUpdate _mainHeroPositionUpdate = new MainHeroPositionUpdate();
    private MainHeroWeapon _mainHeroWeapon = new MainHeroWeapon();

    private PlayerControls _playerControls;
    public bool _heroIsMoving;
    public bool _heroIsRotatingLeft;
    public bool _heroIsRotatingRight;
    public bool _heroIsShootingFirstWeapon;
    public bool _heroIsShootingSecondWeapon;
    private bool _lastFrameHeroMoved;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        //_playerControls.MoveandFire.ShootFirstWeapon.performed += FirstWeaponShootPerfomed;
    }
    private void OnEnable()
    {
        _playerControls.Enable();
    }
    private void OnDisable()
    {
        _playerControls.Disable();

    }
    private void Update()
    {
        if (_playerControls.MoveandFire.MoveForward.ReadValue<float>() == 1)
        {
            _mainHeroPositionUpdate.Move(1.0f);
            _lastFrameHeroMoved = true;
        }
        else if (_playerControls.MoveandFire.MoveForward.ReadValue<float>() == 0 && _lastFrameHeroMoved)
        {
            StartCoroutine(InertMove());
            _lastFrameHeroMoved = false;
        }
        if (_playerControls.MoveandFire.RotateLeft.ReadValue<float>() == 1)
        {
            _mainHeroPositionUpdate.Rotate(true);
        }
        if (_playerControls.MoveandFire.RotateRight.ReadValue<float>() == 1)
        {
            _mainHeroPositionUpdate.Rotate(false);
        }
        if (_playerControls.MoveandFire.ShootFirstWeapon.triggered)
        {
            _mainHeroWeapon.PerfomBulletShoot();
        }
        if (_playerControls.MoveandFire.ShootSecondWeapon.ReadValue<float>() == 1)
        {
            
        }
    }

    private IEnumerator InertMove()
    {
        for (float i = 0.6f; i >= 0.02f; i *= 0.99f)
        {
            _mainHeroPositionUpdate.Move(i);
            yield return new WaitForEndOfFrame();
        }
    }
}
