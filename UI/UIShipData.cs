using UnityEngine;
using TMPro;

public class UIShipData : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _shipCoordinatesText;
    private string _shipCooordinates;
    [SerializeField] private TextMeshProUGUI _shipAngleText;
    private string _shipAngle;

    private void OnEnable()
    {
        MainHeroPositionUpdate.TransformMainHeroAction += ChangeShipCoordinates;
        MainHeroPositionUpdate.RotateMainHeroAction += ChangeShipRotation;
        GameStates.OnGameStateChanged += ClearShipData;
    }
    private void OnDisable()
    {
        MainHeroPositionUpdate.TransformMainHeroAction -= ChangeShipCoordinates;
        MainHeroPositionUpdate.RotateMainHeroAction -= ChangeShipRotation;
        GameStates.OnGameStateChanged -= ClearShipData;
    }

    private void ChangeShipCoordinates(float X, float Y)
    {
        X = (float)Mathf.Round(X * 100f) / 100f;
        Y = (float)Mathf.Round(Y * 100f) / 100f;
        _shipCooordinates = ("X: " + X.ToString() + " / Y: " + Y.ToString());
        _shipCoordinatesText.text = _shipCooordinates;
    }
    
    private void ChangeShipRotation(float angle)
    {
        angle = (float)Mathf.Round(angle * 100f) / 100f;
        _shipAngle = ("Angle: " + angle.ToString());
        _shipAngleText.text = _shipAngle;
    }
    
    private void ClearShipData(GameStates.GameState gameState)
    {
        if (gameState == GameStates.GameState.GameOver)
        {
            _shipCoordinatesText.text = "";
            _shipAngleText.text = "";
        }
    }
}
