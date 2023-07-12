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

    public static IEnumerator DownloadImage(string url, Action<Texture2D> response)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
            response?.Invoke(null);
        }
        else
        {
            Texture2D texture2D = ((DownloadHandlerTexture)request.downloadHandler).texture;
            response?.Invoke(texture2D);
        }
    }
}

public class Response
{
    public bool isSuccess;
    public string result;
}