using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;

public class CarSpawner : MonoBehaviour
{
    public GameObject[] cars;
    public Texture[] sedanImages;
    public Texture[] hatchBackImages;

    [HideInInspector]
    public GameObject car;
    [HideInInspector]
    public Cars carsJson;

    [Header("Car Information")]
    public RawImage[] carImage;
    public Text description;

    public void Start()
    {
        int i = Random.Range(0, (cars.Length));
        car = Instantiate(cars[i], transform.position, transform.rotation) as GameObject;   //Load the scene with a random car and paint with its corresponding description. Initialy there won't be any car spawned.
        car.transform.parent = this.transform;
        Renderer renderer = car.GetComponentInChildren<Renderer>();
        renderer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));  //Random paint applied to the car.
        ReadFromFile();


       if (car.tag == "Hatchback")                    //Added tags to the respective car types to check the spawned car's type
       {
            for(int j=0; j < hatchBackImages.Length; j++)
            {
                carImage[j].texture = hatchBackImages[j];
            }

            foreach (CarInformation ci in carsJson.cars)
            {
                if(ci.name == "Hatchback")          //If the json car name matches the spawned car gameobject's tag name, it loads the respective description. Here it is hatchback.
                {
                    description.text = ci.name + "\r\n" + "\r\n" +"Description"+ "\r\n" +ci.description;
                }
                
            }                        
       }
       else
       {
            for(int k =0; k<sedanImages.Length; k++)
            {
                carImage[k].texture = sedanImages[k];
            }
            foreach (CarInformation ci in carsJson.cars)
            {
                if (ci.name == "Sedan")             //If the json car name matches the spawned car gameobject's tag name, it loads the respective description. Here it is sedan.
                {
                    description.text = ci.name + "\r\n" + "\r\n" + "Description" + "\r\n" + ci.description;
                }

            }
        }
    }

    public void CarRandomizer()         //This method will be called everytime we press the Car randomizer button in the game
    {
        if (car != null)                //If any car is already spawned, this loop will go through the next steps and destroy the already present car.
        {
            Destroy(car);
            int i = Random.Range(0, (cars.Length));
            car = Instantiate(cars[i], transform.position, transform.rotation) as GameObject;
            car.transform.parent = this.transform;
            Renderer renderer = car.GetComponentInChildren<Renderer>();
            renderer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

            if (car.tag == "Hatchback")
            {
                for (int j = 0; j < hatchBackImages.Length; j++)
                {
                    carImage[j].texture = hatchBackImages[j];
                }
                foreach (CarInformation ci in carsJson.cars)
                {
                    if (ci.name == "Hatchback")
                    {
                        description.text = ci.name + "\r\n" + "\r\n" + "Description" + "\r\n" + ci.description;
                    }

                }
            }
            else
            {
                for (int k = 0; k < sedanImages.Length; k++)
                {
                    carImage[k].texture = sedanImages[k];
                }
                foreach (CarInformation ci in carsJson.cars)
                {
                    if (ci.name == "Sedan")
                    {
                        description.text = ci.name + "\r\n" + "\r\n" + "Description" + "\r\n" + ci.description;
                    }

                }
            }
        }
    }
 
    public void ReadFromFile() 
    {
        // string path = Path.Combine(Application.streamingAssetsPath, "car_information.json");    //Getting the file location of the json file and return the value to the Stream Reader to read the file.
        string path = Application.streamingAssetsPath + "/car_information.json";
        string jsonString;
        if (Application.platform == RuntimePlatform.Android)
        {
            WWW filePath = new WWW(path);
            if (string.IsNullOrEmpty(filePath.error))
            {
                jsonString = System.Text.Encoding.UTF8.GetString(filePath.bytes, 3, filePath.bytes.Length - 3);
            }

            while (!filePath.isDone)
            {
                jsonString = filePath.text;
                carsJson = JsonUtility.FromJson<Cars>(jsonString);  //Car name and description is read from the json file saved in the Streaming Assets folder
            }           
        }
        else
        {
            StreamReader reader = new StreamReader(path); //After reading the path, the reader reads the text data and returns as string data to the Json Utility.
            string jsonPCString = reader.ReadToEnd();
            reader.Close();
            carsJson = JsonUtility.FromJson<Cars>(jsonPCString);
        }                
    }
}

[System.Serializable]
public class CarInformation         //A Class stores the values of names and descriptions of the given Cars from the Json file.
{
    public string name;
    public string description;
}

[System.Serializable]
public class Cars               //This class collates the json file data into an array for using the data as variables.
{
    public CarInformation[] cars;
}
