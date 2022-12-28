using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public interface ILoader<Key,Value>
{
    Dictionary<Key, Value> MakeDic();
}
public class PlayerData 
{
    [XmlAttribute]
    public int job;
    [XmlAttribute]
    public string name;
    [XmlAttribute]
    public int hp;
    [XmlAttribute]
    public int mp;
}

[Serializable, XmlRoot("ArrayOfPlayerData")]
public class PlayerDataLoader : ILoader<int,PlayerData>
{
    public List<PlayerData> _playerData = new List<PlayerData>();

    public Dictionary<int,PlayerData> MakeDic()
    {
        Dictionary<int, PlayerData> dic = new Dictionary<int, PlayerData>();

        foreach(PlayerData data in _playerData)
        {
            dic.Add(data.job, data);
        }

        return dic;
    }
}
