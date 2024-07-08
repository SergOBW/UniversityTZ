using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LoadAfterStart : MonoBehaviour
{
    [SerializeField]private Transform jawModel;
    [SerializeField]private Transform teethMainTransform;
    private Transform[] _teeth;
    
    private Dictionary<string, Transform> _teethDict;
    
    private void Awake()
    {
        Transform[] allChildren = teethMainTransform.GetComponentsInChildren<Transform>();
        _teeth = allChildren.Where(t => t != teethMainTransform).ToArray();
        if (_teeth.Length <= 0)
        {
            Debug.LogError("There is no teeth found! Check please.");
        }
        
    }

    void Start()
    {
        _teethDict = new Dictionary<string, Transform>();
        foreach (Transform tooth in _teeth)
        {
            _teethDict[tooth.name] = tooth;
        }
        LoadData();
    }

    void LoadData()
    {
        string path = Application.persistentDataPath + "/jawData.txt";
        if (!File.Exists(path))
        {
            Debug.LogWarning("Save file not found!");
            return;
        }

        StreamReader reader = new StreamReader(path);
        string json;

        while ((json = reader.ReadLine()) != null)
        {
            TransformData data = JsonUtility.FromJson<TransformData>(json);
            if (data.name == "Jaw")
            {
                jawModel.position = data.position;
                jawModel.rotation = data.rotation;
            }
            else
            {
                if (_teethDict.ContainsKey(data.name))
                {
                    Transform tooth = _teethDict[data.name];
                    tooth.position = data.position;
                    tooth.rotation = data.rotation;
                    if (data.parentName == "null")
                    {
                        tooth.parent = null;
                    }
                    else
                    {
                        if (_teethDict.ContainsKey(data.parentName))
                        {
                            tooth.parent = _teethDict[data.parentName];
                        }
                        else if (data.parentName == "Jaw")
                        {
                            tooth.parent = jawModel;
                        }
                    }
                }
            }
        }

        reader.Close();
    }
}