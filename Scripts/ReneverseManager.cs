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
        _reneAPICreds.APIKey = "f19a1334-0ded-4e94-8031-a82bd3c67d6d";
        _reneAPICreds.PrivateKey = "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQDFwoHk4SNNfQOFlQpSUgLRF3ylzq6ICc7wHL/dA7iFthAm+MUdVJvMQicrHGPQOAW64dp8ZsTkQ6uWa1POcsB36somSNt86BsZMSKWMcRn6U3MSF2jwzTocfxt+oQrWw7GYDLIMeD1Z4Mqc5dpgmfVqGDxSP2mX2sEFHUZmafOHsNZlgOn4LWHwS7k0+sp3y9+ai/hCGt1yKXXyB/+JYAr+f46dP6QfQVpJbPQZ0SSP2akoDNGod+he+1P2yR9rOn7ck7qLiaEACSw5FekmoMHE9rSwtgiidgYlqXeNjOmSQtghlakvS0vmw65TPVgoUZdZotJ6tV8rzHaRgwblxpZAgMBAAECggEASL33vqNrs2SRJ5E/dzFQdO3hPTIts7973YmNr3Pfa1uhTFyEUPoexr7snmh2dJAu12OdVNYOhXI4yv8WmCFQl6uHCRcSmTrdQOJ4eOu8dYZ1Rfe2bfbvXrMS6bdU54e6gxC+jTH/r/UP480Z/Ebp/8bMReSV3J1LhXgq7Kw+88tqYCEj+vqUYZV1Yznop5Ejrr2RQFmwvNfWMeT3S1PWTK/xhFB8BOzcqcd0QxcV6qeKfJZIJrIR9nh7bT0cRGK7vOjIEjYU4sOQpXFXWeJW5lfvZ3NstIzp8HvuKspTUw+N1C2RxsPf1kSUvCd0chiXzTI3RVyodEwmzPsKIDinowKBgQDqZWirVtuFdWC3MMLxUjlS3KPVH0s7OB08onbbuuLWElF36d93vwvfROTdpEhZZG6oSOpqaluKpBB9ch2DWPsEFeiD841BLuIuTFOXC2/7/HYrEJjUNKcAaXhTi6dqTnGK4Bm+JR+Au998zXPuG2W+lNi+cqIcC6tRQrIwMJfP9wKBgQDX/Kl3DFUZ8F0343q7X4hOFO+QUTx30PY7BXw974jXpFIFYVNmh8aGG+JCLuj3THKrOyiJb3sA1GGLxET1pKDny8fEyGvW7cCTPk8FAcsLT601rsZP5ta5yStolt44LjWMv7Oq3z+kTi10NerEbNksXKzTVCOICNYYbaHP32R0LwKBgQDiXeVj85I1pVIuGM1ruXja6XLflEVXf63crRoUwrvm+fHr2NWUE9EnYqWU1993VWL46tJYyzZ1AlVakSanfvuMyouvQzsbxGdzRwtDSCxyMzL8DB9McT0HTNSD+s9H/1HwSNUTqU5vSQFgyho3zgXItH9ODe85Hpvo8nybxZIXqQKBgQDC55n1eAeDtMgqGvkyBO93pqTYUraCWOsaL8UCDxopnnr1p/Ie3/9iHd97YeGQ6EfCBUx7WUJiaUTtX1vrX3hkNBw9k67c8QeK1/tuxUN5HZlsjB8hE2pJlSO66gn2IJzqOkPjujowBuw1pkIIp0EDK046Ff9KTs8ElEokz65SuQKBgCIFlxzm5Wy71r/uDryBD+MmYBWv9Qb4sDxeFUU/azUajpSG1t0K3cTY3uUeU8ev/IZ1pZyzU0nmgRfeCidSnV1ll4AVsmCSWhjVAl5w3FNAE05MKw9vJoaxmj69BHkC7NNlN8+JpWfb+CfOzQdGbkleStmCIEN6UqtSfavW6v5l";
        _reneAPICreds.GameID = "feda45f2-d8f3-4aa1-9c06-4792e91ba596";
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
