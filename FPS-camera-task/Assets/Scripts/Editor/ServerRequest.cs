using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.IO.Compression;
using System.Security;
using UnityEngine.Networking;

public class ServerRequest : Editor
{
    private class Settings {
        public int speed = default;
        public int health = default;
        public string fullName = default;
        public string base64Texture = default;
    }
    
    
    [MenuItem("Tools/Download data")]
    public static void DownloadZip() {
        var url = "https://dminsky.com/settings.zip";
        var folderPath = Path.Combine(Application.persistentDataPath, "LoadableResources");

        if (Directory.Exists(folderPath) == false)
            Directory.CreateDirectory(folderPath);

        var archivePath = Path.Combine(folderPath, "archive.zip");
        
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
            
            var exctractionDir = Path.Combine(folderPath, "unzipped");

            try {
                Unzip(archivePath, exctractionDir);
            }
            catch (DirectoryNotFoundException e) {
                if (EditorUtility.DisplayDialog("Unzipping settings error.",
                    "Directory for unzipping does not exists. Create?.", "Create.", "Cancel")) {
                    
                    Directory.CreateDirectory(exctractionDir);
                    Unzip(archivePath, exctractionDir);
                }
            }
            catch (PathTooLongException e) {
                EditorUtility.DisplayDialog("Unzipping data error", "Please provide valid path for unzipping.", "Ok.");
                Debug.LogWarning(e.Message);
            }
            catch (Exception e) {
                EditorUtility.DisplayDialog("Unzipping data error", "Cannot unzip data from settings archive file.", "Ok.");
                Debug.LogWarning(e.Message);
            }
            
            try {
                var settingsFilePath = Path.Combine(exctractionDir, "settings.json");
                var playerSettings = ReadSettings(settingsFilePath);
                ApplySettings(playerSettings);
            }
            catch (Exception e) {
                EditorUtility.DisplayDialog("Applying settings error.", "Something went wrong during applying settings.", "Ok.");
                Debug.LogWarning(e.Message);
            }
        };
    }

    private static void Unzip(string archivePath, string extractDirectory) {
        try {
            Debug.Log("Unzipping...");
            ZipFile.ExtractToDirectory(archivePath, extractDirectory);
            Debug.Log("Unzipping completed.");
        }
        catch (DirectoryNotFoundException e) {
            Debug.LogWarning("Unzipping folder path does not exists.");
            throw;
        }
        catch (FileNotFoundException e) {
            Debug.LogWarning("The archive to unzip does not exist.");
            throw;
        }
        catch (PathTooLongException e) {
            Debug.LogWarning("Please provide valid path.");
            throw;
        }
        catch (IOException e) when (Directory.Exists(extractDirectory)) {
            Debug.LogWarning("One or more files from archive is already exists. Deleting...");
            if (Directory.Exists(extractDirectory)) {
                Directory.Delete(extractDirectory, true);
                
                ZipFile.ExtractToDirectory(archivePath, extractDirectory);
            }
        }
    }

    private static Settings ReadSettings(string settingsFilePath) {
        try {
            Debug.Log("Reading settings...");
            string settingsJson = File.ReadAllText(settingsFilePath);
            return JsonUtility.FromJson<Settings>(settingsJson);
        }
        catch (FileNotFoundException e) {
            Debug.LogWarning($"Cannot find settings file at path \"{settingsFilePath}\"");
            throw;
        }
        catch (SecurityException e) {
            Debug.LogWarning($"Cannot read file settings data at path \"{settingsFilePath}\". Please provide reading permission.");
            throw;
        }
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
