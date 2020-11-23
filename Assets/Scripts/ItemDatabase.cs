using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDatabase : MonoBehaviour
{

    public List<Item> items = new List<Item>();

    private void Awake()
    {
        BuildDatabase();
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string itemName)
    {
        return items.Find(item => item.title == itemName);
    }

    void BuildDatabase()
    {
        items = new List<Item>() {
            new Item(0, "Sunflower", "Testbeskrivelse",
            new Dictionary<string, int>
            {
                {"Power", 15 },
                {"Defence", 10 }
            }),
            new Item(1, "Mint Leaf", "Testbeskrivelse",
            new Dictionary<string, int>
            {
                {"Power", 15 },
                {"Defence", 10 }
            })

            };
    }
}
