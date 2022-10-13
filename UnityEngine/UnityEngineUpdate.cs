using UnityEngine;

public class UnityEngineUpdate : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;
    public static UnityEngineUpdate Instance;
    private AsteroidsPositionUpdate _asteroidsPositionUpdate = new AsteroidsPositionUpdate();
    private BulletPositionUpdate _bulletPositionUpdate = new BulletPositionUpdate();

    private PoolAsteroid _poolAsteroid;
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
    }

    private void Start()
    {
        _poolAsteroid = new PoolAsteroid(GameConfig.NumberOfAsteroids);
        _poolBullet = new PoolBullet(GameConfig.NumberOfBullets);
        _poolUFO = new PoolUFO(GameConfig.NumberOfUFO);
        _poolMainHero = new PoolMainHero();
        _mainHero = _poolMainHero.MainHero;
    }

    private void FixedUpdate()
    {
        foreach (var asteroid in PoolAsteroid._asteroidList)
        {
            if (asteroid.activeSelf == true)
            {
                int.TryParse(asteroid.name.Replace(ConstStrings.ASTEROIDNAME, ""), out int index);
                _asteroidsPositionUpdate.Transform(index);
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
                    if (asteroid.activeSelf == true)
                    {
                        int.TryParse(asteroid.name.Replace(ConstStrings.ASTEROIDNAME, ""), out int asteroidIndex);
                        _collisionDetected = CollisionDetection.CheckCollision
                            (EntityPool.BulletEntitiesPool[bulletIndex].CurrentX,
                            EntityPool.BulletEntitiesPool[bulletIndex].CurrentY,
                            EntityPool.AsteroidEntitiesPool[asteroidIndex].CurrentX,
                            EntityPool.AsteroidEntitiesPool[asteroidIndex].CurrentY,
                            GameConfig.AsteroidRadius);
                        if (_collisionDetected)
                        {
                            Debug.Log("Collision detected!"); // delete
                            //EntityPool.AsteroidEntitiesPool[asteroidIndex].CurrentX 
                            _poolAsteroid.ReturnToPool(asteroidIndex);
                            PoolBullet.ReturnToPool(bulletIndex);


                        }
                    }
                    
                }
                
            }
        }

        foreach (var ufo in PoolUFO._ufoList)
        {
            if (ufo.activeSelf == true)
            {
                int.TryParse(ufo.name.Replace(ConstStrings.UFONAME, ""), out int index);
                //
            }
        }

    }
}
