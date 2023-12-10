using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager UIManager;
    public PlayerController PlayerController;
    private PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        playerData = PlayerController.Init();
        UIManager.Init(playerData.GetMoney());
        PlayerController.InitAction(UIManager.BuyItem,UIManager.UpdateMoneyText, UIManager.OpenMenu, UIManager.SetCurrentState);
        UIManager.InitAction(PlayerController.EquipOutfit,PlayerController.SellSelectedItem, PlayerController.ResetState);
    }
}
