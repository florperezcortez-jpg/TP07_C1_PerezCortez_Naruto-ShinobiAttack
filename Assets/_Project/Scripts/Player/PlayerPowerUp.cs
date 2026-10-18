using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    [SerializeField] private WeaponData chakraWeapon;
    [SerializeField] private float duration = 10f;
    [SerializeField] private RuntimeAnimatorController chakraController;
    private PlayerCombat combat;
    private Animator animator;
    private WeaponData normalWeapon;
    private RuntimeAnimatorController normalController;
    private PlayerHealth playerHealth;
    private float endTime;
    private bool isActive;

    private void Awake()
    {
        combat = GetComponent<PlayerCombat>();
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>();

        if(combat != null)
        normalWeapon = combat.Weapon;

        if (animator != null)
            normalController = animator.runtimeAnimatorController;
    }

    public void Activate()
    {

        if (combat == null || animator == null)
        {
            return;
        }
        if(chakraWeapon == null || chakraController == null)
        {
            return;
        }

        isActive = true; 
        endTime = Time.time + duration;
        combat.SetWeapon(chakraWeapon);
        animator.runtimeAnimatorController = chakraController;
        playerHealth.SetChakraInvulnerable(true);
   
        if(AudioManager.Instance != null)
        AudioManager.Instance.PlayTransform();
    }

    private void Update()
    {
        if(isActive && Time.time >= endTime)
        {
            Deactivate();
        }
    }

    private void Deactivate()
    {
        isActive = false; 
        combat.SetWeapon(normalWeapon);
        animator.runtimeAnimatorController = normalController;
        playerHealth.SetChakraInvulnerable(false);
    }
}
