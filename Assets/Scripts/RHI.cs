using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RHI : MonoBehaviour
{
    public RawImage imageDisplay;
    public Button getImageButton;

    private const string url = "https://konachan.com/post.json?limit=1&tags=+uncensored+-pool:309+order:random+rating:explicit";

    private void Start()
    {
        getImageButton.onClick.AddListener(() => StartCoroutine(GetImage()));
    }

    private IEnumerator GetImage()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            string imageUrl = JsonHelper.FromJson<ImageResponse>(json)[0].file_url;

            UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(imageUrl);
            yield return imageRequest.SendWebRequest();

            if (imageRequest.isNetworkError || imageRequest.isHttpError)
            {
                Debug.LogError(imageRequest.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(imageRequest);
                imageDisplay.texture = texture;
                imageDisplay.gameObject.SetActive(true);
            }
        }
    }

    [Serializable]
    private class ImageResponse
    {
        public string file_url;
    }
}
