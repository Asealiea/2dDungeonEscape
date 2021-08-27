using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager:: Instance is null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion



    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private Text _playerGemsText;
    [SerializeField] private Image _selectionImage;



    private void Start()
    {
        if (_shopPanel == null) Debug.LogError(transform.name +":: Shop Panel is null");
        if (_playerGemsText == null) Debug.LogError(transform.name + ":: PlayerGems text is null");
        if (_selectionImage == null) Debug.LogError(transform.name + ":: Selection Image is null");

    }


    public void OpenShop(int gems)
    {
        UIManager.Instance.UpdateSelectionImage(700f);
        _playerGemsText.text = gems.ToString() + " G";
        _shopPanel.SetActive(true);
    }

    public void CloseShop()
    {
        _shopPanel.SetActive(false);
    }

    public void UpdateSelectionImage(float ypos)
    {
        _selectionImage.rectTransform.anchoredPosition = new Vector2(_selectionImage.rectTransform.anchoredPosition.x, ypos);
    }
}
