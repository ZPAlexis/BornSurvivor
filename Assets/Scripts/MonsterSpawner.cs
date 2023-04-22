using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public LogicManager logic;
    public GameObject[] spawnGroup1;
    public GameObject[] spawnGroup2;
    public GameObject[] spawnGroup3;
    public GameObject[] spawnGroup4;
    public GameObject[] spawnGroup5;
    public GameObject[] spawnGroup6;
    public GameObject[] spawnGroup7;
    public GameObject[] spawnGroup8;
    public GameObject[] spawnGroup9;
    public GameObject[] spawnGroup10;
    public GameObject[] spawnGroup11;
    public GameObject[] spawnGroup12;
    public GameObject[] spawnGroup13;
    public GameObject[] spawnGroup14;
    public float spawnRate = 6;
    public float spawnOffset = 1.5f;
    private float spawnTimer = 0;
    List<GameObject[]>list = new List<GameObject[]>(); 

    void Start()
    {
        // FillList();
        spawnEasyGroup();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicManager>();

    }
    void Update()
    {
        if(spawnTimer<spawnRate)
        {
            spawnTimer += Time.deltaTime;
        }
        else
        {
            spawnTimer = 0;
            if(logic.GetDifficulty() == "Easy")
            {
                spawnEasyGroup();
            }
            if(logic.GetDifficulty() == "Normal")
            {
                spawnNormalGroup();
            }
            if(logic.GetDifficulty() == "Hard")
            {
                spawnHardGroup();
            }
            if(logic.GetDifficulty() == "Very Hard")
            {
                spawnVeryHardGroup();
            }
            if(logic.GetDifficulty() == "Insane")
            {
                spawnInsaneGroup();
            }
            if(logic.GetDifficulty() == "Impossible")
            {
                spawnImpossibleGroup();
            }
            //check difficulty to spawn
            //based on difficulty -> spawn a whole group from a selection of groups
        }
    }

    void spawn(GameObject[][] group)
    {
        int rand = Random.Range(0, group.Length);
        GameObject[] result = group[rand];
        for(int i = 0; i<result.Length; i++)
        {
            float lowYOffsetPoint = transform.position.y - spawnOffset;
            float highYOffsetPoint = transform.position.y + spawnOffset;
            float lowXOffsetPoint = transform.position.x - spawnOffset;
            float highXOffsetPoint = transform.position.x + spawnOffset;
            GameObject instantiatedObject = Instantiate(result[i], new Vector3(Random.Range(lowXOffsetPoint, highXOffsetPoint), Random.Range(lowYOffsetPoint, highYOffsetPoint), 0 ), transform.rotation, gameObject.transform);
        }
    }

    void spawnEasyGroup()
    {
        GameObject[][] group = new GameObject[][] 
        {
            spawnGroup1,
            spawnGroup2,
            spawnGroup3,
            spawnGroup4
        };
        spawn(group);
        //Debug.Log("Spawned " + regularSpawn[randomIndex].name.ToString());
    }

    void spawnNormalGroup()
    {
        GameObject[][] group = new GameObject[][] 
        {
            spawnGroup3,
            spawnGroup4,
            spawnGroup5,
            spawnGroup6
        };
        spawn(group);
    }

    void spawnHardGroup()
    {
        GameObject[][] group = new GameObject[][] 
        {
            spawnGroup5,
            spawnGroup6,
            spawnGroup7,
            spawnGroup8
        };
        spawn(group);
    }
    
    void spawnVeryHardGroup()
    {
        GameObject[][] group = new GameObject[][] 
        {
            spawnGroup7,
            spawnGroup8,
            spawnGroup9,
            spawnGroup10
        };
        spawn(group);
    }
    void spawnInsaneGroup()
    {
        GameObject[][] group = new GameObject[][] 
        {
            spawnGroup9,
            spawnGroup10,
            spawnGroup11,
            spawnGroup12
        };
        spawn(group);
    }
    void spawnImpossibleGroup()
    {
        GameObject[][] group = new GameObject[][] 
        {
            spawnGroup11,
            spawnGroup12,
            spawnGroup13,
            spawnGroup14
        };
        spawn(group);
    }
}