using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using UnityEngine.Networking;
using System;

public class APIHelper : Singleton<APIHelper>
{
    public static Root GetLoginResult()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://saola.vegto.me/signin");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        request.Method = "POST";
        request.ContentType = "application/json";
        // request.ContentLength = DATA.Length;
        StreamWriter requestWriter = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<Root>(json);

    }

    public void SendJson(string url, string json, Action action)
    {
        StartCoroutine(PostRequestCoroutine(url, json, action));
    }

    private IEnumerator PostRequestCoroutine(string url, string json, Action action)
    {
        var jsonBinary = System.Text.Encoding.UTF8.GetBytes(json);

        DownloadHandlerBuffer downloadHandlerBuffer = new DownloadHandlerBuffer();

        UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(jsonBinary);
        uploadHandlerRaw.contentType = "application/json";

        UnityWebRequest www =
            new UnityWebRequest(url, "POST", downloadHandlerBuffer, uploadHandlerRaw);

        yield return www.SendWebRequest();

        if (www.isNetworkError)
            Debug.LogError(string.Format("{0}: {1}", www.url, www.error));
        else
        {
            Debug.Log(string.Format("Response: {0}", www.downloadHandler.text));
            action?.Invoke();
        }
    }
}
