using UnityEngine.Advertisements;
using UnityEngine;

public class UnityAds : MonoBehaviour
{

    string androidID = "4755667";
    bool testMode = true;
    void Start()
    {
        Advertisement.Initialize(androidID, testMode);
        Debug.Log("advertisement loaded");
    }

    public static void ShowInterstitialAds()
    {
        Advertisement.Show("Interstitial_Android");
        Debug.Log("showing ads");

    }
}
