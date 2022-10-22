using UnityEngine;
using System;

public class UnityEngineUpdate : MonoBehaviour
{
    public static Action<float, float, float, int> smallAsteroidsSpawn;

    [SerializeField] PlayerController _playerController;
    public static UnityEngineUpdate Instance;
    private AsteroidsPositionUpdate _asteroidsPositionUpdate = new AsteroidsPositionUpdate();
    private BulletPositionUpdate _bulletPositionUpdate = new BulletPositionUpdate();
    private UFOPositionUpdate _ufoPositionUpdate = new UFOPositionUpdate();

    private PoolAsteroid _poolAsteroid;
    private PoolSmallAsteroid _poolSmallAsteroid;
    private PoolBullet _poolBullet;
    private PoolUFO _poolUFO;
    private PoolMainHero _poolMainHero;

    public static GameObject _mainHero;

    private bool _collisionDetected;

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
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        _poolAsteroid = new PoolAsteroid(GameConfig.NumberOfAsteroids);
        _poolSmallAsteroid = new PoolSmallAsteroid(GameConfig.NumberOfSmallAsteroids);
        _poolBullet = new PoolBullet(GameConfig.NumberOfBullets);
        _poolUFO = new PoolUFO(GameConfig.MaxNumberOfUFO);
        _poolMainHero = new PoolMainHero();
        _mainHero = _poolMainHero.MainHero;
        GameStates.ChangeGameState(GameStates.GameState.PlayMode);
    }

    private void FixedUpdate()
    {
        foreach (var asteroid in PoolAsteroid._asteroidList)
        {
            if (asteroid.activeSelf == true)
            {
                int.TryParse(asteroid.name.Replace(ConstStrings.ASTEROIDNAME, ""), out int asteroidIndex);
                _asteroidsPositionUpdate.TransformAsteroid(asteroidIndex);

                // check for MainHero + Asteroid
                _collisionDetected = CollisionDetection.CheckCollision
                    (PoolEntity.AsteroidEntitiesPool[asteroidIndex].CurrentX,
                     PoolEntity.AsteroidEntitiesPool[asteroidIndex].CurrentY,
                     PoolEntity.MainHero.CurrentX,
                     PoolEntity.MainHero.CurrentY,
                     GameConfig.AsteroidRadius + GameConfig.MainHeroRadius);
                if (_collisionDetected)
                {
                    GameStates.ChangeGameState(GameStates.GameState.GameOver);
                }
            }
        }

        foreach (var smallAsteroid in PoolSmallAsteroid._smallAsteroidList)
        {
            if (smallAsteroid.activeSelf == true)
            {
                int.TryParse(smallAsteroid.name.Replace(ConstStrings.SMALLASTEROIDNAME, ""), out int asteroidIndex);
                _asteroidsPositionUpdate.TransformSmallAsteroid(asteroidIndex);

                // check for MainHero + smallAsteroid
                _collisionDetected = CollisionDetection.CheckCollision
                    (PoolEntity.SmallAsteroidEntitiesPool[asteroidIndex].CurrentX,
                     PoolEntity.SmallAsteroidEntitiesPool[asteroidIndex].CurrentY,
                     PoolEntity.MainHero.CurrentX,
                     PoolEntity.MainHero.CurrentY,
                     GameConfig.SmallAsteroidRaduis + GameConfig.MainHeroRadius);
                if (_collisionDetected)
                {
                    GameStates.ChangeGameState(GameStates.GameState.GameOver);
                }
            }
        }

        foreach (var ufo in PoolUFO._ufoList)
        {
            if (ufo.activeSelf == true)
            {
                int.TryParse(ufo.name.Replace(ConstStrings.UFONAME, ""), out int ufoIndex);
                _ufoPositionUpdate.TransformUFO(ufoIndex);

                // check for MainHero + UFO
                _collisionDetected = CollisionDetection.CheckCollision
                    (PoolEntity.UFOEntitiesPool[ufoIndex].CurrentX,
                     PoolEntity.UFOEntitiesPool[ufoIndex].CurrentY,
                     PoolEntity.MainHero.CurrentX,
                     PoolEntity.MainHero.CurrentY,
                     GameConfig.UFORadius + GameConfig.MainHeroRadius);
                if (_collisionDetected)
                {
                    GameStates.ChangeGameState(GameStates.GameState.GameOver);
                }

            }
        }

        foreach (var bullet in PoolBullet._bulletList)
        {
            if (bullet.activeSelf == true)
            {
                int.TryParse(bullet.name.Replace(ConstStrings.BULLETNAME, ""), out int bulletIndex);
                _bulletPositionUpdate.Move(bulletIndex);

                foreach(var asteroid in PoolAsteroid._asteroidList)
                {
                    if (asteroid.activeSelf)
                    {
                        int.TryParse(asteroid.name.Replace(ConstStrings.ASTEROIDNAME, ""), out int asteroidIndex);
                        _collisionDetected = CollisionDetection.CheckCollision
                            (PoolEntity.BulletEntitiesPool[bulletIndex].CurrentX,
                            PoolEntity.BulletEntitiesPool[bulletIndex].CurrentY,
                            PoolEntity.AsteroidEntitiesPool[asteroidIndex].CurrentX,
                            PoolEntity.AsteroidEntitiesPool[asteroidIndex].CurrentY,
                            GameConfig.AsteroidRadius);

                        if (_collisionDetected)
                        {
                            _poolAsteroid.ReturnToPool(asteroidIndex);
                            PoolBullet.ReturnToPool(bulletIndex);

                            smallAsteroidsSpawn?.Invoke
                                (PoolEntity.AsteroidEntitiesPool[asteroidIndex].CurrentX,
                                PoolEntity.AsteroidEntitiesPool[asteroidIndex].CurrentY,
                                0, asteroidIndex);

                                smallAsteroidsSpawn?.Invoke
                                    (PoolEntity.AsteroidEntitiesPool[asteroidIndex].CurrentX,
                                    PoolEntity.AsteroidEntitiesPool[asteroidIndex].CurrentY,
                                    0, GameConfig.NumberOfSmallAsteroids - 1 - asteroidIndex);
                            ScoreRepository.CurrentScore += GameConfig.ScoreForAsteroid;
                        }
                    }
                }
                foreach (var smallAsteroid in PoolSmallAsteroid._smallAsteroidList)
                {
                    if (smallAsteroid.activeInHierarchy)
                    {
                        int.TryParse(smallAsteroid.name.Replace(ConstStrings.SMALLASTEROIDNAME, ""), out int smallAsteroidIndex);
                        _collisionDetected = CollisionDetection.CheckCollision
                            (PoolEntity.BulletEntitiesPool[bulletIndex].CurrentX,
                            PoolEntity.BulletEntitiesPool[bulletIndex].CurrentY,
                            PoolEntity.SmallAsteroidEntitiesPool[smallAsteroidIndex].CurrentX,
                            PoolEntity.SmallAsteroidEntitiesPool[smallAsteroidIndex].CurrentY,
                            GameConfig.SmallAsteroidRaduis);
                        if (_collisionDetected)
                        {
                            _poolSmallAsteroid.ReturnToPool(smallAsteroidIndex);
                            PoolBullet.ReturnToPool(bulletIndex);
                            ScoreRepository.CurrentScore += GameConfig.ScoreForSmallAsteroid;
                        }
                    }
                }
                // check for bullet + UFO
                foreach (var ufo in PoolUFO._ufoList)
                {
                    if (ufo.activeInHierarchy)
                    {
                        int.TryParse(ufo.name.Replace(ConstStrings.UFONAME, ""), out int UFOIndex);
                        _collisionDetected = CollisionDetection.CheckCollision
                            (PoolEntity.BulletEntitiesPool[bulletIndex].CurrentX,
                            PoolEntity.BulletEntitiesPool[bulletIndex].CurrentY,
                            PoolEntity.UFOEntitiesPool[UFOIndex].CurrentX,
                            PoolEntity.UFOEntitiesPool[UFOIndex].CurrentY,
                            GameConfig.UFORadius);
                        if (_collisionDetected)
                        {
                            _poolUFO.ReturnToPool(UFOIndex);
                            PoolBullet.ReturnToPool(bulletIndex);
                            ScoreRepository.CurrentScore += GameConfig.ScoreForUFO;
                        }
                    }
                }
            }
        }
    }

    private void OnDisable()
    {
        _poolBullet.DisableAction();
        _poolSmallAsteroid.DisableAction();
    }
}
