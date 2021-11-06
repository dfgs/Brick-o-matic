using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
	public static class NameSpaceUtils
	{
		private static Regex externalResourceRegex = new Regex(@"(?<Root>[^.]+)\.(?<Name>.+)");

		public static bool IsImportedReference(string Name, out string ImportedNameSpace, out string ImportedResourceName)
		{
			Match match;

			ImportedNameSpace = null;
			ImportedResourceName = null;

			match = externalResourceRegex.Match(Name);
			if (!match.Success) return false;

			ImportedNameSpace = match.Groups["Root"].Value;
			ImportedResourceName = match.Groups["Name"].Value;

			return true;
		}

		public static IResourceProvider GetResourceLocation(IResourceProvider ResourceProvider,string Name,out string LocalName)
		{
			string importedNameSpace;
			ImportedResources importedResources;

			if (NameSpaceUtils.IsImportedReference(Name, out importedNameSpace, out LocalName))
			{
				if (!ResourceProvider.TryGetResource(importedNameSpace, out importedResources)) return null;
				if (importedResources.Scene == null) return null;
				return importedResources.Scene;
			}
			return null;
		}


	}
}
