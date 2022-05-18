using UnityEngine;
using System.Collections.Generic;

namespace HKLabs
{
    public class TileManager : MonoBehaviour
    {
        #region Variables

        [Tooltip ("The ground prefabs that need to be spawned")]
        [SerializeField] private GameObject[] _groundPrefabs;

        [Space]
        [Tooltip ("To store the position of the player")]
        [SerializeField] private Transform _playerTransform;

        [Space]
        [Header ("Tile Manager Properties")]
        [Tooltip ("So the destruction of the tiles is done properly without introducing bugs")]
        [SerializeField] private float _safeZone = 5.0f;

        /// <summary>
        /// To know where on the x-axis to spawn <see cref="_groundPrefabs"/>
        /// <para> Because the y and z axes are always going to be constant </para>
        /// </summary>
        private float _spawnX = 0.0f;

        /// <summary>
        /// To provide the offset for the spawning.
        /// <para> This is the actual length of the <see cref="_groundPrefabs"/> </para>
        /// </summary>
        private float _prefabLength = 15f;

        /// <summary>
        /// How many of the ground prefabs should be spawned at a time?
        /// </summary>
        private int _amountOfPrefabsOnScreen = 3;

        private int _lastPrefabIndex = 0;

        /// <summary>
        /// To store the prefabs from the array and destroy them properly
        /// </summary>
        private List<GameObject> _activePrefabs;

        #endregion

        #region Builtin Methods

        // Start is called before the first frame update
        void Start ()
        {
            _activePrefabs = new List<GameObject> ();

            for (int i = 0; i < _amountOfPrefabsOnScreen; i++)
            {
                if (i < 2)
                    SpawnPrefab (0);

                else
                    SpawnPrefab ();
            }
        }

        // Update is called once per frame
        void Update ()
        {
            if (_playerTransform.position.x - _safeZone > (_spawnX - _amountOfPrefabsOnScreen * _prefabLength))
            {
                SpawnPrefab ();
                DeletePrefab ();
            }
        }

        #endregion

        #region Custom Methods

        private void SpawnPrefab (int prefabIndex = -1)
        {
            GameObject go;

            if (prefabIndex == -1)
                go = Instantiate (_groundPrefabs[RandomPrefabIndex ()]);

            else
                go = Instantiate (_groundPrefabs[prefabIndex]);

            go.transform.SetParent (transform);
            go.transform.position = Vector3.right * _spawnX;
            _spawnX += _prefabLength;
            _activePrefabs.Add (go);
        }

        private void DeletePrefab ()
        {
            Destroy (_activePrefabs[0]);
            _activePrefabs.RemoveAt (0);
        }

        private int RandomPrefabIndex ()
        {
            if (_groundPrefabs.Length <= 1)
                return 0;

            int randomIndex = _lastPrefabIndex;

            while (randomIndex == _lastPrefabIndex)
                randomIndex = Random.Range (0, _groundPrefabs.Length);

            _lastPrefabIndex = randomIndex;
            return randomIndex;
        }

        #endregion
    }
}