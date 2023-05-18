using Rene.Sdk;
using Rene.Sdk.Api.Game.Data;
using ReneVerse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReneverseManager : MonoBehaviour
{
    //Global Stats
    public static bool LoginStatus = false;
    public static Dictionary<string, bool> SkinStats = new Dictionary<string, bool>();
    public static string EmailHandler;

    public GameObject Email;
    public TextMeshProUGUI Timer;

    public GameObject SignInPanel;
    public GameObject CountdownPanel;

    public GameObject AlternateSignIn;
    public TextMeshProUGUI AlternateTimer;

    ReneAPICreds _reneAPICreds;
    // Start is called before the first frame update
    void Start()
    {
        _reneAPICreds = ScriptableObject.CreateInstance<ReneAPICreds>();
        SkinStats["Beetal"] = true;
        if (LoginStatus)
            SignInPanel.SetActive(false);
    }

    public async void SignIn()
    {
        await ConnectUser();
    }

    public async void SignInBeatRider()
    {
        await ConnectBeatRider();
    }

    async Task ConnectUser()
    {
        _reneAPICreds.APIKey = "f19a1334-0ded-4e94-8031-a82bd3c67d6d";
        _reneAPICreds.PrivateKey = "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQDFwoHk4SNNfQOFlQpSUgLRF3ylzq6ICc7wHL/dA7iFthAm+MUdVJvMQicrHGPQOAW64dp8ZsTkQ6uWa1POcsB36somSNt86BsZMSKWMcRn6U3MSF2jwzTocfxt+oQrWw7GYDLIMeD1Z4Mqc5dpgmfVqGDxSP2mX2sEFHUZmafOHsNZlgOn4LWHwS7k0+sp3y9+ai/hCGt1yKXXyB/+JYAr+f46dP6QfQVpJbPQZ0SSP2akoDNGod+he+1P2yR9rOn7ck7qLiaEACSw5FekmoMHE9rSwtgiidgYlqXeNjOmSQtghlakvS0vmw65TPVgoUZdZotJ6tV8rzHaRgwblxpZAgMBAAECggEASL33vqNrs2SRJ5E/dzFQdO3hPTIts7973YmNr3Pfa1uhTFyEUPoexr7snmh2dJAu12OdVNYOhXI4yv8WmCFQl6uHCRcSmTrdQOJ4eOu8dYZ1Rfe2bfbvXrMS6bdU54e6gxC+jTH/r/UP480Z/Ebp/8bMReSV3J1LhXgq7Kw+88tqYCEj+vqUYZV1Yznop5Ejrr2RQFmwvNfWMeT3S1PWTK/xhFB8BOzcqcd0QxcV6qeKfJZIJrIR9nh7bT0cRGK7vOjIEjYU4sOQpXFXWeJW5lfvZ3NstIzp8HvuKspTUw+N1C2RxsPf1kSUvCd0chiXzTI3RVyodEwmzPsKIDinowKBgQDqZWirVtuFdWC3MMLxUjlS3KPVH0s7OB08onbbuuLWElF36d93vwvfROTdpEhZZG6oSOpqaluKpBB9ch2DWPsEFeiD841BLuIuTFOXC2/7/HYrEJjUNKcAaXhTi6dqTnGK4Bm+JR+Au998zXPuG2W+lNi+cqIcC6tRQrIwMJfP9wKBgQDX/Kl3DFUZ8F0343q7X4hOFO+QUTx30PY7BXw974jXpFIFYVNmh8aGG+JCLuj3THKrOyiJb3sA1GGLxET1pKDny8fEyGvW7cCTPk8FAcsLT601rsZP5ta5yStolt44LjWMv7Oq3z+kTi10NerEbNksXKzTVCOICNYYbaHP32R0LwKBgQDiXeVj85I1pVIuGM1ruXja6XLflEVXf63crRoUwrvm+fHr2NWUE9EnYqWU1993VWL46tJYyzZ1AlVakSanfvuMyouvQzsbxGdzRwtDSCxyMzL8DB9McT0HTNSD+s9H/1HwSNUTqU5vSQFgyho3zgXItH9ODe85Hpvo8nybxZIXqQKBgQDC55n1eAeDtMgqGvkyBO93pqTYUraCWOsaL8UCDxopnnr1p/Ie3/9iHd97YeGQ6EfCBUx7WUJiaUTtX1vrX3hkNBw9k67c8QeK1/tuxUN5HZlsjB8hE2pJlSO66gn2IJzqOkPjujowBuw1pkIIp0EDK046Ff9KTs8ElEokz65SuQKBgCIFlxzm5Wy71r/uDryBD+MmYBWv9Qb4sDxeFUU/azUajpSG1t0K3cTY3uUeU8ev/IZ1pZyzU0nmgRfeCidSnV1ll4AVsmCSWhjVAl5w3FNAE05MKw9vJoaxmj69BHkC7NNlN8+JpWfb+CfOzQdGbkleStmCIEN6UqtSfavW6v5l";
        _reneAPICreds.GameID = "feda45f2-d8f3-4aa1-9c06-4792e91ba596";
        API ReneAPI = API.Init(_reneAPICreds.APIKey, _reneAPICreds.PrivateKey, _reneAPICreds.GameID);
        EmailHandler = Email.GetComponent<TMP_InputField>().text;
        bool connected = await ReneAPI.Game().Connect(EmailHandler);
        Debug.Log(connected);
        if (!connected) return;
        StartCoroutine(ConnectReneService(ReneAPI));
    }

    // Update is called once per frame
    void Update()
    {

    }

    async Task ConnectBeatRider()
    {
        _reneAPICreds.APIKey = "e617d1c2-4efe-4203-8a2d-088df7c550ac";
        _reneAPICreds.PrivateKey = "MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQCZK2pGrbCDuWq0/pwFBefifh5CUpgulJwN4EXZ7fUmkcHNCoyiI42U0bWAGtHlFPzESwqzXSZR/wWkYiMmfpDydrfaZksYW6w4zvaacVjpnOaCuS6m0Cm9zRdY1ovX55XQ4YdMuYRZ1LBtr+kLOyoQQAZReJyRl6NbvTIsx5jLbneNTMONbNxM2HDhqLDSZSJXkFsdzkhBtQ1LaJwlC3NQibwuRX2885TxwNUcahDuih9snPGReruacx4ZwMX16sumb3zU7jfRahxYgcF2cRIKmAoTDSnn5BqQkBaYrCzrMUnCfWaKMcadzWOXqgwZrKMCpDvdZa19eej12UQmvNjnAgMBAAECggEACg1jrEYCHSSbxuTxpJGM9K9JP7OmLpYGbd5OFBQXydNSdqkrR9YK5ZbwkS4LBqLredutu2eXUbwc1ZTmCkTPl3h7u91VyI0pQhGXKDvcu99vvQMq3bv9cuoh9TmGmptYy9YmG1hB9UrnooAJsxJ/DrgpxTDXIBj/Gbcff8ZAfJOzFaWWeWmPbUDr2VAgcXV5zoVydj2CfYRPzng3kZukz8S9bJtRNX4i7n51hG8BlcpOaBaD1smvgJFywxkTiJlwFMEHBNTx1/ToSX52QJ3BnU+rUxfOQl3k2X3sTDs0YjmApwcVcSf584sSQeqGLGJ0/TpkmMnyV3htv8TsankgiQKBgQDOSoBNdq33UUPKP5bxMOlIAGokkQMTW7DTI7qhIowh9cc951l/YqKgJinN+qcUzqFoLLn6JxZVXTyZNKs2ZnLf0qCBy7+AUjSjArYTXQqb/BcW4hs1QZG9pPAzdyH7N9JuQMtAqKV9HMYpwtefmhqiiMWIRyKhaEWb2ZYlKSWJiQKBgQC+FAGHGwLdE+Q/FCh2v/Tsgw9GdFIr7WQQd/JIcX6q+PjJm7kV4V0XQV41sEezX70KXPdzl16mfktxxj6rTEvXlEtp/SfeFuE6LDxv8zlLumvXmBvXrVqR26g1bPUAU8586QUVn7TNPNpiPW9P88HABeov9FF3jO4LBRfDCfNi7wKBgGiPdozM9MyAkj23EYja48MtAp/aKJbtSKkcWQJHgoPMEdscok5g7lECRvoya/Gt8j3dPb6/hSBri8WT3pxKPTuZhOWFImGmSSu+ug8Cf9gkZIeiv2u0+mwHaACOB9lPqAdeLCdv08Ggjgioy6YH9Cwh6w1yEOmC8pVWKjZXrsERAoGBAIegqNJpoKp1JhkoXhMVt0MH5V9lYri7Y/ooTDYK3dJLYuIgfnmxXAZa+0kd5puERdReL6dILB5q4ZRmW5NJFpjV1NXk8IyVENK8e8d56rkxZP/qJnvH02deL/EnNM6t/hm8/4bFdXI46K7OnV2UVfyZe9gJ4hOG+NfeI21k7Uj7AoGAM/5Gmz8ev8qupPPl+EwdU/4A/g/kPoQXdDHNY2pJOAF1kuLK3Lhmmq7WCrMiFC0/AFPBvgaQNVUdwHTN/ulxPDENr5m++TBBwSmfhkd6UiTUzOc/H+G7nQdo7eP1K+rTe9xwqOwjgXZNp9kzB9Ocxjq0t346KAtaZa4bQzEonvQ=";
        _reneAPICreds.GameID = "2d9bcf87-1bc8-41df-b6b1-db40cf1ceab4";
        API ReneAPI = API.Init(_reneAPICreds.APIKey, _reneAPICreds.PrivateKey, _reneAPICreds.GameID);
        EmailHandler = Email.GetComponent<TMP_InputField>().text;
        bool connected = await ReneAPI.Game().Connect(EmailHandler);
        Debug.Log(connected);
        if (!connected) return;
        StartCoroutine(ConnectReneService(ReneAPI));
    }

    private IEnumerator ConnectReneService(API reneApi)
    {
        AlternateSignIn.SetActive(true);
        CountdownPanel.SetActive(true);
        var counter = 30;
        var userConnected = false;
        //Interval how often the code checks that user accepted to log in
        var secondsToDecrement = 1;
        while (counter >= 0 && !userConnected)
        {
            Timer.text = counter.ToString();
            AlternateTimer.text = counter.ToString();
            if (reneApi.IsAuthorized())
            {

                CountdownPanel.SetActive(false);
                AlternateSignIn.SetActive(false);
                SignInPanel.SetActive(false);

                yield return GetUserAssetsAsync(reneApi);
                userConnected = true;
                LoginStatus = true;
            }

            yield return new WaitForSeconds(secondsToDecrement);
            counter -= secondsToDecrement;
        }
        CountdownPanel.SetActive(false);
        AlternateSignIn.SetActive(false);
    }

    private async Task GetUserAssetsAsync(API reneApi)
    {
        AssetsResponse.AssetsData userAssets = await reneApi.Game().Assets();
        //By this way you could check in the Unity console your NFT assets
        userAssets?.Items.ForEach(asset =>
        {
            Debug.Log(asset.Metadata.Name);
            SkinStats[asset.Metadata.Name.ToString()] = true;
        });
    }

}
