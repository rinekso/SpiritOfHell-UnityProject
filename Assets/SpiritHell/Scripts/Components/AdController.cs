using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdController : MonoBehaviour
{
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardBasedVideoAd;
    private float deltaTime = 0.0f;
    private static string outputMessage = string.Empty;
    private bool conAd;
    public static string OutputMessage
    {
        set { outputMessage = value; }
    }

    public void Start()
    {
            
//        AndroidNativeFunctions.ShowToast("ca-app-pub-8325729314474996~5932340612");

#if UNITY_ANDROID
        string appId = "ca-app-pub-8793311481503149~5085937771";
#elif UNITY_IPHONE
        string appId = "ca-app-pub-7911348052455769~5649252046";
#else
        string appId = "unexpected_platform";
#endif

        // MobileAds.SetiOSAppPauseOnBackground(true);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
        rewardBasedVideoAd = RewardBasedVideoAd.Instance;

        this.CreateAndLoadRewardedAd();
        // this.RequestBanner();
        // this.RequestInterstitial();
    }

    public void Update()
    {
        // Calculate simple moving average for time to render screen. 0.1 factor used as smoothing
        // value.
        this.deltaTime += (Time.deltaTime - this.deltaTime) * 0.1f;
    }

    /*
    this.RequestBanner();
    this.bannerView.Destroy();
    this.RequestInterstitial();
    this.ShowInterstitial();
    this.interstitial.Destroy();
    this.CreateAndLoadRewardedAd();
    this.ShowRewardedAd();
    */
    // Returns an ad request with custom ad targeting.
    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .AddTestDevice(AdRequest.TestDeviceSimulator)
            .AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
            .AddKeyword("game")
            .SetGender(Gender.Male)
            .SetBirthday(new DateTime(1985, 1, 1))
            .TagForChildDirectedTreatment(false)
            .AddExtra("color_bg", "9B30FF")
            .Build();
    }

    private void RequestBanner()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up banner ad before creating a new one.
        if (this.bannerView != null)
        {
            this.bannerView.Destroy();
        }

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

        // Register for ad events.
        this.bannerView.OnAdLoaded += this.HandleAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this.bannerView.OnAdOpening += this.HandleAdOpened;
        this.bannerView.OnAdClosed += this.HandleAdClosed;
        this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;

        // Load a banner ad.
        this.bannerView.LoadAd(this.CreateAdRequest());
    }

    private void RequestInterstitial()
    {
        // These ad units are configured to always serve test ads.
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        // string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up interstitial ad before creating a new one.
        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }

        // Create an interstitial.
        this.interstitial = new InterstitialAd(adUnitId);

        // Register for ad events.
        this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
        this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
        this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
        this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
        this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

        // Load an interstitial ad.
        this.interstitial.LoadAd(this.CreateAdRequest());
    }

    public void CreateAndLoadRewardedAd()
    {
        this.LoadRewardedAd();
        // Called when an ad request has successfully loaded.
        this.rewardBasedVideoAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardBasedVideoAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardBasedVideoAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardBasedVideoAd.OnAdRewarded += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardBasedVideoAd.OnAdClosed += HandleRewardedAdClosed;

    }

    public void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
        else
        {
            AndroidNativeFunctions.ShowToast("Interstitial is not ready yet");
        }
    }

    public void ShowRewardedAd()
    {
        if (this.rewardBasedVideoAd.IsLoaded())
        {
            this.rewardBasedVideoAd.Show();
        }
        else
        {
            this.LoadRewardedAd();
        }
    }
    private void LoadRewardedAd()
    {
#if UNITY_EDITOR
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-8793311481503149/8833611098";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        string adUnitId = "unexpected_platform";
#endif
        // Load rewarded ad instance.
        this.rewardBasedVideoAd.LoadAd(CreateAdRequest(),adUnitId);
    }
    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        AndroidNativeFunctions.ShowToast("HandleAdLoaded event received");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        AndroidNativeFunctions.ShowToast("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        AndroidNativeFunctions.ShowToast("HandleAdOpened event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        AndroidNativeFunctions.ShowToast("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        AndroidNativeFunctions.ShowToast("HandleAdLeftApplication event received");
    }

    #endregion

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        // AndroidNativeFunctions.ShowToast("HandleInterstitialLoaded event received");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        // AndroidNativeFunctions.ShowToast(
        //     "HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        // AndroidNativeFunctions.ShowToast("HandleInterstitialOpened event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        //this.interstitial.Destroy();
        this.RequestInterstitial();
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        // AndroidNativeFunctions.ShowToast("HandleInterstitialLeftApplication event received");
    }

    #endregion

    #region RewardedAd callback handlers

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
 //       AndroidNativeFunctions.ShowToast("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, EventArgs args)
    {
        this.LoadRewardedAd();
        //       AndroidNativeFunctions.ShowToast(
        //            "HandleRewardedAdFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
//        AndroidNativeFunctions.ShowToast("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, EventArgs args)
    {
        this.LoadRewardedAd();
        // AndroidNativeFunctions.ShowToast(
        //     "HandleRewardedAdFailedToShow event received with message: " + args.ToString());
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {

        this.LoadRewardedAd();
        //        AndroidNativeFunctions.ShowToast("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;

        /*        AndroidNativeFunctions.ShowToast(
                    "HandleRewardedAdRewarded event received for "
                                + amount.ToString() + " " + type);
                                */
    }

    #endregion
}
