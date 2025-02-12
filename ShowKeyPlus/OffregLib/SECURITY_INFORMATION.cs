﻿using System;

namespace OffregLib
{
	// Token: 0x02000013 RID: 19
	public enum SECURITY_INFORMATION : uint
	{
		// Token: 0x04000041 RID: 65
		OWNER_SECURITY_INFORMATION = 1U,
		// Token: 0x04000042 RID: 66
		GROUP_SECURITY_INFORMATION,
		// Token: 0x04000043 RID: 67
		DACL_SECURITY_INFORMATION = 4U,
		// Token: 0x04000044 RID: 68
		SACL_SECURITY_INFORMATION = 8U,
		// Token: 0x04000045 RID: 69
		LABEL_SECURITY_INFORMATION = 16U,
		// Token: 0x04000046 RID: 70
		PROTECTED_DACL_SECURITY_INFORMATION = 2147483648U,
		// Token: 0x04000047 RID: 71
		PROTECTED_SACL_SECURITY_INFORMATION = 1073741824U,
		// Token: 0x04000048 RID: 72
		UNPROTECTED_DACL_SECURITY_INFORMATION = 536870912U,
		// Token: 0x04000049 RID: 73
		UNPROTECTED_SACL_SECURITY_INFORMATION = 268435456U
	}
}
