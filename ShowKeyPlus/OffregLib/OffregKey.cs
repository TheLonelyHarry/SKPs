using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace OffregLib
{
	// Token: 0x0200000D RID: 13
	public class OffregKey : OffregBase
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000031B8 File Offset: 0x000013B8
		// (set) Token: 0x0600007F RID: 127 RVA: 0x000031C0 File Offset: 0x000013C0
		public string Name { get; protected set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000031C9 File Offset: 0x000013C9
		// (set) Token: 0x06000081 RID: 129 RVA: 0x000031D1 File Offset: 0x000013D1
		public string FullName { get; protected set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000031DC File Offset: 0x000013DC
		public int SubkeyCount
		{
			get
			{
				return (int)this._metadata.SubKeysCount;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000031FC File Offset: 0x000013FC
		public int ValueCount
		{
			get
			{
				return (int)this._metadata.ValuesCount;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000321C File Offset: 0x0000141C
		internal OffregKey(OffregKey parent, IntPtr ptr, string name)
		{
			this._intPtr = ptr;
			this.Name = name;
			this.FullName = ((parent == null || parent.FullName == null) ? "" : (parent.FullName + "\\")) + name;
			this._parent = parent;
			this._metadata = new QueryInfoKeyData();
			this.RefreshMetadata();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003290 File Offset: 0x00001490
		internal OffregKey(OffregKey parentKey, string name)
		{
			Win32Result win32Result = OffregNative.OpenKey(parentKey._intPtr, name, out this._intPtr);
			bool flag = win32Result > Win32Result.ERROR_SUCCESS;
			if (flag)
			{
				throw new Win32Exception((int)win32Result);
			}
			this.Name = name;
			this.FullName = ((parentKey.FullName == null) ? "" : (parentKey.FullName + "\\")) + name;
			this._parent = parentKey;
			this._metadata = new QueryInfoKeyData();
			this.RefreshMetadata();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000331C File Offset: 0x0000151C
		private void RefreshMetadata()
		{
			uint num = 0U;
			uint subKeysCount = 0U;
			uint maxSubKeyLen = 0U;
			uint maxClassLen = 0U;
			uint valuesCount = 0U;
			uint maxValueNameLen = 0U;
			uint maxValueLen = 0U;
			uint sizeSecurityDescriptor = 0U;
			System.Runtime.InteropServices.ComTypes.FILETIME lastWriteTime = default(System.Runtime.InteropServices.ComTypes.FILETIME);
			StringBuilder stringBuilder = new StringBuilder((int)num);
			Win32Result win32Result = OffregNative.QueryInfoKey(this._intPtr, stringBuilder, ref num, ref subKeysCount, ref maxSubKeyLen, ref maxClassLen, ref valuesCount, ref maxValueNameLen, ref maxValueLen, ref sizeSecurityDescriptor, ref lastWriteTime);
			bool flag = win32Result == Win32Result.ERROR_MORE_DATA;
			if (flag)
			{
				num += 1U;
				stringBuilder = new StringBuilder((int)num);
				win32Result = OffregNative.QueryInfoKey(this._intPtr, stringBuilder, ref num, ref subKeysCount, ref maxSubKeyLen, ref maxClassLen, ref valuesCount, ref maxValueNameLen, ref maxValueLen, ref sizeSecurityDescriptor, ref lastWriteTime);
				bool flag2 = win32Result > Win32Result.ERROR_SUCCESS;
				if (flag2)
				{
					throw new Win32Exception((int)win32Result);
				}
			}
			else
			{
				bool flag3 = win32Result > Win32Result.ERROR_SUCCESS;
				if (flag3)
				{
					throw new Win32Exception((int)win32Result);
				}
			}
			this._metadata.Class = stringBuilder.ToString();
			this._metadata.LastWriteTime = lastWriteTime;
			this._metadata.SubKeysCount = subKeysCount;
			this._metadata.MaxSubKeyLen = maxSubKeyLen;
			this._metadata.MaxClassLen = maxClassLen;
			this._metadata.ValuesCount = valuesCount;
			this._metadata.MaxValueNameLen = maxValueNameLen;
			this._metadata.MaxValueLen = maxValueLen;
			this._metadata.SizeSecurityDescriptor = sizeSecurityDescriptor;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003458 File Offset: 0x00001658
		public SubKeyContainer[] EnumerateSubKeys()
		{
			SubKeyContainer[] array = new SubKeyContainer[this._metadata.SubKeysCount];
			for (uint num = 0U; num < this._metadata.SubKeysCount; num += 1U)
			{
				uint capacity = this._metadata.MaxSubKeyLen + 1U;
				uint capacity2 = this._metadata.MaxClassLen + 1U;
				StringBuilder stringBuilder = new StringBuilder((int)capacity);
				StringBuilder stringBuilder2 = new StringBuilder((int)capacity2);
				System.Runtime.InteropServices.ComTypes.FILETIME lastWriteTime = default(System.Runtime.InteropServices.ComTypes.FILETIME);
				Win32Result win32Result = OffregNative.EnumKey(this._intPtr, num, stringBuilder, ref capacity, stringBuilder2, ref capacity2, ref lastWriteTime);
				bool flag = win32Result > Win32Result.ERROR_SUCCESS;
				if (flag)
				{
					throw new Win32Exception((int)win32Result);
				}
				array[(int)num] = new SubKeyContainer
				{
					Name = stringBuilder.ToString(),
					Class = stringBuilder2.ToString(),
					LastWriteTime = lastWriteTime
				};
			}
			return array;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003538 File Offset: 0x00001738
		public string[] GetSubKeyNames()
		{
			string[] array = new string[this._metadata.SubKeysCount];
			for (uint num = 0U; num < this._metadata.SubKeysCount; num += 1U)
			{
				uint capacity = this._metadata.MaxSubKeyLen + 1U;
				StringBuilder stringBuilder = new StringBuilder((int)capacity);
				Win32Result win32Result = OffregNative.EnumKey(this._intPtr, num, stringBuilder, ref capacity, null, IntPtr.Zero, IntPtr.Zero);
				bool flag = win32Result > Win32Result.ERROR_SUCCESS;
				if (flag)
				{
					throw new Win32Exception((int)win32Result);
				}
				array[(int)num] = stringBuilder.ToString();
			}
			return array;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000035CC File Offset: 0x000017CC
		public OffregKey OpenSubKey(string name)
		{
			return new OffregKey(this, name);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000035E8 File Offset: 0x000017E8
		public bool TryOpenSubKey(string name, out OffregKey key)
		{
			IntPtr ptr;
			Win32Result win32Result = OffregNative.OpenKey(this._intPtr, name, out ptr);
			bool flag = win32Result > Win32Result.ERROR_SUCCESS;
			bool result;
			if (flag)
			{
				key = null;
				result = false;
			}
			else
			{
				key = new OffregKey(this, ptr, name);
				result = true;
			}
			return result;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003624 File Offset: 0x00001824
		public OffregKey CreateSubKey(string name, RegOption options = RegOption.REG_OPTION_RESERVED)
		{
			IntPtr ptr;
			KeyDisposition keyDisposition;
			Win32Result win32Result = OffregNative.CreateKey(this._intPtr, name, null, options, IntPtr.Zero, out ptr, out keyDisposition);
			bool flag = win32Result > Win32Result.ERROR_SUCCESS;
			if (flag)
			{
				throw new Win32Exception((int)win32Result);
			}
			OffregKey result = new OffregKey(this, ptr, name);
			this.RefreshMetadata();
			return result;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003674 File Offset: 0x00001874
		public void Delete()
		{
			bool flag = this._parent == null;
			if (flag)
			{
				throw new InvalidOperationException("Cannot delete the root key");
			}
			Win32Result win32Result = OffregNative.DeleteKey(this._intPtr, null);
			bool flag2 = win32Result > Win32Result.ERROR_SUCCESS;
			if (flag2)
			{
				throw new Win32Exception((int)win32Result);
			}
			this._parent.RefreshMetadata();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000036C4 File Offset: 0x000018C4
		public void DeleteSubKey(string name)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new ArgumentNullException("name");
			}
			Win32Result win32Result = OffregNative.DeleteKey(this._intPtr, name);
			bool flag2 = win32Result > Win32Result.ERROR_SUCCESS;
			if (flag2)
			{
				throw new Win32Exception((int)win32Result);
			}
			this.RefreshMetadata();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003708 File Offset: 0x00001908
		public void DeleteSubKeyTree(string name)
		{
			using (OffregKey offregKey = this.OpenSubKey(name))
			{
				OffregKey.DeleteSubKeyTree(offregKey);
			}
			this.RefreshMetadata();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000374C File Offset: 0x0000194C
		private static void DeleteSubKeyTree(OffregKey key)
		{
			string[] subKeyNames = key.GetSubKeyNames();
			foreach (string name in subKeyNames)
			{
				try
				{
					using (OffregKey offregKey = key.OpenSubKey(name))
					{
						OffregKey.DeleteSubKeyTree(offregKey);
					}
				}
				catch (Win32Exception ex)
				{
					int nativeErrorCode = ex.NativeErrorCode;
					int num = nativeErrorCode;
					if (num != 2)
					{
						throw;
					}
				}
			}
			key.Delete();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000037E0 File Offset: 0x000019E0
		public string[] GetValueNames()
		{
			string[] array = new string[this._metadata.ValuesCount];
			for (uint num = 0U; num < this._metadata.ValuesCount; num += 1U)
			{
				uint capacity = this._metadata.MaxValueNameLen + 1U;
				StringBuilder stringBuilder = new StringBuilder((int)capacity);
				Win32Result win32Result = OffregNative.EnumValue(this._intPtr, num, stringBuilder, ref capacity, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
				bool flag = win32Result > Win32Result.ERROR_SUCCESS;
				if (flag)
				{
					throw new Win32Exception((int)win32Result);
				}
				array[(int)num] = stringBuilder.ToString();
			}
			return array;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003878 File Offset: 0x00001A78
		public ValueContainer[] EnumerateValues()
		{
			ValueContainer[] array = new ValueContainer[this._metadata.ValuesCount];
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.AllocHGlobal((int)this._metadata.MaxValueLen);
				for (uint num = 0U; num < this._metadata.ValuesCount; num += 1U)
				{
					uint capacity = this._metadata.MaxValueNameLen + 1U;
					uint maxValueLen = this._metadata.MaxValueLen;
					StringBuilder stringBuilder = new StringBuilder((int)capacity);
					RegValueType regValueType;
					Win32Result win32Result = OffregNative.EnumValue(this._intPtr, num, stringBuilder, ref capacity, out regValueType, intPtr, ref maxValueLen);
					bool flag = win32Result > Win32Result.ERROR_SUCCESS;
					if (flag)
					{
						throw new Win32Exception((int)win32Result);
					}
					byte[] array2 = new byte[maxValueLen];
					Marshal.Copy(intPtr, array2, 0, (int)maxValueLen);
					ValueContainer valueContainer = new ValueContainer();
					bool flag2 = !Enum.IsDefined(typeof(RegValueType), regValueType);
					if (flag2)
					{
						this.WarnDebugForValueType(stringBuilder.ToString(), regValueType);
						regValueType = RegValueType.REG_BINARY;
					}
					valueContainer.Name = stringBuilder.ToString();
					object data;
					valueContainer.InvalidData = !OffregHelper.TryConvertValueDataToObject(regValueType, array2, out data);
					valueContainer.Data = data;
					valueContainer.Type = regValueType;
					array[(int)num] = valueContainer;
				}
			}
			finally
			{
				bool flag3 = intPtr != IntPtr.Zero;
				if (flag3)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return array;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000039F0 File Offset: 0x00001BF0
		public RegValueType GetValueKind(string name)
		{
			RegValueType result;
			Win32Result value = OffregNative.GetValue(this._intPtr, null, name, out result, IntPtr.Zero, IntPtr.Zero);
			bool flag = value > Win32Result.ERROR_SUCCESS;
			if (flag)
			{
				throw new Win32Exception((int)value);
			}
			return result;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003A2C File Offset: 0x00001C2C
		public object GetValue(string name)
		{
			Tuple<RegValueType, byte[]> tuple = this.GetValueInternal(name);
			bool flag = !Enum.IsDefined(typeof(RegValueType), tuple.Item1);
			if (flag)
			{
				this.WarnDebugForValueType(name, tuple.Item1);
				tuple = new Tuple<RegValueType, byte[]>(RegValueType.REG_BINARY, tuple.Item2);
			}
			object result;
			OffregHelper.TryConvertValueDataToObject(tuple.Item1, tuple.Item2, out result);
			return result;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003A9C File Offset: 0x00001C9C
		public bool TryParseValue(string name, out object data)
		{
			Tuple<RegValueType, byte[]> tuple = this.GetValueInternal(name);
			bool flag = !Enum.IsDefined(typeof(RegValueType), tuple.Item1);
			if (flag)
			{
				this.WarnDebugForValueType(name, tuple.Item1);
				tuple = new Tuple<RegValueType, byte[]>(RegValueType.REG_BINARY, tuple.Item2);
			}
			return OffregHelper.TryConvertValueDataToObject(tuple.Item1, tuple.Item2, out data);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003B08 File Offset: 0x00001D08
		public bool ValueExist(string name)
		{
			uint num = 0U;
			RegValueType regValueType;
			Win32Result value = OffregNative.GetValue(this._intPtr, null, name, out regValueType, IntPtr.Zero, ref num);
			return value == Win32Result.ERROR_SUCCESS;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003B38 File Offset: 0x00001D38
		public bool SubkeyExist(string name)
		{
			IntPtr zero = IntPtr.Zero;
			bool result;
			try
			{
				Win32Result win32Result = OffregNative.OpenKey(this._intPtr, name, out zero);
				result = (win32Result == Win32Result.ERROR_SUCCESS);
			}
			finally
			{
				bool flag = zero != IntPtr.Zero;
				if (flag)
				{
					OffregNative.CloseKey(zero);
				}
			}
			return result;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003B90 File Offset: 0x00001D90
		public byte[] GetValueBytes(string name)
		{
			Tuple<RegValueType, byte[]> valueInternal = this.GetValueInternal(name);
			return valueInternal.Item2;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003BB0 File Offset: 0x00001DB0
		public void SetValue(string name, string value, RegValueType type = RegValueType.REG_SZ)
		{
			byte[] array = new byte[OffregHelper.StringEncoding.GetByteCount(value) + 2];
			OffregHelper.StringEncoding.GetBytes(value, 0, value.Length, array, 0);
			this.SetValue(name, type, array);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003BF0 File Offset: 0x00001DF0
		public void SetValue(string name, string[] values, RegValueType type = RegValueType.REG_MULTI_SZ)
		{
			foreach (string value in values)
			{
				bool flag = string.IsNullOrEmpty(value);
				if (flag)
				{
					throw new ArgumentException("No empty strings allowed");
				}
			}
			int num = 0;
			foreach (string s in values)
			{
				num += OffregHelper.StringEncoding.GetByteCount(s) + 2;
			}
			num += 2;
			byte[] array = new byte[num];
			int num2 = 0;
			for (int k = 0; k < values.Length; k++)
			{
				num2 += OffregHelper.StringEncoding.GetBytes(values[k], 0, values[k].Length, array, num2) + 2;
			}
			this.SetValue(name, type, array);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003CB4 File Offset: 0x00001EB4
		public void SetValue(string name, byte[] value, RegValueType type = RegValueType.REG_BINARY)
		{
			this.SetValue(name, type, value);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003CC4 File Offset: 0x00001EC4
		public void SetValue(string name, int value, RegValueType type = RegValueType.REG_DWORD)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			bool flag = type == RegValueType.REG_DWORD_BIG_ENDIAN;
			if (flag)
			{
				Array.Reverse(bytes);
			}
			this.SetValue(name, type, bytes);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003CF4 File Offset: 0x00001EF4
		public void SetValue(string name, long value, RegValueType type = RegValueType.REG_QWORD)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			this.SetValue(name, type, bytes);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003D14 File Offset: 0x00001F14
		private void SetValue(string name, RegValueType type, byte[] data)
		{
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.AllocHGlobal(data.Length);
				Marshal.Copy(data, 0, intPtr, data.Length);
				Win32Result win32Result = OffregNative.SetValue(this._intPtr, name, type, intPtr, (uint)data.Length);
				bool flag = win32Result > Win32Result.ERROR_SUCCESS;
				if (flag)
				{
					throw new Win32Exception((int)win32Result);
				}
			}
			finally
			{
				bool flag2 = intPtr != IntPtr.Zero;
				if (flag2)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			this.RefreshMetadata();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003D94 File Offset: 0x00001F94
		public void DeleteValue(string name)
		{
			Win32Result win32Result = OffregNative.DeleteValue(this._intPtr, name);
			bool flag = win32Result > Win32Result.ERROR_SUCCESS;
			if (flag)
			{
				throw new Win32Exception((int)win32Result);
			}
			this.RefreshMetadata();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003DC8 File Offset: 0x00001FC8
		internal Tuple<RegValueType, byte[]> GetValueInternal(string name)
		{
			uint num = 0U;
			RegValueType item;
			Win32Result value = OffregNative.GetValue(this._intPtr, null, name, out item, IntPtr.Zero, ref num);
			bool flag = value > Win32Result.ERROR_SUCCESS;
			if (flag)
			{
				throw new Win32Exception((int)value);
			}
			byte[] array = new byte[num];
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.AllocHGlobal((int)num);
				value = OffregNative.GetValue(this._intPtr, null, name, out item, intPtr, ref num);
				bool flag2 = value > Win32Result.ERROR_SUCCESS;
				if (flag2)
				{
					throw new Win32Exception((int)value);
				}
				Marshal.Copy(intPtr, array, 0, (int)num);
			}
			finally
			{
				bool flag3 = intPtr != IntPtr.Zero;
				if (flag3)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return new Tuple<RegValueType, byte[]>(item, array);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003E84 File Offset: 0x00002084
		public override void Close()
		{
			bool flag = this._intPtr != IntPtr.Zero && this._ownsPointer;
			if (flag)
			{
				Win32Result win32Result = OffregNative.CloseKey(this._intPtr);
				bool flag2 = win32Result > Win32Result.ERROR_SUCCESS;
				if (flag2)
				{
					throw new Win32Exception((int)win32Result);
				}
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003137 File Offset: 0x00001337
		public override void Dispose()
		{
			this.Close();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003ED0 File Offset: 0x000020D0
		private void WarnDebugForValueType(string valueName, RegValueType parsedType)
		{
			Debug.WriteLine(string.Concat(new string[]
			{
				"WARNING-OFFREGLIB: unknown RegValueType ",
				parsedType.ToString(),
				" converted to Binary in EnumerateValues() at key: ",
				this.FullName,
				", value: ",
				valueName
			}));
		}

		// Token: 0x04000010 RID: 16
		private OffregKey _parent;

		// Token: 0x04000011 RID: 17
		private bool _ownsPointer = true;

		// Token: 0x04000012 RID: 18
		private QueryInfoKeyData _metadata;
	}
}
