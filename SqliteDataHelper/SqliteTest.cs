using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;


[Table(name: nameof(Player))]
public class Player
{
    [PrimaryKey]
    public int id { get; set; }
    public string name { get; set; }
    public string headImg { get; set; }
}

public class SqliteTest : MonoBehaviour
{
    SQLiteConnection database;
    // Start is called before the first frame update
    void Start()
    {
        //var datapath = string.Format(@$"{Directory.GetCurrentDirectory()}\", "PlayersData");
        //print(datapath);
        
        //链接数据库
        SqliteDataHelper.Instance.SQLiteConnectionInStreamingAssets("PlayersData");
        //检查是否存在
        print(SqliteDataHelper.Instance.IsTableExist("Player"));
        //创建数据库
        SqliteDataHelper.Instance.connection.CreateTable<Player>();
        //添加数据
        Player player = new Player()
        { id = 1, name = "alaya", headImg = "img" };
        SqliteDataHelper.Instance.connection.InsertOrReplace(player);
        //批量添加数据
        try
        {
            List<Player> players = new List<Player>();
            players.Add(new Player()
            { id = 100, name = "test1", headImg = "img1" });
            players.Add(new Player()
            { id = 200, name = "test2", headImg = "img3" });
            SqliteDataHelper.Instance.connection.InsertAll(players);
        }
        catch
        {
            print("只能添加一次");
        }

        //查询整个表
        string sql = $"select * from {nameof(Player)}";
        var datas = SqliteDataHelper.Instance.connection.Query(new TableMapping(typeof(Player)), sql);
        //按字段查询
        Player test1 =  SqliteDataHelper.Instance.connection.Find<Player>(player => player.name.Contains("alaya"));
        //按key查询
        Player test2 = SqliteDataHelper.Instance.connection.Get<Player>(1);

        //删除表中所有数据
        //SqliteDataHelper.Instance.connection.DeleteAll<Player>();
        //按key删除字段
        //SqliteDataHelper.Instance.connection.Delete<Player>(200);

        //更新字段
        player.name = "wahahaha";
        SqliteDataHelper.Instance.connection.Update(player);
    }
}
