using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static int currentScore = 0;
    int bestScore;

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
    class SaveData
    {
        public int score;
    }

    public void TrySaveBestScore()
    {
        var data = new SaveData();
        if (currentScore < bestScore) return;
        data.score = currentScore;
        var json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestScore()
    {
        var path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.score;
        }
    }

    public int GetScore() => currentScore;
    public int GetBestScore() => bestScore;
}
