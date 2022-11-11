
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DBManager : MonoBehaviour
{
    IDbConnection dbConnection;

    public GameObject ingredPrefab;

    [SerializeField]
    public Canvas canvas;


    private void Awake()
    {

    }

    void Start()
    {

        OpenDatabase();

        GetAllIngredients();
    }


    void OpenDatabase()
    {
        Debug.Log("Abriendo DB");
        string dbUri = "URI=file:alchENTIemist.db";
        dbConnection = new SqliteConnection(dbUri);
        dbConnection.Open();
        Debug.Log("DB abierta");
    }

    void GetAllIngredients()
    {
        string query = "SELECT * FROM ingredients";

        IDbCommand cmd = dbConnection.CreateCommand();
        cmd.CommandText = query;

        IDataReader dataReader = cmd.ExecuteReader();

        int i = 0;
        while (dataReader.Read())
        {
            string typeIngredient = dataReader.GetString(1);
            var newIngredientUI = Instantiate(ingredPrefab, new Vector3(0, i / 1.7f, 0), Quaternion.identity);
            newIngredientUI.name = typeIngredient;
            newIngredientUI.transform.parent = canvas.transform;
            Debug.Log(typeIngredient);
            i++;
        }
    }
    

}
