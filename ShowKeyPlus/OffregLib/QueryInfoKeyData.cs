using System;
using System.Runtime.InteropServices.ComTypes;

namespace OffregLib
{
	// Token: 0x02000015 RID: 21
	internal class QueryInfoKeyData
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000430F File Offset: 0x0000250F
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00004317 File Offset: 0x00002517
		public string Class { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004320 File Offset: 0x00002520
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x00004328 File Offset: 0x00002528
		public uint SubKeysCount { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004331 File Offset: 0x00002531
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00004339 File Offset: 0x00002539
		public uint MaxSubKeyLen { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004342 File Offset: 0x00002542
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x0000434A File Offset: 0x0000254A
		public uint MaxClassLen { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004353 File Offset: 0x00002553
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x0000435B File Offset: 0x0000255B
		public uint ValuesCount { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004364 File Offset: 0x00002564
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x0000436C File Offset: 0x0000256C
		public uint MaxValueNameLen { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004375 File Offset: 0x00002575
		// (set) Token: 0x060000EA RID: 234 RVA: 0x0000437D File Offset: 0x0000257D
		public uint MaxValueLen { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004386 File Offset: 0x00002586
		// (set) Token: 0x060000EC RID: 236 RVA: 0x0000438E File Offset: 0x0000258E
		public uint SizeSecurityDescriptor { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004397 File Offset: 0x00002597
		// (set) Token: 0x060000EE RID: 238 RVA: 0x0000439F File Offset: 0x0000259F
		public FILETIME LastWriteTime { get; set; }
	}
}
