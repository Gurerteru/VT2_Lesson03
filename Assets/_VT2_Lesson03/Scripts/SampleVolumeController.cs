using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SampleVolumeController : MonoBehaviour
{
    [SerializeField]private Volume _globalVolume;

    [Header("=== パラメーター ===")]
    public float HP,MaxHP;      // 体力値
    public float HpRate;        // 体力率


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 無限回復
        OnHeal(0.1f);

        // スペースキーを押したら体力を減らす
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            OnDamage(50f);
        }

        if (_globalVolume != null)
        {
            // Gloubal Volume のプロファイルから[Vignette]コンポーネントを取得する
            if (_globalVolume.profile.TryGet(out Vignette vignette))
            {
                vignette.center.overrideState = true;
                vignette.center.value = Camera.main.WorldToViewportPoint(transform.position);

                vignette.intensity.overrideState = true;
                // vignette.intensity.value = 1 - (HpRate);
                vignette.intensity.value = Mathf.Lerp( vignette.intensity.value, 1 - (HpRate), Time.deltaTime * 5f);

                vignette.color.overrideState = true;
                vignette.color.value = new Color(1f, 0f, 0f);
            }

            // 自作のボリュームを取得して弄れる
            if(_globalVolume.profile.TryGet(out SamplePostProcessingVolumeComponent customVolume))
            {
                customVolume.intensity.overrideState = true;
                customVolume.intensity.value = Mathf.PingPong(Time.time, 1.0f);
            }
        }
    }

    // === 体力を減らすメソッド === //
    public void OnDamage(float damage)
    {
        HP -= damage;
        HP = Mathf.Clamp(HP, 0, MaxHP);

        // 体力率を計算する
        HpRate = HP / MaxHP;
    }

    // === 体力を回復メソッド === //
    public void OnHeal(float heal)
    {
        HP += heal;
        HP = Mathf.Clamp(HP, 0, MaxHP);

        // 体力率を計算する
        HpRate = HP / MaxHP;
    }
}
