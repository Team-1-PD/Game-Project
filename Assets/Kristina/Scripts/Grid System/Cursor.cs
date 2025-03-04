using System.Collections;
using UnityEngine;

namespace kristina
{
    public class Cursor : MonoBehaviour
    {
        [SerializeField] Grid grid;
        [SerializeField] GameObject highlighter;

        const int HEIGHT = 0;

        public Vector2Int currentGridPos { get; private set; }

        //bool highlighting = false;


    }
}