using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    [SerializeField]private Transform jawModel;
    [SerializeField]private Transform teethMainTransform;
    private Transform[] _teeth;
    
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
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(SaveData);
    }

    void SaveData()
    {
        string path = Application.persistentDataPath + "/jawData.txt";
        StreamWriter writer = new StreamWriter(path, false);

        SaveTransform(writer, "Jaw", jawModel);
        foreach (Transform tooth in _teeth)
        {
            SaveTransform(writer, tooth.name, tooth);
        }

        writer.Close();
    }

    void SaveTransform(StreamWriter writer, string name, Transform transform)
    {
        string parentName = transform.parent ? transform.parent.name : "null";
        TransformData data = new TransformData(name, transform.position, transform.rotation, parentName);
        writer.WriteLine(JsonUtility.ToJson(data));
    }
}

[System.Serializable]
public struct TransformData
{
    public string name;
    public Vector3 position;
    public Quaternion rotation;
    public string parentName;

    public TransformData(string name, Vector3 pos, Quaternion rot, string parentName)
    {
        this.name = name;
        position = pos;
        rotation = rot;
        this.parentName = parentName;
    }
}