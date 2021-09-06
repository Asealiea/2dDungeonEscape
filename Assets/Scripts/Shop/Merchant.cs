using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    [SerializeField] private int _selectedItem;
    [SerializeField] private Player _player;
    [SerializeField] private int _itemCost;
    [SerializeField] private string _itemName;
    [SerializeField] private bool _shop = false;
    [SerializeField] private int _controllerSelected = 0;
    [SerializeField] private float _timer, _timer2 = 0.085f;
    [SerializeField] private float _adTimer = 0f;
    [SerializeField] private GameObject _buy;
    [SerializeField] private GameObject _buyItems;
    [SerializeField] private bool _bought1, _bought2, _bought3;


    #region Controller Support for shop
    private void Update()
    {
        if (_shop)
        {
            if (Input.GetAxis("Vertical") == 1f)
            {
                if (_timer2 <= 0)
                {
                    PreviousItem();
                    _timer2 = 0.085f;
                }
                else
                {
                    _timer2 -= Time.deltaTime;
                }

            }
            else if (Input.GetAxis("Vertical") == -1f)
            {
                if (_timer <= 0)
                {
                    NextItem();
                    _timer = 0.085f;
                }
                else
                {
                    _timer -= Time.deltaTime;
                }

            }
            if (Input.GetAxisRaw("Submit") == 1)
            {
                if (_timer <= 0)
                {
                    if (_controllerSelected != 0) // if selected an item and not the ads.
                    {
                        BuyItem();
                        _timer = 0.085f;
                        _controllerSelected = 0;
                    }
                    else
                    {
                        if (_adTimer <= 0)
                        {
                            AdsManager.Instance.ShowRewardedAd();
                            _adTimer = 1f;
                        }
                        else
                        {
                            _adTimer -= Time.deltaTime;
                        }                       
                    }
                }
                else
                {
                    _timer -= Time.deltaTime;
                }
            }
        }            
    }

    private void NextItem()
    {
      //  Debug.Log("you selected the next item");
        _controllerSelected++;
        if (_controllerSelected > 0 && _controllerSelected < 4)
        {
            SelectItem(_controllerSelected);
        }
        if (_controllerSelected >= 4)
        {
            _controllerSelected = 1;
            SelectItem(_controllerSelected);
            //select the ads.
        }
    //    Debug.Log(_controllerSelected);
    }

    private void PreviousItem()
    {
       // Debug.Log("you selected the previous item");
        _controllerSelected--;
        if (_controllerSelected > 0 && _controllerSelected < 4)
        {
            SelectItem(_controllerSelected);
        }
        if (_controllerSelected <= 0)
        {
            _controllerSelected = 0;
            SelectItem(_controllerSelected);
            //select the ads.
        }
    //    Debug.Log(_controllerSelected);
    }

#endregion

    #region Enter and Exit Shop
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {          
            _player = other.GetComponent<Player>();//to this           
            if (_player != null)
            {
                _buy.SetActive(true);
                _buyItems.SetActive(true);
                _shop = true;
                _controllerSelected = 0;
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
            _shop = false;
            _buy.SetActive(false);
            _buyItems.SetActive(false);
            if (player != null)
            {
                player.Shop(); //re-enables attack after exiting shop
            }
        }
    }

#endregion


    public void SelectItem(int itemID)
    {
        _selectedItem = itemID;
        Debug.Log("Selected " + itemID);
        switch (itemID)
        {
            case 0: //Ads
                UIManager.Instance.UpdateSelectionImage(700f);
                UIManager.Instance.AdsSelected(true);
                break;

            case 1: //flame Sword
                UIManager.Instance.AdsSelected(false);
                UIManager.Instance.UpdateSelectionImage(227f);
                _itemCost = 200;
                 _itemName = "a Flame Sword";
                break;
            case 2://Boots of Flight
                UIManager.Instance.UpdateSelectionImage(118f);
                _itemCost = 50;
                _itemName = "the Boots of Flight";
                Debug.Log(_itemName);
                break;
            case 3://key to Castle
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
        if ((_selectedItem == 1 && _bought1) ||(_selectedItem == 2 && _bought2) ||(_selectedItem == 3 && _bought3))
        {
            Debug.Log("You have alread bought this item");
            UIManager.Instance.MessageOn();
            return;
        }
        Debug.Log(_itemName);
        if (_player.DiamondsOnHand() >= _itemCost)
        {          
            _player.ShopPurchance(_itemCost,_selectedItem);
            UIManager.Instance.OpenShop(_player.DiamondsOnHand());
            UIManager.Instance.UpdateGemUI(_player.DiamondsOnHand());
            Debug.Log("You bought " + _itemName);
            if (_selectedItem == 1)  _player.GetComponentInChildren<Attack>().hasFireSword = true;
            switch (_selectedItem)
            {
                case 1:
                    _bought1 = true;
                    break;
                case 2:
                    _bought2 = true;
                    break;
                case 3:
                    _bought3 = true;
                    break;
                default:
                    break;
            }
        }
        else
        {       
            Debug.Log("You don't have enough diamonds, good bye");    
            UIManager.Instance.CloseShop();
        }
    }
 
}
