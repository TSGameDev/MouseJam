using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHUDManager : MonoBehaviour
{
    [SerializeField] private Slider normalAttackCooldownSlider;
    [SerializeField] private TextMeshProUGUI normalAttackCooldownTxt;
    [SerializeField] private Slider ability1CooldownSlider;
    [SerializeField] private TextMeshProUGUI ability1CooldownTxt;
    [SerializeField] private Slider ability2CooldownSlider;
    [SerializeField] private TextMeshProUGUI ability2CooldownTxt;
    [SerializeField] private Slider ability3CooldownSlider;
    [SerializeField] private TextMeshProUGUI ability3CooldownTxt;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthSliderTxt;

    public static UIHUDManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void SetUpAbilityUI(AbilityCore _NormalAttack, AbilityCore _Ability1, AbilityCore _Ability2, AbilityCore _Ability3)
    {
        normalAttackCooldownSlider.minValue = 0;
        normalAttackCooldownSlider.maxValue = _NormalAttack.GetAbilityCooldown();

        ability1CooldownSlider.minValue = 0;
        ability1CooldownSlider.maxValue = _Ability1.GetAbilityCooldown();

        ability2CooldownSlider.minValue = 0;
        ability2CooldownSlider.maxValue = _Ability2.GetAbilityCooldown();

        ability3CooldownSlider.minValue = 0;
        ability3CooldownSlider.maxValue = _Ability3.GetAbilityCooldown();
    }

    public void SetUpHealthUI(int _MaxHealth, int _CurrentHeatlh, int _MinHealth = 0)
    {
        healthSlider.minValue = _MinHealth;
        healthSlider.maxValue = _MaxHealth;
        healthSlider.value = _CurrentHeatlh;
        healthSliderTxt.text = _CurrentHeatlh.ToString();
    }

    public void UpdateNormalAttackUI(bool _AttackOnCooldown, float _AttackCooldown = 0)
    {
        normalAttackCooldownSlider.gameObject.SetActive(_AttackOnCooldown);
        normalAttackCooldownSlider.value = _AttackCooldown;
        normalAttackCooldownTxt.text = Mathf.Ceil(_AttackCooldown).ToString();
    }

    public void UpdateAbility1UI(bool _AttackOnCooldown, float _AttackCooldown = 0)
    {
        ability1CooldownSlider.gameObject.SetActive(_AttackOnCooldown);
        ability1CooldownSlider.value = _AttackCooldown;
        ability1CooldownTxt.text = Mathf.Ceil(_AttackCooldown).ToString();
    }

    public void UpdateAbility2UI(bool _AttackOnCooldown, float _AttackCooldown = 0)
    {
        ability2CooldownSlider.gameObject.SetActive(_AttackOnCooldown);
        ability2CooldownSlider.value = _AttackCooldown;
        ability2CooldownTxt.text = Mathf.Ceil(_AttackCooldown).ToString();
    }

    public void UpdateAbility3UI(bool _AttackOnCooldown, float _AttackCooldown = 0)
    {
        ability3CooldownSlider.gameObject.SetActive(_AttackOnCooldown);
        ability3CooldownSlider.value = _AttackCooldown;
        ability3CooldownTxt.text = Mathf.Ceil(_AttackCooldown).ToString();
    }

    public void UpdateHealthSlider(int _NewHealthValue)
    {
        healthSlider.value = _NewHealthValue;
        healthSliderTxt.text = _NewHealthValue.ToString();
    }
}
