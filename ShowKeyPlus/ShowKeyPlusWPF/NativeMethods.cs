using System;
using System.Runtime.InteropServices;

namespace ShowKeyPlusWPF
{
	// Token: 0x02000018 RID: 24
	public class NativeMethods
	{
		// Token: 0x060000F6 RID: 246
		[DllImport("kernel32")]
		public static extern uint EnumSystemFirmwareTables(uint FirmwareTableProviderSignature, IntPtr pFirmwareTableBuffer, uint BufferSize);

		// Token: 0x060000F7 RID: 247
		[DllImport("kernel32")]
		public static extern uint GetSystemFirmwareTable(uint FirmwareTableProviderSignature, uint FirmwareTableID, IntPtr pFirmwareTableBuffer, uint BufferSize);

		// Token: 0x060000F8 RID: 248
		[DllImport("pidgenx.dll", CharSet = CharSet.Unicode, EntryPoint = "PidGenX")]
		public static extern int PidGenx(string ProductKey, string PkeyPath, string MSPID, int UnknownUsage, IntPtr ProductID, IntPtr DigitalProductID, IntPtr DigitalProductID4);

		// Token: 0x060000F9 RID: 249
		[DllImport("kernel32.dll")]
		public static extern bool AttachConsole(int dwProcessId);

		// Token: 0x060000FA RID: 250
		[DllImport("kernel32.dll")]
		public static extern IntPtr LoadLibrary(string dllToLoad);

		// Token: 0x060000FB RID: 251
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

		// Token: 0x060000FC RID: 252
		[DllImport("kernel32.dll")]
		public static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x060000FD RID: 253
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetCurrentProcess();

		// Token: 0x02000019 RID: 25
		// (Invoke) Token: 0x06000100 RID: 256
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		public delegate int PidGenX([MarshalAs(UnmanagedType.LPWStr)] string ProductKey, [MarshalAs(UnmanagedType.LPWStr)] string PkeyPath, [MarshalAs(UnmanagedType.LPWStr)] string MSPID, int UnknownUsage, IntPtr ProductID, IntPtr DigitalProductID, IntPtr DigitalProductID4);
	}
}
