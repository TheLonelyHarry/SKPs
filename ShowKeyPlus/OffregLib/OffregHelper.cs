using System;
using System.Text;

namespace OffregLib
{
	// Token: 0x02000009 RID: 9
	internal static class OffregHelper
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002E04 File Offset: 0x00001004
		public static Encoding StringEncoding
		{
			get
			{
				return Encoding.Unicode;
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002E1C File Offset: 0x0000101C
		public static bool TryConvertValueDataToObject(RegValueType type, byte[] data, out object parsedData)
		{
			parsedData = data;
			bool result;
			switch (type)
			{
			case RegValueType.REG_NONE:
				result = false;
				break;
			case RegValueType.REG_SZ:
			case RegValueType.REG_EXPAND_SZ:
			case RegValueType.REG_LINK:
			{
				bool flag = data.Length % 2 != 0;
				if (flag)
				{
					result = false;
				}
				else
				{
					int num = 0;
					while (data.Length > num + 2 && (data[num] != 0 || data[num + 1] > 0))
					{
						num += 2;
					}
					parsedData = OffregHelper.StringEncoding.GetString(data, 0, num);
					result = true;
				}
				break;
			}
			case RegValueType.REG_BINARY:
				result = true;
				break;
			case RegValueType.REG_DWORD:
			{
				bool flag2 = data.Length != 4;
				if (flag2)
				{
					result = false;
				}
				else
				{
					parsedData = BitConverter.ToInt32(data, 0);
					result = true;
				}
				break;
			}
			case RegValueType.REG_DWORD_BIG_ENDIAN:
			{
				bool flag3 = data.Length != 4;
				if (flag3)
				{
					result = false;
				}
				else
				{
					Array.Reverse(data);
					parsedData = BitConverter.ToInt32(data, 0);
					result = true;
				}
				break;
			}
			case RegValueType.REG_MULTI_SZ:
			{
				bool flag4 = data.Length % 2 != 0;
				if (flag4)
				{
					result = false;
				}
				else
				{
					bool flag5 = data.Length == 0;
					if (flag5)
					{
						result = false;
					}
					else
					{
						bool flag6 = data.Length == 2 && data[0] == 0 && data[1] == 0;
						if (flag6)
						{
							parsedData = new string[0];
							result = true;
						}
						else
						{
							bool flag7 = data[data.Length - 4] != 0 || data[data.Length - 3] != 0 || data[data.Length - 2] != 0 || data[data.Length - 1] > 0;
							if (flag7)
							{
								result = false;
							}
							else
							{
								string @string = OffregHelper.StringEncoding.GetString(data, 0, data.Length - 4);
								parsedData = @string.Split(new char[1]);
								result = true;
							}
						}
					}
				}
				break;
			}
			case RegValueType.REG_RESOURCE_LIST:
				result = true;
				break;
			case RegValueType.REG_FULL_RESOURCE_DESCRIPTOR:
				result = true;
				break;
			case RegValueType.REG_RESOURCE_REQUIREMENTS_LIST:
				result = true;
				break;
			case RegValueType.REG_QWORD:
			{
				bool flag8 = data.Length != 8;
				if (flag8)
				{
					result = false;
				}
				else
				{
					parsedData = BitConverter.ToInt64(data, 0);
					result = true;
				}
				break;
			}
			default:
				throw new ArgumentOutOfRangeException("TryConvertValueDataToObject was given an invalid RegValueType: " + type.ToString());
			}
			return result;
		}

		// Token: 0x04000005 RID: 5
		public const int SingleCharBytes = 2;
	}
}
