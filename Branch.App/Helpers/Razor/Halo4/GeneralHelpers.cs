﻿using System;
using System.Globalization;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Branch.Core.BranchStuff;
using Branch.Models.Services.Branch;
using Branch.Models.Services.Halo4;
using Branch.Models.Services.Halo4._343.DataModels;

namespace Branch.App.Helpers.Razor.Halo4
{
	public class GeneralHelpers
	{
		public static int GetHighestCsr(CommonModels.CurrentSkillRank topSkillRank)
		{
			if (topSkillRank == null || topSkillRank.CompetitiveSkillRank == null)
				return 0;

			return (int) topSkillRank.CompetitiveSkillRank;
		}

		public static string GetCsrLevelUrl(int csrLevel, string size = "medium", string version = "v1")
		{
			return string.Format("https://assets.halowaypoint.com/games/h4/csr/{0}/{1}/{2}.png", version, size, csrLevel);
		}

		public static string GetRawAssetUrl(CommonModels.ImageUrl imageUrl, string size = "medium")
		{
			return string.Format("{0}{1}", GlobalStorage.H4WaypointManager.RegisteredWebApp.Settings[imageUrl.BaseUrl],
				imageUrl.AssetUrl.Replace("{size}", size));
		}

		public static string GetRawAssetUrl(CommonModels.ImageUrl imageUrl, int size)
		{
			return string.Format("{0}{1}", GlobalStorage.H4WaypointManager.RegisteredWebApp.Settings[imageUrl.BaseUrl],
				imageUrl.AssetUrl.Replace("{size}", size.ToString(CultureInfo.InvariantCulture)));
		}

		public static string GetPlayerModelUrl(string gamertag, string size = "large", string pose = "fullbody")
		{
			var url = GlobalStorage.H4WaypointManager.GetPlayerModelUrl(gamertag, size, pose);

			return GamerIdReplacementManager.GetReplacementGamerId(gamertag, GlobalStorage.AzureStorage) == gamertag
				? url
				: String.Format("data:image/png;base64,{0}", Convert.ToBase64String(new HttpClient().GetByteArrayAsync(url).Result));
		}

		public static string RemoveGuestIdentifier(string gamertag)
		{
			return Regex.Replace(gamertag, @"\((\d)\)", "", RegexOptions.None);
		}

		public static Tuple<string, string> GetGameVictoryStatus(Enums.Result result, bool completed)
		{
			if (!completed)
				return new Tuple<string, string>("dnf", "DNF");

			switch (result)
			{
				case Enums.Result.Draw:
					return new Tuple<string, string>("dnf", "DNF");

				case Enums.Result.Lost:
					return new Tuple<string, string>("los", "Lost");

				case Enums.Result.Won:
					return new Tuple<string, string>("win", "Won");

				default:
					return new Tuple<string, string>("", "");
			}
		}

		public static CsrType GetPlaylistCsrOrientation(PlaylistModel playlist)
		{
			var playlistOrientation = GlobalStorage.H4WaypointManager.GetPlaylistOrientation(playlist.Id);
			if (playlistOrientation == null)
				return CsrType.Unknown;

			return playlistOrientation.IsTeam ? CsrType.Team : CsrType.Individual;
		}
	}
}
