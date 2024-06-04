using System.Collections;
using System.Collections.Generic;
using Domain.Entities;
using Unity.Netcode;
using UnityEngine;

public static class PlayerHealthRegistry
{
    public static List<HealthComponent> Players = new();

    public static void AddPlayerHealthComponent(HealthComponent player)
    {
        Players.Add(player);
    }

    public static List<HealthComponent> GetPlayersHealthComponent()
    {
        return Players;
    }
}