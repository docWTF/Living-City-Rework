using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Brushes/Object")]
[CustomGridBrush(false, true, false, "Object")]
public class Object : GridBrushBase
{
    public GameObject prefab;

    public override void Paint(GridLayout gridLayout, GameObject brushTarget, Vector3Int position)
    {
        // Instantiate the prefab at the given position
        if (prefab != null)
        {
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            if (instance != null)
            {
                Undo.MoveGameObjectToScene(instance, brushTarget.scene, "Paint GameObjects");
                instance.transform.position = gridLayout.LocalToWorld(gridLayout.CellToLocalInterpolated(position + new Vector3(.5f, .5f, .5f)));
            }
        }
    }

}
