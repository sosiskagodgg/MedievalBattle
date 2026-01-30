[System.Serializable]
/// <summary>
/// Represents a weapon with damage and duration properties.
/// </summary>
public class Weapon
{
    public string Name { get; set; }

    private float _baseDamage;
    public float Damage
    {
        get => _baseDamage * weaponDamageCoefficient;
        set => _baseDamage = value;
    }

    private float _baseDuration;
    public float Duration
    {
        get => _baseDuration * weaponDurationCoefficient;
        set => _baseDuration = value;
    }

    private float _hp;
    public float Hp { get=> _hp; set=> _hp=value; }

    public float weaponDamageCoefficient = 1f;
    public float weaponDurationCoefficient = 1f;

    public Weapon(float damage,float duration, float hp )
    {
        _baseDamage = damage;
        _baseDuration = duration;
        Hp = hp;
    }
}