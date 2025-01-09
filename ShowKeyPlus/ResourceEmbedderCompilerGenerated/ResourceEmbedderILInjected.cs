using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ResourceEmbedderCompilerGenerated
{
	// Token: 0x0200004E RID: 78
	[CompilerGenerated]
	public static class ResourceEmbedderILInjected
	{
		// Token: 0x0600023C RID: 572 RVA: 0x0000FBD8 File Offset: 0x0000DDD8
		private static Assembly FindMainAssembly(AssemblyName requestedAssemblyName)
		{
			if (requestedAssemblyName == null)
			{
				throw new ArgumentNullException("requestedAssemblyName");
			}
			if (!requestedAssemblyName.Name.EndsWith(".resources", StringComparison.InvariantCultureIgnoreCase))
			{
				throw new ArgumentException("Not a resource assembly");
			}
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			string b = requestedAssemblyName.Name.Substring(0, requestedAssemblyName.Name.Length - ".resources".Length);
			foreach (Assembly assembly in assemblies)
			{
				if (assembly.GetName().Name == b)
				{
					return assembly;
				}
			}
			return null;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000FC68 File Offset: 0x0000DE68
		private static Assembly LoadFromResource(AssemblyName requestedAssemblyName, Assembly requestingAssembly)
		{
			if (requestedAssemblyName == null || requestedAssemblyName.CultureInfo == null)
			{
				return null;
			}
			for (;;)
			{
				string arg = requestedAssemblyName.Name.Substring(0, requestedAssemblyName.Name.Length - ".resources".Length);
				string name = string.Format("{0}.{1}.resources.dll", arg, requestedAssemblyName.CultureInfo.Name);
				Assembly assembly = requestingAssembly ?? ResourceEmbedderILInjected.FindMainAssembly(requestedAssemblyName);
				if (assembly == null)
				{
					break;
				}
				using (Stream manifestResourceStream = assembly.GetManifestResourceStream(name))
				{
					if (manifestResourceStream != null)
					{
						byte[] array = new byte[manifestResourceStream.Length];
						manifestResourceStream.Read(array, 0, array.Length);
						return Assembly.Load(array);
					}
				}
				string name2 = requestedAssemblyName.CultureInfo.Parent.Name;
				if (string.IsNullOrEmpty(name2))
				{
					goto Block_5;
				}
				requestedAssemblyName = new AssemblyName(requestedAssemblyName.FullName.Replace(string.Format("Culture={0}", requestedAssemblyName.CultureInfo.Name), string.Format("Culture={0}", name2)));
			}
			return null;
			Block_5:
			return null;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000FD7C File Offset: 0x0000DF7C
		private static bool IsLocalizedAssembly(AssemblyName requestedAssemblyName)
		{
			return requestedAssemblyName.Name.EndsWith(".resources", StringComparison.InvariantCultureIgnoreCase);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000FD90 File Offset: 0x0000DF90
		public static Assembly AssemblyResolve(object sender, ResolveEventArgs args)
		{
			AssemblyName requestedAssemblyName;
			try
			{
				requestedAssemblyName = new AssemblyName(args.Name);
			}
			catch (Exception ex) when (ex is ArgumentException || ex is FileLoadException)
			{
				return null;
			}
			if (!ResourceEmbedderILInjected.IsLocalizedAssembly(requestedAssemblyName))
			{
				return null;
			}
			return ResourceEmbedderILInjected.LoadFromResource(requestedAssemblyName, args.RequestingAssembly);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000FE00 File Offset: 0x0000E000
		public static void Attach()
		{
			AppDomain.CurrentDomain.AssemblyResolve += ResourceEmbedderILInjected.AssemblyResolve;
		}
	}
}
