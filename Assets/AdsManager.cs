using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class AdsManager : MonoBehaviour
{

#if UNITY_IOS
    private string gameId = "4212956";
#elif UNITY_ANDROID
    private string gameId = "4212957";
#endif


    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    bool testMode = false;

    void Start()
    {
        // Initialize the Ads service:
        Advertisement.Initialize(gameId, testMode);
        StartCoroutine( ShowBannerWhenReady() );
    }

    public static void ShowInterstitialAd()
    {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady())
        {
            Advertisement.Show("Interstitial_Android");
            // Replace mySurfacingId with the ID of the placements you wish to display as shown in your Unity Dashboard.
        }
        else
        {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }

    IEnumerator ShowBannerWhenReady() {

        while (!Advertisement.IsReady("Banner_Android"))
        {
            yield return new WaitForSeconds(1f);
        }

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show("Banner_Android");
    }

}
