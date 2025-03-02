using System;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : Singleton<ConstructionManager>
{
    public int currentConstruction = -1;
    public List<ConstructionBase> allConstructions = new List<ConstructionBase>();
    public List<ConstructionBase> availableConstructions = new List<ConstructionBase>();

    public List<GameObject> allFences = new List<GameObject>();

    public Transform constructionsParent;

    private bool isMouseOnTerrain = false;

    private void OnEnable()
    {
        GameEvent.Register(Event.NightEnd, OnNightEnd);
    }

    private void OnDisable()
    {
        GameEvent.Unregister(Event.NightEnd, OnNightEnd);
    }

    protected override void OnAwake()
    {
        foreach (ConstructionBase construction in allConstructions)
        {
            construction.Init();
        }
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
            if (!isMouseOnTerrain)
            {
                availableConstructions[currentConstruction].OnMouseEnter();
                isMouseOnTerrain = true;
            }

            availableConstructions[currentConstruction].ProcessMove(hit.point);

            if (Input.GetMouseButtonDown(0))
            {
                availableConstructions[currentConstruction].ProcessClick(hit.point);
            }
        }
        else if (isMouseOnTerrain)
        {
            availableConstructions[currentConstruction].OnMouseLeave();
            isMouseOnTerrain = false;
        }
    }

    public void ChangeSelectedConstruction(int index)
    {
        if (currentConstruction != -1)
        {
            availableConstructions[currentConstruction].ProcessCancel();
        }

        currentConstruction = index;
    }

    private void OnNightEnd()
    {
        GameEvent<float>.Call(Event.MoneyPreviewReceived, 0);
        ChangeSelectedConstruction(-1);
    }
}
