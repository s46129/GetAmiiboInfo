using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetAmiiboInfo : MonoBehaviour
{
    private string HostURL = "https://www.amiiboapi.com/api/amiibo/";

    [SerializeField] TMP_InputField inputField;


    public void Search()
    {
        StartCoroutine(APIManager.Get(HostURL + $"/?name={inputField.text}", (response) =>
        {
            if (response.isSuccess)
            {
                Debug.Log(response.result);
            }
            else
            {
                Debug.Log(response.result);
            }
        }));
    }
}