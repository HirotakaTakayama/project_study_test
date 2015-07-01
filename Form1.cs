using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        //constractor
        public Form1()
        {
            InitializeComponent();

        }
        
        

        //Form1 design load
        private void Form1_Load(object sender, System.EventArgs e)
        {
        }


        //connect to database
        private void button1_Click(object sender, EventArgs e)
        {
            string dbConnectionString = "Data Source=test.db";
            using (SQLiteConnection _db_connect = new SQLiteConnection(dbConnectionString))
            {
                try
                {
                    _db_connect.Open();
                    MessageBox.Show("接続できました。", "タイトル", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "タイトル", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        //add data
        private void button2_Click(object sender, EventArgs e)
        {
            string dbConnectionString = "Data Source=test.db";
            using (SQLiteConnection _db_connect = new SQLiteConnection(dbConnectionString))
            {
                _db_connect.Open();
                using (SQLiteTransaction trans = _db_connect.BeginTransaction())
                {
                    SQLiteCommand cmd = _db_connect.CreateCommand();

                    // インサート文
                    cmd.CommandText = "INSERT INTO Test (Name, Age) VALUES (@Name, @Age)";

                    // パラメータのセット
                    cmd.Parameters.Add("Name", System.Data.DbType.String);
                    cmd.Parameters.Add("Age", System.Data.DbType.Int64);

                    // recordの追加
                    cmd.Parameters["Name"].Value = "田中";
                    cmd.Parameters["Age"].Value = 25;
                    cmd.ExecuteNonQuery();

                    cmd.Parameters["Name"].Value = "高橋";
                    cmd.Parameters["Age"].Value = 30;
                    cmd.ExecuteNonQuery();

                    // コミット
                    trans.Commit();

                    _db_connect.Close();
                    MessageBox.Show("レコードを追加しました。", "タイトル", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        //get data and close db
        private void button3_Click(object sender, EventArgs e)
        {
            string dbConnectionString = "Data Source=test.db";
            using (SQLiteConnection _db_connect = new SQLiteConnection(dbConnectionString))
            {
                _db_connect.Open();
                SQLiteCommand cmd = _db_connect.CreateCommand();
                cmd.CommandText = "SELECT * FROM Test";
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Debug.WriteLine("名前:" + reader["Name"].ToString());
                        Debug.WriteLine("年齢:" + reader["Age"].ToString());
                    }
                }
                _db_connect.Close();
                MessageBox.Show("データを取得しました。", "タイトル", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        //create table
        private void button1_Click_1(object sender, EventArgs e)
        {
            string dbConnectionString = "Data Source=test.db";
            using (SQLiteConnection _db_connect = new SQLiteConnection(dbConnectionString))
            {
                _db_connect.Open();
                SQLiteCommand cmd = _db_connect.CreateCommand();
                cmd.CommandText = "CREATE TABLE Test (Id integer primary key AUTOINCREMENT, Name TEXT, Age INTEGER)";
                cmd.ExecuteNonQuery();

                _db_connect.Close();
                MessageBox.Show("テーブルの生成ができました。", "タイトル", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        } 



    } //end class

}
