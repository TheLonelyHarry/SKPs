using System;
using System.Reflection;
using System.Resources;

namespace ShowKeyPlusWPF
{
	// Token: 0x02000021 RID: 33
	public class Resource
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00005418 File Offset: 0x00003618
		public string GetResourceString(string res)
		{
			Assembly assembly = base.GetType().Assembly;
			ResourceManager resourceManager = new ResourceManager("ShowKeyPlus.Strings.Resource", assembly);
			return resourceManager.GetString(res);
		}
	}
}
