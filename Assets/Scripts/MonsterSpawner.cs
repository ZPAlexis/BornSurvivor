using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] regularSpawn;
    public float spawnRate = 3;
    // public float heighOffset = 8;
    private float timer = 0;
    // private float difficultyClock = 0;
    // public float easyDifficulty = 30;
    // public float mediumDifficulty = 60;
    // public float hardDifficulty = 90;
    List<int>list = new List<int>(); 

    void Start()
    {
        FillList();
        spawnEnemy(GetNonRepeatRandom());
    }
    void Update()
    {
        if(timer<spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            spawnEnemy(GetNonRepeatRandom());
        }
    }
    void FillList()
    {
        for(int i = 0; i < regularSpawn.Length; i++)
        {
            list.Add(i);
        }
    }

    int GetNonRepeatRandom()
    {
        if(list.Count == 0)
        {
        FillList();
        }
        int rand = Random.Range(0, list.Count);
        int value = list[rand];
        list.RemoveAt(rand);
        return value;
    }

    void spawnEnemy(int randomIndex)
    {
        GameObject instantiatedObject = Instantiate(regularSpawn[randomIndex], new Vector3(transform.position.x, transform.position.y, 0 ), transform.rotation) as GameObject;
        Debug.Log("Spawned " + regularSpawn[randomIndex].name.ToString());
    }

    // void Update()
    // {
    //      difficultyClock += Time.deltaTime;

    //     if(timer<spawnRate)
    //     {
    //         timer += Time.deltaTime;
    //     }
    //     else
    //     {
    //         timer = 0;
    //         if(difficultyClock<easyDifficulty)
    //         {
    //             spawnEasySpike(GetNonRepeatRandom());
    //         }
    //         else if(difficultyClock<mediumDifficulty)
    //         {
    //             spawnMediumSpike(GetNonRepeatRandom());
    //         }
    //         else if(difficultyClock<hardDifficulty)
    //         {
    //             spawnHardSpike(GetNonRepeatRandom());
    //         }
    //         else
    //         {
    //             spawnEnrageSpike(GetNonRepeatRandom());
    //         }
    //     }
    
    // }



    // void spawnHardSpike(int randomIndex)
    // {
    //     float lowestPoint = transform.position.y - heighOffset;
    //     float highestPoint = transform.position.y + heighOffset;

    //     GameObject instantiatedObject = Instantiate(mediumSpawn[randomIndex], new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0 ), transform.rotation) as GameObject;
        
    // }

    // void spawnMediumSpike(int randomIndex)
    // {
    //     float lowestPoint = transform.position.y - heighOffset;
    //     float highestPoint = transform.position.y + heighOffset;

    //     GameObject instantiatedObject = Instantiate(hardSpawn[randomIndex], new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0 ), transform.rotation) as GameObject;
        
    // }

    // void spawnEnrageSpike(int randomIndex)
    // {
    //     float lowestPoint = transform.position.y - heighOffset;
    //     float highestPoint = transform.position.y + heighOffset;

    //     GameObject instantiatedObject = Instantiate(enrageSpawn[randomIndex], new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0 ), transform.rotation) as GameObject;
        
    // }
}
