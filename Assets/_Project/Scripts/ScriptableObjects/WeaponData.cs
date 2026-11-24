using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    // Warning: Todos los campos son public y cualquier clase puede modificar el asset en runtime (y en el editor esos cambios quedan guardados).
    // Pasalos a [SerializeField] private y exponé properties de solo lectura.
    public GameObject projectilePrefab;
    public float fireRate = 0.4f;
    public int damage = 1;
    public float projectileSpeed = 10f;
    public float projectileLifetime = 2f;

    public float projectileTilt = 30f;
}
