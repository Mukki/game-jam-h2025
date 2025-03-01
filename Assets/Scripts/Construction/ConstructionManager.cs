using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : Singleton<ConstructionManager>
{
    public int currentConstruction = -1;
    public List<ConstructionBase> allConstructions = new List<ConstructionBase>();
    public List<ConstructionBase> availableConstructions = new List<ConstructionBase>();

    public GameObject constructionMarkerPrefab;

    private GameObject constructionMarker;

    public override void Awake()
    {
        base.Awake();

        constructionMarker = Instantiate(constructionMarkerPrefab);
        constructionMarker.SetActive(false);
    }

    private void Update()
    {
        if (currentConstruction == -1)
        {
            return;
        }

        if (Input.GetMouseButtonDown(1))
        {
            availableConstructions[currentConstruction].ProcessCancel();
        }

        LayerMask layerMask = LayerMask.GetMask("Terrain");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            constructionMarker.SetActive(true);
            constructionMarker.transform.position = hit.point;

            if (Input.GetMouseButtonDown(0))
            {
                availableConstructions[currentConstruction].ProcessClick(hit.point);
            }
        }
        else
        {
            constructionMarker.SetActive(false);
        }
    }
}
