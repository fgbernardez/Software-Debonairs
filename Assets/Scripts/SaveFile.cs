using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using Mono.Data.Sqlite;
using TMPro;
using System; 

public class SaveFile : MonoBehaviour
{

    PlayerDB db;


    [SerializeField] public TMP_InputField inputName;
    public TMP_InputField saveFileName;

    [System.Serializable]

    public class Save
    {
        public string name;
        public int scene;
        public long playerID;
        public string fileName;
    }

    Save currentPlayer;

    public Save newSave = new Save();

    public void createSave()
        {
            //collect data from input
            Save newSave = new Save();
            newSave.name = inputName.text;
            newSave.scene = 1;
            db = GameObject.FindGameObjectWithTag("dbTag").GetComponent<PlayerDB>(); //dbTag is assigned to Database object in Unity
            db.CreateDB();
            db.addUser(newSave.name);
            newSave.playerID = db.getLatestPlayerID();
            newSave.fileName = newSave.playerID.ToString();

            string json = JsonUtility.ToJson(newSave, true);
            File.WriteAllText(Application.dataPath + "/Saves/" + newSave.fileName + ".json",json);
            saveFileName.text = newSave.fileName;
            
        }

        public void editSaveName()
        {
            db = GameObject.FindGameObjectWithTag("dbTag").GetComponent<PlayerDB>();
            string fileName = saveFileName.text;
            string directory = Application.dataPath + "/Saves/" + fileName + ".json";
            if (File.Exists(directory))
            {

                string json = File.ReadAllText(Application.dataPath + "/Saves/" + fileName + ".json");
                Save data = JsonUtility.FromJson<Save>(json);
                data.name = inputName.text;
                string jsonToSave = JsonUtility.ToJson(data, true);
                File.WriteAllText(Application.dataPath + "/Saves/" + fileName + ".json",jsonToSave);
                db.editName(long.Parse(fileName),data.name);
            }   
            else
            {
                Debug.Log("File does not exist");
                return;
            }
        }

        public void Delete()
        {
            db = GameObject.FindGameObjectWithTag("dbTag").GetComponent<PlayerDB>();
            string profileId = saveFileName.text;

            // base case - if the profileId is null, return right away
            if (profileId == null)
            {
                return;
            }

            string fullPath = (Application.dataPath + "/Saves/" + profileId + ".json");
            Debug.Log(fullPath);
            
            
            // ensure the data file exists at this path before deleting the directory
            if (File.Exists(fullPath))
            {
                // delete the profile folder and everything within it
                File.Delete(Application.dataPath + "/Saves/" + profileId + ".json");
                db.deleteUser(long.Parse(profileId)); 
            }
            else
            {
                Debug.LogWarning("Tried to delete profile data, but data was not found at path: " + fullPath);
            }
            
        } 

        public void loadSave ()
        {
            string filename = saveFileName.text;
            string json = File.ReadAllText(Application.dataPath + "/Saves/" + filename + ".json");
            Save data = JsonUtility.FromJson<Save>(json);
            inputName.text = data.name;
            currentPlayer = data;
            string jsonToPrint = JsonUtility.ToJson(newSave, true);
            Debug.Log(jsonToPrint);
        }
    
}
