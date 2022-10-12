using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFirstWeaponCooldown : MonoBehaviour
{
    [SerializeField] private Image _imageCooldown;
    private float cooldownTimer;

    private void Start()
    {
        _imageCooldown.fillAmount = 1.0f;
        MainHeroWeapon.FirstShootPerfomed += StartCooldown;
    }



    private void StartCooldown()
    {
        cooldownTimer = 0f;
        _imageCooldown.fillAmount = 0f;
        StartCoroutine(StartCooldownCoroutine());

        //for (int i = 0; i < 50; i++)
        //{
        //    await Task.Delay(10);
        //    _imageCooldown.fillAmount = Mathf.Clamp(_imageCooldown.fillAmount += 0.02f, 0f, 1f);
        //}
    }

    IEnumerator StartCooldownCoroutine()
    {
        while (cooldownTimer <= 0.5f)
        {
            cooldownTimer += Time.deltaTime;
            _imageCooldown.fillAmount =
                Mathf.Clamp(_imageCooldown.fillAmount += Time.deltaTime * 2, 0f, 1f);
            yield return new WaitForEndOfFrame();
        }
        //yield return null;
    }
}
