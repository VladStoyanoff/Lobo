using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Navigation : MonoBehaviour
{

    List<GameObject> enemyBases = new List<GameObject>();

    Spawner spawner;

    const int PI = 180;

    [SerializeField] GameObject radar;
    [SerializeField] List<SpriteRenderer> sprites;
    [SerializeField] int radarRadius = 3;

    [SerializeField] GameObject cannon;

    bool baseToTheNorth;

    void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    void Update()
    {
        UpdateRadarForAllBases();
        UpdateCannonDirectionRadar();
        UpdatePlayerLocation();
    }

    //Debug.Log(vectorToEnemyBase);
    //Debug.Log(distanceBetweenPlayerBase);
    //Debug.Log(angle);

    //Debug.Log(vectorToEnemyBase.x > 1f / 2f * radarRadius);
    //Debug.Log(vectorToEnemyBase.x < Mathf.Sqrt(3f) / 2f * radarRadius);
    //Debug.Log(vectorToEnemyBase.y > 1f / 2f * radarRadius);
    //Debug.Log(vectorToEnemyBase.y < Mathf.Sqrt(3f) / 2f * radarRadius);
    //Debug.Log(angle > pi / 6);
    //Debug.Log(angle < pi / 3);

    void UpdateRadarForAllBases()
    {
        enemyBases = spawner.GetEnemyBases();
        var vectorLengthList = new List<float>();

        // Find distances to all enemy bases and add them to a list. If the distance is beyond the radar's range dont try to locate the base
        for (int i = 0; i < enemyBases.Count; i++)
        {
            var distanceBetweenPlayerBase = Vector2.Distance(gameObject.transform.position, enemyBases[i].transform.position);
            vectorLengthList.Add(distanceBetweenPlayerBase);
            if (distanceBetweenPlayerBase > radarRadius) continue;

            UpdateRadarForSingleBase(i, radarRadius);
        }

        //if (baseToTheNorth == false)
        //{
        //    deactivateSprite;
        //}

        //baseToTheNorth = false;

        //// If the radar hasnt located a single base, locate the closest one to the player.
        //if (allSpritesBlack == false) return;
        //var closestenemybase = vectorLengthList.Min();
        //var index = vectorLengthList.IndexOf(closestenemybase);
        //UpdateRadarForSingleBase(index, Mathf.Infinity);
    }

    void UpdateRadarForSingleBase(int index, float radarRadius)
    {
        var vectorToEnemyBase = enemyBases[index].transform.position - gameObject.transform.position;
        var angle = Vector2.Angle(vectorToEnemyBase.normalized, Vector2.right);

        if (vectorToEnemyBase.x > 0 &&
            vectorToEnemyBase.x < radarRadius &&
            vectorToEnemyBase.y < 1f / 2f * radarRadius &&
            vectorToEnemyBase.y > -1f / 2f * radarRadius &&
            // the angle calculation ensures for both boundaries, because Vector3.Angle does not calculate angles between 180 and 360
            angle < PI / 6)

        {
            Debug.Log("east!");
        }

        if (vectorToEnemyBase.x > 0 &&
            vectorToEnemyBase.x < Mathf.Sqrt(3f) / 2f * radarRadius &&
            vectorToEnemyBase.y > 0 &&
            vectorToEnemyBase.y < Mathf.Sqrt(3f) / 2f * radarRadius &&
            angle > PI / 6 &&
            angle < PI / 3)

        {
            var activatedSprite = true;
            Debug.Log("northeast!");
        }
        

        if (vectorToEnemyBase.x < 1f / 2f * radarRadius &&
            vectorToEnemyBase.x > -1f / 2f * radarRadius &&
            vectorToEnemyBase.y > 0 &&
            vectorToEnemyBase.y < radarRadius &&
            angle > PI / 3f &&
            angle < 2 * PI / 3f)

        {
            Debug.Log("north!");
            baseToTheNorth = true;
        }

        if (vectorToEnemyBase.x < 0 &&
            vectorToEnemyBase.x > -Mathf.Sqrt(3f) / 2f * radarRadius &&
            vectorToEnemyBase.y > 0 &&
            vectorToEnemyBase.y < Mathf.Sqrt(3f) / 2f * radarRadius &&
            angle > 2 * PI / 3 &&
            angle < 5 * PI / 6)

        {
            Debug.Log("northwest!");
        }

        if (vectorToEnemyBase.x < 0 &&
            vectorToEnemyBase.x > -radarRadius &&
            vectorToEnemyBase.y < 1f / 2f * radarRadius &&
            vectorToEnemyBase.y > -1f / 2f * radarRadius &&
            // the angle calculation ensures for both boundaries, because Vector3.Angle does not calculate angles between 180 and 360
            angle > 5 * PI / 6)

        {
            Debug.Log("west!");
        }

        if (vectorToEnemyBase.x < 0 &&
            vectorToEnemyBase.x > -Mathf.Sqrt(3f) / 2f * radarRadius &&
            vectorToEnemyBase.y < 0 &&
            vectorToEnemyBase.y > -Mathf.Sqrt(3f) / 2f * radarRadius &&
            angle > 2 * PI / 3 &&
            angle < 5 * PI / 6)

        {
            Debug.Log("southwest!");
        }

        if (vectorToEnemyBase.x < 1f / 2f * radarRadius &&
            vectorToEnemyBase.x > -1f / 2f * radarRadius &&
            vectorToEnemyBase.y < 0 &&
            vectorToEnemyBase.y > -radarRadius &&
            angle > PI / 3f &&
            angle < 2 * PI / 3f)

        {
            Debug.Log("south!");
        }

        if (vectorToEnemyBase.x > 0 &&
            vectorToEnemyBase.x < Mathf.Sqrt(3f) / 2f * radarRadius &&
            vectorToEnemyBase.y < 0 &&
            vectorToEnemyBase.y > -Mathf.Sqrt(3f) / 2f * radarRadius &&
            angle > PI / 6 &&
            angle < PI / 3)

        {
            Debug.Log("southeast!");
        }
    }

    void UpdateCannonDirectionRadar()
    {
        if (cannon.transform.localEulerAngles.z < PI / 6 || cannon.transform.localEulerAngles.z > 11*PI / 6)
        {
            Debug.Log("12");
        }

        if (cannon.transform.localEulerAngles.z > PI / 6 && cannon.transform.localEulerAngles.z < PI / 3)
        {
            Debug.Log("1030");
        }

        if (cannon.transform.localEulerAngles.z > PI / 3 && cannon.transform.localEulerAngles.z < 2 * PI / 3)
        {
            Debug.Log("9");
        }

        if (cannon.transform.localEulerAngles.z > 2 * PI / 3 && cannon.transform.localEulerAngles.z < 5 * PI / 6)
        {
            Debug.Log("730");
        }

        if (cannon.transform.localEulerAngles.z > 5 * PI / 6 && cannon.transform.localEulerAngles.z < 7 * PI / 6)
        {
            Debug.Log("6");
        }

        if (cannon.transform.localEulerAngles.z > 7 * PI/6 && cannon.transform.localEulerAngles.z < 4 * PI / 3)
        {
            Debug.Log("430");
        }

        if (cannon.transform.localEulerAngles.z > 4*PI/3 && cannon.transform.localEulerAngles.z < 5*PI / 3)
        {
            Debug.Log("3");
        }

        if(cannon.transform.localEulerAngles.z > 5*PI/3 && cannon.transform.localEulerAngles.z < 11*PI / 6)
        {
            Debug.Log("130");
        }
    }

    void UpdatePlayerLocation()
    {

    }
}
