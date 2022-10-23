using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

   [SerializeField] Player player;
   [SerializeField] GameObject gameOverPanel;
   [SerializeField] GameObject grass;
   [SerializeField] GameObject road;
   [SerializeField] int extent = 7;
   [SerializeField] int frontDistance = 10;
   [SerializeField] int minZPos = -5;
   [SerializeField] int maxSameTerrainRepeat = 3;

   


    Dictionary<int, TerrainBlock> map = new Dictionary<int, TerrainBlock>(50);
    TMP_Text gameOverText;
   private void Start() 
   {
        gameOverPanel.SetActive(false);
        gameOverText = gameOverPanel.GetComponentInChildren<TMP_Text>();


        for (int z = minZPos; z <= 0; z++)
        {
            CreateTerrain(grass,z);
        }



        for (int z = 1; z < frontDistance; z++)
        {
            var prefab = GetNextRandomTerrainPrefab(z);
            CreateTerrain(prefab, z);
            
        }    

        player.setUp(minZPos, extent);
   }


   private void Update() 
   {
        if (player.IsDie && gameOverPanel.activeInHierarchy == false)
        {
            ShowGameOverPanel();
        }
   }

   void ShowGameOverPanel()
   {
        gameOverText.text = "Your Score: " + player.MaxTravel;
        gameOverPanel.SetActive(true);
   }

   private void CreateTerrain(GameObject prefab, int zPos)
   {
        var go = Instantiate(prefab, new Vector3(0,0,zPos), Quaternion.identity);
        var tb = go.GetComponent<TerrainBlock>();
        tb.Build(extent);
        map.Add(zPos,tb);


   }

   private GameObject GetNextRandomTerrainPrefab(int pos)
   {
        bool isUniform = true;

        var tbRef = map[pos-1];
        for (int distance = 2; distance <= maxSameTerrainRepeat; distance++)
        {
            if (map[pos-distance].GetType() != tbRef.GetType())
            {
                isUniform = false;
                break;
            }
        }

        if (isUniform)
        {
            if (tbRef is Grass)
            {
                return road;
            }
            else
            {
                return grass;
            }
        }

        return Random.value > 0.5f ? road:grass;
   }

}
