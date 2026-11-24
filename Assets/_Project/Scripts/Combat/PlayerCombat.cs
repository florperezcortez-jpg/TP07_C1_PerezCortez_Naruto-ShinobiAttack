using UnityEngine.InputSystem;
using UnityEngine;
// Suggestion: Este using de VisualScripting no se usa en el archivo, borralo.
using Unity.VisualScripting.Antlr3.Runtime;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private WeaponData weapon;
    [SerializeField] private Transform firePoint;
    [SerializeField] private InputActionReference attack;
    public WeaponData Weapon => weapon;

    private float nextFireTime;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        attack.action.Enable();
    }

    private void OnDisable()
    {
        attack.action.Disable();
    }

    private void Update()
    {
        // Bug: Con Time.timeScale = 0 (pausa o game over) Update sigue corriendo y Time.time no avanza, así que con el menú de pausa abierto
        // se puede seguir disparando: se instancia el proyectil, suena el disparo y se dispara el trigger Attack. Chequeá que el juego no esté pausado.
        if (attack.action.WasPressedThisFrame() && Time.time >= nextFireTime)

        {
            nextFireTime = Time.time + weapon.fireRate;
            Shoot();

        }

    }

    public void SetWeapon (WeaponData newWeapon)
    {
        weapon = newWeapon;
    }
    private void Shoot()
    {
        if(weapon == null)
        {
            Debug.LogError("PlayerCombat: falta asignar weápon", this);
            return;
        }
        if(firePoint == null)
        {
            Debug.LogError("PlayerCombat: falta asignar firepoint", this);
            return;
        }
        if (weapon.projectilePrefab == null)
        {
            Debug.LogError("PlayerCombat: Weapon no tiene Projectile Prefab.", this);
            return;
        }
        if (animator == null)
        {
            Debug.LogError("PlayerCombat: falta el Animator en Player.", this);
            return;
        }
        
        // Warning: Nombre de parámetro del Animator como string mágico ("Attack", y lo mismo "Hurt" y "Death" en PlayerHealth).
        // Guardalo en un static readonly int con Animator.StringToHash("Attack").
        animator.SetTrigger("Attack");
        GameObject projectile = Instantiate(weapon.projectilePrefab, firePoint.position, weapon.projectilePrefab.transform.rotation);
       
        Projectile projectileScript = projectile.GetComponent<Projectile>();


        if (projectileScript == null)
        {
            Debug.LogError("PlayerCombat: el prefab no tiene el componente Projectile.", projectile);
            Destroy(projectile);
            return;
        }

        float direction = Mathf.Sign(firePoint.localPosition.x);
        projectileScript.Launch(direction, weapon);

        if(AudioManager.Instance != null)
        AudioManager.Instance.PlayShoot();
    }
}
