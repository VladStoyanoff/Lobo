using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_InputField levelIF;
    int levelSetting;
    [SerializeField] TMP_InputField densityIF;
    int densitySetting;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text bestDensityText;
    [SerializeField] TMP_Text bestLevelText;
    [SerializeField] TMP_Text lastDensityText;
    [SerializeField] TMP_Text lastLevelText;


    ScoreManager scoreManager;

    bool hasParsedLevelSetting;
    bool hasParsedDensitySetting;

    void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void Start()
    {
        scoreManager.LoadBestScore();
    }

    void Update()
    {
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + scoreManager.GetScore().ToString();
        highScoreText.text = "High Score: " + scoreManager.GetBestScore().ToString();
        bestLevelText.text = scoreManager.GetBestLevel().ToString();
        bestDensityText.text = scoreManager.GetBestDensity().ToString();
    }

    public void ReadLevelIF()
    {
        hasParsedLevelSetting = int.TryParse(levelIF.GetComponent<TMP_InputField>().text, out var result);
        if (hasParsedLevelSetting)
        {
            levelSetting = result;
        }
        else
        {
            Debug.LogError("The level setting will only accept integers from 1-9 as input");
        }
    }

    public void ReadDensityIF()
    {
        hasParsedDensitySetting = int.TryParse(densityIF.GetComponent<TMP_InputField>().text, out var result);
        if (hasParsedDensitySetting && result > 0 && result < 6)
        {
            densitySetting = result;
        }
        else
        {
            Debug.LogError("The density setting will only accept integers from 1-5 as input");
        }
    }

    public void SetLastLevelSettings()
    {
        lastDensityText.text = densitySetting.ToString();
        lastLevelText.text = levelSetting.ToString();
    }

    public int GetLevelSetting() => levelSetting;
    public int GetDensitySetting() => densitySetting;
    public bool GetLevelSettingBool() => hasParsedLevelSetting;
    public bool GetDensitySettingBool() => hasParsedDensitySetting;
}