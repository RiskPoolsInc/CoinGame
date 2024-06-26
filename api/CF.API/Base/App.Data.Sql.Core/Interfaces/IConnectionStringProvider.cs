namespace App.Data.Sql.Core.Interfaces; 

public interface IConnectionStringProvider {
    string GetConnection(string connectionKey);
}