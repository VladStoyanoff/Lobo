using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static int currentScore = 0;
    int bestScore;
    int bestLevel;
    int bestDensity;

    public void ModifyScore(int score)
    {
        currentScore += score;
        currentScore = Mathf.Clamp(currentScore, 0, int.MaxValue);
    }

    public void ClearScore()
    {
        currentScore = 0;
    }

    [System.Serializable]
    class SaveBestData
    {
        public int score;
        public int density;
        public int level;
    }

    [System.Serializable]
    class SaveLastRun
    {
        public int density;
        public int level;
    }

    public void TrySaveBestScore()
    {
        var data = new SaveBestData();
        if (currentScore < bestScore) return;
        data.score = currentScore;
        data.density = FindObjectOfType<UIManager>().GetDensitySetting();
        data.level = FindObjectOfType<UIManager>().GetLevelSetting();
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/saveBestScoreFile.json", json);
    }


    public void LoadBestScore()
    {
        var path = Application.persistentDataPath + "/saveBestScoreFile.json";
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<SaveBestData>(json);

            bestScore = data.score;
            bestDensity = data.density;
            bestLevel = data.level;
        }
    }

    public int GetScore() => currentScore;
    public int GetBestScore() => bestScore;
    public int GetBestLevel() => bestLevel;
    public int GetBestDensity() => bestDensity;
}
