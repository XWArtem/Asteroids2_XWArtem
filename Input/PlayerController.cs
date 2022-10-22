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

    private bool _isActive;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        _playerControls.Enable();
        GameStates.OnGameStateChanged += SwitchControllerActivity;
    }
    private void OnDisable()
    {
        _playerControls.Disable();
        GameStates.OnGameStateChanged -= SwitchControllerActivity;
    }
    private void Update()
    {
        if (_isActive)
        {
            if (_playerControls.MoveandFire.MoveForward.ReadValue<float>() == 1)
            {
                _mainHeroPositionUpdate.Move(GameConfig.MainHeroMoveSpeed);
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
            if (_playerControls.MoveandFire.EscapeButton.ReadValue<float>() == 1)
            {
                GameStates.ChangeGameState(GameStates.GameState.GameOver);
            }
            
        }
    }

    private void SwitchControllerActivity(GameStates.GameState gameState)
    {
        switch (gameState)
        {
            case GameStates.GameState.GameOver:
                _isActive = false;
                break;
            case GameStates.GameState.PlayMode:
                _isActive = true;
                break;
            case GameStates.GameState.MainMenu:
                break;
            case GameStates.GameState.ScoreScreen:
                break;
            default:
                _isActive = true;
                break;
        }
    }

    private IEnumerator InertMove()
    {
        for (float i = 0.6f; i >= 0.04f; i *= 0.98f)
        {
            _mainHeroPositionUpdate.Move(i);
            yield return new WaitForEndOfFrame();
        }
    }
}
