using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : Singleton<ConstructionManager>
{
    public int currentConstruction = -1;
    public List<ConstructionBase> allConstructions = new List<ConstructionBase>();
    public List<ConstructionBase> availableConstructions = new List<ConstructionBase>();

    private void Update()
    {
        if (currentConstruction == -1)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            LayerMask layerMask = LayerMask.GetMask("Terrain");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                availableConstructions[currentConstruction].ProcessClick(hit.point);
            }
        }
    }
}
