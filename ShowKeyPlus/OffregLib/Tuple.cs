using System;

namespace OffregLib
{
	// Token: 0x02000016 RID: 22
	internal class Tuple<K, V>
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000043A8 File Offset: 0x000025A8
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x000043B0 File Offset: 0x000025B0
		public K Item1 { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000043B9 File Offset: 0x000025B9
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x000043C1 File Offset: 0x000025C1
		public V Item2 { get; set; }

		// Token: 0x060000F4 RID: 244 RVA: 0x00002137 File Offset: 0x00000337
		public Tuple()
		{
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000043CA File Offset: 0x000025CA
		public Tuple(K item1, V item2)
		{
			this.Item1 = item1;
			this.Item2 = item2;
		}
	}
}
