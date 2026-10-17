using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Fuentes")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Musica")]
    [SerializeField] private AudioClip music;

    [Header("Efectos")]
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip playerHurtClip;
    [SerializeField] private AudioClip enemyDeathClip;
    [SerializeField] private AudioClip pickupLifeClip;
    [SerializeField] private AudioClip pickupRamenClip;
    [SerializeField] private AudioClip chakraClip;
    [SerializeField] private AudioClip transformClip;
    [SerializeField] private AudioClip uiClickClip;
    [SerializeField] private AudioClip gameOverClip;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (music != null)
        {
            musicSource.clip = music;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayJump() { Play(jumpClip); }
    public void PlayShoot() { Play(shootClip); }
    public void PlayPlayerHurt() {  Play(playerHurtClip); }
    public void PlayEnemyDeath() { Play(enemyDeathClip); }
    public void PlayPickupRamen() {  Play(pickupRamenClip); }
    public void PlayPickupLife() { Play(pickupLifeClip);}
    public void PlayChakra() {  Play(chakraClip); }
    public void PlayTransform() { Play(transformClip); }
    public void PlayUIClick() { Play(uiClickClip);}

    public void PlayGameOver() { Play(gameOverClip); }

    private void Play(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
