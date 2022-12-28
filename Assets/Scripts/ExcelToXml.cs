using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public static class ExcelToXml
{
    [MenuItem("Tools/ParseXml")]
    public static void ParseXml()
    {        
        PlayerDataParse();
    }
    public static void PlayerDataParse()
    {
        
        if (File.Exists($"{Application.dataPath}/Resources/Data/Excel/PlayerData.csv") == false)
        {
            Debug.Log("Failed ParseXml :( ");
            return;
        }
            

        List<PlayerData> playerData = new List<PlayerData>();

        string[] cols = Resources.Load<TextAsset>($"Data/Excel/PlayerData").text.Replace("\r","").Split("\n");
        
        for(int i= 1; i< cols.Length; i++)
        {
            string[] rows = cols[i].Split(",");

            if (rows.Length == 0)
                continue;
            if (string.IsNullOrEmpty(rows[0]))
                continue;

            playerData.Add(new PlayerData()
            {
                job = int.Parse(rows[0]),
                name = rows[1],
                hp = int.Parse(rows[2]),
                mp = int.Parse(rows[3])
            }                
            );
        }

        string xmlString = ToXml(playerData);
        File.WriteAllText($"{Application.dataPath}/Resources/Data/PlayerData.xml",xmlString);
        AssetDatabase.Refresh();
        Debug.Log("Successed ParseXml!!");
    }
    
    public class EncodingUTF8StringWriter :StringWriter
    {
        public EncodingUTF8StringWriter(StringBuilder sb) : base(sb)
        {

        }
        public override Encoding Encoding => Encoding.UTF8;
    }

    public static string ToXml<T>(T obj)
    {
        using (EncodingUTF8StringWriter sw = new EncodingUTF8StringWriter(new StringBuilder()))
        {            
            XmlSerializer xs = new XmlSerializer(typeof(T));
            xs.Serialize(sw, obj);
            return sw.ToString();
        }

    }
}
