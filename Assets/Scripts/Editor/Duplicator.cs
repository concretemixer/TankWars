using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Duplicator : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    [MenuItem("Extra/Duplicate Selected")]
    public static void DuplicateSelected()
    {
        Object prefabRoot = PrefabUtility.GetPrefabParent(Selection.activeGameObject);

        for (int x = 0; x < 33; x++)
        {
            for (int z = 0; z < 33; z++)
            {
                GameObject o;
                if (prefabRoot != null)
                    o = PrefabUtility.InstantiatePrefab(prefabRoot) as GameObject;
                else
                    o = Instantiate(Selection.activeGameObject);

                o.transform.parent = Selection.activeGameObject.transform.parent;
                o.name = "Item_" + x + "_" + z;
                o.transform.localPosition = new Vector3(x * 10, 0, z * 10);
            }
        }
    }

    [MenuItem("Extra/Generate Land")]
    public static void GenerateLand()
    {
        Generate(0, 0, 32, 32);
    }

    static void Generate(int x1, int z1, int x2, int z2)
    {
        float y11 = Selection.activeGameObject.transform.Find("Item_" + x1 + "_" + z1).localPosition.y;
        float y12 = Selection.activeGameObject.transform.Find("Item_" + x1 + "_" + z2).localPosition.y;
        float y21 = Selection.activeGameObject.transform.Find("Item_" + x2 + "_" + z1).localPosition.y;
        float y22 = Selection.activeGameObject.transform.Find("Item_" + x2 + "_" + z2).localPosition.y;

        int midx = ((x1 + x2) / 2);
        int midz = ((z1 + z2) / 2);

        float midh = (y11 + y21 + y12 + y22) / 4;
        midh += Random.Range(-2, 2);

        Transform mid = Selection.activeGameObject.transform.Find("Item_" + midx + "_" + midz);
        mid.position = new Vector3(mid.position.x, midh, mid.position.z);

        float h = (y11 + y12 + midh) / 3;
        h += Random.Range(-1, 1);
        mid = Selection.activeGameObject.transform.Find("Item_" + x1 + "_" + midz);
        mid.position = new Vector3(mid.position.x, h, mid.position.z);

        h = (y21 + y22 + midh) / 3;
        h += Random.Range(-1, 1);
        mid = Selection.activeGameObject.transform.Find("Item_" + x2 + "_" + midz);
        mid.position = new Vector3(mid.position.x, h, mid.position.z);

        h = (y11 + y21 + midh) / 3;
        h += Random.Range(-1, 1);
        mid = Selection.activeGameObject.transform.Find("Item_" + midx + "_" + z1);
        mid.position = new Vector3(mid.position.x, h, mid.position.z);

        h = (y12 + y22 + midh) / 3;
        h += Random.Range(-1, 1);
        mid = Selection.activeGameObject.transform.Find("Item_" + midx + "_" + z2);
        mid.position = new Vector3(mid.position.x, h, mid.position.z);


        if (x2 - x1 <= 2)
            return;

        Generate(x1, z1, midx, midz);
        Generate(midx, z1, x2, midz);
        Generate(x1, midz, midx, z2);
        Generate(midx, midz, x2, z2);
    }
}