namespace rhwebapi.Models ;
using Npgsql;
public class Connection{
    public Connection(){}
    //------------------------------------------------------------------------------------------------------POSTGRESQL
    public NpgsqlConnection GetConnection()
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=motmirado;Database=bdrh";
        NpgsqlConnection connection = new NpgsqlConnection(connectionString);
        connection.Open();
        return connection;
    }
    //------------------------------------------------------------------------------------------------------POSTGRES
    public List<Dictionary<string, object>>? ExecuteSelectQuery(NpgsqlConnection connection, string query){
        Console.WriteLine(query);
        bool isOpen=false;
        if(connection==null){ connection=new Connection().GetConnection(); isOpen=true; }
        var result = new List<Dictionary<string, object>>();
        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
        {
            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    row[reader.GetName(i)] = reader.GetValue(i);
                }
                if (row.Count > 0){
                    result.Add(row);
                }
            }
            reader.Close();
        }
        if(isOpen==true){ connection.Close(); }
        if (result.Count == 0){
            return null;
        }
        return result;
    }

    public void ExecuteNotSelectQuery(NpgsqlConnection connection,string query){
        Console.WriteLine(query);
        bool isOpen=false;
        if(connection==null){ connection=new Connection().GetConnection(); isOpen=true; }
        NpgsqlCommand command = new NpgsqlCommand(query, connection);
        command.ExecuteNonQuery();
        if(isOpen==true){ connection.Close();
        }
    }

    public bool isExist(NpgsqlConnection connection, string nametable, string namecol, string value, bool isWithCote){
        string query="select * from "+nametable+" where "+namecol+"=";
        if(isWithCote==true){
            query=query+"\'"+value+"\'";
        }else{
            query=""+value;
        }
        List<Dictionary<string, object>>? result = this.ExecuteSelectQuery(connection, query);
        if(result==null){ return false; }
        else{ return true; }
    }
    public bool isExist(NpgsqlConnection connection, string nametable, string suitRqt){
        string query="select * from "+nametable;
        if(suitRqt!=null){
            query=query+" "+suitRqt;
        }
        List<Dictionary<string, object>>? result = this.ExecuteSelectQuery(connection, query);
        if(result==null){ return false; }
        else{ return true; }
    }

}

//dotnet add package Npgsql
