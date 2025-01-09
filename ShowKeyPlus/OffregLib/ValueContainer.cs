using System;

namespace OffregLib
{
	// Token: 0x0200000C RID: 12
	public class ValueContainer
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003174 File Offset: 0x00001374
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000317C File Offset: 0x0000137C
		public string Name { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003185 File Offset: 0x00001385
		// (set) Token: 0x06000078 RID: 120 RVA: 0x0000318D File Offset: 0x0000138D
		public object Data { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003196 File Offset: 0x00001396
		// (set) Token: 0x0600007A RID: 122 RVA: 0x0000319E File Offset: 0x0000139E
		public RegValueType Type { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000031A7 File Offset: 0x000013A7
		// (set) Token: 0x0600007C RID: 124 RVA: 0x000031AF File Offset: 0x000013AF
		public bool InvalidData { get; set; }
	}
}
