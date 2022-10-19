public static class GameConfig
{
    // ===== main hero ===== 
    public const float MainHeroSpawnPositionCoordinateX = 0.0f;
    public const float MainHeroSpawnPositionCoordinateY = 0.0f;
    public const float MainHeroRotateForce = 1.5f;
    public const float MainHeroRadius = 0.2f;

    // ===== quantity ===== 
    public const int NumberOfAsteroids = 15;
    public const int NumberOfSmallAsteroids = 30;
    public const int NumberOfBullets = 10;
    public const int MinNumberOfUFO = 0;
    public const int MaxNumberOfUFO = 3;

    // ===== cooldowns ===== 
    public const float FirstWeaponCooldown = 0.6f;
    public const float SecondWeaponCooldown = 2.5f;

    // ===== asteroids =====
    public const float AsteroidMoveSpeed = 0.004f;
    public const float AsteroidRotationForce = 2f;
    public const float AsteroidRadius = 0.6f;
    public const float SmallAsteroidRaduis = 0.3f;

    // ===== bullets =====
    public const float BulletMoveSpeed = 0.25f;

    // ===== in-game data =====
    public const int ScoreForAsteroid = 5;
    public const int ScoreForSmallAsteroid = 3;
    public const int ScoreForUFO = 10;
}
