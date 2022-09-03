using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class UnityEngineUpdate : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;
    public static UnityEngineUpdate Instance;
    private AsteroidsPositionUpdate _asteroidsPositionUpdate = new AsteroidsPositionUpdate();
    private BulletPositionUpdate _bulletPositionUpdate = new BulletPositionUpdate();

    public GameObject MainHero;
    public List<GameObject> Asteroids = new List<GameObject>();
    public List<GameObject> Bullets = new List<GameObject>();
    
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
    private void TransformMainHero(float newX, float newY)
    {
        if (MainHero == null)
        {
            return;
        }
        MainHero.transform.position = new Vector2(newX, newY);
    }
    private void RotateMainHero(float newRotationAngle)
    {
        if (MainHero == null)
        {
            return;
        }
        MainHero.transform.rotation = Quaternion.Euler(0.0f, 0.0f, newRotationAngle);
    }
    private void TransformAsteroid(float[] newX, float[] newY)
    {
        if (Asteroids.Count == 0)
        {
            return;
        }
        foreach (GameObject asteroid in Asteroids)
        {
            int index = Asteroids.IndexOf(asteroid);
            asteroid.transform.position = new Vector2(newX[index], newY[index]);
        }
    }
    private void RotateAsteroid(float[] newRotationAngle)
    {
        if (Asteroids.Count == 0)
        {
            return;
        }
        foreach (GameObject asteroid in Asteroids)
        {
            int index = Asteroids.IndexOf(asteroid);
            asteroid.transform.rotation = Quaternion.Euler(0.0f, 0.0f, newRotationAngle[index]);
        }
    }
    private void TransformBullet(float[] newX, float[] newY)
    {
        if (Bullets.Count == 0)
        {
            return;
        }
        foreach (GameObject bullet in Bullets)
        {
            int index = Bullets.IndexOf(bullet);
            //Debug.Log("NewX is: " + newX[index] + "NewY is: " + newY[index]);
            bullet.transform.position = new Vector2(newX[index], newY[index]);
        }
    }
    private void Update()
    {
        _asteroidsPositionUpdate.Move();
        _asteroidsPositionUpdate.Rotate();
        int[] bulletsIndexes = new int[Bullets.Count];
        foreach(var bullet in Bullets)
        {
            
        }
        //for (int i = )
        //    ObjectEntityRepository.AllObjectsEntities.Find(e => e.Name.Contains(ConstStrings.BulletName)).Index;
        _bulletPositionUpdate.Move(Bullets.Count);
    }
}
