using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
public class LevelManager : MonoBehaviour
{
    public int levelHeight = 10;
    public int levelWidth = 10;
    public GameObject[] waterTiles;

    private Transform levelHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitializeList()
    {
        gridPositions.Clear();
        for (int x = 1; x < levelWidth; x++)
        {
            for (int y = 1; y < levelHeight; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void levelSetup()
    {
        levelHolder = new GameObject("Level").transform;
        for (int x = -1; x < levelWidth+1; x++)
        {
            for (int y = -1; y < levelHeight+1; y++)
            {
                GameObject toInstantiate = waterTiles[Random.Range(0, waterTiles.Length)];
                GameObject instance = Instantiate(toInstantiate, new Vector3(x*2, y*2, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(levelHolder);

            }
        }
    }
    
    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;

    }
    void LayoutObjectAtRandom(GameObject[] tileArray, int min, int max)
    {
        int objectCount = Random.Range(min, max + 1);
        for(int i=0;i<objectCount;i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }
    public void SetupScene(int level)
    {
        levelSetup();
        InitializeList();
        LayoutObjectAtRandom(waterTiles, 1, 50);
    }
}
