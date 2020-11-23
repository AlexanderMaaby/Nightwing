using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    public Item item;
    public Image spriteImage;

    private void Awake()
    {
        spriteImage = GetComponent<Image>();
    }

    public void UpdateItem(Item item)
    {
        this.item = item;
        if (this.item != null)
        {
            spriteImage.color = new Color32(48, 25, 52, 100); ;
            spriteImage.sprite = this.item.icon;
        }
        else
        {
            spriteImage.color = Color.clear;
        }
    }
}
