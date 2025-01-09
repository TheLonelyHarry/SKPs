using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Costura
{
	// Token: 0x0200004C RID: 76
	[CompilerGenerated]
	internal static class AssemblyLoader
	{
		// Token: 0x06000232 RID: 562 RVA: 0x0000DCFB File Offset: 0x0000BEFB
		private static string CultureToString(CultureInfo culture)
		{
			if (culture == null)
			{
				return "";
			}
			return culture.Name;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000DD0C File Offset: 0x0000BF0C
		private static Assembly ReadExistingAssembly(AssemblyName name)
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			Assembly[] assemblies = currentDomain.GetAssemblies();
			foreach (Assembly assembly in assemblies)
			{
				AssemblyName name2 = assembly.GetName();
				if (string.Equals(name2.Name, name.Name, StringComparison.InvariantCultureIgnoreCase) && string.Equals(AssemblyLoader.CultureToString(name2.CultureInfo), AssemblyLoader.CultureToString(name.CultureInfo), StringComparison.InvariantCultureIgnoreCase))
				{
					return assembly;
				}
			}
			return null;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000DD7C File Offset: 0x0000BF7C
		private static void CopyTo(Stream source, Stream destination)
		{
			byte[] array = new byte[81920];
			int count;
			while ((count = source.Read(array, 0, array.Length)) != 0)
			{
				destination.Write(array, 0, count);
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
		private static Stream LoadStream(string fullName)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			if (fullName.EndsWith(".compressed"))
			{
				using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(fullName))
				{
					using (DeflateStream deflateStream = new DeflateStream(manifestResourceStream, CompressionMode.Decompress))
					{
						MemoryStream memoryStream = new MemoryStream();
						AssemblyLoader.CopyTo(deflateStream, memoryStream);
						memoryStream.Position = 0L;
						return memoryStream;
					}
				}
			}
			return executingAssembly.GetManifestResourceStream(fullName);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000DE34 File Offset: 0x0000C034
		private static Stream LoadStream(Dictionary<string, string> resourceNames, string name)
		{
			string fullName;
			if (resourceNames.TryGetValue(name, out fullName))
			{
				return AssemblyLoader.LoadStream(fullName);
			}
			return null;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000DE54 File Offset: 0x0000C054
		private static byte[] ReadStream(Stream stream)
		{
			byte[] array = new byte[stream.Length];
			stream.Read(array, 0, array.Length);
			return array;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000DE7C File Offset: 0x0000C07C
		private static Assembly ReadFromEmbeddedResources(Dictionary<string, string> assemblyNames, Dictionary<string, string> symbolNames, AssemblyName requestedAssemblyName)
		{
			string text = requestedAssemblyName.Name.ToLowerInvariant();
			if (requestedAssemblyName.CultureInfo != null && !string.IsNullOrEmpty(requestedAssemblyName.CultureInfo.Name))
			{
				text = requestedAssemblyName.CultureInfo.Name + "." + text;
			}
			byte[] rawAssembly;
			using (Stream stream = AssemblyLoader.LoadStream(assemblyNames, text))
			{
				if (stream == null)
				{
					return null;
				}
				rawAssembly = AssemblyLoader.ReadStream(stream);
			}
			using (Stream stream2 = AssemblyLoader.LoadStream(symbolNames, text))
			{
				if (stream2 != null)
				{
					byte[] rawSymbolStore = AssemblyLoader.ReadStream(stream2);
					return Assembly.Load(rawAssembly, rawSymbolStore);
				}
			}
			return Assembly.Load(rawAssembly);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000DF3C File Offset: 0x0000C13C
		public static Assembly ResolveAssembly(object sender, ResolveEventArgs e)
		{
			object obj = AssemblyLoader.nullCacheLock;
			lock (obj)
			{
				if (AssemblyLoader.nullCache.ContainsKey(e.Name))
				{
					return null;
				}
			}
			AssemblyName assemblyName = new AssemblyName(e.Name);
			Assembly assembly = AssemblyLoader.ReadExistingAssembly(assemblyName);
			if (assembly != null)
			{
				return assembly;
			}
			assembly = AssemblyLoader.ReadFromEmbeddedResources(AssemblyLoader.assemblyNames, AssemblyLoader.symbolNames, assemblyName);
			if (assembly == null)
			{
				object obj2 = AssemblyLoader.nullCacheLock;
				lock (obj2)
				{
					AssemblyLoader.nullCache[e.Name] = true;
				}
				if ((assemblyName.Flags & AssemblyNameFlags.Retargetable) != AssemblyNameFlags.None)
				{
					assembly = Assembly.Load(assemblyName);
				}
			}
			return assembly;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000E014 File Offset: 0x0000C214
		// Note: this type is marked as 'beforefieldinit'.
		static AssemblyLoader()
		{
			AssemblyLoader.assemblyNames.Add("af-za.modernwpf.controls.resources", "costura.af-za.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("af-za.modernwpf.resources", "costura.af-za.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("am-et.modernwpf.controls.resources", "costura.am-et.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("am-et.modernwpf.resources", "costura.am-et.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ar-sa.modernwpf.controls.resources", "costura.ar-sa.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ar-sa.modernwpf.resources", "costura.ar-sa.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("az-latn-az.modernwpf.controls.resources", "costura.az-latn-az.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("az-latn-az.modernwpf.resources", "costura.az-latn-az.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("be-by.modernwpf.controls.resources", "costura.be-by.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("be-by.modernwpf.resources", "costura.be-by.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("bg-bg.modernwpf.controls.resources", "costura.bg-bg.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("bg-bg.modernwpf.resources", "costura.bg-bg.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("bn-bd.modernwpf.controls.resources", "costura.bn-bd.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("bn-bd.modernwpf.resources", "costura.bn-bd.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("bs-latn-ba.modernwpf.controls.resources", "costura.bs-latn-ba.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("bs-latn-ba.modernwpf.resources", "costura.bs-latn-ba.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ca-es.modernwpf.controls.resources", "costura.ca-es.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ca-es.modernwpf.resources", "costura.ca-es.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("commonservicelocator", "costura.commonservicelocator.dll.compressed");
			AssemblyLoader.assemblyNames.Add("costura", "costura.costura.dll.compressed");
			AssemblyLoader.symbolNames.Add("costura", "costura.costura.pdb.compressed");
			AssemblyLoader.assemblyNames.Add("cs.microsoft.servicehub.framework.resources", "costura.cs.microsoft.servicehub.framework.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("cs.microsoft.visualstudio.threading.resources", "costura.cs.microsoft.visualstudio.threading.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("cs.microsoft.visualstudio.utilities.resources", "costura.cs.microsoft.visualstudio.utilities.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("cs.microsoft.visualstudio.validation.resources", "costura.cs.microsoft.visualstudio.validation.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("cs.streamjsonrpc.resources", "costura.cs.streamjsonrpc.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("cs-cz.modernwpf.controls.resources", "costura.cs-cz.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("cs-cz.modernwpf.resources", "costura.cs-cz.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("da-dk.modernwpf.controls.resources", "costura.da-dk.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("da-dk.modernwpf.resources", "costura.da-dk.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("de.microsoft.servicehub.framework.resources", "costura.de.microsoft.servicehub.framework.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("de.microsoft.visualstudio.threading.resources", "costura.de.microsoft.visualstudio.threading.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("de.microsoft.visualstudio.utilities.resources", "costura.de.microsoft.visualstudio.utilities.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("de.microsoft.visualstudio.validation.resources", "costura.de.microsoft.visualstudio.validation.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("de.streamjsonrpc.resources", "costura.de.streamjsonrpc.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("de-de.modernwpf.controls.resources", "costura.de-de.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("de-de.modernwpf.resources", "costura.de-de.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("el-gr.modernwpf.controls.resources", "costura.el-gr.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("el-gr.modernwpf.resources", "costura.el-gr.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("en.microsoft.visualstudio.utilities.resources", "costura.en.microsoft.visualstudio.utilities.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("en-gb.modernwpf.controls.resources", "costura.en-gb.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("en-gb.modernwpf.resources", "costura.en-gb.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("es.microsoft.servicehub.framework.resources", "costura.es.microsoft.servicehub.framework.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("es.microsoft.visualstudio.threading.resources", "costura.es.microsoft.visualstudio.threading.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("es.microsoft.visualstudio.utilities.resources", "costura.es.microsoft.visualstudio.utilities.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("es.microsoft.visualstudio.validation.resources", "costura.es.microsoft.visualstudio.validation.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("es.streamjsonrpc.resources", "costura.es.streamjsonrpc.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("es-es.modernwpf.controls.resources", "costura.es-es.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("es-es.modernwpf.resources", "costura.es-es.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("es-mx.modernwpf.controls.resources", "costura.es-mx.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("es-mx.modernwpf.resources", "costura.es-mx.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("et-ee.modernwpf.controls.resources", "costura.et-ee.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("et-ee.modernwpf.resources", "costura.et-ee.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("eu-es.modernwpf.controls.resources", "costura.eu-es.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("eu-es.modernwpf.resources", "costura.eu-es.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("fa-ir.modernwpf.controls.resources", "costura.fa-ir.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("fa-ir.modernwpf.resources", "costura.fa-ir.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("fi-fi.modernwpf.controls.resources", "costura.fi-fi.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("fi-fi.modernwpf.resources", "costura.fi-fi.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("fluentwpf", "costura.fluentwpf.dll.compressed");
			AssemblyLoader.assemblyNames.Add("fr.microsoft.servicehub.framework.resources", "costura.fr.microsoft.servicehub.framework.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("fr.microsoft.visualstudio.threading.resources", "costura.fr.microsoft.visualstudio.threading.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("fr.microsoft.visualstudio.utilities.resources", "costura.fr.microsoft.visualstudio.utilities.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("fr.microsoft.visualstudio.validation.resources", "costura.fr.microsoft.visualstudio.validation.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("fr.streamjsonrpc.resources", "costura.fr.streamjsonrpc.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("fr-ca.modernwpf.controls.resources", "costura.fr-ca.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("fr-ca.modernwpf.resources", "costura.fr-ca.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("fr-fr.modernwpf.controls.resources", "costura.fr-fr.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("fr-fr.modernwpf.resources", "costura.fr-fr.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("galasoft.mvvmlight", "costura.galasoft.mvvmlight.dll.compressed");
			AssemblyLoader.assemblyNames.Add("galasoft.mvvmlight.extras", "costura.galasoft.mvvmlight.extras.dll.compressed");
			AssemblyLoader.symbolNames.Add("galasoft.mvvmlight.extras", "costura.galasoft.mvvmlight.extras.pdb.compressed");
			AssemblyLoader.symbolNames.Add("galasoft.mvvmlight", "costura.galasoft.mvvmlight.pdb.compressed");
			AssemblyLoader.assemblyNames.Add("galasoft.mvvmlight.platform", "costura.galasoft.mvvmlight.platform.dll.compressed");
			AssemblyLoader.symbolNames.Add("galasoft.mvvmlight.platform", "costura.galasoft.mvvmlight.platform.pdb.compressed");
			AssemblyLoader.assemblyNames.Add("gl-es.modernwpf.controls.resources", "costura.gl-es.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("gl-es.modernwpf.resources", "costura.gl-es.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ha-latn-ng.modernwpf.controls.resources", "costura.ha-latn-ng.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ha-latn-ng.modernwpf.resources", "costura.ha-latn-ng.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("he-il.modernwpf.controls.resources", "costura.he-il.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("he-il.modernwpf.resources", "costura.he-il.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("hi-in.modernwpf.controls.resources", "costura.hi-in.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("hi-in.modernwpf.resources", "costura.hi-in.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("hr-hr.modernwpf.controls.resources", "costura.hr-hr.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("hr-hr.modernwpf.resources", "costura.hr-hr.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("hu-hu.modernwpf.controls.resources", "costura.hu-hu.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("hu-hu.modernwpf.resources", "costura.hu-hu.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("id-id.modernwpf.controls.resources", "costura.id-id.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("id-id.modernwpf.resources", "costura.id-id.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("is-is.modernwpf.controls.resources", "costura.is-is.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("is-is.modernwpf.resources", "costura.is-is.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("it.microsoft.servicehub.framework.resources", "costura.it.microsoft.servicehub.framework.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("it.microsoft.visualstudio.threading.resources", "costura.it.microsoft.visualstudio.threading.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("it.microsoft.visualstudio.utilities.resources", "costura.it.microsoft.visualstudio.utilities.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("it.microsoft.visualstudio.validation.resources", "costura.it.microsoft.visualstudio.validation.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("it.streamjsonrpc.resources", "costura.it.streamjsonrpc.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("it-it.modernwpf.controls.resources", "costura.it-it.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("it-it.modernwpf.resources", "costura.it-it.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ja.microsoft.servicehub.framework.resources", "costura.ja.microsoft.servicehub.framework.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ja.microsoft.visualstudio.threading.resources", "costura.ja.microsoft.visualstudio.threading.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ja.microsoft.visualstudio.utilities.resources", "costura.ja.microsoft.visualstudio.utilities.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ja.microsoft.visualstudio.validation.resources", "costura.ja.microsoft.visualstudio.validation.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ja.streamjsonrpc.resources", "costura.ja.streamjsonrpc.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ja-jp.modernwpf.controls.resources", "costura.ja-jp.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ja-jp.modernwpf.resources", "costura.ja-jp.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ka-ge.modernwpf.controls.resources", "costura.ka-ge.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ka-ge.modernwpf.resources", "costura.ka-ge.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("kk-kz.modernwpf.controls.resources", "costura.kk-kz.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("kk-kz.modernwpf.resources", "costura.kk-kz.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("km-kh.modernwpf.controls.resources", "costura.km-kh.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("km-kh.modernwpf.resources", "costura.km-kh.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("kn-in.modernwpf.controls.resources", "costura.kn-in.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("kn-in.modernwpf.resources", "costura.kn-in.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ko.microsoft.servicehub.framework.resources", "costura.ko.microsoft.servicehub.framework.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ko.microsoft.visualstudio.threading.resources", "costura.ko.microsoft.visualstudio.threading.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ko.microsoft.visualstudio.utilities.resources", "costura.ko.microsoft.visualstudio.utilities.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ko.microsoft.visualstudio.validation.resources", "costura.ko.microsoft.visualstudio.validation.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ko.streamjsonrpc.resources", "costura.ko.streamjsonrpc.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ko-kr.modernwpf.controls.resources", "costura.ko-kr.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ko-kr.modernwpf.resources", "costura.ko-kr.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("lo-la.modernwpf.controls.resources", "costura.lo-la.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("lo-la.modernwpf.resources", "costura.lo-la.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("lt-lt.modernwpf.controls.resources", "costura.lt-lt.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("lt-lt.modernwpf.resources", "costura.lt-lt.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("lv-lv.modernwpf.controls.resources", "costura.lv-lv.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("lv-lv.modernwpf.resources", "costura.lv-lv.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("messagepack.annotations", "costura.messagepack.annotations.dll.compressed");
			AssemblyLoader.assemblyNames.Add("messagepack", "costura.messagepack.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.bcl.asyncinterfaces", "costura.microsoft.bcl.asyncinterfaces.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.diagnostics.tracing.eventsource", "costura.microsoft.diagnostics.tracing.eventsource.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.servicehub.client", "costura.microsoft.servicehub.client.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.servicehub.framework", "costura.microsoft.servicehub.framework.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.servicehub.resources", "costura.microsoft.servicehub.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.visualstudio.remotecontrol", "costura.microsoft.visualstudio.remotecontrol.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.visualstudio.rpccontracts", "costura.microsoft.visualstudio.rpccontracts.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.visualstudio.telemetry", "costura.microsoft.visualstudio.telemetry.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.visualstudio.threading", "costura.microsoft.visualstudio.threading.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.visualstudio.utilities", "costura.microsoft.visualstudio.utilities.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.visualstudio.utilities.internal", "costura.microsoft.visualstudio.utilities.internal.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.visualstudio.validation", "costura.microsoft.visualstudio.validation.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.win32.primitives", "costura.microsoft.win32.primitives.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.win32.registry", "costura.microsoft.win32.registry.dll.compressed");
			AssemblyLoader.assemblyNames.Add("microsoft.xaml.behaviors", "costura.microsoft.xaml.behaviors.dll.compressed");
			AssemblyLoader.symbolNames.Add("microsoft.xaml.behaviors", "costura.microsoft.xaml.behaviors.pdb.compressed");
			AssemblyLoader.assemblyNames.Add("mk-mk.modernwpf.controls.resources", "costura.mk-mk.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("mk-mk.modernwpf.resources", "costura.mk-mk.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ml-in.modernwpf.controls.resources", "costura.ml-in.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ml-in.modernwpf.resources", "costura.ml-in.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("modernwpf.controls", "costura.modernwpf.controls.dll.compressed");
			AssemblyLoader.assemblyNames.Add("modernwpf", "costura.modernwpf.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ms-my.modernwpf.controls.resources", "costura.ms-my.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ms-my.modernwpf.resources", "costura.ms-my.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("nb-no.modernwpf.controls.resources", "costura.nb-no.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("nb-no.modernwpf.resources", "costura.nb-no.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("nerdbank.streams", "costura.nerdbank.streams.dll.compressed");
			AssemblyLoader.symbolNames.Add("nerdbank.streams", "costura.nerdbank.streams.pdb.compressed");
			AssemblyLoader.assemblyNames.Add("netstandard", "costura.netstandard.dll.compressed");
			AssemblyLoader.assemblyNames.Add("newtonsoft.json", "costura.newtonsoft.json.dll.compressed");
			AssemblyLoader.assemblyNames.Add("nl-nl.modernwpf.controls.resources", "costura.nl-nl.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("nl-nl.modernwpf.resources", "costura.nl-nl.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("nn-no.modernwpf.controls.resources", "costura.nn-no.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("nn-no.modernwpf.resources", "costura.nn-no.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("pl.microsoft.servicehub.framework.resources", "costura.pl.microsoft.servicehub.framework.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("pl.microsoft.visualstudio.threading.resources", "costura.pl.microsoft.visualstudio.threading.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("pl.microsoft.visualstudio.utilities.resources", "costura.pl.microsoft.visualstudio.utilities.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("pl.microsoft.visualstudio.validation.resources", "costura.pl.microsoft.visualstudio.validation.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("pl.streamjsonrpc.resources", "costura.pl.streamjsonrpc.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("pl-pl.modernwpf.controls.resources", "costura.pl-pl.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("pl-pl.modernwpf.resources", "costura.pl-pl.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("prism", "costura.prism.dll.compressed");
			AssemblyLoader.symbolNames.Add("prism", "costura.prism.pdb.compressed");
			AssemblyLoader.assemblyNames.Add("prism.unity.wpf", "costura.prism.unity.wpf.dll.compressed");
			AssemblyLoader.symbolNames.Add("prism.unity.wpf", "costura.prism.unity.wpf.pdb.compressed");
			AssemblyLoader.assemblyNames.Add("prism.wpf", "costura.prism.wpf.dll.compressed");
			AssemblyLoader.symbolNames.Add("prism.wpf", "costura.prism.wpf.pdb.compressed");
			AssemblyLoader.assemblyNames.Add("pt.microsoft.visualstudio.utilities.resources", "costura.pt.microsoft.visualstudio.utilities.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("pt-br.microsoft.servicehub.framework.resources", "costura.pt-br.microsoft.servicehub.framework.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("pt-br.microsoft.visualstudio.threading.resources", "costura.pt-br.microsoft.visualstudio.threading.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("pt-br.microsoft.visualstudio.validation.resources", "costura.pt-br.microsoft.visualstudio.validation.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("pt-br.modernwpf.controls.resources", "costura.pt-br.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("pt-br.modernwpf.resources", "costura.pt-br.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("pt-br.streamjsonrpc.resources", "costura.pt-br.streamjsonrpc.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("pt-pt.modernwpf.controls.resources", "costura.pt-pt.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("pt-pt.modernwpf.resources", "costura.pt-pt.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ro-ro.modernwpf.controls.resources", "costura.ro-ro.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ro-ro.modernwpf.resources", "costura.ro-ro.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ru.microsoft.servicehub.framework.resources", "costura.ru.microsoft.servicehub.framework.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ru.microsoft.visualstudio.threading.resources", "costura.ru.microsoft.visualstudio.threading.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ru.microsoft.visualstudio.utilities.resources", "costura.ru.microsoft.visualstudio.utilities.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ru.microsoft.visualstudio.validation.resources", "costura.ru.microsoft.visualstudio.validation.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ru.streamjsonrpc.resources", "costura.ru.streamjsonrpc.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ru-ru.modernwpf.controls.resources", "costura.ru-ru.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ru-ru.modernwpf.resources", "costura.ru-ru.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("sk-sk.modernwpf.controls.resources", "costura.sk-sk.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("sk-sk.modernwpf.resources", "costura.sk-sk.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("sl-si.modernwpf.controls.resources", "costura.sl-si.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("sl-si.modernwpf.resources", "costura.sl-si.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("sq-al.modernwpf.controls.resources", "costura.sq-al.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("sq-al.modernwpf.resources", "costura.sq-al.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("sr-latn-rs.modernwpf.controls.resources", "costura.sr-latn-rs.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("sr-latn-rs.modernwpf.resources", "costura.sr-latn-rs.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("streamjsonrpc", "costura.streamjsonrpc.dll.compressed");
			AssemblyLoader.assemblyNames.Add("sv-se.modernwpf.controls.resources", "costura.sv-se.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("sv-se.modernwpf.resources", "costura.sv-se.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("sw-ke.modernwpf.controls.resources", "costura.sw-ke.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("sw-ke.modernwpf.resources", "costura.sw-ke.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.appcontext", "costura.system.appcontext.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.buffers", "costura.system.buffers.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.collections.concurrent", "costura.system.collections.concurrent.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.collections", "costura.system.collections.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.collections.immutable", "costura.system.collections.immutable.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.collections.nongeneric", "costura.system.collections.nongeneric.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.collections.specialized", "costura.system.collections.specialized.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.componentmodel", "costura.system.componentmodel.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.componentmodel.eventbasedasync", "costura.system.componentmodel.eventbasedasync.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.componentmodel.primitives", "costura.system.componentmodel.primitives.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.componentmodel.typeconverter", "costura.system.componentmodel.typeconverter.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.console", "costura.system.console.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.data.common", "costura.system.data.common.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.contracts", "costura.system.diagnostics.contracts.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.debug", "costura.system.diagnostics.debug.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.diagnosticsource", "costura.system.diagnostics.diagnosticsource.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.fileversioninfo", "costura.system.diagnostics.fileversioninfo.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.process", "costura.system.diagnostics.process.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.stacktrace", "costura.system.diagnostics.stacktrace.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.textwritertracelistener", "costura.system.diagnostics.textwritertracelistener.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.tools", "costura.system.diagnostics.tools.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.tracesource", "costura.system.diagnostics.tracesource.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.diagnostics.tracing", "costura.system.diagnostics.tracing.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.drawing.primitives", "costura.system.drawing.primitives.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.dynamic.runtime", "costura.system.dynamic.runtime.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.globalization.calendars", "costura.system.globalization.calendars.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.globalization", "costura.system.globalization.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.globalization.extensions", "costura.system.globalization.extensions.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.compression", "costura.system.io.compression.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.compression.zipfile", "costura.system.io.compression.zipfile.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io", "costura.system.io.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.filesystem", "costura.system.io.filesystem.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.filesystem.driveinfo", "costura.system.io.filesystem.driveinfo.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.filesystem.primitives", "costura.system.io.filesystem.primitives.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.filesystem.watcher", "costura.system.io.filesystem.watcher.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.isolatedstorage", "costura.system.io.isolatedstorage.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.memorymappedfiles", "costura.system.io.memorymappedfiles.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.pipelines", "costura.system.io.pipelines.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.pipes", "costura.system.io.pipes.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.io.unmanagedmemorystream", "costura.system.io.unmanagedmemorystream.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.linq", "costura.system.linq.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.linq.expressions", "costura.system.linq.expressions.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.linq.parallel", "costura.system.linq.parallel.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.linq.queryable", "costura.system.linq.queryable.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.memory", "costura.system.memory.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.http", "costura.system.net.http.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.nameresolution", "costura.system.net.nameresolution.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.networkinformation", "costura.system.net.networkinformation.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.ping", "costura.system.net.ping.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.primitives", "costura.system.net.primitives.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.requests", "costura.system.net.requests.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.security", "costura.system.net.security.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.sockets", "costura.system.net.sockets.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.webheadercollection", "costura.system.net.webheadercollection.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.websockets.client", "costura.system.net.websockets.client.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.net.websockets", "costura.system.net.websockets.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.numerics.vectors", "costura.system.numerics.vectors.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.objectmodel", "costura.system.objectmodel.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.reflection", "costura.system.reflection.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.reflection.extensions", "costura.system.reflection.extensions.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.reflection.primitives", "costura.system.reflection.primitives.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.resources.reader", "costura.system.resources.reader.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.resources.resourcemanager", "costura.system.resources.resourcemanager.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.resources.writer", "costura.system.resources.writer.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.compilerservices.unsafe", "costura.system.runtime.compilerservices.unsafe.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.compilerservices.visualc", "costura.system.runtime.compilerservices.visualc.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime", "costura.system.runtime.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.extensions", "costura.system.runtime.extensions.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.handles", "costura.system.runtime.handles.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.interopservices", "costura.system.runtime.interopservices.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.interopservices.runtimeinformation", "costura.system.runtime.interopservices.runtimeinformation.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.numerics", "costura.system.runtime.numerics.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.serialization.formatters", "costura.system.runtime.serialization.formatters.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.serialization.json", "costura.system.runtime.serialization.json.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.serialization.primitives", "costura.system.runtime.serialization.primitives.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.runtime.serialization.xml", "costura.system.runtime.serialization.xml.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.accesscontrol", "costura.system.security.accesscontrol.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.claims", "costura.system.security.claims.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.cryptography.algorithms", "costura.system.security.cryptography.algorithms.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.cryptography.csp", "costura.system.security.cryptography.csp.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.cryptography.encoding", "costura.system.security.cryptography.encoding.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.cryptography.primitives", "costura.system.security.cryptography.primitives.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.cryptography.x509certificates", "costura.system.security.cryptography.x509certificates.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.principal", "costura.system.security.principal.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.principal.windows", "costura.system.security.principal.windows.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.security.securestring", "costura.system.security.securestring.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.text.encoding", "costura.system.text.encoding.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.text.encoding.extensions", "costura.system.text.encoding.extensions.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.text.regularexpressions", "costura.system.text.regularexpressions.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.threading.accesscontrol", "costura.system.threading.accesscontrol.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.threading", "costura.system.threading.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.threading.overlapped", "costura.system.threading.overlapped.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.threading.tasks.dataflow", "costura.system.threading.tasks.dataflow.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.threading.tasks", "costura.system.threading.tasks.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.threading.tasks.extensions", "costura.system.threading.tasks.extensions.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.threading.tasks.parallel", "costura.system.threading.tasks.parallel.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.threading.thread", "costura.system.threading.thread.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.threading.threadpool", "costura.system.threading.threadpool.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.threading.timer", "costura.system.threading.timer.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.valuetuple", "costura.system.valuetuple.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.windows.interactivity", "costura.system.windows.interactivity.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.xml.readerwriter", "costura.system.xml.readerwriter.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.xml.xdocument", "costura.system.xml.xdocument.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.xml.xmldocument", "costura.system.xml.xmldocument.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.xml.xmlserializer", "costura.system.xml.xmlserializer.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.xml.xpath", "costura.system.xml.xpath.dll.compressed");
			AssemblyLoader.assemblyNames.Add("system.xml.xpath.xdocument", "costura.system.xml.xpath.xdocument.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ta-in.modernwpf.controls.resources", "costura.ta-in.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("ta-in.modernwpf.resources", "costura.ta-in.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("te-in.modernwpf.controls.resources", "costura.te-in.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("te-in.modernwpf.resources", "costura.te-in.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("th-th.modernwpf.controls.resources", "costura.th-th.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("th-th.modernwpf.resources", "costura.th-th.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("tr.microsoft.servicehub.framework.resources", "costura.tr.microsoft.servicehub.framework.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("tr.microsoft.visualstudio.threading.resources", "costura.tr.microsoft.visualstudio.threading.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("tr.microsoft.visualstudio.utilities.resources", "costura.tr.microsoft.visualstudio.utilities.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("tr.microsoft.visualstudio.validation.resources", "costura.tr.microsoft.visualstudio.validation.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("tr.streamjsonrpc.resources", "costura.tr.streamjsonrpc.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("tr-tr.modernwpf.controls.resources", "costura.tr-tr.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("tr-tr.modernwpf.resources", "costura.tr-tr.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("uk-ua.modernwpf.controls.resources", "costura.uk-ua.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("uk-ua.modernwpf.resources", "costura.uk-ua.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("unity.abstractions", "costura.unity.abstractions.dll.compressed");
			AssemblyLoader.symbolNames.Add("unity.abstractions", "costura.unity.abstractions.pdb.compressed");
			AssemblyLoader.assemblyNames.Add("unity.container", "costura.unity.container.dll.compressed");
			AssemblyLoader.symbolNames.Add("unity.container", "costura.unity.container.pdb.compressed");
			AssemblyLoader.assemblyNames.Add("uz-latn-uz.modernwpf.controls.resources", "costura.uz-latn-uz.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("uz-latn-uz.modernwpf.resources", "costura.uz-latn-uz.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("vi-vn.modernwpf.controls.resources", "costura.vi-vn.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("vi-vn.modernwpf.resources", "costura.vi-vn.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("zh-cn.modernwpf.controls.resources", "costura.zh-cn.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("zh-cn.modernwpf.resources", "costura.zh-cn.modernwpf.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("zh-hans.microsoft.servicehub.framework.resources", "costura.zh-hans.microsoft.servicehub.framework.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("zh-hans.microsoft.visualstudio.threading.resources", "costura.zh-hans.microsoft.visualstudio.threading.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("zh-hans.microsoft.visualstudio.utilities.resources", "costura.zh-hans.microsoft.visualstudio.utilities.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("zh-hans.microsoft.visualstudio.validation.resources", "costura.zh-hans.microsoft.visualstudio.validation.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("zh-hans.streamjsonrpc.resources", "costura.zh-hans.streamjsonrpc.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("zh-hant.microsoft.servicehub.framework.resources", "costura.zh-hant.microsoft.servicehub.framework.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("zh-hant.microsoft.visualstudio.threading.resources", "costura.zh-hant.microsoft.visualstudio.threading.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("zh-hant.microsoft.visualstudio.utilities.resources", "costura.zh-hant.microsoft.visualstudio.utilities.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("zh-hant.microsoft.visualstudio.validation.resources", "costura.zh-hant.microsoft.visualstudio.validation.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("zh-hant.streamjsonrpc.resources", "costura.zh-hant.streamjsonrpc.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("zh-tw.modernwpf.controls.resources", "costura.zh-tw.modernwpf.controls.resources.dll.compressed");
			AssemblyLoader.assemblyNames.Add("zh-tw.modernwpf.resources", "costura.zh-tw.modernwpf.resources.dll.compressed");
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000FBA4 File Offset: 0x0000DDA4
		public static void Attach()
		{
			if (Interlocked.Exchange(ref AssemblyLoader.isAttached, 1) == 1)
			{
				return;
			}
			AppDomain currentDomain = AppDomain.CurrentDomain;
			currentDomain.AssemblyResolve += AssemblyLoader.ResolveAssembly;
		}

		// Token: 0x04000913 RID: 2323
		private static object nullCacheLock = new object();

		// Token: 0x04000914 RID: 2324
		private static Dictionary<string, bool> nullCache = new Dictionary<string, bool>();

		// Token: 0x04000915 RID: 2325
		private static Dictionary<string, string> assemblyNames = new Dictionary<string, string>();

		// Token: 0x04000916 RID: 2326
		private static Dictionary<string, string> symbolNames = new Dictionary<string, string>();

		// Token: 0x04000917 RID: 2327
		private static int isAttached;
	}
}
