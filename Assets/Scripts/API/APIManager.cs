using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager
{
    public static IEnumerator Get(string uri, Action<Response> response)
    {
        using var request = UnityWebRequest.Get(uri);
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            var _response = new Response
            {
                isSuccess = false,
                result = request.error
            };
            response?.Invoke(_response);
            Debug.Log(request.error);
        }
        else
        {
            var _response = new Response
            {
                isSuccess = true,
                result = request.downloadHandler.text
            };
            response?.Invoke(_response);
            Debug.Log(request.downloadHandler.text);
        }
    }
}

public class Response
{
    public bool isSuccess;
    public string result;
}