using System.Collections;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    GameManager gameManager;
    UIManager uiManager;

    [SerializeField] float spawnRate = 10;
    [SerializeField] GameObject[] enemyUnitPrefabs;
    int index;
    int randomModifier;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // The switch statement manages the chance of spawning a specific enemy from the bases. From case 3 to case 8, sometimes it assigns index twice.
    // This design was chosen in order to avoid "staircase complexity" where there's lots of indententions of code consecutively.
    IEnumerator SpawnEnemies()
    {
        while (gameManager.GetIsGameActiveBool())
        {
            randomModifier = Random.Range(0, 100);
            switch (uiManager.GetLevelSetting())
            {
                case 1:
                    index = 0;
                    break;
                case 2:
                    index = randomModifier < 80 ? index = 0 : index = 1;
                    break;
                case 3:
                    index = randomModifier > 70 && randomModifier < 90 ? index = 1 : index = 2;
                    if (randomModifier < 70) index = 0;
                    break;
                case 4:
                    index = randomModifier > 50 && randomModifier < 75 ? index = 1 : index = 2;
                    if (randomModifier < 50) index = 0;
                    break;
                case 5:
                    index = randomModifier > 40 && randomModifier < 70 ? index = 1 : index = 2;
                    if (randomModifier < 40) index = 0;
                    break;
                case 6:
                    index = randomModifier > 33 && randomModifier < 66 ? index = 1 : index = 2;
                    if (randomModifier < 33) index = 0;
                    break;
                case 7:
                    index = randomModifier > 25 && randomModifier < 40 ? index = 1 : index = 2;
                    if (randomModifier < 25) index = 0;
                    break;
                case 8:
                    index = randomModifier > 10 && randomModifier < 45 ? index = 1 : index = 2;
                    if (randomModifier < 10) index = 0;
                    break;
                case 9:
                    index = randomModifier < 50 ? index = 1 : index = 2;
                    break;
            }

            Instantiate(enemyUnitPrefabs[index], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
