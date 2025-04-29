using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameHandler : MonoBehaviour
{
    public GameObject player;
    public GameObject marine1;
    public GameObject marine2;
    public GameObject marine3;
    public GameObject marine4;
    public GameObject marine5;
    public GameObject marine6;
    public GameObject rock;
    public GameObject rock2;
    public GameObject rock3;

    private EnemyHealth marine1_healthscript;
    private EnemyHealth marine2_healthscript;
    private EnemyHealth marine3_healthscript;
    private EnemyHealth marine4_healthscript;
    private EnemyHealth marine5_healthscript;
    private EnemyHealth marine6_healthscript;
    private void Awake()
    {
        marine1_healthscript = marine1.GetComponent<EnemyHealth>();
        marine2_healthscript = marine2.GetComponent<EnemyHealth>();
        marine3_healthscript = marine3.GetComponent<EnemyHealth>();
        marine4_healthscript = marine4.GetComponent<EnemyHealth>();
        marine5_healthscript = marine5.GetComponent<EnemyHealth>();
        marine6_healthscript = marine6.GetComponent<EnemyHealth>();
        Load();
        SaveObject saveObject = new SaveObject
        {
            orbsAmount = 5,
        };
        string json = JsonUtility.ToJson(saveObject);
        Debug.Log(json);

        SaveObject loadedSaveObject = JsonUtility.FromJson<SaveObject>(json);
        Debug.Log(loadedSaveObject.orbsAmount);
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Save();
            Debug.Log("Save pressed");
        }
    }

    private void Save()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 marine1_position = marine1.transform.position;
        Vector3 marine2_position = marine2.transform.position;
        Vector3 marine3_position = marine3.transform.position;
        Vector3 marine4_position = marine4.transform.position;
        Vector3 marine5_position = marine5.transform.position;
        Vector3 marine6_position = marine6.transform.position;

        float marine1_health = marine1_healthscript.current_health;
        float marine2_health = marine2_healthscript.current_health;
        float marine3_health = marine3_healthscript.current_health;
        float marine4_health = marine4_healthscript.current_health;
        float marine5_health = marine5_healthscript.current_health;
        float marine6_health = marine6_healthscript.current_health;

        Vector3 rock_position = rock.transform.position;
        Vector3 rock2_position = rock2.transform.position;
        Vector3 rock3_position = rock3.transform.position;


        SaveObject saveObject = new SaveObject { 
            playerPosition = playerPosition,
            marine1_position = marine1_position,
            marine2_position = marine2_position,    
            marine3_position = marine3_position,
            marine4_position = marine4_position,
            marine5_position = marine5_position,
            marine6_position = marine6_position,

            marine1_health = marine1_health,
            marine2_health = marine2_health,    
            marine3_health = marine3_health,
            marine4_health = marine4_health,
            marine5_health = marine5_health,
            marine6_health = marine6_health,
            rock_position = rock_position,
            rock2_position = rock2_position,
            rock3_position = rock3_position,
        };
        string json = JsonUtility.ToJson(saveObject);

        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    private void Load()
    {
        if(File.Exists(Application.dataPath + "/save.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            player.transform.position = saveObject.playerPosition;
            marine1.transform.position = saveObject.marine1_position;
            marine2.transform.position = saveObject.marine2_position;
            marine3.transform.position = saveObject.marine3_position;
            marine4.transform.position = saveObject.marine4_position;
            marine5.transform.position = saveObject.marine5_position;
            marine6.transform.position = saveObject.marine6_position;

            marine1_healthscript.current_health = saveObject.marine1_health;
            marine2_healthscript.current_health = saveObject.marine2_health;
            marine3_healthscript.current_health = saveObject.marine3_health;
            marine4_healthscript.current_health = saveObject.marine4_health;
            marine5_healthscript.current_health = saveObject.marine5_health;
            marine6_healthscript.current_health = saveObject.marine6_health;

            rock.transform.position = saveObject.rock_position;
            rock2.transform.position = saveObject.rock2_position;
            rock3.transform.position = saveObject.rock3_position;
        }
    }
}

public struct SaveObject
{
    public int orbsAmount;
    public Vector3 playerPosition;
    public Vector3 marine1_position;
    public Vector3 marine2_position;
    public Vector3 marine3_position;
    public Vector3 marine4_position;
    public Vector3 marine5_position;
    public Vector3 marine6_position;

    public float marine1_health;
    public float marine2_health;
    public float marine3_health;
    public float marine4_health;
    public float marine5_health;
    public float marine6_health;
    public Vector3 rock_position;
    public Vector3 rock2_position;
    public Vector3 rock3_position;
}
