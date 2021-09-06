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


    [SerializeField] private GameObject _adsSelection;
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private Text _playerGemsText;
    [SerializeField] private Image _selectionImage;
    [SerializeField] private Text _gemCountUIText;
    [SerializeField] private GameObject _1health, _2health, _3health, _4health;
    [SerializeField] private CanvasGroup _resumeButton, _pauseButton;
    [SerializeField] private GameObject _alreadyBoughtImage;




    private void Start()
    {
        if (_shopPanel == null) Debug.LogError(transform.name +":: Shop Panel is null");
        if (_playerGemsText == null) Debug.LogError(transform.name + ":: PlayerGems text is null");
        if (_selectionImage == null) Debug.LogError(transform.name + ":: Selection Image is null");
        if (_gemCountUIText == null) Debug.LogError(transform.name + ":: Gem UI Text is null");

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        _pauseButton.alpha = 0;
        _resumeButton.alpha = 1;
        _pauseButton.blocksRaycasts = false;
        Debug.Log("pause called");
       
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        _pauseButton.alpha = 1;
        _resumeButton.alpha = 0;
        _pauseButton.blocksRaycasts = true;
        Debug.Log("reume called");
    }

    public void AdsSelected(bool Selected)
    {
        if (Selected)
        {
            _adsSelection.SetActive(true);
        }
        else if (!Selected)
        {
            _adsSelection.SetActive(false);
        }
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

    public void UpdateGemUI(int gems)
    {
        _gemCountUIText.text = gems.ToString();

    }

    public void MessageOff()
    {
        _alreadyBoughtImage.SetActive(false);
    }

    public void MessageOn()
    {
        _alreadyBoughtImage.SetActive(true);
    }

    public void UpdateLives(int lives)
    {
        switch (lives)
        {
            case 0:
                _1health.SetActive(false);
                _2health.SetActive(false);
                _3health.SetActive(false);
                _4health.SetActive(false);
                break;
            case 1:
                _1health.SetActive(true);
                _2health.SetActive(false);
                _3health.SetActive(false);
                _4health.SetActive(false);
                break;
            case 2:
                _1health.SetActive(true);
                _2health.SetActive(true);
                _3health.SetActive(false);
                _4health.SetActive(false);
                break;
            case 3:
                _1health.SetActive(true);
                _2health.SetActive(true);
                _3health.SetActive(true);
                _4health.SetActive(false);
                break;
            case 4:
                _1health.SetActive(true);
                _2health.SetActive(true);
                _3health.SetActive(true);
                _4health.SetActive(true);
                break;


            default:
                break;
        }
    }
}
