using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace ShowKeyPlusWPF
{
	// Token: 0x02000022 RID: 34
	public class OEM
	{
		// Token: 0x06000146 RID: 326 RVA: 0x0000544C File Offset: 0x0000364C
		public string GetKey()
		{
			string text = string.Empty;
			string text2 = string.Empty;
			Resource resource = new Resource();
			string result;
			try
			{
				byte[] array;
				bool flag = OEM.CheckOEM(out array, "MSDM");
				if (flag)
				{
					try
					{
						text2 = "MSDM";
						int num = BitConverter.ToInt32(array, 52);
						bool flag2 = num > 0;
						if (flag2)
						{
							text = Encoding.ASCII.GetString(array, 56, num);
							return text;
						}
					}
					catch (Exception ex)
					{
						return ex.Message;
					}
				}
				bool flag3 = OEM.CheckOEM(out array, "SLIC") && string.IsNullOrEmpty(text);
				if (flag3)
				{
					text2 = "SLIC";
					text = BitConverter.ToString(array, 226, 4);
					string text3 = text;
					string a = text3;
					if (!(a == "00-00-00-00"))
					{
						if (!(a == "01-00-02-00"))
						{
							if (!(a == "02-00-02-00"))
							{
								if (!(a == "03-00-02-00"))
								{
									result = resource.GetResourceString("txtUnknownOEM");
								}
								else
								{
									result = resource.GetResourceString("txtWinServerR2OEM");
								}
							}
							else
							{
								result = resource.GetResourceString("txtWinServerOEM");
							}
						}
						else
						{
							result = resource.GetResourceString("txtWin7OEM");
						}
					}
					else
					{
						result = resource.GetResourceString("txtWinVistaOEM");
					}
				}
				else
				{
					result = resource.GetResourceString("txtNotPresentOEM");
				}
			}
			catch (Exception ex2)
			{
				result = string.Concat(new string[]
				{
					"Error: Getting OEM key from ",
					text2,
					" table",
					Environment.NewLine,
					"Key data: ",
					text,
					Environment.NewLine,
					"Source:",
					ex2.Source,
					Environment.NewLine,
					ex2.Message
				});
			}
			return result;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005648 File Offset: 0x00003848
		private static bool CheckOEM(out byte[] buffer, string table)
		{
			try
			{
				byte[] bytes = Encoding.ASCII.GetBytes("ACPI");
				string value = string.Concat(Array.ConvertAll<byte, string>(bytes, (byte x) => x.ToString("X2")));
				uint firmwareTableProviderSignature = Convert.ToUInt32(value, 16);
				uint num = NativeMethods.EnumSystemFirmwareTables(firmwareTableProviderSignature, IntPtr.Zero, 0U);
				IntPtr intPtr = Marshal.AllocHGlobal(Convert.ToInt32(num));
				buffer = new byte[num];
				NativeMethods.EnumSystemFirmwareTables(firmwareTableProviderSignature, intPtr, num);
				Marshal.Copy(intPtr, buffer, 0, buffer.Length);
				Marshal.FreeHGlobal(intPtr);
				bool flag = Encoding.ASCII.GetString(buffer).Contains(table);
				if (flag)
				{
					byte[] bytes2 = Encoding.ASCII.GetBytes(table);
					Array.Reverse(bytes2);
					uint firmwareTableID = Convert.ToUInt32(string.Concat(Array.ConvertAll<byte, string>(bytes2, (byte x) => x.ToString("X2"))), 16);
					num = NativeMethods.GetSystemFirmwareTable(firmwareTableProviderSignature, firmwareTableID, IntPtr.Zero, 0U);
					buffer = new byte[num];
					intPtr = Marshal.AllocHGlobal(Convert.ToInt32(num));
					NativeMethods.GetSystemFirmwareTable(firmwareTableProviderSignature, firmwareTableID, intPtr, num);
					Marshal.Copy(intPtr, buffer, 0, buffer.Length);
					Marshal.FreeHGlobal(intPtr);
					return true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: checkOEM" + Environment.NewLine + ex.Message, "Windows Key information", MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			buffer = null;
			return false;
		}
	}
}
