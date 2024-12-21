using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Request
{
    public class Request : MonoBehaviour
    {
        private const string BearerToken = "kPERnYcWAY46xaSy8CEzanosAgsWM84Nx7SKM4QBSqPq6c7StWfGxzhxPfDh8MaP";
        
        public static async Task<UnityWebRequest> SendRequestAsync(string url, string jsonBody, string methodType)
        {
            using UnityWebRequest request = new UnityWebRequest(url, methodType)
            {
                uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonBody)),
                downloadHandler = new DownloadHandlerBuffer()
            };

            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", $"Bearer {BearerToken}");

            Debug.Log($"<color=yellow>Creating {methodType} request</color> to URL: <color=orange>{url}</color>");
            if (!string.IsNullOrEmpty(jsonBody))
            {
                Debug.Log($"Request Body:\n{jsonBody}");
            }

            var operation = request.SendWebRequest();

            while (!operation.isDone)
                await Task.Yield(); 

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"<color=red>Error:</color> {request.error} for URL: <color=orange>{url}</color>");
                throw new Exception(request.error);
            }

            Debug.Log($"<color=green>Response received</color> from URL: <color=orange>{url}</color>, Response Body:\n{request.downloadHandler.text}");
            return request;
        }
    }
}