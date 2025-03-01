using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : Singleton<ConstructionManager>
{
    public int currentConstruction = -1;
    public List<ConstructionBase> allConstructions = new List<ConstructionBase>();
    public List<ConstructionBase> availableConstructions = new List<ConstructionBase>();

    public GameObject constructionMarkerPrefab;

    private GameObject constructionMarker;

    public GameObject ghostPreview;

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
            ResetGhost();
        }

        LayerMask layerMask = LayerMask.GetMask("Terrain");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            ghostPreview.SetActive(true);
            availableConstructions[currentConstruction].ProcessMove(hit.point);

            if (Input.GetMouseButtonDown(0))
            {
                availableConstructions[currentConstruction].ProcessClick(hit.point);
            }
        }
        else
        {
            ghostPreview.SetActive(false);
        }
    }

    public void ChangeSelectedConstruction(int index)
    {
        if (currentConstruction != -1)
        {
            availableConstructions[currentConstruction].ProcessCancel();
        }

        currentConstruction = index;

        ResetGhost();
    }

    public void ResetGhost()
    {
        Destroy(ghostPreview);
        ghostPreview = Instantiate(availableConstructions[currentConstruction].prefab);

        ghostPreview.GetComponentInChildren<Collider>().enabled = false;

        Color color = ghostPreview.GetComponentInChildren<Renderer>().material.color;
        color.a = 0.1f;
        ghostPreview.GetComponentInChildren<Renderer>().material.color = color;

        ghostPreview.SetActive(false);
    }
}
