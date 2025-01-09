using System;

namespace OffregLib
{
	// Token: 0x02000012 RID: 18
	[Flags]
	public enum RegOption : uint
	{
		// Token: 0x0400003A RID: 58
		REG_OPTION_RESERVED = 0U,
		// Token: 0x0400003B RID: 59
		REG_OPTION_NON_VOLATILE = 0U,
		// Token: 0x0400003C RID: 60
		REG_OPTION_VOLATILE = 1U,
		// Token: 0x0400003D RID: 61
		REG_OPTION_CREATE_LINK = 2U,
		// Token: 0x0400003E RID: 62
		REG_OPTION_BACKUP_RESTORE = 4U,
		// Token: 0x0400003F RID: 63
		REG_OPTION_OPEN_LINK = 8U
	}
}
