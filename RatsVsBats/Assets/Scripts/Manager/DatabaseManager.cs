using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Data;
using System.Collections;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.IO;

public class DatabaseManager : MonoBehaviour
{
    public MySqlConnection dbConnection;

    string host = "rats-vs-bats-db.ctusuewsqph4.us-east-1.rds.amazonaws.com";
    string user = "developer";
    string password = "adminVoliac13";
    string database = "sys";
    string port = "3306";

    public static DatabaseManager instance;


    void Start()
    {
        instance = this;
        //OpenSql();
    }

    public void OpenSql()
    {
        string connectionString = "Server=" + host + ";Port=" + port + ";Database=" + database + ";Uid=" + user + ";Pwd=" + password;
        Debug.Log(connectionString);

        ConnectToDatabase(connectionString);
    }

    private void ConnectToDatabase(string conString)
    {
        try
        {
            dbConnection = new MySqlConnection(conString);
            dbConnection.Open();
            Debug.Log(dbConnection.State.ToString());
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            Debug.Log(dbConnection.State.ToString());
            Debug.LogError("Error: " + e.Message);
        }
    }

    IEnumerator CloseCon(MySqlConnection dbConnection2)
    {
        yield return new WaitForSeconds(3f);
        dbConnection2.Close();
    }

    public DataSet CreateTable(string name, string[] col, string[] colType)
    {
        if (col.Length != colType.Length)
        {
            throw new Exception("columns.Length != colType.Length");
        }
        string query = "CREATE TABLE " + name + " (" + col[0] + " " + colType[0];
        for (int i = 1; i < col.Length; ++i)
        {
            query += ", " + col + " " + colType;
        }
        query += ")";
        return ExecuteQuery(query);
    }

    public DataSet CreateTableAutoID(string name, string[] col, string[] colType)
    {
        if (col.Length != colType.Length)
        {
            throw new Exception("columns.Length != colType.Length");
        }
        string query = "CREATE TABLE " + name + "(" + col[0] + " " + colType[0] + " NOT NULL AUTO INCREMENT";
        for (int i = 1; i < col.Length; ++i)
        {
            query += ", " + col + " " + colType;
        }
        query += ", PRIMARY KEY(" + col[0] + ")" + ")";
        Debug.Log(query);
        return ExecuteQuery(query);
    }

    public DataSet InsertInto(string tableName, string[] values)
    {
        string query = "INSERT INTO " + tableName + " VALUES(" + "'" + values[0] + "'";
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + "'" + values + "'";
        }
        query += ")";
        Debug.Log(query);
        return ExecuteQuery(query);
    }

    public DataSet InsertInto(string tableName, string[] col, string[] values)
    {
        if (col.Length != values.Length)
        {
            throw new Exception("columns.Length != colType.Length");
        }
        string query = "INSERT INTO " + tableName + "(" + col[0];
        for (int i = 1; i < col.Length; ++i)
        {
            query += ", " + col;
        }
        query += ") VALUES(" + "'" + values[0] + "'";
        for (int i = 1; i < values.Length; ++i)
        {
            query += ", " + "'" + values + "'";
        }
        query += ")";
        Debug.Log(query);
        return ExecuteQuery(query);
    }

    public DataSet SelectWhere(string tableName, string[] items, string[] col, string[] operation, string[] values)
    {
        if (col.Length != operation.Length || operation.Length != values.Length)
        {
            throw new Exception("col.Length != operation.Length != values.Length");
        }
        string query = "SELECT " + items[0];
        for (int i = 1; i < items.Length; ++i)
        {
            query += ", " + items;
        }
        query += " FROM " + tableName + " WHERE " + col[0] + operation[0] + "'" + values[0] + "’ ";
        for (int i = 1; i < col.Length; ++i)
        {
            query += " AND " + col + operation + "'" + values[0] + "’ ";
        }
        return ExecuteQuery(query);
    }

    public DataSet UpdateInto(string tableName, string[] cols, string[] colsvalues, string selectkey, string selectvalue)
    {
        string query = "UPDATE " + tableName + " SET " + cols[0] + " = " + colsvalues[0];
        for (int i = 1; i < colsvalues.Length; ++i)
        {
            query += ", " + cols + " =" + colsvalues;
        }
        query += " WHERE " + selectkey + " = " + selectvalue + " ";
        return ExecuteQuery(query);
    }

    public DataSet Delete(string tableName, string[] cols, string[] colsvalues)
    {
        string query = "DELETE FROM " + tableName + " WHERE " + cols[0] + " = " + colsvalues[0];
        for (int i = 1; i < colsvalues.Length; ++i)
        {
            query += " or " + cols + " = " + colsvalues;
        }
        Debug.Log(query);
        return ExecuteQuery(query);
    }

    public void Close()
    {
        if (dbConnection != null)
        {
            dbConnection.Close();
            dbConnection.Dispose();
            dbConnection = null;
        }
    }

    public DataSet ExecuteQuery(string sqlString)
    {
        if (dbConnection.State == ConnectionState.Open)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(sqlString, dbConnection);
                da.Fill(ds);
            }
            catch (Exception ee)
            {
                throw new Exception("SQL:" + sqlString + "/ n" + ee.Message.ToString());
            }
            finally { }
            return ds;
        }
        return null;
    }
}
