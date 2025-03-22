using System;
using System.IO;
using System.Linq;
using System.Reflection;
using ModdingAPI.RPGModdingAPI;
using UnityEngine;

public class ModLoader : MonoBehaviour
{
    private void Start()
    {
        //LoadMods(@"C:\RPGPrototypeMods");
    }

    private static void LoadMods(string modFolderPath)
    {
        Debug.Log("Loading assets...");
        
        foreach (string file in Directory.GetFiles(modFolderPath, "*.dll"))
        {
            Assembly modAssembly = Assembly.LoadFile(file);
            Debug.Log($"Found assembly:{modAssembly.FullName}");
            
            var actionTypes = modAssembly.GetTypes()
                .Where(t => typeof(DebugMessage).IsAssignableFrom(t) && !t.IsAbstract);

            foreach (Type actionType in actionTypes)
            {
                Debug.Log(actionType.FullName);
                if (Activator.CreateInstance(actionType) is DebugMessage newObject)
                {
                    Debug.Log(newObject.Message);
                }
            }
        }
    }
}