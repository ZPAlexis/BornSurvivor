using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPool : MonoBehaviour
{
    public GameObject cardPrefab;
    public Vector3 adjust = new Vector3(300f, 0.0f, 0.0f);
    public List<PowerUp> powerUpList = new List<PowerUp>();

    public void GenerateOptions()
    {
        List<int> list = new List<int>();
        for(int i = 0; i < powerUpList.Count; i++)
        {
            list.Add(i);
        }
        int rand1 = Random.Range(0, list.Count);
        int value1 = list[rand1];
        list.RemoveAt(rand1);
        GameObject powerUpGameObject1 = Instantiate(cardPrefab, gameObject.transform);
        powerUpGameObject1.transform.localPosition = transform.localPosition;
        powerUpGameObject1.GetComponent<PowerUpCard>().powerupData = powerUpList[value1];

        int rand2 = Random.Range(0, list.Count);
        int value2 = list[rand2];
        list.RemoveAt(rand2);
        GameObject powerUpGameObject2 = Instantiate(cardPrefab, gameObject.transform);
        powerUpGameObject2.transform.localPosition = transform.localPosition + adjust;
        powerUpGameObject2.GetComponent<PowerUpCard>().powerupData = powerUpList[value2];

        int rand3 = Random.Range(0, list.Count);
        int value3 = list[rand3];
        list.RemoveAt(rand3);
        GameObject powerUpGameObject3 = Instantiate(cardPrefab, gameObject.transform);
        powerUpGameObject3.transform.localPosition = transform.localPosition - adjust;
        powerUpGameObject3.GetComponent<PowerUpCard>().powerupData = powerUpList[value3];
    }

    public void InstantiateLeftPowerUp() {
        //GameObject powerUpGameObject = Instantiate(cardPrefab, spawnPosition, Quaternion.identity);
        //powerUpGameObject.GetComponent<PowerUpCard>().powerupData = powerUpList[randomNumber1];
    }
    public void IntantiateMiddlePowerUp() {

    }
    public void InstantiateRightPowerUp(){

    }
    
    void RemoveChoosenOption(PowerUp choice)
    {
        //find choice in powerUpList
        //removeAt choice location from powerUpList
    }

}