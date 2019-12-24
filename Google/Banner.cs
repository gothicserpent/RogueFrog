using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class Banner : MonoBehaviour
{
private BannerView bannerView;

public void Start()
{
	#if UNITY_ANDROID
	string appId = "ca-app-pub-4294087848191572~2076113164";     //APPID SET FROM UNITY
	#elif UNITY_IPHONE
	string appId = "ca-app-pub-4294087848191572~2076113164";     //APPID SET FROM UNITY
	#else
	string appId = "unexpected_platform";
	#endif

	// Initialize the Google Mobile Ads SDK.
	MobileAds.Initialize(appId);

	this.RequestBanner();
}

private void RequestBanner()
{
	#if UNITY_ANDROID
	string adUnitId = "ca-app-pub-3940256099942544/6300978111";     //TEST ID
	//string adUnitId = "ca-app-pub-4294087848191572/9528061921"; //REAL ID - ONLY USE IN FINAL.
	#elif UNITY_IPHONE
	string adUnitId = "ca-app-pub-3940256099942544/2934735716";     //TEST ID
	//string adUnitId = "ca-app-pub-4294087848191572/9528061921"; //REAL ID - ONLY USE IN FINAL.
	#else
	string adUnitId = "unexpected_platform";
	#endif

	// Create a 320x50 banner at the bottom of the screen.
	AdSize adSize = new AdSize(320, 50);
	bannerView = new BannerView(adUnitId, adSize, AdPosition.Bottom);

	// Create an empty ad request.
	AdRequest request = new AdRequest.Builder()
	                    .AddTestDevice("FECC1BDF32D17C87")
	                    .AddTestDevice("03c99f1375cb1cec5a21bfbef6cedac4d8b97353")
	                    .Build();

	// Load the banner with the request.
	this.bannerView.LoadAd(request);

	//Debug.Log("ID: " + GetDeviceID());
}

}
