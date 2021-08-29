using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    private int _selectedItem;
    private Player _player;
    private int _itemCost;
    private string _itemName;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {          
            _player = other.GetComponent<Player>();//to this           
            if (_player != null)
            {
                _player.Shop(); //extra method to disbale being able to attack when in the shop
                UIManager.Instance.OpenShop(_player.DiamondsOnHand());
            }
            
        }
    } 




    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.CloseShop();
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.Shop(); //re-enables attack after exiting shop
            }
        }
    }

 
    
    public void SelectItem(int itemID)
    {
        _selectedItem = itemID;
        Debug.Log("Selected " + itemID);
        switch (itemID)
        {
            case 0: //flame Sword
                UIManager.Instance.UpdateSelectionImage(227f);
                _itemCost = 200;
                 _itemName = "a Flame Sword";
                break;
            case 1://Boots of Flight
                UIManager.Instance.UpdateSelectionImage(118f);
                _itemCost = 400;
                _itemName = "the Boots of Flight";
                break;
            case 2://key to Castle
                UIManager.Instance.UpdateSelectionImage(15f);
                _itemCost = 100;
                 _itemName = "the Key to Castle";
                break;

            default:
                UIManager.Instance.UpdateSelectionImage(700f);
                break;
        }
    }
    

    public void BuyItem()
    {
       
        if (_player.DiamondsOnHand() >= _itemCost)
        {          
            _player.ShopPurchance(_itemCost,_selectedItem);
            UIManager.Instance.OpenShop(_player.DiamondsOnHand());
            UIManager.Instance.UpdateGemUI(_player.DiamondsOnHand());
            Debug.Log("You bought " + _itemName);
            if (_selectedItem == 0)  _player.GetComponentInChildren<Attack>().hasFireSword = true;
        }
        else
        {       
            Debug.Log("You don't have enough diamonds, good bye");    
            UIManager.Instance.CloseShop();
        }
    }
 
}
