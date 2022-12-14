using System;
using System.Collections;
using System.Collections.Generic;
using GFramework;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Http封装
/// </summary>
public class Http
{
    GLogger logger = new GLogger("Http");

    public void UnityGet(string url, Action<string> successCallback, Action failedCallback)
    {
        UnityWebRequest request = new UnityWebRequest(url, "GET");
        MonoLoop.Instance.StartCoroutine(UnityRequest(request, successCallback, failedCallback));
    }

    public void UnityPost(string url, Action<string> successCallback, Action failedCallback, byte[] data)
    {
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        MonoLoop.Instance.StartCoroutine(UnityRequest(request, successCallback, failedCallback, data));
    }

    public void DownloadFile(string url, byte[] fileName, Action<string, byte[]> successCallback, Action failedCallback)
    {
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        MonoLoop.Instance.StartCoroutine(Downloading(request, fileName, successCallback, failedCallback));
    }

    IEnumerator Downloading(UnityWebRequest request, byte[] data, Action<string, byte[]> successCallback, Action failedCallback)
    {
        request.uploadHandler = new UploadHandlerRaw(data);
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            logger.W("http请求失败");
            failedCallback.Invoke();
        }
        else
        {
            Dictionary<string, string> headers = request.GetResponseHeaders();
            string fileName = headers["fileName"];
            successCallback.Invoke(fileName, request.downloadHandler.data);
        }
    }

    IEnumerator UnityRequest(UnityWebRequest request, Action<string> successCallback, Action failedCallback, byte[] data = null)
    {
        request.uploadHandler = new UploadHandlerRaw(data);
        request.downloadHandler = new DownloadHandlerBuffer();
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            logger.W("http请求失败");
            failedCallback.Invoke();
        }
        else
        {
            successCallback.Invoke(request.downloadHandler.text);
        }
    }

    [Obsolete]
    public void UnityWWW(string url, Action<byte[]> callback)
    {
        MonoLoop.Instance.StartCoroutine(WWWRequest(url, (data) => callback.Invoke(data)));
    }

    [Obsolete]
    IEnumerator WWWRequest(string url, Action<byte[]> callback)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.isDone)
        {
            logger.P("下载完成");
            byte[] bytes = www.bytes;
            callback.Invoke(bytes);
        }
    }
}