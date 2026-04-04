using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hinagata.Commons;

//ジェネリッククラス。where 以下は TKey が null を許容しないということ。
public class JsonStorage<TKey, TValue> where TKey : notnull
{
    private readonly string _filePath;
    //デフォルトの json を指定する場合使用
    private readonly string _defaultJSONString;
    //保存時のオプション
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        WriteIndented = true, // これで改行とインデントが入る
        Converters = { new JsonStringEnumConverter() } //これで Enum が文字列に変換して保存されるように
    };

    public JsonStorage(string fileName, string defaultJSONString = "{}")
    {
        //実行ファイル + fileName で json ファイルのパスを生成
        _filePath = Path.Combine(AppContext.BaseDirectory, fileName);
        _defaultJSONString = defaultJSONString;
    }

    public void SaveJson(Dictionary<TKey,TValue> data)
    {
        string json = JsonSerializer.Serialize(data, _options);
        File.WriteAllText(_filePath, json);
    }

    public Dictionary<TKey, TValue> LoadJson()
    {
        //ファイルが無い場合にはデフォルト Json のディクショナリを返す
        if (!File.Exists(_filePath))
        {
            return LoadDefaultJson();
        }

        try
        {
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<Dictionary<TKey, TValue>>(json) ?? LoadDefaultJson();
        }
        catch
        {
            //ファイルが壊れている場合等、一旦仮
            return LoadDefaultJson();
        }
    }

    
    private Dictionary<TKey,TValue> LoadDefaultJson()
    {
        string defaultJson = _defaultJSONString;
        return JsonSerializer.Deserialize<Dictionary<TKey, TValue>>(defaultJson) ?? new Dictionary<TKey, TValue>(); ;
    }
}
