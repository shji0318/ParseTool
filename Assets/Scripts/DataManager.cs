using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public Dictionary<int, PlayerData> Player { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        Player = LoadXml<PlayerDataLoader, int, PlayerData>("PlayerData").MakeDic();
    }

    // Update is called once per frame
    public Loader LoadXml<Loader,Key,Value>(string name) where Loader : ILoader<Key,Value>
    {
        XmlSerializer xs = new XmlSerializer(typeof(Loader));
        TextAsset ta = Resources.Load<TextAsset>($"Data/{name}");
        using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(ta.text)))
            return (Loader)xs.Deserialize(ms);

    }
}
