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

    ReneAPICreds _reneAPICreds;
    API ReneAPI;
    NotificationManager NotificationManager;
    // Start is called before the first frame update
    void Start()
    {
        NotificationManager = NotificationManager.notificationManager;

        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            NotificationManager.Notify("WebGL version lacks login to reneverse, Kindly continue with Guest Mode");
        }

        _reneAPICreds = ScriptableObject.CreateInstance<ReneAPICreds>();
        SkinStats["Beetal"] = true;
        if (LoginStatus)
            SignInPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }

    public async void SignIn()
    {
        await ConnectUser();
    }

    public void SignUp()
    {
        Application.OpenURL("https://app.reneverse.io/register");
    }

    public async void MintCars(string CarName){
        await Mint(CarName);
    }

    public void GuestMode()
    {
        SignInPanel.SetActive(false);
    }

    async Task ConnectUser()
    {
        _reneAPICreds.APIKey = "0616b7fe-f0fa-43ac-a57d-9d826642834e";
        _reneAPICreds.PrivateKey = "MIIEvgIBADANBgkqhkiG9w0BAQEFAASCBKgwggSkAgEAAoIBAQDzSvN8kto5IVkbl9JM/ZiuvFqTuU6c/tVWBiO7YLnDwdeqpDVtTgjZo3X4G3wsj3AdVxKcAW24Budk/vTMWnfr9IFH5m0zxfH0vhHUVG3uPJ1A1jwUNlLE7FmsivvrqHlbQ/NIOZf1DDpdJaLBUZgjVVJ0vc6Iqm8hNkDdrqAccEfeq2RKBCCUYPi8xvXz4i1RMXHChRr73mIV4r4WJEzkebbz992SVxOgni44/aFGWffPcMLJoXXl6ICMccIJNRpZALbUGiHl10V1MnF9PFppyvLk6MLfA9p9ojiKF4wCeOxkSTa+Epw0bqLymhAOqL2hqcZG98pKaEowEuJoSQAdAgMBAAECggEAOw0vKkZupz075qGkDsHi5E6dYYux1BNabqXQ2HMyw5vyH935hc1SAplVUeJB8oLiQIzY3HrQScGLOo1Tl6JBx0iydGQuj0l1X+UeaL4RbKjTtmAJdxJ0Zo3DekjFur5KrmdAzoAELRtJs0AmT+vhFHpqKCHF1pAfpx0HA4eWHzB8ZaoFnP7aaqFopmebAftqTV7h1dlXkXjfvweMZMk122qzui2Qqxed9MTPcIIRVHTNPp/xWqCqCyrw4Ag0ehGxuvj5PrpCUqm+FeQ8C6C5KSAMYzeaE51Ql62mLucYsZQ9RO39/5h/bcpf9vuAa+hCHF2pkJrzKOnONx69LNvoYQKBgQD+Mc/J8mq5NlhOSuuX7NTdWfzYrdqDec+MDSM0snkuA4DxVnNB/CuFjqanDLZIN6FEtuMQXMwwSCbuqp3imum1iX3rAp6XEXu8naDsG8psllKrl4suVaJnPMQ4tgfJwLXvb5JV4EgTc622IXC7PmLya2+660/aYYbzAVvnL/vyaQKBgQD1BVE55K8PHl7f2JwtamQA8yWcj668vELc7HuyEYRr4FXKUQlqBiT4z/y08BjGmQsadTeUrNIbV1jdVEBeBKt5uLHuP0fdeJtyMDDAZdu/Wa/G4zZzvAluaMqXvhbg77fF+kdNkZzHpFHJeEkPr2ouVoe2gjEXEUaNAB/sLc6BlQKBgAcx477ElM6/QgqdRkPbmT7WsDh120yDYyOEr61rK9Domnq6RrLkb1rtabwquPIcWP036/9nkQQA1tFElQl39wuDY8QGI/UEsqrpD0f/lWAzdQ2UUYUzOVCQwMEWLexA/yVS1CKIIaIjURRpp+Y04toXvmbdCDqXLhmsvSwzCH+ZAoGBAKsm+rU5A/vImDc+5OFohtCPB//T8hhOXVpbKpCZYenE+8hmUPApuJvBFWICsRvQ/guOQ7PsAJwuqJl6Z7gFBQ7yr/+fXoDa5aKe/P74Z8bDTGDeiEPR3risJJBYrTyU1sdJa5NImr5uDt9v0YFOZBpYQVaAnO/jFmgZ5TKiULT9AoGBAMMI51dt77vLbKqHcTAL8JU6ChOdJhLLuAMIZDE7eYn2WFBuQXqA8Ay7rVUYjTW7goDCBQogvJEbPObZACJ+gRnB+9Fp7cvvNr5cgFppRK40aacGrRZF9HtCsxtzVQ5Olp9JunS2+Ic6AyGyqihdWhXL5SxjNceE6QQDPUzQYVt8";
        _reneAPICreds.GameID = "dc58103e-544b-466b-8f6c-5d27d87584b8";
        ReneAPI = API.Init(_reneAPICreds.APIKey, _reneAPICreds.PrivateKey, _reneAPICreds.GameID);
        EmailHandler = Email.GetComponent<TMP_InputField>().text;
        bool connected = await ReneAPI.Game().Connect(EmailHandler);
        Debug.Log(connected);
        if (!connected) return;
        StartCoroutine(ConnectReneService(ReneAPI));
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
                
                
                yield return GetUserAssetsAsync(reneApi);
                
              
                userConnected = true;
                LoginStatus = true;
                NotificationManager.Notify("Connected to Reneverse");
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
        userAssets?.Items.ForEach(asset =>
        {
            SkinStats[asset.Metadata.Name.ToString()] = true;
        });
    }

    public async Task Mint(string CarName)
    {
        if(LoginStatus == false)
        {
            NotificationManager.Notify("Currently in Guest Mode, Restart to Log In");
            return;
        }
        if (CarName == "Toyoyo" && !SkinStats.ContainsKey("Toyoyo") )
        {
            //Asset Template ID
            string assetTemplateId = "905861f9-1ce7-4061-98c6-a915e20cc167";

            //Color Attribute
            AssetMetadata.AssetAttribute Color = new()
            {
                displayType = "text",
                traitType = "Color",
                value = "Red"
            };

            //Type Attribute
            AssetMetadata.AssetAttribute Type = new()
            {
                displayType = "text",
                traitType = "Type",
                value = "Muscle"
            };

            // Adding Toyoyo's Metadata
            var assetMetadata = new AssetMetadata
            {
                name = "Toyoyo",
                description = "A Sturdy Muscle car with 2x Multiplier.",
                imageFilename = "ToyoyoRender",
                attributes = new List<AssetMetadata.AssetAttribute>()
                {
                    Color,
                    Type
                }, 
            };

            try
            {
                var response = await ReneAPI.Game().AssetMint(assetTemplateId);
                Debug.Log(response);
                SkinStats["Toyoyo"] = true;
                NotificationManager.Notify("Asset Minting in progress");
            }
            catch (Exception e)
            {
                Debug.Log(e);
                NotificationManager.Notify("There was a problem minting this asset");
            }
            
        }

        if (CarName == "Tristar" && !SkinStats.ContainsKey("Tristar"))
        {

            //Asset Template ID
            string assetTemplateId = "c002e713-b9fa-481d-bb6d-3d99a08b7ac0";

            //Color Attribute
            AssetMetadata.AssetAttribute Color = new()
            {
                displayType = "text",
                traitType = "Color",
                value = "Black"
            };

            //Type Attribute
            AssetMetadata.AssetAttribute Type = new()
            {
                displayType = "text",
                traitType = "Type",
                value = "Sports"
            };

            // Adding Tristar's Metadata
            var assetMetadata = new AssetMetadata()
            {
                name = "Tristar",
                description = "A Superfast sports car with 3x Multiplier.",
                imageFilename = "TristarRender",
                animationFilename = null,
                attributes = new List<AssetMetadata.AssetAttribute>()
                {
                    Color,
                    Type
                },
            };

            try
            {
                var Response = await ReneAPI.Game().AssetMint(assetTemplateId);
                Debug.Log(Response);
                SkinStats["Tristar"] = true;
                NotificationManager.Notify("Asset Minting in progress");
            }
            catch (Exception e)
            {
                Debug.Log(e);
                NotificationManager.Notify("There was a problem minting this asset");
            }
        }
    }

    //Car TemplateId : 099492f0-a5e6-4030-a165-c59cafcabdc2
    //Cube TemplateId : fab7b93f-239a-4651-a119-165d10df38ca
}
