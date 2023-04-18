using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;
using System.Xml.Serialization;


public class DataManager : MonoBehaviour, IManager      
{
    private string state;
    private string dataPath;
    private string textFile;
    private string streamingTextFile;
    private string xmlLevelProgress;
    public string State 
    {
        get { return state; }
        set { state = value; }
    }

    private void Awake()
    {
        dataPath = Application.persistentDataPath + "/Player_data/";
        Debug.Log(dataPath);

        textFile = dataPath + "Save_Data.txt";
        streamingTextFile = dataPath + "Steaming_Save_Data.txt";
        xmlLevelProgress = dataPath + "Progress_Data.xml";
        

    }
    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        state = "Data Manager initialzied";
        Debug.Log(state);

        FileSystemInfo();
        NewDirectory();
        WriteToXML(xmlLevelProgress);
        ReadFromSteam(xmlLevelProgress);
    }


    public void WriteToXML (string filename)
    {
        if (!File.Exists(filename))
        {
            FileStream xmlStream = File.Create(filename);

            XmlWriter xmlWriter = XmlWriter.Create(xmlStream);

            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("level_progress");

            for (int i = 1; i < 5; i++)
            {
                xmlWriter.WriteElementString("level", "Level-" + i);
            }

            xmlWriter.WriteEndElement();

            xmlWriter.Close();
            xmlStream.Close();
        }
    }

    public void WriteToStream(string filename)
    {
        if (!File.Exists(filename))
        {
            StreamWriter newStream = File.CreateText(filename);

            newStream.WriteLine("<Save Data> for HERO BORN\n");
            
            
            newStream.Close();
            Debug.Log("New file created with StreamWriter!");
        }

        StreamWriter streamWriter = File.AppendText(filename);

        streamWriter.WriteLine("Game ended: " + DateTime.Now);
        streamWriter.Close();
        Debug.Log("File contents updated with StreamWriter!");

    }

    public void ReadFromSteam(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File doesn't exists!");
            return;
        }

        StreamReader streamReader = new StreamReader(filename);
        Debug.Log(streamReader.ReadToEnd());
    }


    public void FileSystemInfo()
    {
        Debug.LogFormat("Path separator character: {0}", Path.PathSeparator);
        Debug.LogFormat("Directory separator character: {0}", Path.DirectorySeparatorChar);
        Debug.LogFormat("Current directory: {0}", Directory.GetCurrentDirectory());
        Debug.LogFormat("Temporary path: {0}", Path.GetTempPath());
        Debug.LogFormat("Persistance data path: {0}", Application.persistentDataPath);
    }

    public void NewDirectory()
    {
        if (Directory.Exists(dataPath))
        {
            Debug.Log("Directory already exists");
            return;
        }

        Directory.CreateDirectory(dataPath);
        Debug.Log("New directory created!");
    }

    public void DeleteDirectory()
    {
        if (!Directory.Exists(dataPath))
        {
            Debug.Log("Directory doesn't exist or has already been deleted");
            return;
        }

        Directory.Delete(dataPath, true);
        Debug.Log("Directory sucessfully deleted!");
    }

    public void NewTextFile()
    {
        if (File.Exists(textFile))
        {
            Debug.Log("File already exists");
            return;
        }
        File.WriteAllText(textFile, "<SAVE DATA>\n");

        Debug.Log("New file crated!");
    }

    public void UpdateTextFile()
    {
        if(!File.Exists(textFile))
        {
            Debug.Log("File doesn't exists");
            return;
        }
        File.AppendAllText(textFile, $"Game started: {DateTime.Now}\n");
        Debug.Log("File updated succedsfully");
    }

    public void ReadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File doesn't exists!");
            return;
        }
        Debug.Log(File.ReadAllText(filename));
    }

    public void DeleteFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File doesn't exists");
            return;
        }
        File.Delete(filename);
        Debug.Log("File succesfully deleted!");
    }

    
}
