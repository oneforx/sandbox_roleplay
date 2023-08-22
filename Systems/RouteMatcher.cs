using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Roleplay.Systems
{
	public class RouteMatcher
	{
		public static Dictionary<string, string> GetParams(string routePattern, string currentUrl)
		{
			var paramMatches = new Dictionary<string, string>();

			// Échapper tout caractère spécial dans le motif de l'itinéraire et remplacer les paramètres par des regex
			string pattern = "^" + Regex.Escape(routePattern)
				.Replace("\\:", "(?<")
				.Replace("/", "/?")
				+ "$";

			pattern = pattern.Replace(">", ">[^/]+"); // paramètres correspondants sans "/"

			var match = Regex.Match(currentUrl, pattern, RegexOptions.IgnoreCase);

			if (match.Success)
			{
				foreach (var groupName in match.Groups.Keys)
				{
					if (groupName != "0") // Éviter la valeur par défaut du groupe entier
					{
						paramMatches[groupName] = match.Groups[groupName].Value;
					}
				}
			}

			return paramMatches;
		}
	}
}
