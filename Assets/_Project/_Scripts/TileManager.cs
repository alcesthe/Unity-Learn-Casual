using UnityEngine;
using System.Collections.Generic;

namespace UnityLearnCasual
{
	public class TileManager : MonoBehaviour
	{
		[SerializeField] GameObject[] tilePrefabs;
		[SerializeField] float zSpawn = 0;
		[SerializeField] float tileLength = 74;
		[SerializeField] int numberOfTiles = 5;
		[SerializeField] Transform player;

		private List<GameObject> activeTiles = new List<GameObject>();
		
	    void Start()
 	    {
			for(int i=0; i<numberOfTiles; i++)
            {
				SpawnTile(Random.Range(0, tilePrefabs.Length));
			}
	    }

        private void Update()
        {
            if(player.position.z - 40 > zSpawn - (numberOfTiles * tileLength))
            {
				SpawnTile(Random.Range(0, tilePrefabs.Length));
				DeleteTile();
			}
        }

        private void DeleteTile()
        {
			Destroy(activeTiles[0]);
			activeTiles.RemoveAt(0);
        }

        public void SpawnTile(int tileIndex)
        {
			GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
			activeTiles.Add(go);
			zSpawn += tileLength;
        }
	}
}
