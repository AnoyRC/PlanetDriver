using Rene.Sdk;
using Rene.Sdk.Api.Game.Data;
using ReneVerse;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReneverseManager : MonoBehaviour
{
    public GameObject Email;
    public TextMeshProUGUI Timer;
    public GameObject SignInPanel;
    public GameObject CountdownPanel;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public async void SignIn()
    {
        await ConnectUser();
    }

    async Task ConnectUser()
    {
        ReneAPICreds _reneAPICreds = ScriptableObject.CreateInstance<ReneAPICreds>();
        _reneAPICreds.APIKey = "541fdebb-07ae-4a1b-8f0b-a92d4fdfc517";
        _reneAPICreds.PrivateKey = "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQDJWoZUk/30WzBHOg5D9gMTQs8G7vFSbCI6izdnY9+WDOtAomB7yoUukQ1MAAcMGWvpJ/4S4BUMfiFQaVeja6drtwsW1EPnGGpzMEWnwlU5aCeGIlGpGX5lRnbS8rRlF67AXnU0j3FQM+Lpy2DSQbB7tfj9tpoHpLdI78lkjpUyC1r7cYuw4NOAei91dZZFnwi9qLdpf3dIq0K4Rr92Hq4quNPxO00x7IbyNKr16mqqYt+reQCIZJoXA3r4ACIJ/OPrjfZYEYP9EOILIXq/IktABilMtdT147zHAKl/biYTT/jdYOgEl9zexpC/hWUkWVuDS9bUZ836ACO8T+STsAq7AgMBAAECggEAA60T68Jh7g6mJH7CWOpKZxUeq308SalwvInjIo+XuukUeHMeeNQbN1ztlbggaM+kATc6M8UEjVxWJl4t++1Quq42VR3d+X+gHedBNk1defVVV2UUAZbWEGpHbBWDRp82kQuIb0O5KWIxH9yXMW+CldeeKp+lU0NmFz5NenkB705ueebbM2foOjb8Dp2jW4OxySCcQ/PEdB4j6RWVSRCTUA5RoYyEZ7ypdE5KdsjjWuz2O8mbNU664PsHaxLtc3lF4KyOUVrD5lx1mfL3QDbiaOqzhSt/+T2ZDqyvPSSPJuBHxjxTYd+lp4oCIXgFlk4Q0P2oN7vC253MsYBna9ViyQKBgQDlfzbpejHQ0lLONuVj3afHFroLrTrBBbFOmiAQomFOZmEPizzvjnJ8Ik3+lJYpxchk21Pm+t9IslgkUeP4Us61dCMVn6yv29KOi7j8qioSVQfzsk35KUuzJrcsaZZebW/2Xfpu4O4XJcrkZViPIl55DS3oCGCS80i9Ssc8tmIDiQKBgQDgm0no3tS+1GgmmT1LTKzXkTzlK2mIZHDNBcLmUUFpeVKXmkZD6MjYN9T20jkob0l2ewYvEu+4y+xnNWMxAt+Vf4/k6V9Re1mBu2ip55TrZqL4VPjkF5IXDBHg048vbl4a/gqpBeqz0TuA0RpREf5+9YjCh/6LtuBp6MtR6OZXIwKBgQCNdj4V6tF8I7kRDbuWNFIwIS0q92vJAUZK6iwrtLwARJE9sJkuHQMqy0aCT3rEYvrkWGG+dhNeTziPor7AGeL67IyXzqX2fKysWyn67LUkOa++IsF3fWIBGzM7uBYNPK4QEdvFrvZz5ELNSD8vc4MCYQJUDZ3h1v6WV9q57L476QKBgQCnB7SqIfQyScF+Wt0zPz0WPmL7xhIAJTmhrlQndWvgpSGfrlHstJOP/803FFT/Veta2dhab4mocrZGdnRigVGGWsvGnyqMbN++U4FdQOQFP1mYtZA2B7VTwF0XRh0oV7pR9nQ1CDpnKQVIIbiha3FINw5SJFJO0lwHRuDU9A8/4wKBgAvy2hwdA1QmjovEZ5WGPyUFHSfLsClV14v0yJc5VAy4jouj++V3y6Wo+07STArjMXSA5seKnNDbRffdkBwr/fiUL/H6UncHE0HWfDnmyrHJFoegE+wl0SBERVlmUEE5VpJZzrieH0JJOHBt7xfaTY9fsBUR9Pw8B8C3CGBJO/HS";
        _reneAPICreds.GameID = "15f63b8c-6442-42f5-9a02-c6b6e5d45876";
        var ReneAPI = API.Init(_reneAPICreds.APIKey, _reneAPICreds.PrivateKey, _reneAPICreds.GameID);
        string input = Email.GetComponent<TMP_InputField>().text;
        bool connected = await ReneAPI.Game().Connect(input);
        Debug.Log(connected);
        if (!connected) return;
        StartCoroutine(ConnectReneService(ReneAPI));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ConnectReneService(API reneApi)
    {
        CountdownPanel.SetActive(true);
        var counter = 30;
        var userConnected = false;
        //Interval how often the code checks that user accepted to log in
        var secondsToDecrement = 1;
        while (counter >= 0 && !userConnected)
        {
            Timer.text = counter.ToString();
            if (reneApi.IsAuthorized())
            {

                CountdownPanel.SetActive(false);
                SignInPanel.SetActive(false);
                //Here can be added any extra logic once the user logged in



                yield return GetUserAssetsAsync(reneApi);
                userConnected = true;
            }

            yield return new WaitForSeconds(secondsToDecrement);
            counter -= secondsToDecrement;
        }
        CountdownPanel.SetActive(false);
    }

    private async Task GetUserAssetsAsync(API reneApi)
    {
        AssetsResponse.AssetsData userAssets = await reneApi.Game().Assets();
        //By this way you could check in the Unity console your NFT assets
        userAssets?.Items.ForEach
        (asset => Debug.Log
            ($" - Asset Id '{asset.NftId}' Name '{asset.Metadata.Name}"));
        userAssets?.Items.ForEach(asset =>
        {
            string assetName = asset.Metadata.Name;
            string assetImageUrl = asset.Metadata.Image;
            string assetStyle = "";
            asset.Metadata?.Attributes?.ForEach(attribute =>
            {
                //Keep in mind that this TraitType should be preset in your Reneverse Account
                if (attribute.TraitType == "Style")
                {
                    assetStyle = attribute.Value;
                }
            });
            //An example of how you could keep retrieved information
            Asset assetObj = new Asset(assetName, assetImageUrl, assetStyle);
            //one of many ways to add it to the game logic 
            //_assetManager.userAssets.Add(assetObj);
        });
    }

}
public class Asset
{
    public string AssetName { get; set; }
    public string AssetUrl { get; set; }

    public string AssetStyle { get; set; }
    public Asset(string assetName, string assetUrl, string assetStyle)
    {
        AssetName = assetName;
        AssetUrl = assetUrl;
        AssetStyle = assetStyle;
    }
}
