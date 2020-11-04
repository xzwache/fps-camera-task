using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.IO.Compression;
using UnityEngine.Networking;

public class ServerRequest : Editor
{
    private class Settings {
        public int speed;
        public int health;
        public string fullName;
        public string base64Texture;
    }

    [MenuItem("Tools/Download data")]
    public static void DownloadZip() {
        string url = "https://dminsky.com/settings.zip";
        string folderPath = "Assets/LoadableResources";

        if (Directory.Exists(folderPath) == false)
            Directory.CreateDirectory(folderPath);
        string archivePath = Path.Combine(folderPath, "archive.zip");
        
        var request = UnityWebRequest.Get(url);
        DownloadHandler handler = new DownloadHandlerFile(archivePath);
        request.downloadHandler = handler;

        var operation = request.SendWebRequest();
        operation.completed += asyncOperation => {
            if (request.isHttpError || request.isNetworkError) {
                Debug.Log("Cannot download file: some error occured.");
                return;
            }
            Debug.Log("Download file completely.");
            string exctractionDir = Path.Combine(folderPath, "unzipped");
            Unzip(archivePath, exctractionDir);
            string settingsFilePath = Path.Combine(exctractionDir, "settings.json");
            var playerSettings = ReadSettings(settingsFilePath);
            ApplySettings(playerSettings);
        };

    }

    private static void Unzip(string archivePath, string extractDirectory) {
        Debug.Log("Unzipping...");
        if (Directory.Exists(extractDirectory)) {
            Directory.Delete(extractDirectory, true);
        }
        ZipFile.ExtractToDirectory(archivePath, extractDirectory);
    }

    private static Settings ReadSettings(string settingsFilePath) {
        Debug.Log("Reading settings...");
        string settingsJson = File.ReadAllText(settingsFilePath);
        return JsonUtility.FromJson<Settings>(settingsJson);
    }

    private static void ApplySettings(Settings settings) {
        Debug.Log("Applying settings...");
        var player = FindObjectOfType<Player>();
        if (player == null) {
            Debug.Log("Cannot apply player settings: can't find the player in scene.");
            return;
        }

        player.Health = settings.health;
        player.Speed = settings.speed;
        player.FullName = settings.fullName;

        var floor = GameObject.FindGameObjectWithTag("Floor");
        if (floor == null) {
            Debug.Log("Cannot apply floor settings: can't find the floor in scene.");
            return;
        }
        
        Texture2D floorTexture = new Texture2D(1,2);
        var textureBytes = Convert.FromBase64String(settings.base64Texture);
        floorTexture.LoadImage(textureBytes);

        var floorMaterial = floor.GetComponent<MeshRenderer>().sharedMaterial;
        floorMaterial.SetTexture("_MainTex", floorTexture);
    }
}
