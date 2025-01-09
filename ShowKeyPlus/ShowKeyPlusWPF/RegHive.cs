using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OffregLib;

namespace ShowKeyPlusWPF
{
	// Token: 0x02000028 RID: 40
	public class RegHive
	{
		// Token: 0x0600015E RID: 350 RVA: 0x00006BB8 File Offset: 0x00004DB8
		public List<byte[]> GetReg(string temphive, bool load)
		{
			if (load)
			{
				try
				{
					string name = "Microsoft\\Windows NT\\CurrentVersion";
					using (OffregHive offregHive = OffregHive.Open(temphive))
					{
						OffregKey offregKey;
						bool flag = offregHive.Root.TryOpenSubKey(name, out offregKey);
						if (flag)
						{
							bool flag2 = !offregKey.ValueExist("UBR");
							if (flag2)
							{
								this.list.SetValue("CSDVersion", 5);
							}
							for (int i = 0; i <= this.list.Length - 1; i++)
							{
								bool flag3 = offregKey.ValueExist(this.list[i]);
								if (flag3)
								{
									object value = offregKey.GetValue(this.list[i]);
									bool flag4 = value != null && i != 1 && value.ToString().Length > 0;
									if (flag4)
									{
										bool flag5 = value.GetType() == typeof(byte[]);
										if (flag5)
										{
											this.pk.Add((byte[])value);
										}
										else
										{
											this.pk.Add(Encoding.ASCII.GetBytes(value.ToString()));
										}
									}
									else
									{
										bool flag6 = this.pk.Count == 2;
										if (flag6)
										{
											this.pk.Add((byte[])value);
										}
									}
								}
								else
								{
									this.pk.Add(null);
								}
								bool flag7 = i == 0;
								if (flag7)
								{
									List<byte[]> sourceOS = this.GetSourceOS(temphive);
									bool flag8 = sourceOS.Count > 0 && !sourceOS[0].Skip(51).Take(14).ToArray<byte>().SequenceEqual(this.pk[0].Skip(51).Take(14).ToArray<byte>());
									if (flag8)
									{
										this.pk.Add(sourceOS[0]);
										this.pk.Add(sourceOS[1]);
									}
									else
									{
										name = "Microsoft\\Internet Explorer\\Registration";
										bool flag9 = offregHive.Root.SubkeyExist(name);
										if (flag9)
										{
											bool flag10 = offregHive.Root.TryOpenSubKey(name, out offregKey);
											if (flag10)
											{
												bool flag11 = offregKey.ValueExist(this.list[0]);
												if (flag11)
												{
													object value2 = offregKey.GetValue(this.list[0]);
													bool flag12 = value2.GetType() == typeof(byte[]);
													if (flag12)
													{
														this.pk.Add((byte[])value2);
													}
												}
												else
												{
													this.pk.Add(this.pk[0]);
												}
												bool flag13 = offregKey.ValueExist(this.list[1]);
												if (flag13)
												{
													object value3 = offregKey.GetValue(this.list[1]);
													bool flag14 = value3.GetType() == typeof(byte[]);
													if (flag14)
													{
														this.pk.Add((byte[])value3);
													}
												}
											}
										}
									}
									offregKey.Close();
									name = "Microsoft\\Windows NT\\CurrentVersion";
									offregHive.Root.TryOpenSubKey(name, out offregKey);
								}
							}
						}
						offregKey.Close();
						offregKey.Dispose();
					}
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
			}
			return this.pk;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00006F50 File Offset: 0x00005150
		private List<byte[]> GetSourceOS(string temphive)
		{
			List<byte[]> list = new List<byte[]>();
			string text = "Setup\\";
			using (OffregHive offregHive = OffregHive.Open(temphive.Replace("SOFTWARE", "SYSTEM")))
			{
				OffregKey offregKey = offregHive.Root.OpenSubKey(text);
				foreach (SubKeyContainer subKeyContainer in offregKey.EnumerateSubKeys())
				{
					bool flag = subKeyContainer.Name.Contains("Source OS");
					if (flag)
					{
						text += subKeyContainer.Name;
						break;
					}
				}
				OffregKey offregKey2;
				bool flag2 = offregHive.Root.TryOpenSubKey(text, out offregKey2);
				if (flag2)
				{
					bool flag3 = offregKey2.ValueExist(this.list[0]);
					if (flag3)
					{
						object value = offregKey2.GetValue(this.list[0]);
						bool flag4 = value.GetType() == typeof(byte[]);
						if (flag4)
						{
							list.Add(value as byte[]);
						}
					}
					bool flag5 = offregKey2.ValueExist(this.list[1]);
					if (flag5)
					{
						object value2 = offregKey2.GetValue(this.list[1]);
						bool flag6 = value2.GetType() == typeof(byte[]);
						if (flag6)
						{
							list.Add(value2 as byte[]);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x040007D2 RID: 2002
		private string[] list = new string[]
		{
			"DigitalProductId",
			"DigitalProductId4",
			"ProductName",
			"ProductId",
			"BuildLabEx",
			"UBR",
			"CurrentBuild"
		};

		// Token: 0x040007D3 RID: 2003
		private List<byte[]> pk = new List<byte[]>();
	}
}
