using System;
using System.ComponentModel;

namespace OffregLib
{
	// Token: 0x0200000A RID: 10
	public class OffregHive : OffregBase
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003034 File Offset: 0x00001234
		// (set) Token: 0x06000067 RID: 103 RVA: 0x0000303C File Offset: 0x0000123C
		public OffregKey Root { get; private set; }

		// Token: 0x06000068 RID: 104 RVA: 0x00003045 File Offset: 0x00001245
		internal OffregHive(IntPtr hivePtr)
		{
			this._intPtr = hivePtr;
			this.Root = new OffregKey(null, this._intPtr, null);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000306C File Offset: 0x0000126C
		public void SaveHive(string targetFile, uint majorVersionTarget, uint minorVersionTarget)
		{
			Win32Result win32Result = OffregNative.SaveHive(this._intPtr, targetFile, majorVersionTarget, minorVersionTarget);
			bool flag = win32Result > Win32Result.ERROR_SUCCESS;
			if (flag)
			{
				throw new Win32Exception((int)win32Result);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003098 File Offset: 0x00001298
		public static OffregHive Create()
		{
			IntPtr hivePtr;
			Win32Result win32Result = OffregNative.CreateHive(out hivePtr);
			bool flag = win32Result > Win32Result.ERROR_SUCCESS;
			if (flag)
			{
				throw new Win32Exception((int)win32Result);
			}
			return new OffregHive(hivePtr);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000030C8 File Offset: 0x000012C8
		public static OffregHive Open(string hiveFile)
		{
			IntPtr hivePtr;
			Win32Result win32Result = OffregNative.OpenHive(hiveFile, out hivePtr);
			bool flag = win32Result > Win32Result.ERROR_SUCCESS;
			if (flag)
			{
				throw new Win32Exception((int)win32Result);
			}
			return new OffregHive(hivePtr);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000030F8 File Offset: 0x000012F8
		public override void Close()
		{
			bool flag = this._intPtr != IntPtr.Zero;
			if (flag)
			{
				Win32Result win32Result = OffregNative.CloseHive(this._intPtr);
				bool flag2 = win32Result > Win32Result.ERROR_SUCCESS;
				if (flag2)
				{
					throw new Win32Exception((int)win32Result);
				}
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003137 File Offset: 0x00001337
		public override void Dispose()
		{
			this.Close();
		}
	}
}
