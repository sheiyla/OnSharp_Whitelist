using Onsharp.Entities;
using Onsharp.Events;
using Onsharp.Plugins;
using SteamIDs_Engine;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OnSharp_Chat
{

    [PluginMeta("onsharp-whitelist", "Whitelist Plugin", "1.0", "Codeskull", IsDebug = true)]
    public class OnSharp_Whitelist : Plugin
    {

        private readonly List<string> whitelist = new List<string>();

        public override void OnStart() 
        {
            whitelist.Add("76561198080004249"); //Steam ID 64
        }

        public override void OnStop() 
        {
            whitelist.Clear();
        }

        [ServerEvent(EventType.PlayerSteamAuth)]
        public void OnPlayerSteamAuth(Player player)
        {
            string steamid = "U:1:" + player.SteamID; // Steam ID 2/3
            string result;
            if (Regex.IsMatch(steamid, SteamIDRegex.Steam2Regex)) result = SteamIDConvert.Steam2ToSteam64(steamid).ToString();
            else if (Regex.IsMatch(steamid, SteamIDRegex.Steam64Regex)) result = steamid;
            else if (Regex.IsMatch(steamid, SteamIDRegex.Steam32Regex)) result = SteamIDConvert.Steam32ToSteam64(steamid).ToString();
            else result = "error";
            if (result == "error") 
                player.Kick("You are not connected steam !");
            else
                if (!whitelist.Contains(result)) player.Kick("You're not on the whitelist !");
        }

    }

}