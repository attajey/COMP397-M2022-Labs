using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

[System.Serializable]
class PlayerData
{
    public string position;
    public string rotation;

}


[System.Serializable]
public class GameSaveManager
{
    /*********************** SINGLETON PATTERN *************************/

    // Step 1 -- Declare a private static instance of the class
    private static GameSaveManager m_instance = null;

    // Step 2 -- Declare your constructor and destructor funtions private 
    private GameSaveManager() { }

    // Step 3 -- Create an Instance method/property  ( used as the "gateway" to the class itself )
    public static GameSaveManager Instance()
    {
        return m_instance ??= new GameSaveManager();

        /*if (m_instance == null)
        {
            m_instance = new GameSaveManager();
        }

        return m_instance;*/
    }

    /*********************** SINGLETON PATTERN *************************/

    // Serialize Data ( Encodes the data )
    public void SaveGame(Transform playerTransform)
    {

        // Step 1 - Create a Binary formatter object and setup the data path for the file
        //BinaryFormatter bf = new BinaryFormatter();
        //FileStream file = File.Create(Application.persistentDataPath + "/MySavedData.dat");
        // Step 2 - Create a player data object
        //PlayerData data = new PlayerData
        //{
            // Step 3 - Save the data to the PlayerData object
        //    position = JsonUtility.ToJson(playerTransform.position),
        //    rotation = JsonUtility.ToJson(playerTransform.rotation.eulerAngles)
        //};
        //Step 4 - Serialize
        //bf.Serialize(file, data);
        //file.Close();



        // Debug.Log(JsonUtility.ToJson(playerTransform.position));
        PlayerPrefs.SetString("playerPosition", JsonUtility.ToJson(playerTransform.position));
        PlayerPrefs.SetString("playerRotation", JsonUtility.ToJson(playerTransform.rotation.eulerAngles));

        PlayerPrefs.Save();

        Debug.Log("Game Saved");

    }

    // Deserializes Data ( Decodes the data )
    public void LoadGame(Transform playerTransform)
    {
        //if (File.Exists(Application.persistentDataPath + "/MySavedData.dat"))
        if (PlayerPrefs.HasKey("playerPosition") && PlayerPrefs.HasKey("playerRotation"))
        {
            // Step 1 - Create a Binary formatter object and setup the data path for the file
            //BinaryFormatter bf = new BinaryFormatter();
            //FileStream file = File.Open(Application.persistentDataPath + "/MySavedData.dat", FileMode.Open);

            // Step 2 - Create a playerData object and deserialized the data from the file
            //PlayerData data = (PlayerData)bf.Deserialize(file);
            //file.Close();

            // Step 3 - Adjust the Player Transform accordingly i.e. bring the data back into the game



            playerTransform.gameObject.GetComponent<CharacterController>().enabled = false;

            playerTransform.position = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("playerPosition")); //data.position);//
            playerTransform.rotation = Quaternion.Euler(JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString("playerRotation")));// data.rotation));//

            playerTransform.gameObject.GetComponent<CharacterController>().enabled = true;
            
            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No Game Data Found !! ");
        }
    }

    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + "/MySavedData.dat"))
        {
            File.Delete(Application.persistentDataPath + "/MySavedData.dat");
            Debug.Log("Game Data Cleared !");
        }
        else
        {
            Debug.Log("No Saved Data to Delete !! ");
        }
        //PlayerPrefs.DeleteAll();
    }
}
