using UnityEngine;

public class UnityEngineEntryPoint : MonoBehaviour
{
    private ObjectSpawner _objectSpawner;
    private void Awake()
    {
        _objectSpawner = new ObjectSpawner();
    }
    private void Start()
    {
        _objectSpawner.SpawnMainHero();
        for (int i = 0; i < SceneConfig.NumberOfAsteroids; i++)
        {
            _objectSpawner.SpawnAsteroid(i);
        }
    }
}
