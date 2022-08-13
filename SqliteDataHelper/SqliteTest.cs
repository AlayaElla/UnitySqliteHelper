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
        
        //�������ݿ�
        SqliteDataHelper.Instance.SQLiteConnectionInStreamingAssets("PlayersData");
        //����Ƿ����
        print(SqliteDataHelper.Instance.IsTableExist("Player"));
        //�������ݿ�
        SqliteDataHelper.Instance.connection.CreateTable<Player>();
        //�������
        Player player = new Player()
        { id = 1, name = "alaya", headImg = "img" };
        SqliteDataHelper.Instance.connection.InsertOrReplace(player);
        //�����������
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
            print("ֻ�����һ��");
        }

        //��ѯ������
        string sql = $"select * from {nameof(Player)}";
        var datas = SqliteDataHelper.Instance.connection.Query(new TableMapping(typeof(Player)), sql);
        //���ֶβ�ѯ
        Player test1 =  SqliteDataHelper.Instance.connection.Find<Player>(player => player.name.Contains("alaya"));
        //��key��ѯ
        Player test2 = SqliteDataHelper.Instance.connection.Get<Player>(1);

        //ɾ��������������
        //SqliteDataHelper.Instance.connection.DeleteAll<Player>();
        //��keyɾ���ֶ�
        //SqliteDataHelper.Instance.connection.Delete<Player>(200);

        //�����ֶ�
        player.name = "wahahaha";
        SqliteDataHelper.Instance.connection.Update(player);
    }
}
