using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Xml;

public class DataManager : MonoBehaviour, IManager
{
    private string _state;
    private string _dataPath;
    private string _textFile;
    private string _streamingTextFile;
    private string _xmlLevelProgress;

    public string State
    {
        get { return _state; }
        set { _state = value; } 
    }

    void Awake()
    {
        _dataPath = Application.persistentDataPath + "/Player_Data/";

        Debug.Log(_dataPath);

        _textFile = _dataPath + "Save_Data.txt";
        _streamingTextFile = _dataPath + "Streaming_Save_Data.txt";
        _xmlLevelProgress = _dataPath + "Progress_Data.xml";
    }

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        _state = "Data Manager initialized";
        Debug.Log(_state);

        NewDirectory();
        NewTextFile();
        // UpdateTextFile();
        WriteToStream(_streamingTextFile);
        // ReadFromFile(_textFile);
        ReadFromStream(_streamingTextFile);
    }

    // This method display the filesystem's info
    private void FilesystemInfo()
    {
        Debug.LogFormat("Path separator character: {0}", Path.PathSeparator);
        Debug.LogFormat("Directory separator character: {0}", Path.DirectorySeparatorChar);
        Debug.LogFormat("Current directory: {0}", Directory.GetCurrentDirectory());
        Debug.LogFormat("Temporary path: {0}", Path.GetTempPath());
    }

    public void NewDirectory()
    {
        if (Directory.Exists(_dataPath))
        {
            Debug.Log("Directory already exists...");
            return;
        }

        Directory.CreateDirectory(_dataPath);
        Debug.Log("New Directory Created");
    }

    public void DeleteDirectory()
    {
        if (!Directory.Exists(_dataPath))
        {
            Debug.Log("Directory doesn't exist or has already be deleted.");
            return;
        }
        Directory.Delete(_dataPath, true);
        Debug.Log("Directory deleted successfully");
    }

    public void NewTextFile()
    {
        if (File.Exists(_textFile))
        {
            Debug.Log("File already exists");
            return;
        }
        File.WriteAllText(_textFile, "<SAVE DATA>\n\n");
        Debug.Log("New file created!");
    }

    public void UpdateTextFile()
    {
        if (!File.Exists(_textFile))
        {
            Debug.Log("File doesn't exist...");
            return;
        }
        File.AppendAllText(_textFile, $"Game started: {DateTime.Now}\n");

        Debug.Log("File updated successfully!");
    }

    public void ReadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("#ERROR: File does not exist.");
            return;
        }
        Debug.Log(File.ReadAllText(filename));
    }

    public void DeleteFile(string filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File does not exist or has already been deleted");
            return;
        }
        File.Delete(_textFile);

        Debug.Log("File successfully deleted!");
    }

    public void WriteToStream(String filename)
    {
        if (!File.Exists(filename))
        {
            StreamWriter newStream = File.CreateText(filename);

            newStream.WriteLine("<Save Data> for HERO BORN \n \n");
            newStream.Close();
            Debug.Log("New file created with stream writer!");
        }

        StreamWriter streamWriter = File.AppendText(filename);

        streamWriter.WriteLine("Game ended: " + DateTime.Now);
        streamWriter.Close();
        Debug.Log("File contents updated with StreamWriter!");
    }

    public void ReadFromStream(String filename)
    {
        if (!File.Exists(filename))
        {
            Debug.Log("File does not exist!");
            return;
        }
        StreamReader streamReader = new StreamReader(filename);
        Debug.Log(streamReader.ReadToEnd());
    }

    public void WriteToXML(String filename)
    {

    }
}
