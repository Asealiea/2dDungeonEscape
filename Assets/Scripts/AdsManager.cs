using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class AdsManager : MonoBehaviour
{

    #region Singleton
    private static AdsManager _instance;
    public static AdsManager Instance
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


    public void ShowRewardedAd()
    {
        
        if (Advertisement.IsReady("Rewarded_Android"))
        {
            var options = new ShowOptions 
            { 
                resultCallback = HandleShowResults
            };
            
            Advertisement.Show("Rewarded_Android", options);
        }
    }


    void HandleShowResults(ShowResult result)
    {
        switch (result)
        {
        
            case ShowResult.Failed: 
                Debug.Log("Video failed, video might not have been ready");
                break;
            case ShowResult.Skipped:
                Debug.Log("Video Skipped, no reward");
                break;
            case ShowResult.Finished:
                //award diamonds to player
                GameManager.Instance.Player.UpdateDiamonds(100);
                UIManager.Instance.OpenShop(GameManager.Instance.Player.DiamondsOnHand());
                break;
            default:
                break;
        }
    } 


    




}
