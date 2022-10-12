using UnityEngine;
using System.Collections.Generic;

public class UnityEngineUpdate : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;
    public static UnityEngineUpdate Instance;
    private AsteroidsPositionUpdate _asteroidsPositionUpdate = new AsteroidsPositionUpdate();
    private BulletPositionUpdate _bulletPositionUpdate = new BulletPositionUpdate();
    private GameObjectPool _gameObjectPool; // убрать

    private PoolAsteroid _poolAsteroid;
    private PoolBullet _poolBullet;
    private PoolUFO _poolUFO;
    private PoolMainHero _poolMainHero;

    private GameObject _mainHero;

    public List<GameObject> Asteroids = new List<GameObject>(); // убрать
    public List<GameObject> Bullets = new List<GameObject>(); // убрать ?

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        MainHeroPositionUpdate.TransformMainHeroAction += TransformMainHero;
        MainHeroPositionUpdate.RotateMainHeroAction += RotateMainHero;
        AsteroidsPositionUpdate.TransformAsteroid += TransformAsteroid;
        AsteroidsPositionUpdate.RotateAsteroid += RotateAsteroid;
        BulletPositionUpdate.TransformBullet += TransformBullet;
    }

    private void Start()
    {
        _poolAsteroid = new PoolAsteroid(GameConfig.NumberOfAsteroids);
        _poolBullet = new PoolBullet(GameConfig.NumberOfBullets);
        _poolUFO = new PoolUFO(GameConfig.NumberOfUFO);
        _poolMainHero = new PoolMainHero();
        _mainHero = _poolMainHero.MainHero;

        //_gameObjectPool = new GameObjectPool
        //    (GameConfig.NumberOfAsteroids,
        //    GameConfig.NumberOfBullets,
        //    GameConfig.NumberOfUFO); // убрать
    }

    private void TransformMainHero(float newX, float newY)
    {
        _mainHero.transform.position = new Vector2(newX, newY);
    }

    private void RotateMainHero(float newRotationAngle)
    {
        _mainHero.transform.rotation = Quaternion.Euler(0.0f, 0.0f, newRotationAngle);
    }

    private void TransformAsteroid(int asteroidIndex, float newX, float newY)
    {
        var asteroid = _poolAsteroid._asteroidList[asteroidIndex];
        asteroid.transform.position = new Vector2(newX, newY);
    }

    private void RotateAsteroid(int asteroidIndex, float newRotationAngle)
    {
        var asteroid = _poolAsteroid._asteroidList[asteroidIndex];
        asteroid.transform.rotation = Quaternion.Euler(0.0f, 0.0f, newRotationAngle);
    }

    private void TransformBullet(int bulletIndex, float newX, float newY, bool isOutOfRange)
    {
        var bullet = _poolBullet._bulletList[bulletIndex];
        bullet.transform.position = new Vector2(newX, newY);

        if (isOutOfRange)
        {
            
            _poolBullet.ReturnToPool(bulletIndex);
            EntityPool.BulletEntitiesPool.Find
                (e => e.Name.Contains(ConstStrings.BULLETNAME + bulletIndex.ToString())).CurrentX = 0;
            EntityPool.BulletEntitiesPool.Find
                (e => e.Name.Contains(ConstStrings.BULLETNAME + bulletIndex.ToString())).CurrentY = 0;



            _poolBullet._bulletList[bulletIndex].SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        foreach (var asteroid in _poolAsteroid._asteroidList)
        {
            if (asteroid.activeSelf == true)
            {
                int.TryParse(asteroid.name.Replace(ConstStrings.ASTEROIDNAME, ""), out int index);
                _asteroidsPositionUpdate.Transform(index);
            }
        }

        foreach (var bullet in _poolBullet._bulletList)
        {
            if (bullet.activeSelf == true)
            {
                int.TryParse(bullet.name.Replace(ConstStrings.BULLETNAME, ""), out int index);
                _bulletPositionUpdate.Move(index);
            }
        }

        foreach (var ufo in _poolUFO._ufoList)
        {
            if (ufo.activeSelf == true)
            {
                int.TryParse(ufo.name.Replace(ConstStrings.UFONAME, ""), out int index);
                //
            }
        } 
    }
}
