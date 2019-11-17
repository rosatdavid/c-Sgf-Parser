using UnityEngine;
using System.Collections;
using UnityEditor;
using Grid = LibFever.Grid;

    public static class InputManager
    {
        public static LayerMask GetMaskForGrid()
        {
            return LayerMask.GetMask("UserInteractRaycast");
        }
        public static bool ScreenToBoard(Camera cam,LibFever.Grid grille, Vector3 mouse_pos, LayerMask mask, out DataStructures.Point board_index)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(mouse_pos);
            if (Physics.Raycast(ray, out hit, 100.0f, mask))
            {
                Debug.Log("hit : " + hit.point);
                board_index = LibFever.Grid.getCaseIndex(grille, hit.point);
                Debug.Log("index : " + board_index);
                return true;
            }
            board_index = new DataStructures.Point(-1, -1);
            return false;
        }

    }

