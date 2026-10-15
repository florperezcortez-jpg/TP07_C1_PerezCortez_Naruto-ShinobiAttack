using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private InputActionReference attack;
    [SerializeField] private float fireRate = 0.4f;

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
        if (attack.action.WasPressedThisFrame() && Time.time >= nextFireTime)

        {
            nextFireTime = Time.time + fireRate;
            Shoot();

        }

    }
    private void Shoot()
    {
        animator.SetTrigger("Attack");
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, projectilePrefab.transform.rotation);
        float direction = Mathf.Sign(firePoint.localPosition.x);
        projectile.GetComponent<Projectile>().SetDirection(direction);
    }
}
