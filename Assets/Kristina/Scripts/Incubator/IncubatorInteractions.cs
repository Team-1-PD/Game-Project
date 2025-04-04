using System.Collections.Generic;
using UnityEngine;

namespace kristina
{
    public class IncubatorInteractions : MonoBehaviour
    {
        Transform player_cursor;
        Vector3 last_position = new(100f,100f,100f);

        List<PlantIncubator> nearby_incubators = new();

        void Start()
        {
            player_cursor = FindFirstObjectByType<Cursor>().transform;
        }

        void Update()
        {
            if (nearby_incubators.Count > 1)
                SelectCurrentIncubator();
        }

        private void SelectCurrentIncubator()
        {
            if (Vector3.Distance(player_cursor.position, last_position) < .3f)
                return; //only check if the player moves/rotates enough
            PlantIncubator nearest = null;
            float nearest_distance = 1000f;
            foreach (PlantIncubator incubator in nearby_incubators)
            {
                float distance = Vector3.Distance(player_cursor.position, incubator.transform.position);
                if (distance < nearest_distance)
                {
                    nearest_distance = distance;
                    nearest = incubator;
                }
            }
            last_position = player_cursor.position;

            if (nearest == null) return; //should never be null, but just in case

            Debug.Log("selected incubator");
            WorldInteractions.Instance.Nearest_Incubator = nearest;
        }

        public void AddNearbyIncubator(PlantIncubator incubator)
        {
            nearby_incubators.Add(incubator);
            if (nearby_incubators.Count == 1)
                WorldInteractions.Instance.Nearest_Incubator = incubator;
        }
        public void RemoveNearbyIncubator(PlantIncubator incubator)
        {
            nearby_incubators.Remove(incubator);
            if (nearby_incubators.Count == 0)
                WorldInteractions.Instance.Nearest_Incubator = null;
        }
    }
}