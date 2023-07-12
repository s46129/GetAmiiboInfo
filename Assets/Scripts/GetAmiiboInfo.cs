using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class GetAmiiboInfo : MonoBehaviour
{
    private string HostURL = "https://www.amiiboapi.com/api/amiibo/";

    [SerializeField] TMP_InputField inputField;


    public void Search()
    {
        StartCoroutine(APIManager.Get(HostURL + $"/?name={inputField.text}", (response) =>
        {
            if (response.isSuccess)
            {
                AmiiboInfoList amiiboInfos = JsonConvert.DeserializeObject<AmiiboInfoList>(response.result);
                Debug.Log(amiiboInfos.amiibo[0].name);
            }
            else
            {
                Debug.Log(response.result);
            }
        }));
    }
}