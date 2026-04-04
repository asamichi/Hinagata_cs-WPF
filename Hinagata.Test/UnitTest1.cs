namespace Hinagata.Test;

using Hinagata.Commons;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Security.Cryptography.X509Certificates;

public class JsonStorageTests
{
    [Fact]
    public void SaveJson()
    {
        //準備
        string filename = "Test.json";
        if (File.Exists(filename)) {
            File.Delete(filename);
        }
        JsonStorage<string, string> storage = new JsonStorage<string, string>(filename);
        Dictionary<string, string> data = new Dictionary<string, string>();
        data.Add("host", "localhost");
        data.Add("port", "3306");
        data.Add("user", "admin");
        data.Add("password", "pass");

        //実行
        storage.SaveJson(data);

        //検証
        JsonStorage<string, string> storage2 = new JsonStorage<string, string>(filename);
        Dictionary<string, string> data2 = storage2.LoadJson();
        //data["host"] = "192.168.7.7";
        Assert.Equal(data, data2);
        if (File.Exists(filename))
        {
            //テスト終了時に実体を見たいのでスキップ
            //File.Delete(filename); 
        }
    }

    public static IEnumerable<object[]> GetTestData()
    {
        List<object[]> data = new List<object[]>();

        AddObjectToList(data, new Dictionary<string, string>()
        {
            {"host","lcalhost"},
            { "port","3306"}
        });

        AddObjectToList(data, new Dictionary<int, int>()
        {
            {1,1 },
            {-1,-1 }
        });

        AddObjectToList(data, new Dictionary<string, Dictionary<string, string>>()
        {
            {"user1",new Dictionary<string, string>(){{"type","Attack"}} }
        });


        foreach (var i in data)
        {
            yield return i;
        }
    }

    //記述簡略化のためのメソッド
    private static void AddObjectToList(List<object[]> list,object data)
    {
        list.Add(new object[] { data });
    }



    [Theory]
    //[InlineData(new Dictionary<int, int>()
    //{
    //    {1,1 },
    //    {-1,-1 }
    //})]
    //MemberData(nameof(メソッド)),3] のようにすると引数を渡せる
    [MemberData(nameof(GetTestData))]
    public void TestSaveJson<TKey,Tvalue>(Dictionary<TKey,Tvalue> data) where TKey: notnull
    {
        string filename = "Test2.json";
        if (File.Exists(filename))
        {
            File.Delete(filename);
        }
        var storage = new JsonStorage<TKey, Tvalue>(filename);

        //テストデータの保存
        storage.SaveJson(data);

        //検証
        var storage2 = new JsonStorage<TKey, Tvalue>(filename);
        var data2 = storage2.LoadJson();

        Assert.Equal(data, data2);
        
    }
}
