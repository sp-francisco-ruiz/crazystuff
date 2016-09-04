using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformController : MonoBehaviour 
{
    public GameObject end;

    [SerializeField]
    List<GameObject> ObstaclesRow1;
    [SerializeField]
    List<GameObject> ObstaclesRow2;
    [SerializeField]
    List<GameObject> ObstaclesRow3;


    [SerializeField]
    List<GameObject> PalmTrees;

    void Awake()
    {
        SetObstacles(true);
        SetRandomActivation(PalmTrees);
    }

    public void SetObstacles(bool disable = false)
    {
        SetRowObstacles(ObstaclesRow1, disable);
        SetRowObstacles(ObstaclesRow2, disable);
        SetRowObstacles(ObstaclesRow3, disable);
    }

    void SetRandomActivation(List<GameObject> list)
    {
        for(int i = 0; i < list.Count; ++i)
            list[i].SetActive(Random.Range(0, 100) > 45);
    }

    void SetRowObstacles(List<GameObject> row, bool disable)
    {
        int ObstaclesCount = 0;
        for(int i = 0; i < row.Count; ++i)
        {
            bool isActive = Random.Range(0, 100) > 55 && ObstaclesCount < 2 && !disable;
            if(isActive)
                ++ObstaclesCount;
            row[i].SetActive(isActive);
        }
    }
}
