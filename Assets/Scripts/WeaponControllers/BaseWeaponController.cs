using UnityEngine;

/// <summary>
/// Базовый класс контроллера оружия
/// </summary>
public abstract class BaseWeaponController : MonoBehaviour
{
    [SerializeField]
    protected Transform socketBullet;

    protected WeaponData weaponData;

    protected AvatarWeaponController avatarOwner;

    protected GameObject fxEffect;

    /// <summary>
    /// Инициализация оружия
    /// </summary>
    /// <param name="_weaponData">Дата оружия</param>
    /// <param name="_owner">Хозяин оружия</param>
    public void InitWeapon(WeaponData _weaponData, AvatarWeaponController _owner)
    {
        avatarOwner = _owner;
        weaponData = _weaponData;

        if (socketBullet != null)
        {
            if (socketBullet.childCount > 0)
            {
                fxEffect = socketBullet.GetChild(0).gameObject;
            }
        }
    }

    /// <summary>
    /// Выстрел из оружия
    /// </summary>
    public abstract void Shoot();
}
