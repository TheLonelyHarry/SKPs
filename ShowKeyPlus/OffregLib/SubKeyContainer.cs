using System;
using System.Runtime.InteropServices.ComTypes;

namespace OffregLib
{
	// Token: 0x0200000B RID: 11
	public class SubKeyContainer
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003141 File Offset: 0x00001341
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00003149 File Offset: 0x00001349
		public string Name { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003152 File Offset: 0x00001352
		// (set) Token: 0x06000071 RID: 113 RVA: 0x0000315A File Offset: 0x0000135A
		public string Class { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003163 File Offset: 0x00001363
		// (set) Token: 0x06000073 RID: 115 RVA: 0x0000316B File Offset: 0x0000136B
		public FILETIME LastWriteTime { get; set; }
	}
}
