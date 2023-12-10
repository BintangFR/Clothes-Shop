using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SlotButton : MonoBehaviour
{
    public Button Button;
    public Image Icon;
    private ItemData Item;
    private UnityAction OnSelected;
    private bool IsEmpty = true;

    public void Init(UnityAction onSelected)
    {
        OnSelected += onSelected;
        Button.onClick.AddListener(() => OnSelected.Invoke());
        Button.interactable = false;
    }

    public void setIsEmpty(bool isEmpty)
    {
        this.IsEmpty = isEmpty;
    }

    public bool getIsEmpty()
    {
        return IsEmpty;
    }

    public void SetItem(ItemData item)
    {
        this.Item = item;
        Icon.sprite = item.Icon;
        IsEmpty = false;
        Button.interactable = true;
    }

    public ItemData getItem()
    {
        return Item;
    }
}
