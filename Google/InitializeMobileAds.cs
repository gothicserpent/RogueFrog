using UnityEngine;
using GoogleMobileAds.Api;

public class InitializeMobileAds : MonoBehaviour
{
    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
    }
}
