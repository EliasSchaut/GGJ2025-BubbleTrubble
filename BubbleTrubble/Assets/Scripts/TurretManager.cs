using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretManager: MonoBehaviour
{
    [SerializeField] private BubbleManager bubbleManager;
    private int lastTurretIndex = -1;
    private Turret[] turrets;

    void Start()
    {
        turrets = FindObjectsByType<Turret>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
    }

    void Update()
    {
        List<Turret> turretsRequesting = TurretsRequestingAmmo();
        if (!(turretsRequesting.Count > 0) || !bubbleManager.HasBubbleOnSink()) return;

        Bubble bubble = bubbleManager.PopBubbleOnSink()!;
        Turret turretPreffered = GetPreferredTurretToLoad(turretsRequesting);
        turretPreffered.AddAmmo(bubble);
    }

    List<Turret> TurretsRequestingAmmo()
    {
        List<Turret> turretsRequesting = new List<Turret>();
        foreach (Turret turret in turrets)
        {
            if (turret.IsOpenForAmmo && turret.CanAddAmmo)
            {
                turretsRequesting.Add(turret);
            }
        }
        
        return turretsRequesting;
    }

    Turret GetPreferredTurretToLoad(List<Turret> turretsRequesting)
    {
        if (turretsRequesting.Count == 0) return null;
        lastTurretIndex = (lastTurretIndex + 1) % turretsRequesting.Count;
        return turretsRequesting[lastTurretIndex];
    }

}