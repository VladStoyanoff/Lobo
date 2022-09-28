using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    [SerializeField] GameObject radar;
    [SerializeField] List<SpriteRenderer> sprites;
    Spawner spawner;

    void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    void Update()
    {
        UpdateEnemyBaseLocations();
        UpdatePlayerDirectionRadar();
        UpdatePlayerLocation();
    }

    void UpdateEnemyBaseLocations()
    {
        // Find distances to all enemy Bases
        var enemyBases = spawner.GetEnemyBases();
        var vectorLengthList = new List<float>();
        var radius = 3;
        var baseNotOnRadar = false;
        if (Input.GetKey(KeyCode.T))
        {
            for (int i = 0; i < enemyBases.Count; i++)
            {
                var vectorToEnemyBase = enemyBases[i].transform.position - gameObject.transform.position;
                Debug.Log(vectorToEnemyBase);
                var distanceBetweenPlayerBase = Vector2.Distance(gameObject.transform.position, enemyBases[i].transform.position);
                Debug.Log(distanceBetweenPlayerBase);
                vectorLengthList.Add(distanceBetweenPlayerBase);
                //if (distanceBetweenPlayerBase > radius)
                //{
                //    baseNotOnRadar = true;
                //    continue;
                //}
                Debug.Log((-.5f) * radius);
                Debug.Log(vectorToEnemyBase.x > (-.5f) * radius);
                Debug.Log(vectorToEnemyBase.x < (.5f) * radius);
                Debug.Log(vectorToEnemyBase.y > 0);
                Debug.Log(vectorToEnemyBase.y < radius);
                if (vectorToEnemyBase.x > (-1 / 2) * radius &&
                    vectorToEnemyBase.x < (1 / 2) * radius &&
                    vectorToEnemyBase.y > 0 &&
                    vectorToEnemyBase.y < radius)

                {
                    Debug.Log("?");
                    sprites[1].gameObject.SetActive(true);
                }

                //    if (vectorToEnemyBase.x > -(Mathf.Sqrt(3)/2) * radius &&
                //        vectorToEnemyBase.x < -(1/2) * radius &&
                //        vectorToEnemyBase.y > 1/2 * radius &&
                //        vectorToEnemyBase.y < Mathf.Sqrt(3) / 2 * radius)

                //    {
                //        Sprite[upleft].SetActive(true);
                //    }

                //    if (vectorToEnemyBase.x > 1 / 2 * radius &&
                //        vectorToEnemyBase.x < Mathf.Sqrt(3) / 2 * radius &&
                //        vectorToEnemyBase.y < Mathf.Sqrt(3) /2 * radius &&
                //        vectorToEnemyBase.y > 1/2 * radius)

                //    {
                //        Sprite[upright].SetActive(true);
                //    }

                //    //
                //    if (vectorToEnemyBase.x > 1 / 2 * radius &&
                //        vectorToEnemyBase.x < Mathf.Sqrt(3) / 2 * radius &&
                //        vectorToEnemyBase.y < Mathf.Sqrt(3) / 2 * radius &&
                //        vectorToEnemyBase.y > 1 / 2 * radius)

                //    {
                //        Sprite[bothright].SetActive(true);
                //    }
                //}
            }
        }

        
        //if (baseNotOnRadar && allSpritesAreBlack)
        //{
        //    var closestenemybase = vectorLengthList.Min();
        //    var index = vectorLengthList.IndexOf(closestenemybase);
        //    var vectorToEnemyBase = gameObject.transform.position - enemyBases[index].transform.position;

        //    // basically do the same logic as above but change radius to the length of the distance between the base and player

        //}

        //// Update sprite if the distance covers certain criteria
        //if (Vector2.Distance(gameObject.transform.position, enemyBases[index].transform.position) <?> a && <?> b) return;

        //// FIX: Switch case for the 4 sprites
        //updatesprite;
    }

    void UpdatePlayerDirectionRadar()
    {

    }

    void UpdatePlayerLocation()
    {

    }
}
