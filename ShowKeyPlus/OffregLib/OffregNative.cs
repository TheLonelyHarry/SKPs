using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace OffregLib
{
	// Token: 0x02000014 RID: 20
	public static class OffregNative
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003F24 File Offset: 0x00002124
		private static bool Is64BitProcess
		{
			get
			{
				return IntPtr.Size == 8;
			}
		}

		// Token: 0x060000A4 RID: 164
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "ORCreateHive")]
		private static extern Win32Result CreateHive32(out IntPtr rootKeyHandle);

		// Token: 0x060000A5 RID: 165
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "ORCreateHive")]
		private static extern Win32Result CreateHive64(out IntPtr rootKeyHandle);

		// Token: 0x060000A6 RID: 166 RVA: 0x00003F40 File Offset: 0x00002140
		public static Win32Result CreateHive(out IntPtr rootKeyHandle)
		{
			return OffregNative.Is64BitProcess ? OffregNative.CreateHive64(out rootKeyHandle) : OffregNative.CreateHive32(out rootKeyHandle);
		}

		// Token: 0x060000A7 RID: 167
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "OROpenHive")]
		private static extern Win32Result OpenHive32(string path, out IntPtr rootKeyHandle);

		// Token: 0x060000A8 RID: 168
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "OROpenHive")]
		private static extern Win32Result OpenHive64(string path, out IntPtr rootKeyHandle);

		// Token: 0x060000A9 RID: 169 RVA: 0x00003F68 File Offset: 0x00002168
		public static Win32Result OpenHive(string path, out IntPtr rootKeyHandle)
		{
			return OffregNative.Is64BitProcess ? OffregNative.OpenHive64(path, out rootKeyHandle) : OffregNative.OpenHive32(path, out rootKeyHandle);
		}

		// Token: 0x060000AA RID: 170
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "ORCloseHive")]
		private static extern Win32Result CloseHive32(IntPtr rootKeyHandle);

		// Token: 0x060000AB RID: 171
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "ORCloseHive")]
		private static extern Win32Result CloseHive64(IntPtr rootKeyHandle);

		// Token: 0x060000AC RID: 172 RVA: 0x00003F94 File Offset: 0x00002194
		public static Win32Result CloseHive(IntPtr rootKeyHandle)
		{
			return OffregNative.Is64BitProcess ? OffregNative.CloseHive64(rootKeyHandle) : OffregNative.CloseHive32(rootKeyHandle);
		}

		// Token: 0x060000AD RID: 173
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "ORSaveHive")]
		private static extern Win32Result SaveHive32(IntPtr rootKeyHandle, string path, uint dwOsMajorVersion, uint dwOsMinorVersion);

		// Token: 0x060000AE RID: 174
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "ORSaveHive")]
		private static extern Win32Result SaveHive64(IntPtr rootKeyHandle, string path, uint dwOsMajorVersion, uint dwOsMinorVersion);

		// Token: 0x060000AF RID: 175 RVA: 0x00003FBC File Offset: 0x000021BC
		public static Win32Result SaveHive(IntPtr rootKeyHandle, string path, uint dwOsMajorVersion, uint dwOsMinorVersion)
		{
			return OffregNative.Is64BitProcess ? OffregNative.SaveHive64(rootKeyHandle, path, dwOsMajorVersion, dwOsMinorVersion) : OffregNative.SaveHive32(rootKeyHandle, path, dwOsMajorVersion, dwOsMinorVersion);
		}

		// Token: 0x060000B0 RID: 176
		[DllImport("offreg.dll", EntryPoint = "ORCloseKey")]
		private static extern Win32Result CloseKey32(IntPtr hKey);

		// Token: 0x060000B1 RID: 177
		[DllImport("offreg.dll", EntryPoint = "ORCloseKey")]
		private static extern Win32Result CloseKey64(IntPtr hKey);

		// Token: 0x060000B2 RID: 178 RVA: 0x00003FEC File Offset: 0x000021EC
		public static Win32Result CloseKey(IntPtr hKey)
		{
			return OffregNative.Is64BitProcess ? OffregNative.CloseKey64(hKey) : OffregNative.CloseKey32(hKey);
		}

		// Token: 0x060000B3 RID: 179
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "ORCreateKey")]
		private static extern Win32Result CreateKey32(IntPtr hKey, string lpSubKey, string lpClass, RegOption dwOptions, IntPtr lpSecurityDescriptor, out IntPtr phkResult, out KeyDisposition lpdwDisposition);

		// Token: 0x060000B4 RID: 180
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "ORCreateKey")]
		private static extern Win32Result CreateKey64(IntPtr hKey, string lpSubKey, string lpClass, RegOption dwOptions, IntPtr lpSecurityDescriptor, out IntPtr phkResult, out KeyDisposition lpdwDisposition);

		// Token: 0x060000B5 RID: 181 RVA: 0x00004014 File Offset: 0x00002214
		public static Win32Result CreateKey(IntPtr hKey, string lpSubKey, string lpClass, RegOption dwOptions, IntPtr lpSecurityDescriptor, out IntPtr phkResult, out KeyDisposition lpdwDisposition)
		{
			return OffregNative.Is64BitProcess ? OffregNative.CreateKey64(hKey, lpSubKey, lpClass, dwOptions, lpSecurityDescriptor, out phkResult, out lpdwDisposition) : OffregNative.CreateKey32(hKey, lpSubKey, lpClass, dwOptions, lpSecurityDescriptor, out phkResult, out lpdwDisposition);
		}

		// Token: 0x060000B6 RID: 182
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "ORDeleteKey")]
		private static extern Win32Result DeleteKey32(IntPtr hKey, string lpSubKey);

		// Token: 0x060000B7 RID: 183
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "ORDeleteKey")]
		private static extern Win32Result DeleteKey64(IntPtr hKey, string lpSubKey);

		// Token: 0x060000B8 RID: 184 RVA: 0x00004050 File Offset: 0x00002250
		public static Win32Result DeleteKey(IntPtr hKey, string lpSubKey)
		{
			return OffregNative.Is64BitProcess ? OffregNative.DeleteKey64(hKey, lpSubKey) : OffregNative.DeleteKey32(hKey, lpSubKey);
		}

		// Token: 0x060000B9 RID: 185
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "ORDeleteValue")]
		private static extern Win32Result DeleteValue32(IntPtr hKey, string lpValueName);

		// Token: 0x060000BA RID: 186
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "ORDeleteValue")]
		private static extern Win32Result DeleteValue64(IntPtr hKey, string lpValueName);

		// Token: 0x060000BB RID: 187 RVA: 0x0000407C File Offset: 0x0000227C
		public static Win32Result DeleteValue(IntPtr hKey, string lpValueName)
		{
			return OffregNative.Is64BitProcess ? OffregNative.DeleteValue64(hKey, lpValueName) : OffregNative.DeleteValue32(hKey, lpValueName);
		}

		// Token: 0x060000BC RID: 188
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "OREnumKey")]
		private static extern Win32Result EnumKey32(IntPtr hKey, uint dwIndex, StringBuilder lpName, ref uint lpcchName, StringBuilder lpClass, ref uint lpcchClass, ref System.Runtime.InteropServices.ComTypes.FILETIME lpftLastWriteTime);

		// Token: 0x060000BD RID: 189
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "OREnumKey")]
		private static extern Win32Result EnumKey64(IntPtr hKey, uint dwIndex, StringBuilder lpName, ref uint lpcchName, StringBuilder lpClass, ref uint lpcchClass, ref System.Runtime.InteropServices.ComTypes.FILETIME lpftLastWriteTime);

		// Token: 0x060000BE RID: 190 RVA: 0x000040A8 File Offset: 0x000022A8
		public static Win32Result EnumKey(IntPtr hKey, uint dwIndex, StringBuilder lpName, ref uint lpcchName, StringBuilder lpClass, ref uint lpcchClass, ref System.Runtime.InteropServices.ComTypes.FILETIME lpftLastWriteTime)
		{
			return OffregNative.Is64BitProcess ? OffregNative.EnumKey64(hKey, dwIndex, lpName, ref lpcchName, lpClass, ref lpcchClass, ref lpftLastWriteTime) : OffregNative.EnumKey32(hKey, dwIndex, lpName, ref lpcchName, lpClass, ref lpcchClass, ref lpftLastWriteTime);
		}

		// Token: 0x060000BF RID: 191
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "OREnumKey")]
		private static extern Win32Result EnumKey32(IntPtr hKey, uint dwIndex, StringBuilder lpName, ref uint lpcchName, StringBuilder lpClass, IntPtr lpcchClass, IntPtr lpftLastWriteTime);

		// Token: 0x060000C0 RID: 192
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "OREnumKey")]
		private static extern Win32Result EnumKey64(IntPtr hKey, uint dwIndex, StringBuilder lpName, ref uint lpcchName, StringBuilder lpClass, IntPtr lpcchClass, IntPtr lpftLastWriteTime);

		// Token: 0x060000C1 RID: 193 RVA: 0x000040E4 File Offset: 0x000022E4
		public static Win32Result EnumKey(IntPtr hKey, uint dwIndex, StringBuilder lpName, ref uint lpcchName, StringBuilder lpClass, IntPtr lpcchClass, IntPtr lpftLastWriteTime)
		{
			return OffregNative.Is64BitProcess ? OffregNative.EnumKey64(hKey, dwIndex, lpName, ref lpcchName, lpClass, lpcchClass, lpftLastWriteTime) : OffregNative.EnumKey32(hKey, dwIndex, lpName, ref lpcchName, lpClass, lpcchClass, lpftLastWriteTime);
		}

		// Token: 0x060000C2 RID: 194
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "OREnumValue")]
		private static extern Win32Result EnumValue32(IntPtr hKey, uint dwIndex, StringBuilder lpValueName, ref uint lpcchValueName, out RegValueType lpType, IntPtr lpData, ref uint lpcbData);

		// Token: 0x060000C3 RID: 195
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "OREnumValue")]
		private static extern Win32Result EnumValue64(IntPtr hKey, uint dwIndex, StringBuilder lpValueName, ref uint lpcchValueName, out RegValueType lpType, IntPtr lpData, ref uint lpcbData);

		// Token: 0x060000C4 RID: 196 RVA: 0x00004120 File Offset: 0x00002320
		public static Win32Result EnumValue(IntPtr hKey, uint dwIndex, StringBuilder lpValueName, ref uint lpcchValueName, out RegValueType lpType, IntPtr lpData, ref uint lpcbData)
		{
			return OffregNative.Is64BitProcess ? OffregNative.EnumValue64(hKey, dwIndex, lpValueName, ref lpcchValueName, out lpType, lpData, ref lpcbData) : OffregNative.EnumValue32(hKey, dwIndex, lpValueName, ref lpcchValueName, out lpType, lpData, ref lpcbData);
		}

		// Token: 0x060000C5 RID: 197
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "OREnumValue")]
		private static extern Win32Result EnumValue32(IntPtr hKey, uint dwIndex, StringBuilder lpValueName, ref uint lpcchValueName, IntPtr lpType, IntPtr lpData, IntPtr lpcbData);

		// Token: 0x060000C6 RID: 198
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "OREnumValue")]
		private static extern Win32Result EnumValue64(IntPtr hKey, uint dwIndex, StringBuilder lpValueName, ref uint lpcchValueName, IntPtr lpType, IntPtr lpData, IntPtr lpcbData);

		// Token: 0x060000C7 RID: 199 RVA: 0x0000415C File Offset: 0x0000235C
		public static Win32Result EnumValue(IntPtr hKey, uint dwIndex, StringBuilder lpValueName, ref uint lpcchValueName, IntPtr lpType, IntPtr lpData, IntPtr lpcbData)
		{
			return OffregNative.Is64BitProcess ? OffregNative.EnumValue64(hKey, dwIndex, lpValueName, ref lpcchValueName, lpType, lpData, lpcbData) : OffregNative.EnumValue32(hKey, dwIndex, lpValueName, ref lpcchValueName, lpType, lpData, lpcbData);
		}

		// Token: 0x060000C8 RID: 200
		[DllImport("offreg.dll", EntryPoint = "ORGetKeySecurity")]
		private static extern Win32Result GetKeySecurity32(IntPtr hKey, SECURITY_INFORMATION securityInformation, IntPtr pSecurityDescriptor, ref uint lpcbSecurityDescriptor);

		// Token: 0x060000C9 RID: 201
		[DllImport("offreg.dll", EntryPoint = "ORGetKeySecurity")]
		private static extern Win32Result GetKeySecurity64(IntPtr hKey, SECURITY_INFORMATION securityInformation, IntPtr pSecurityDescriptor, ref uint lpcbSecurityDescriptor);

		// Token: 0x060000CA RID: 202 RVA: 0x00004198 File Offset: 0x00002398
		public static Win32Result GetKeySecurity(IntPtr hKey, SECURITY_INFORMATION securityInformation, IntPtr pSecurityDescriptor, ref uint lpcbSecurityDescriptor)
		{
			return OffregNative.Is64BitProcess ? OffregNative.GetKeySecurity64(hKey, securityInformation, pSecurityDescriptor, ref lpcbSecurityDescriptor) : OffregNative.GetKeySecurity32(hKey, securityInformation, pSecurityDescriptor, ref lpcbSecurityDescriptor);
		}

		// Token: 0x060000CB RID: 203
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "ORGetValue")]
		private static extern Win32Result GetValue32(IntPtr hKey, string lpSubKey, string lpValue, out RegValueType pdwType, IntPtr pvData, ref uint pcbData);

		// Token: 0x060000CC RID: 204
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "ORGetValue")]
		private static extern Win32Result GetValue64(IntPtr hKey, string lpSubKey, string lpValue, out RegValueType pdwType, IntPtr pvData, ref uint pcbData);

		// Token: 0x060000CD RID: 205 RVA: 0x000041C8 File Offset: 0x000023C8
		public static Win32Result GetValue(IntPtr hKey, string lpSubKey, string lpValue, out RegValueType pdwType, IntPtr pvData, ref uint pcbData)
		{
			return OffregNative.Is64BitProcess ? OffregNative.GetValue64(hKey, lpSubKey, lpValue, out pdwType, pvData, ref pcbData) : OffregNative.GetValue32(hKey, lpSubKey, lpValue, out pdwType, pvData, ref pcbData);
		}

		// Token: 0x060000CE RID: 206
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "ORGetValue")]
		private static extern Win32Result GetValue32(IntPtr hKey, string lpSubKey, string lpValue, out RegValueType pdwType, IntPtr pvData, IntPtr pcbData);

		// Token: 0x060000CF RID: 207
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "ORGetValue")]
		private static extern Win32Result GetValue64(IntPtr hKey, string lpSubKey, string lpValue, out RegValueType pdwType, IntPtr pvData, IntPtr pcbData);

		// Token: 0x060000D0 RID: 208 RVA: 0x00004200 File Offset: 0x00002400
		public static Win32Result GetValue(IntPtr hKey, string lpSubKey, string lpValue, out RegValueType pdwType, IntPtr pvData, IntPtr pcbData)
		{
			return OffregNative.Is64BitProcess ? OffregNative.GetValue64(hKey, lpSubKey, lpValue, out pdwType, pvData, pcbData) : OffregNative.GetValue32(hKey, lpSubKey, lpValue, out pdwType, pvData, pcbData);
		}

		// Token: 0x060000D1 RID: 209
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "OROpenKey")]
		private static extern Win32Result OpenKey32(IntPtr hKey, string lpSubKey, out IntPtr phkResult);

		// Token: 0x060000D2 RID: 210
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "OROpenKey")]
		private static extern Win32Result OpenKey64(IntPtr hKey, string lpSubKey, out IntPtr phkResult);

		// Token: 0x060000D3 RID: 211 RVA: 0x00004238 File Offset: 0x00002438
		public static Win32Result OpenKey(IntPtr hKey, string lpSubKey, out IntPtr phkResult)
		{
			return OffregNative.Is64BitProcess ? OffregNative.OpenKey64(hKey, lpSubKey, out phkResult) : OffregNative.OpenKey32(hKey, lpSubKey, out phkResult);
		}

		// Token: 0x060000D4 RID: 212
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "ORQueryInfoKey")]
		private static extern Win32Result QueryInfoKey32(IntPtr hKey, StringBuilder lpClass, ref uint lpcchClass, ref uint lpcSubKeys, ref uint lpcbMaxSubKeyLen, ref uint lpcbMaxClassLen, ref uint lpcValues, ref uint lpcbMaxValueNameLen, ref uint lpcbMaxValueLen, ref uint lpcbSecurityDescriptor, ref System.Runtime.InteropServices.ComTypes.FILETIME lpftLastWriteTime);

		// Token: 0x060000D5 RID: 213
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "ORQueryInfoKey")]
		private static extern Win32Result QueryInfoKey64(IntPtr hKey, StringBuilder lpClass, ref uint lpcchClass, ref uint lpcSubKeys, ref uint lpcbMaxSubKeyLen, ref uint lpcbMaxClassLen, ref uint lpcValues, ref uint lpcbMaxValueNameLen, ref uint lpcbMaxValueLen, ref uint lpcbSecurityDescriptor, ref System.Runtime.InteropServices.ComTypes.FILETIME lpftLastWriteTime);

		// Token: 0x060000D6 RID: 214 RVA: 0x00004264 File Offset: 0x00002464
		public static Win32Result QueryInfoKey(IntPtr hKey, StringBuilder lpClass, ref uint lpcchClass, ref uint lpcSubKeys, ref uint lpcbMaxSubKeyLen, ref uint lpcbMaxClassLen, ref uint lpcValues, ref uint lpcbMaxValueNameLen, ref uint lpcbMaxValueLen, ref uint lpcbSecurityDescriptor, ref System.Runtime.InteropServices.ComTypes.FILETIME lpftLastWriteTime)
		{
			return OffregNative.Is64BitProcess ? OffregNative.QueryInfoKey64(hKey, lpClass, ref lpcchClass, ref lpcSubKeys, ref lpcbMaxSubKeyLen, ref lpcbMaxClassLen, ref lpcValues, ref lpcbMaxValueNameLen, ref lpcbMaxValueLen, ref lpcbSecurityDescriptor, ref lpftLastWriteTime) : OffregNative.QueryInfoKey32(hKey, lpClass, ref lpcchClass, ref lpcSubKeys, ref lpcbMaxSubKeyLen, ref lpcbMaxClassLen, ref lpcValues, ref lpcbMaxValueNameLen, ref lpcbMaxValueLen, ref lpcbSecurityDescriptor, ref lpftLastWriteTime);
		}

		// Token: 0x060000D7 RID: 215
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "ORSetValue")]
		private static extern Win32Result SetValue32(IntPtr hKey, string lpValueName, RegValueType dwType, IntPtr lpData, uint cbData);

		// Token: 0x060000D8 RID: 216
		[DllImport("offreg.dll", CharSet = CharSet.Unicode, EntryPoint = "ORSetValue")]
		private static extern Win32Result SetValue64(IntPtr hKey, string lpValueName, RegValueType dwType, IntPtr lpData, uint cbData);

		// Token: 0x060000D9 RID: 217 RVA: 0x000042B0 File Offset: 0x000024B0
		public static Win32Result SetValue(IntPtr hKey, string lpValueName, RegValueType dwType, IntPtr lpData, uint cbData)
		{
			return OffregNative.Is64BitProcess ? OffregNative.SetValue64(hKey, lpValueName, dwType, lpData, cbData) : OffregNative.SetValue32(hKey, lpValueName, dwType, lpData, cbData);
		}

		// Token: 0x060000DA RID: 218
		[DllImport("offreg.dll", EntryPoint = "ORSetKeySecurity")]
		private static extern Win32Result SetKeySecurity32(IntPtr hKey, SECURITY_INFORMATION securityInformation, IntPtr pSecurityDescriptor);

		// Token: 0x060000DB RID: 219
		[DllImport("offreg.dll", EntryPoint = "ORSetKeySecurity")]
		private static extern Win32Result SetKeySecurity64(IntPtr hKey, SECURITY_INFORMATION securityInformation, IntPtr pSecurityDescriptor);

		// Token: 0x060000DC RID: 220 RVA: 0x000042E4 File Offset: 0x000024E4
		public static Win32Result SetKeySecurity(IntPtr hKey, SECURITY_INFORMATION securityInformation, IntPtr pSecurityDescriptor)
		{
			return OffregNative.Is64BitProcess ? OffregNative.SetKeySecurity64(hKey, securityInformation, pSecurityDescriptor) : OffregNative.SetKeySecurity32(hKey, securityInformation, pSecurityDescriptor);
		}

		// Token: 0x0400004A RID: 74
		private const string OffRegDllName32 = "offreg.dll";

		// Token: 0x0400004B RID: 75
		private const string OffRegDllName64 = "offreg.dll";
	}
}
