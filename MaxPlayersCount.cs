using BepInEx;
using HarmonyLib;
using BepInEx.Configuration;
using System.Collections.Generic;
using UnityEngine;

namespace PieceReescale
{
    [BepInPlugin(PluginGUID, PluginGUID, Version)]
    public class MaxPlayersCount : BaseUnityPlugin
    {
        public const string PluginGUID = "Detalhes.MaxPlayers";
        public const string Name = "MaxPlayers";
        public const string Version = "1.0.0";

        Harmony harmony = new Harmony(PluginGUID);

        public static ConfigEntry<int> MaxPlayers;        

        public void Awake()
        {
            Config.SaveOnConfigSet = true;

            MaxPlayers = Config.Bind("Server config", "MaxPlayers", 50,
                       new ConfigDescription("MaxPlayers", null));
            harmony.PatchAll();  
        }

        [HarmonyPatch(typeof(ZNet), "Awake")]
        public static class ZNetAwake
        {
            private static void Postfix(ref ZNet __instance)
            {
                int maxPlayers = MaxPlayers.Value;
                if (maxPlayers >= 1)
                {
                    __instance.m_serverPlayerLimit = maxPlayers;
                }
            }
        }

    }
}

