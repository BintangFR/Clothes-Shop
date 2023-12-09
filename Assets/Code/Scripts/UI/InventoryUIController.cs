using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    public List<SlotButton> Slot;
    public Button PositiveButton;
    public Button BackButton;
    public string DefaultPositiveText;
    public string Selling = "Sell";
    public SlotButton SelectedSlot;
    private UnityAction<string> OnEquip;

    // Start is called before the first frame update
 

    public void Init(UnityAction<string> onEquip)
    {
        OnEquip += onEquip;
        //Load Current Icon
        foreach (var slot in Slot)
        {
            slot.Init(() =>
            {
                SelectedSlot = slot;
                PositiveButton.interactable = true;
            });
        }
        PositiveButton.interactable = false;
        PositiveButton.onClick.AddListener(() => EquipSelectedItem());
        BackButton.onClick.AddListener(() => gameObject.SetActive(false));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(ItemData item)
    {
        foreach (var slot in Slot)
        {
            if (slot.getIsEmpty())
            {
                slot.SetItem(item);
                return;
            }
        }
    }

    public void EquipSelectedItem()
    {
        // Check if an item is selected
        if (SelectedSlot != null && SelectedSlot.getItem() != null)
        {
            OnEquip.Invoke(SelectedSlot.getItem().ItemName);
        }
    }
}
