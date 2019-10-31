using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class ObjectSpawning : MonoBehaviour
{
    #region Variabler
    DirectoryInfo dir; //Lokations variabel?//
    public FileInfo[] gObject; //Liste over prefabs i prefabs mappen//

    string fullPath; //Streng til lokation af filer(prefabs//
    string assetPath;  //Streng til lokation af alle assets (mappen)//
    GameObject prefab; //Prefab man vil arbejde med//

    float startRange; //Minimums antal spawnede objekter.//
    float endRange; //Maxiumum antal mulige spawns af objekter//
    float spawnChance; //Chancen for spawn af objekter over minimums antallet//
    float posRangex; //X Position på tile//
    float posRangey; //Y Position på tile//
    float minSize;
    float maxSize;
    float minCopies;
    float maxCopies;
    float copySpawnChance;
    float copiesZoneX;
    float copiesZoneY;

    int[,] prefabValues; //2D array til opbevaring af variablerne til hver af prefabsne//
    int numberOfParents = 0; //Antal prefabs som skal hentes i mappen//
    int currentParent = 0; //Hvilken prefab vi har med af gøre//


    GameObject player;

    #endregion

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        dir = new DirectoryInfo("Assets/Prefabs"); //Adgang til Assets//
        gObject = dir.GetFiles("*.prefab"); //Adgang til Prefabs//

        foreach (FileInfo g in gObject) //Antal prefabs som vi skal arbejde med//
        {
            numberOfParents++;
        }

        //!LÆNGDEN AF ANDEN DIMENSION SKAL OPDATERES, HVIS FLERE VARIABLER SKAL BRUGES PÅ PROPS!//
        prefabValues = new int[numberOfParents, 12]; //Instantiering af 2D array med længden af antal prefabs og det antal variabler de hver især har//

        #region Prefabs instatiering, Parents sættes til alle prefab kopier, variabler til alle prefabs gemmes i vores 2D array

        foreach (FileInfo g in gObject)
        {
            //Lokation af prefab//
            fullPath = g.FullName.Replace(@"\", "/");
            assetPath = "Assets" + fullPath.Replace(Application.dataPath, "");
            prefab = (AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject)) as GameObject);

            prefab.GetComponent<SpriteRenderer>().enabled = false;
            prefab.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = prefab.GetComponent<SpriteRenderer>().sortingOrder - 1;

            //De forskellige variabler hentes//
            startRange = prefab.GetComponent<ObjectProps>().startRange;
            endRange = prefab.GetComponent<ObjectProps>().endRange;
            spawnChance = prefab.GetComponent<ObjectProps>().spawnChance;
            posRangex = prefab.GetComponent<ObjectProps>().posRangex;
            posRangey = prefab.GetComponent<ObjectProps>().posRangey;
            minSize = prefab.GetComponent<ObjectProps>().minSize;
            maxSize = prefab.GetComponent<ObjectProps>().maxSize;
            minCopies = prefab.GetComponent<ObjectProps>().minCopies;
            maxCopies = prefab.GetComponent<ObjectProps>().maxCopies;
            copySpawnChance = prefab.GetComponent<ObjectProps>().copySpawnChance;
            copiesZoneX = prefab.GetComponent<ObjectProps>().copiesZoneX;
            copiesZoneY = prefab.GetComponent<ObjectProps>().copiesZoneY;

            //Vi sætter en parent til prefab//
            GameObject parent = new GameObject();
            parent.name = prefab.name + "Parent" + this.gameObject.name;
            parent.transform.parent = this.transform;

            //Vi spawner max antal mulige prefab kopier og giver dem samme parent. De sættes inaktive (Her skal de bare loades ind i banen for at optimere spillet)//
            for (int i = 0; i < endRange; i++)
            {
                GameObject preFabCopy = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
                preFabCopy.name += i;
                preFabCopy.transform.SetParent(parent.transform);
                preFabCopy.SetActive(false);

                for (int q = 0; q < maxCopies; q++)
                {
                    GameObject prefabMultiCopy = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
                    prefabMultiCopy.name += q;
                    prefabMultiCopy.transform.SetParent(preFabCopy.transform);
                    prefabMultiCopy.SetActive(false);
                }
            }



            //Variablerne for alle prefabs gemmes her i 2D array'et//
            prefabValues[currentParent, 0] = (int)startRange;
            prefabValues[currentParent, 1] = (int)endRange;
            prefabValues[currentParent, 2] = (int)spawnChance;
            prefabValues[currentParent, 3] = (int)posRangex;
            prefabValues[currentParent, 4] = (int)posRangey;
            prefabValues[currentParent, 5] = (int)minSize;
            prefabValues[currentParent, 6] = (int)maxSize;
            prefabValues[currentParent, 7] = (int)minCopies;
            prefabValues[currentParent, 8] = (int)maxCopies;
            prefabValues[currentParent, 9] = (int)copySpawnChance;
            prefabValues[currentParent, 10] = (int)copiesZoneX;
            prefabValues[currentParent, 11] = (int)copiesZoneY;


            //Int tælles op, så vi får fat i næste position i array'et til den næste prefab//
            currentParent++;

            #endregion

        }

        ObjSpawn();
    }

    /// <summary>
    /// Metode til at sætte objekterne i banen til at være aktive.
    /// De får også deres position her. 
    /// </summary>
    public void ObjSpawn()
    {
        DeSpawn();

        //Int til at hold styr på, hvilken prefab vi spawner og dets tilhørende værdier//
        int i = 0;

        //Foreach som kigger gennem alle prefab parents//
        foreach (Transform T in GetComponentsInChildren<Transform>())
        {            
            //Værdierne hentes i 2D array'et//
            startRange = prefabValues[i, 0];
            endRange = prefabValues[i, 1];
            spawnChance = prefabValues[i, 2];
            posRangex = prefabValues[i, 3];
            posRangey = prefabValues[i, 4];
            minSize = prefabValues[i, 5];
            maxSize = prefabValues[i, 6];
            minCopies = prefabValues[i, 7];
            maxCopies = prefabValues[i, 8];
            copySpawnChance = prefabValues[i, 9];
            copiesZoneX = prefabValues[i, 10];
            copiesZoneY = prefabValues[i, 11];


            //Hvis vi har fat i denne transform (tile), så skal vi ikke spawne nogle children (Fiks til en sær fejl. Den skal jo kun have fat i sine børn/prefabparents)//
            if (T == this.transform)
            {
                continue; //Koden skippes derfor, og vi starter forfra i vores foreach//
            }

            //Minimums antal prefab kopier aktiveres og får en position//
            for (int x = 0; x < startRange; x++)            
            {
                T.GetChild(x).gameObject.SetActive(true);
                T.GetChild(x).transform.position = new Vector3(0, 0, 0);
                T.GetChild(x).transform.position = new Vector3(Random.Range(this.transform.position.x - posRangex, this.transform.position.x + posRangex),
                    Random.Range(this.transform.position.y - posRangey, this.transform.position.y + posRangey), -1);
                T.GetChild(x).transform.localScale = Vector3.one * Random.Range(minSize, maxSize);

                for (int t = 1; t <= minCopies; t++)
                {
                    T.GetChild(x).GetChild(t).gameObject.SetActive(true);
                    T.GetChild(x).GetChild(t).transform.position = new Vector3(0, 0, 0);
                    T.GetChild(x).GetChild(t).transform.position = new Vector3(Random.Range(T.GetChild(x).transform.position.x - copiesZoneX,
                        T.GetChild(x).transform.position.x + copiesZoneX),
                        Random.Range(T.GetChild(x).transform.position.y - copiesZoneY, T.GetChild(x).transform.position.y + copiesZoneY), -1);

                    //T.GetChild(x).GetChild(t).SetParent(this.gameObject.transform);
                    //T.GetChild(x).GetChild(t).transform.localScale = Vector3.one * Random.Range(minSize, maxSize);
                    //T.GetChild(x).GetChild(t).SetParent(T.GetChild(x));
                }
            }

            //De resterende prefab kopier aktiveres ud fra en chance helt op til end range(max antal)//
            for (int x = (int)startRange; x < endRange; x++)
            {
                if (Random.Range(0, 100) < spawnChance)
                {
                    T.GetChild(x).gameObject.SetActive(true);
                    T.GetChild(x).transform.position = new Vector3(0, 0, 0);
                    T.GetChild(x).transform.position = new Vector3(Random.Range(this.transform.position.x - posRangex, this.transform.position.x + posRangex),
                        Random.Range(this.transform.position.y - posRangey, this.transform.position.y + posRangey), -1);
                    T.GetChild(x).transform.localScale = Vector3.one * Random.Range(minSize, maxSize);

                    for (int t = 1; t <= maxCopies; t++)
                    {
                        if (Random.Range(0, 100) < copySpawnChance)
                        {
                            T.GetChild(x).GetChild(t).gameObject.SetActive(true);
                            T.GetChild(x).GetChild(t).transform.position = new Vector3(0, 0, 0);
                            T.GetChild(x).GetChild(t).transform.position = new Vector3(Random.Range(T.GetChild(x).transform.position.x - copiesZoneX,
                                T.GetChild(x).transform.position.x + copiesZoneX),
                                Random.Range(T.GetChild(x).transform.position.y - copiesZoneY, T.GetChild(x).transform.position.y + copiesZoneY), -1);

 
                        }

                    }

                }
            }

            i++; //Int plusses for at få fat i de næste prefab værdier i 2D array'et//
        }
        player.GetComponent<SpriteRenderer>().enabled = true;
    }

    /// <summary>
    /// Metode til despawn af objekter. Denne køres når tiles kolliderer med grænsen sat udenom dem.    
    /// </summary>
    public void DeSpawn()
    {
        foreach (Transform T in this.transform)
        {
            for (int i = 0; i <= T.childCount - 1; i++)
            {
                T.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}