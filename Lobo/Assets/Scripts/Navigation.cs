using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{

    List<GameObject> enemyBases = new List<GameObject>();
    [SerializeField] RawImage[] radarRawImageArray;
    [SerializeField] RawImage[] cannonRotationRawImageArray;

    Spawner spawner;

    const int PI = 180;

    [SerializeField] GameObject cannon;
    [SerializeField] int radarRadius = 3;

    Color black = Color.black;
    Color green = Color.green;

    bool baseToTheNorth;
    bool baseToTheNorthwest;
    bool baseToTheWest;
    bool baseToTheSouthwest;
    bool baseToTheSouth;
    bool baseToTheSoutheast;
    bool baseToTheEast;
    bool baseToTheNortheast;

    void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    void Start()
    {
        var navigationpanel = GameObject.FindGameObjectWithTag("UI").transform.GetChild(1);

        for (int i = 0; i < 4; i++)
        {
            radarRawImageArray[i] = navigationpanel.GetChild(5).GetChild(i).GetComponent<RawImage>();
            radarRawImageArray[i].color = black;
        }

        for (int i=0; i < 7; i++)
        {
            cannonRotationRawImageArray[i] = navigationpanel.GetChild(6).GetChild(0).GetChild(0).GetChild(i + 1).GetComponent<RawImage>();
        }
    }

    void Update()
    {
        UpdateRadarForAllBases();
        UpdateCannonDirectionRadar();
    }

    void UpdateRadarForAllBases()
    {
        enemyBases = spawner.GetEnemyBases();
        var vectorLengthList = new List<float>();

        if (baseToTheNorth == true) baseToTheNorth = false;
        if (baseToTheNorthwest == true) baseToTheNorthwest = false;
        if (baseToTheWest == true) baseToTheWest = false;
        if (baseToTheSouthwest == true) baseToTheSouthwest = false;
        if (baseToTheSouth == true) baseToTheSouth = false;
        if (baseToTheSoutheast == true) baseToTheSoutheast = false;
        if (baseToTheEast == true) baseToTheEast = false;
        if (baseToTheNortheast == true) baseToTheNortheast = false;

        // Find distances to all enemy bases and add them to a list. If the distance is beyond the radar's range dont try to locate the base
        for (int i = 0; i < enemyBases.Count; i++)
        {
            var distanceBetweenPlayerBase = Vector2.Distance(gameObject.transform.position, enemyBases[i].transform.position);
            vectorLengthList.Add(distanceBetweenPlayerBase);
            if (distanceBetweenPlayerBase > radarRadius) continue;

            UpdateRadarForSingleBase(i, radarRadius);
        }

        ManageColorsForRadar();

        // If the radar hasnt located a single base, locate the closest one to the player.
        if (baseToTheNorth == false && 
            baseToTheNorthwest == false &&
            baseToTheWest == false &&
            baseToTheSouthwest == false &&
            baseToTheSouth == false &&
            baseToTheSoutheast  == false &&
            baseToTheEast == false &&
            baseToTheNortheast == false)
        {
            var closestenemybase = vectorLengthList.Min();
            var index = vectorLengthList.IndexOf(closestenemybase);
            UpdateRadarForSingleBase(index, Mathf.Infinity);
            ManageColorsForRadar();
        }
    }

    void ManageColorsForRadar()
    {
        for (int i = 0; i < 4; i++)
        {
            radarRawImageArray[i].color = black;
        }

        if (baseToTheNorth)
        {
            ActivateSprite(radarRawImageArray[0], radarRawImageArray[1]);
        }

        if (baseToTheEast)
        {
            ActivateSprite(radarRawImageArray[1], radarRawImageArray[3]);
        }

        if (baseToTheWest)
        {
            ActivateSprite(radarRawImageArray[0], radarRawImageArray[2]);
        }

        if (baseToTheSouth)
        {
            ActivateSprite(radarRawImageArray[2], radarRawImageArray[3]);
        }

        if (baseToTheNortheast)
        {
            ActivateSprite(radarRawImageArray[1], null);
        }

        if (baseToTheNorthwest)
        {
            ActivateSprite(radarRawImageArray[0], null);
        }

        if (baseToTheSoutheast)
        {
            ActivateSprite(radarRawImageArray[3], null);
        }

        if (baseToTheSouthwest)
        {
            ActivateSprite(radarRawImageArray[2], null);
        }
    }

    void ActivateSprite(RawImage imageOne, RawImage imageTwo)
    {
        imageOne.color = green;
        if (imageTwo != null)
        {
            imageTwo.color = green;
        }
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
            baseToTheEast = true;
        }

        if (vectorToEnemyBase.x < 1f / 2f * radarRadius &&
            vectorToEnemyBase.x > -1f / 2f * radarRadius &&
            vectorToEnemyBase.y > 0 &&
            vectorToEnemyBase.y < radarRadius &&
            angle > PI / 3f &&
            angle < 2 * PI / 3f)

        {
            baseToTheNorth = true;
        }

        if (vectorToEnemyBase.x > 0 &&
            vectorToEnemyBase.x < Mathf.Sqrt(3f) / 2f * radarRadius &&
            vectorToEnemyBase.y < 0 &&
            vectorToEnemyBase.y > -Mathf.Sqrt(3f) / 2f * radarRadius &&
            angle > PI / 6 &&
            angle < PI / 3)

        {
            baseToTheSoutheast = true;
        }

        if (vectorToEnemyBase.x > 0 &&
            vectorToEnemyBase.x < Mathf.Sqrt(3f) / 2f * radarRadius &&
            vectorToEnemyBase.y > 0 &&
            vectorToEnemyBase.y < Mathf.Sqrt(3f) / 2f * radarRadius &&
            angle > PI / 6 &&
            angle < PI / 3)

        {
            baseToTheNortheast = true;
        }
       

        if (vectorToEnemyBase.x < 0 &&
            vectorToEnemyBase.x > -Mathf.Sqrt(3f) / 2f * radarRadius &&
            vectorToEnemyBase.y > 0 &&
            vectorToEnemyBase.y < Mathf.Sqrt(3f) / 2f * radarRadius &&
            angle > 2 * PI / 3 &&
            angle < 5 * PI / 6)

        {
            baseToTheNorthwest = true;
        }

        if (vectorToEnemyBase.x < 0 &&
            vectorToEnemyBase.x > -radarRadius &&
            vectorToEnemyBase.y < 1f / 2f * radarRadius &&
            vectorToEnemyBase.y > -1f / 2f * radarRadius &&
            // the angle calculation ensures for both boundaries, because Vector3.Angle does not calculate angles between 180 and 360
            angle > 5 * PI / 6)

        {
            baseToTheWest = true;
        }

        if (vectorToEnemyBase.x < 0 &&
            vectorToEnemyBase.x > -Mathf.Sqrt(3f) / 2f * radarRadius &&
            vectorToEnemyBase.y < 0 &&
            vectorToEnemyBase.y > -Mathf.Sqrt(3f) / 2f * radarRadius &&
            angle > 2 * PI / 3 &&
            angle < 5 * PI / 6)

        {
            baseToTheSouthwest = true;
        }

        if (vectorToEnemyBase.x < 1f / 2f * radarRadius &&
            vectorToEnemyBase.x > -1f / 2f * radarRadius &&
            vectorToEnemyBase.y < 0 &&
            vectorToEnemyBase.y > -radarRadius &&
            angle > PI / 3f &&
            angle < 2 * PI / 3f)

        {
            baseToTheSouth = true;
        }

    }

    void UpdateCannonDirectionRadar()
    {
        if (cannon.transform.localEulerAngles.z < PI / 6 || cannon.transform.localEulerAngles.z > 11 * PI / 6)
        {
            for (int i = 0; i < cannonRotationRawImageArray.Length; i++)
            {
                cannonRotationRawImageArray[i].gameObject.SetActive(false);
            }
        }

        if (cannon.transform.localEulerAngles.z > PI / 6 && cannon.transform.localEulerAngles.z < PI / 3)
        {
            for (int i = 0; i < cannonRotationRawImageArray.Length; i++)
            {
                cannonRotationRawImageArray[i].gameObject.SetActive(false);
            }
            cannonRotationRawImageArray[0].gameObject.SetActive(true);
        }

        if (cannon.transform.localEulerAngles.z > PI / 3 && cannon.transform.localEulerAngles.z < 2 * PI / 3)
        {
            for (int i = 0; i < cannonRotationRawImageArray.Length; i++)
            {
                cannonRotationRawImageArray[i].gameObject.SetActive(false);
            }
            cannonRotationRawImageArray[1].gameObject.SetActive(true);
        }

        if (cannon.transform.localEulerAngles.z > 2 * PI / 3 && cannon.transform.localEulerAngles.z < 5 * PI / 6)
        {
            for (int i = 0; i < cannonRotationRawImageArray.Length; i++)
            {
                cannonRotationRawImageArray[i].gameObject.SetActive(false);
            }
            cannonRotationRawImageArray[2].gameObject.SetActive(true);
        }

        if (cannon.transform.localEulerAngles.z > 5 * PI / 6 && cannon.transform.localEulerAngles.z < 7 * PI / 6)
        {
            for (int i = 0; i < cannonRotationRawImageArray.Length; i++)
            {
                cannonRotationRawImageArray[i].gameObject.SetActive(false);
            }
            cannonRotationRawImageArray[3].gameObject.SetActive(true);
        }

        if (cannon.transform.localEulerAngles.z > 7 * PI / 6 && cannon.transform.localEulerAngles.z < 4 * PI / 3)
        {
            for (int i = 0; i < cannonRotationRawImageArray.Length; i++)
            {
                cannonRotationRawImageArray[i].gameObject.SetActive(false);
            }
            cannonRotationRawImageArray[4].gameObject.SetActive(true);
        }

        if (cannon.transform.localEulerAngles.z > 4 * PI / 3 && cannon.transform.localEulerAngles.z < 5 * PI / 3)
        {
            for (int i = 0; i < cannonRotationRawImageArray.Length; i++)
            {
                cannonRotationRawImageArray[i].gameObject.SetActive(false);
            }
            cannonRotationRawImageArray[5].gameObject.SetActive(true);
        }

        if (cannon.transform.localEulerAngles.z > 5 * PI / 3 && cannon.transform.localEulerAngles.z < 11 * PI / 6)
        {
            for (int i = 0; i < cannonRotationRawImageArray.Length; i++)
            {
                cannonRotationRawImageArray[i].gameObject.SetActive(false);
            }
            cannonRotationRawImageArray[6].gameObject.SetActive(true);
        }
    }
}
