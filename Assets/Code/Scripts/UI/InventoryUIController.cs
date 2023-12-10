using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    public List<SlotButton> Slot;
    public Button PositiveButton;
    public Button BackButton;
    public TextMeshProUGUI PositiveButtonText;
    public string DefaultPositiveText;
    public string Selling = "Sell";
    public SlotButton SelectedSlot;
    private UnityAction<string> OnEquip;
    private UnityAction<ItemData> OnSell;
    private PlayerState currentPlayerState;
    private UnityAction OnMenuClosed;
    // Start is called before the first frame update
    private UnityAction<PlayerState> OnStateChanged;

    public void Init()
    {
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
        PositiveButton.onClick.AddListener(() => PositiveAction());
        BackButton.onClick.AddListener(() =>
        {
            CloseMenu();
        });
    }


    public void InitAction(UnityAction<string> onEquip, UnityAction<ItemData> onSell,  UnityAction onMenuClosed)
    {
        OnEquip += onEquip;
        OnSell += onSell;
        OnMenuClosed += onMenuClosed;
    }

    public void UpdateCurrentPlayerState(PlayerState playerState)
    {
        currentPlayerState = playerState;
    }

    public void OpenMenu(CurrentInteractable type)
    {
        gameObject.SetActive(true);
        if(type == CurrentInteractable.ShopKeeper)
            PositiveButtonText.text = Selling;
    }

    public void CloseMenu()
    {
        OnMenuClosed.Invoke();
        gameObject.SetActive(false);
        PositiveButtonText.text = DefaultPositiveText;
    }

    public void PositiveAction()
    {
        if(currentPlayerState == PlayerState.Selling)
        {
            SellSelectedItem();
        }
        else if(currentPlayerState == PlayerState.BrowseInventory)
        {
            EquipSelectedItem();
        }
        OnMenuClosed.Invoke();
        gameObject.SetActive(false);
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

    public void SellSelectedItem()
    {
        if (SelectedSlot != null && SelectedSlot.getItem() != null)
        {
            OnSell.Invoke(SelectedSlot.getItem());
            SelectedSlot.EmptySlot();
        }
    }
}
