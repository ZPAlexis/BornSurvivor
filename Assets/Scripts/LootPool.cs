using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPool : MonoBehaviour
{
    public GameObject droppedItemPrefab;
    public List<Loot> lootList = new List<Loot>();

    List<Loot> GetDroppedItems()
    {
        int randomNumber = Random.Range(1, 101); // 1-100 inclusive & exclusive
        List<Loot> possibleItems = new List<Loot>();
        foreach(Loot item in lootList)
        {
            if(randomNumber <= item.dropChance)
            {
                possibleItems.Add(item);
            }
        }
        if(possibleItems.Count > 0)
        {
            return possibleItems;
        } 
        else
        {
            Debug.Log("No loot to drop!");
            return null;
        }
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        List<Loot> droppedLoot = GetDroppedItems();
        if(droppedLoot != null)
        {
            foreach(Loot item in droppedLoot)
            {
            GameObject lootGameObject = Instantiate(droppedItemPrefab, spawnPosition, Quaternion.identity);
            lootGameObject.GetComponent<SpriteRenderer>().sprite = item.lootSprite;
            lootGameObject.name = item.lootName;
            float dropForce = 10f;
            Vector2 dropDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            lootGameObject.GetComponent<Rigidbody2D>().AddForce(dropDirection * dropForce, ForceMode2D.Impulse);
            }
        }
    }
    
}
