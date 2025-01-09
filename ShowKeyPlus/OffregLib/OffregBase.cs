using System;

namespace OffregLib
{
	// Token: 0x02000008 RID: 8
	public abstract class OffregBase : IDisposable
	{
		// Token: 0x06000061 RID: 97
		public abstract void Close();

		// Token: 0x06000062 RID: 98
		public abstract void Dispose();

		// Token: 0x04000004 RID: 4
		protected IntPtr _intPtr;
	}
}
