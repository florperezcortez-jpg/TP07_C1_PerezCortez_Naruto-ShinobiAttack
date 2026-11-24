using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
   public static ScoreManager Instance { get; private set; }

    [SerializeField] private TMP_Text scoreText;
    // Suggestion: recordtext no respeta el camelCase del resto (scoreText, ramenText). Renombralo a recordText.
    [SerializeField] private TMP_Text recordtext;
    [SerializeField] private TMP_Text ramenText;
    [SerializeField] private int ramenPoints = 50;

    private int score;
    private int record;
    private int ramen;

    public int Score => score;
    public int Record => record;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // Suggestion: La clave "Record" de PlayerPrefs se repite en AddScore. Definila como private const string RecordKey.
        record = PlayerPrefs.GetInt("Record", 0);
    }

    private void Start()
        {
        UpdateUI();
    }

    public void AddScore(int points)
    {
        score += points;

        if (score > record)
        {
            record = score;
            PlayerPrefs.SetInt("Record", record);
        }
        UpdateUI();
    }
     public void AddRamen()
    {
        ramen++;
        AddScore(ramenPoints);
    }
    private void UpdateUI()
    {
        scoreText.text = score.ToString("D5");
        recordtext.text = "HI " + record.ToString("D5"); //escribe el nro con 5 digitos y ceros adelante
        ramenText.text = "x " + ramen;


    }
}
