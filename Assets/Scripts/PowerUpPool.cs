using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPool : MonoBehaviour
{
    public GameObject cardPrefab;
    public Vector3 adjust = new Vector3(300f, 0.0f, 0.0f);
    public List<PowerUp> powerUpList = new List<PowerUp>();
    public GameObject choice1, choice2, choice3;

    public void GenerateOptions()
    {
        //known bug once powerUpList has less then 3 options
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
        powerUpGameObject1.name = value1.ToString();
        choice1 = powerUpGameObject1;

        int rand2 = Random.Range(0, list.Count);
        int value2 = list[rand2];
        list.RemoveAt(rand2);
        GameObject powerUpGameObject2 = Instantiate(cardPrefab, gameObject.transform);
        powerUpGameObject2.transform.localPosition = transform.localPosition + adjust;
        powerUpGameObject2.GetComponent<PowerUpCard>().powerupData = powerUpList[value2];
        powerUpGameObject2.name = value2.ToString();
        choice2 = powerUpGameObject2;

        int rand3 = Random.Range(0, list.Count);
        int value3 = list[rand3];
        list.RemoveAt(rand3);
        GameObject powerUpGameObject3 = Instantiate(cardPrefab, gameObject.transform);
        powerUpGameObject3.transform.localPosition = transform.localPosition - adjust;
        powerUpGameObject3.GetComponent<PowerUpCard>().powerupData = powerUpList[value3];
        powerUpGameObject3.name = value3.ToString();
        choice3 = powerUpGameObject3;
    }

    public void RemoveOptions()
    {
        if(choice1 != null && choice2 != null && choice3 != null)
        {
        Destroy(choice1, 0);
        Destroy(choice2, 0);
        Destroy(choice3, 0);
        }
    }
    
    public void RemoveChoosenOption(string name)
    {
        int value = int.Parse(name);
        powerUpList.RemoveAt(value);
        //find choice in powerUpList
        //removeAt choice location from powerUpList
    }

}