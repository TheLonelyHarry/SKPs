using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Win32;
using ModernWpf.Controls;
using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using ShowKeyPlus;
using SourceChord.FluentWPF;

namespace ShowKeyPlusWPF.ViewModel
{
	// Token: 0x02000037 RID: 55
	public class MainViewModel : BindableBase
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000BC3F File Offset: 0x00009E3F
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000BC48 File Offset: 0x00009E48
		public ICommand ShowCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<ShowCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				if (object.Equals(this.<ShowCommand>k__BackingField, value))
				{
					return;
				}
				this.<ShowCommand>k__BackingField = value;
				this.OnPropertyChanged(<>PropertyChangedEventArgs.ShowCommand);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000BC78 File Offset: 0x00009E78
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000BC80 File Offset: 0x00009E80
		public ICommand HideCommand
		{
			[CompilerGenerated]
			get
			{
				return this.<HideCommand>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				if (object.Equals(this.<HideCommand>k__BackingField, value))
				{
					return;
				}
				this.<HideCommand>k__BackingField = value;
				this.OnPropertyChanged(<>PropertyChangedEventArgs.HideCommand);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000BCB0 File Offset: 0x00009EB0
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000BCB8 File Offset: 0x00009EB8
		public ICommand HideOriginal
		{
			[CompilerGenerated]
			get
			{
				return this.<HideOriginal>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				if (object.Equals(this.<HideOriginal>k__BackingField, value))
				{
					return;
				}
				this.<HideOriginal>k__BackingField = value;
				this.OnPropertyChanged(<>PropertyChangedEventArgs.HideOriginal);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000BCE8 File Offset: 0x00009EE8
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000BCF0 File Offset: 0x00009EF0
		public ICommand HideOEMKey
		{
			[CompilerGenerated]
			get
			{
				return this.<HideOEMKey>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				if (object.Equals(this.<HideOEMKey>k__BackingField, value))
				{
					return;
				}
				this.<HideOEMKey>k__BackingField = value;
				this.OnPropertyChanged(<>PropertyChangedEventArgs.HideOEMKey);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000BD20 File Offset: 0x00009F20
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000BD28 File Offset: 0x00009F28
		public ICommand ShowDialog
		{
			[CompilerGenerated]
			get
			{
				return this.<ShowDialog>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				if (object.Equals(this.<ShowDialog>k__BackingField, value))
				{
					return;
				}
				this.<ShowDialog>k__BackingField = value;
				this.OnPropertyChanged(<>PropertyChangedEventArgs.ShowDialog);
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000BD58 File Offset: 0x00009F58
		public MainViewModel()
		{
			SystemEvents.UserPreferenceChanged += this.SystemEvents_UserPreferenceChanged;
			this.productKeyInfo = new ProductKeyInfoModel.ProductKeyInfo();
			this.ShowCommand = new DelegateCommand(new Action(this.ShowMethod));
			this.HideCommand = new DelegateCommand(new Action(this.HideMethod));
			this.HideOriginal = new DelegateCommand(new Action(this.HideOriginalKey));
			this.HideOEMKey = new DelegateCommand(new Action(this.HideOEMMethod));
			this.ShowDialog = new DelegateCommand(new Action(this.ShowDialogMethod));
			this.cts = new CancellationTokenSource();
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000BE50 File Offset: 0x0000A050
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000BE68 File Offset: 0x0000A068
		public ProductKeyInfoModel.ProductKeyInfo ProductKeyInfo
		{
			get
			{
				return this.productKeyInfo;
			}
			set
			{
				if (object.Equals(this.productKeyInfo, value))
				{
					return;
				}
				this.SetProperty<ProductKeyInfoModel.ProductKeyInfo>(ref this.productKeyInfo, value, "ProductKeyInfo");
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000BE9C File Offset: 0x0000A09C
		private void HideMethod()
		{
			bool flag = this.ProductKeyInfoModelCol[0].InstalledKey.Count((char x) => x == '-') == 4;
			if (flag)
			{
				string[] array = this.ProductKeyInfoModelCol[0].InstalledKey.Split(new char[]
				{
					'-'
				});
				this.ProductKeyInfoModelCol[0].InstalledKey = (this.ProductKeyInfoModelCol[0].InstalledKey.Contains("*") ? this.InstalledKey : ("*****-*****-*****-*****-" + array[4]));
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000BF50 File Offset: 0x0000A150
		public void HideOriginalKey()
		{
			bool flag = this.ProductKeyInfoModelCol[0].OriginalKey.Count((char x) => x == '-') == 4;
			if (flag)
			{
				string[] array = this.ProductKeyInfoModelCol[0].OriginalKey.Split(new char[]
				{
					'-'
				});
				this.ProductKeyInfoModelCol[0].OriginalKey = (this.ProductKeyInfoModelCol[0].OriginalKey.Contains("*") ? this.IEkey : ("*****-*****-*****-*****-" + array[4]));
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000C004 File Offset: 0x0000A204
		public void HideOEMMethod()
		{
			bool flag = this.ProductKeyInfoModelCol[0].OEMKey.Count((char x) => x == '-') == 4;
			if (flag)
			{
				string[] array = this.ProductKeyInfoModelCol[0].OEMKey.Split(new char[]
				{
					'-'
				});
				this.ProductKeyInfoModelCol[0].OEMKey = (this.ProductKeyInfoModelCol[0].OEMKey.Contains("*") ? this.OEMKey : ("*****-*****-*****-*****-" + array[4]));
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000C0B6 File Offset: 0x0000A2B6
		private void ShowMethod()
		{
			this.ProductKeyInfo.MessageVisibilty = Visibility.Visible;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		[DebuggerStepThrough]
		public void ShowDialogMethod()
		{
			MainViewModel.<ShowDialogMethod>d__43 <ShowDialogMethod>d__ = new MainViewModel.<ShowDialogMethod>d__43();
			<ShowDialogMethod>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ShowDialogMethod>d__.<>4__this = this;
			<ShowDialogMethod>d__.<>1__state = -1;
			<ShowDialogMethod>d__.<>t__builder.Start<MainViewModel.<ShowDialogMethod>d__43>(ref <ShowDialogMethod>d__);
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000C101 File Offset: 0x0000A301
		// (set) Token: 0x060001EE RID: 494 RVA: 0x0000C10C File Offset: 0x0000A30C
		public ObservableCollection<ProductKeyInfoModel.ProductKeyInfo> ProductKeyInfoModelCol
		{
			[CompilerGenerated]
			get
			{
				return this.<ProductKeyInfoModelCol>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.ProductKeyInfoModelCol, value))
				{
					return;
				}
				this.<ProductKeyInfoModelCol>k__BackingField = value;
				this.OnPropertyChanged(<>PropertyChangedEventArgs.ProductKeyInfoModelCol);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000C13C File Offset: 0x0000A33C
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000C144 File Offset: 0x0000A344
		public ObservableCollection<CheckProductKeyInfo> CheckProductKeyModel
		{
			[CompilerGenerated]
			get
			{
				return this.<CheckProductKeyModel>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.CheckProductKeyModel, value))
				{
					return;
				}
				this.<CheckProductKeyModel>k__BackingField = value;
				this.OnPropertyChanged(<>PropertyChangedEventArgs.CheckProductKeyModel);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000C174 File Offset: 0x0000A374
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000C17C File Offset: 0x0000A37C
		public ObservableCollection<AccentBrush> AccentBrushModel
		{
			[CompilerGenerated]
			get
			{
				return this.<AccentBrushModel>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				if (object.Equals(this.AccentBrushModel, value))
				{
					return;
				}
				this.<AccentBrushModel>k__BackingField = value;
				this.OnPropertyChanged(<>PropertyChangedEventArgs.AccentBrushModel);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000C1AC File Offset: 0x0000A3AC
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0000C1B4 File Offset: 0x0000A3B4
		public string MsgTitle
		{
			get
			{
				return this.msgtitle;
			}
			set
			{
				if (string.Equals(this.msgtitle, value, StringComparison.Ordinal))
				{
					return;
				}
				this.msgtitle = value;
				this.OnPropertyChanged(<>PropertyChangedEventArgs.MsgTitle);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000C1E5 File Offset: 0x0000A3E5
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x0000C1F0 File Offset: 0x0000A3F0
		public string MsgContent
		{
			get
			{
				return this.msgcontent;
			}
			set
			{
				if (string.Equals(this.msgcontent, value, StringComparison.Ordinal))
				{
					return;
				}
				this.msgcontent = value;
				this.OnPropertyChanged(<>PropertyChangedEventArgs.MsgContent);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000C221 File Offset: 0x0000A421
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000C22C File Offset: 0x0000A42C
		public ContentDialogResult DialogResult
		{
			get
			{
				return this.dialogResult;
			}
			set
			{
				if (this.dialogResult == value)
				{
					return;
				}
				this.dialogResult = value;
				this.OnPropertyChanged(<>PropertyChangedEventArgs.DialogResult);
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000C25C File Offset: 0x0000A45C
		public bool LoadPKInfo(string path, bool _base, bool hive, string regpath)
		{
			try
			{
				this.pk = this.SetData(path, _base, hive, regpath);
				bool flag = this.pk.Count == 8;
				if (!flag)
				{
					this.ProductKeyInfo.ProgressLabel = this.res.GetResourceString("txtInsufficientDataError");
					this.ProductKeyInfo.CurrentProgress = false;
					return false;
				}
				this.Generic = this.CheckGeneric(this.pk[3].ToString());
				this.Generic2 = this.CheckGeneric(this.pk[4].ToString());
				ObservableCollection<ProductKeyInfoModel.ProductKeyInfo> productKeyInfoModelCol = new ObservableCollection<ProductKeyInfoModel.ProductKeyInfo>
				{
					new ProductKeyInfoModel.ProductKeyInfo
					{
						ProductName = this.pk[0].ToString(),
						Version = this.pk[1].ToString(),
						ProductID = this.pk[2].ToString(),
						InstalledKey = this.pk[3].ToString(),
						OriginalKey = this.pk[4].ToString(),
						OriginalEdition = this.pk[5].ToString(),
						OEMKey = this.pk[6].ToString(),
						OEMDescription = this.pk[7].ToString(),
						Generic = this.CheckGeneric(this.pk[3].ToString()),
						Generic2 = this.CheckGeneric(this.pk[4].ToString()),
						HideOrig = this.HideOrig(),
						HideOEM = this.HideOEM(),
						HideGeneric = this.HideGeneric(),
						HideUpgrades = this.HideUpgrades(),
						CurrentProgress = false
					}
				};
				this.ProductKeyInfoModelCol = productKeyInfoModelCol;
				this.ProductKeyInfo.ProgressLabel = string.Empty;
			}
			catch (Exception ex)
			{
				this.MsgTitle = this.res.GetResourceString("txtError");
				this.MsgContent = ex.Message;
				this.ProductKeyInfo.CurrentProgress = false;
				return false;
			}
			return true;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000C4BC File Offset: 0x0000A6BC
		[DebuggerStepThrough]
		public Task LoadCheckPKAsync(string productKey)
		{
			MainViewModel.<LoadCheckPKAsync>d__67 <LoadCheckPKAsync>d__ = new MainViewModel.<LoadCheckPKAsync>d__67();
			<LoadCheckPKAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<LoadCheckPKAsync>d__.<>4__this = this;
			<LoadCheckPKAsync>d__.productKey = productKey;
			<LoadCheckPKAsync>d__.<>1__state = -1;
			<LoadCheckPKAsync>d__.<>t__builder.Start<MainViewModel.<LoadCheckPKAsync>d__67>(ref <LoadCheckPKAsync>d__);
			return <LoadCheckPKAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000C508 File Offset: 0x0000A708
		public void GetAccentBrush()
		{
			try
			{
				Brush immersiveSystemAccentDark1Brush = AccentColors.ImmersiveSystemAccentDark1Brush;
				this.AccentBrushModel = new ObservableCollection<AccentBrush>
				{
					new AccentBrush
					{
						Accent = immersiveSystemAccentDark1Brush.ToString()
					}
				};
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000C55C File Offset: 0x0000A75C
		public void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
		{
			this.GetAccentBrush();
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000C568 File Offset: 0x0000A768
		[DebuggerStepThrough]
		public Task<List<string>> CheckPKAsync(string pk)
		{
			MainViewModel.<CheckPKAsync>d__70 <CheckPKAsync>d__ = new MainViewModel.<CheckPKAsync>d__70();
			<CheckPKAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<string>>.Create();
			<CheckPKAsync>d__.<>4__this = this;
			<CheckPKAsync>d__.pk = pk;
			<CheckPKAsync>d__.<>1__state = -1;
			<CheckPKAsync>d__.<>t__builder.Start<MainViewModel.<CheckPKAsync>d__70>(ref <CheckPKAsync>d__);
			return <CheckPKAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000C5B4 File Offset: 0x0000A7B4
		private List<string> SetData(string path, bool _base, bool hive, string regpath)
		{
			List<string> list = new List<string>();
			byte? b = null;
			Resource resource = new Resource();
			try
			{
				OEM oem = new OEM();
				KeyDecoder keyDecoder = new KeyDecoder();
				byte[] array;
				byte[] bytes;
				byte[] array2;
				if (_base)
				{
					array = keyDecoder.GetRegistryDigitalProductId(KeyDecoder.Key.IE, hive, string.Empty, regpath);
					bytes = keyDecoder.GetRegistryDigitalProductId(KeyDecoder.Key.IE, hive, "dpId4", regpath);
					array2 = keyDecoder.GetRegistryDigitalProductId(KeyDecoder.Key.Windows, hive, string.Empty, regpath);
					list.Add(keyDecoder.prod);
					string[] array3 = keyDecoder.build.Split(new char[]
					{
						'.'
					});
					bool flag = keyDecoder.currentBuild.Length > 0;
					if (flag)
					{
						array3[0] = keyDecoder.currentBuild;
					}
					array3[1] = "." + keyDecoder.UBR;
					array3[2] = (array3[2].Contains("amd64") ? " (64-bit OS)" : " (32-bit OS)");
					list.Add(array3[0] + array3[1] + array3[2]);
					list.Add(keyDecoder.prodID);
				}
				else
				{
					Task<List<byte[]>> task = Task.Run<List<byte[]>>(() => this.CheckPEAsync(path), this.cts.Token);
					task.Wait();
					List<byte[]> result = task.Result;
					b = new byte?(result[1][66]);
					array2 = result[0];
					array = result[1];
					bytes = result[2];
					byte[] bytes2 = result[3];
					byte[] bytes3 = result[4];
					byte[] bytes4 = result[5];
					byte[] array4 = result[6];
					list.Add(Encoding.ASCII.GetString(bytes2));
					string[] array3 = Encoding.ASCII.GetString(bytes4).Split(new char[]
					{
						'.'
					});
					bool flag2 = result.Count > 7 && result[7] != null;
					if (flag2)
					{
						array3[0] = Encoding.ASCII.GetString(result[7]);
					}
					array3[1] = ((array4 != null) ? ("." + Encoding.ASCII.GetString(array4)) : string.Empty);
					bool flag3 = Convert.ToInt32(array3[0]) >= 22000;
					if (flag3)
					{
						list[0] = list[0].Replace("10", "11");
					}
					array3[2] = (array3[2].Contains("amd64") ? " (64-bit OS)" : " (32-bit OS)");
					list.Add(array3[0] + array3[1] + array3[2]);
					list.Add(Encoding.ASCII.GetString(bytes3));
				}
				string text = Encoding.ASCII.GetString(bytes, 280, 36).Replace("\0", string.Empty).Trim();
				string text2 = Encoding.ASCII.GetString(bytes, 888, 32).Replace("\0", string.Empty).Trim();
				string str = Encoding.ASCII.GetString(bytes, 1016, 22).Replace("\0", string.Empty).Trim();
				string str2 = string.Empty;
				int num = Convert.ToInt32(Regex.Match(text2, "\\d+").Value);
				int num2 = num;
				int num3 = num2;
				if (num3 - 15 > 2)
				{
					if (num3 != 18)
					{
						str2 = "Windows 10 ";
					}
					else
					{
						str2 = (text2.Contains("Blue") ? "Windows 8.1 " : "Windows 8 ");
					}
				}
				else
				{
					str2 = "Windows 7 ";
				}
				text = str2 + Regex.Replace(text, "([a-z])([A-Z])", "$1 $2") + " " + str;
				this.InstalledKey = ((array2 != null) ? keyDecoder.DecodeProductKey(array2) : string.Empty);
				bool flag4 = b != null && !array.GetValue(66).Equals(b);
				if (flag4)
				{
					array.SetValue(b, 66);
				}
				this.InstalledKey = (this.InstalledKey.Contains("BBBBB") ? resource.GetResourceString("txtMAKNotAvailable") : this.InstalledKey);
				list.Add(this.InstalledKey);
				this.IEkey = keyDecoder.DecodeProductKey(array);
				this.IEkey = (this.IEkey.Contains("BBBBB") ? resource.GetResourceString("txtMAKNotAvailable") : this.IEkey);
				list.Add(this.IEkey);
				bool flag5 = this.IEkey != this.InstalledKey;
				if (flag5)
				{
					list.Add(text);
				}
				else
				{
					list.Add(resource.GetResourceString("txtEditionNotAvailable"));
				}
				this.OEMKey = oem.GetKey();
				list.Add(this.OEMKey);
				bool flag6 = this.OEMKey.Count((char x) => x == '-') == 4;
				if (flag6)
				{
					Task<List<string>> task2 = Task.Run<List<string>>(() => this.GetEditionAsync(this.OEMKey, false));
					task2.Wait();
					this.OEMDescription = task2.Result[0];
				}
				bool flag7 = this.OEMDescription != null;
				if (flag7)
				{
					list.Add(this.OEMDescription);
				}
				else
				{
					list.Add(string.Empty);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return list;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000CB78 File Offset: 0x0000AD78
		[DebuggerStepThrough]
		private Task<List<byte[]>> CheckPEAsync(string path)
		{
			MainViewModel.<CheckPEAsync>d__72 <CheckPEAsync>d__ = new MainViewModel.<CheckPEAsync>d__72();
			<CheckPEAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<byte[]>>.Create();
			<CheckPEAsync>d__.<>4__this = this;
			<CheckPEAsync>d__.path = path;
			<CheckPEAsync>d__.<>1__state = -1;
			<CheckPEAsync>d__.<>t__builder.Start<MainViewModel.<CheckPEAsync>d__72>(ref <CheckPEAsync>d__);
			return <CheckPEAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000CBC4 File Offset: 0x0000ADC4
		[DebuggerStepThrough]
		private Task<List<string>> GetEditionAsync(string key, bool check)
		{
			MainViewModel.<GetEditionAsync>d__73 <GetEditionAsync>d__ = new MainViewModel.<GetEditionAsync>d__73();
			<GetEditionAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<string>>.Create();
			<GetEditionAsync>d__.<>4__this = this;
			<GetEditionAsync>d__.key = key;
			<GetEditionAsync>d__.check = check;
			<GetEditionAsync>d__.<>1__state = -1;
			<GetEditionAsync>d__.<>t__builder.Start<MainViewModel.<GetEditionAsync>d__73>(ref <GetEditionAsync>d__);
			return <GetEditionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000CC18 File Offset: 0x0000AE18
		private bool CheckGeneric(string key)
		{
			try
			{
				using (StreamReader streamReader = new StreamReader(base.GetType().Assembly.GetManifestResourceStream("ShowKeyPlus.Resources.generic.json")))
				{
					object arg = JsonConvert.DeserializeObject(streamReader.ReadToEnd());
					if (MainViewModel.<>o__74.<>p__3 == null)
					{
						MainViewModel.<>o__74.<>p__3 = CallSite<Func<CallSite, object, IEnumerable>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(IEnumerable), typeof(MainViewModel)));
					}
					foreach (object arg2 in MainViewModel.<>o__74.<>p__3.Target(MainViewModel.<>o__74.<>p__3, arg))
					{
						if (MainViewModel.<>o__74.<>p__2 == null)
						{
							MainViewModel.<>o__74.<>p__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(MainViewModel), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						Func<CallSite, object, bool> target = MainViewModel.<>o__74.<>p__2.Target;
						CallSite <>p__ = MainViewModel.<>o__74.<>p__2;
						if (MainViewModel.<>o__74.<>p__1 == null)
						{
							MainViewModel.<>o__74.<>p__1 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(MainViewModel), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
							}));
						}
						Func<CallSite, object, string, object> target2 = MainViewModel.<>o__74.<>p__1.Target;
						CallSite <>p__2 = MainViewModel.<>o__74.<>p__1;
						if (MainViewModel.<>o__74.<>p__0 == null)
						{
							MainViewModel.<>o__74.<>p__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Key", typeof(MainViewModel), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						bool flag = target(<>p__, target2(<>p__2, MainViewModel.<>o__74.<>p__0.Target(MainViewModel.<>o__74.<>p__0, arg2), key));
						if (flag)
						{
							return true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return false;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000CE34 File Offset: 0x0000B034
		private GridLength HideOrig()
		{
			return (this.InstalledKey == this.IEkey) ? new GridLength(0.0, GridUnitType.Star) : new GridLength(162.0, GridUnitType.Star);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000CE6C File Offset: 0x0000B06C
		private GridLength HideOEM()
		{
			return (this.OEMKey.Count((char x) => x == '-') != 4) ? new GridLength(0.0, GridUnitType.Star) : new GridLength(162.0, GridUnitType.Star);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000CEC6 File Offset: 0x0000B0C6
		private GridLength HideGeneric()
		{
			return (!this.Generic) ? new GridLength(0.0, GridUnitType.Star) : new GridLength(81.0, GridUnitType.Star);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
		private GridLength HideUpgrades()
		{
			return (!this.Upgrades) ? new GridLength(0.0, GridUnitType.Star) : new GridLength(81.0, GridUnitType.Star);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000CF1C File Offset: 0x0000B11C
		[DebuggerStepThrough]
		public Task<bool> LoadUpgradeAsync(string regpath, CancellationToken cancellation)
		{
			MainViewModel.<LoadUpgradeAsync>d__79 <LoadUpgradeAsync>d__ = new MainViewModel.<LoadUpgradeAsync>d__79();
			<LoadUpgradeAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<LoadUpgradeAsync>d__.<>4__this = this;
			<LoadUpgradeAsync>d__.regpath = regpath;
			<LoadUpgradeAsync>d__.cancellation = cancellation;
			<LoadUpgradeAsync>d__.<>1__state = -1;
			<LoadUpgradeAsync>d__.<>t__builder.Start<MainViewModel.<LoadUpgradeAsync>d__79>(ref <LoadUpgradeAsync>d__);
			return <LoadUpgradeAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000894 RID: 2196
		private CancellationTokenSource cts;

		// Token: 0x04000895 RID: 2197
		public ProductKeyInfoModel.ProductKeyInfo productKeyInfo;

		// Token: 0x0400089B RID: 2203
		private readonly Resource resource = new Resource();

		// Token: 0x0400089C RID: 2204
		public string InstalledKey;

		// Token: 0x0400089D RID: 2205
		public string IEkey;

		// Token: 0x0400089E RID: 2206
		public string OEMKey;

		// Token: 0x0400089F RID: 2207
		public string OEMDescription = string.Empty;

		// Token: 0x040008A0 RID: 2208
		private bool Generic = false;

		// Token: 0x040008A1 RID: 2209
		private bool Generic2 = false;

		// Token: 0x040008A2 RID: 2210
		public bool Upgrades = false;

		// Token: 0x040008A3 RID: 2211
		private string msgtitle;

		// Token: 0x040008A4 RID: 2212
		private string msgcontent;

		// Token: 0x040008A5 RID: 2213
		private ContentDialogResult dialogResult;

		// Token: 0x040008A6 RID: 2214
		private readonly Resource res = new Resource();

		// Token: 0x040008A7 RID: 2215
		private List<string> pk = new List<string>();

		// Token: 0x02000038 RID: 56
		private enum PkConfig
		{
			// Token: 0x040008AC RID: 2220
			Default,
			// Token: 0x040008AD RID: 2221
			Win10,
			// Token: 0x040008AE RID: 2222
			Win8,
			// Token: 0x040008AF RID: 2223
			Office2010,
			// Token: 0x040008B0 RID: 2224
			Office2013,
			// Token: 0x040008B1 RID: 2225
			Office2016,
			// Token: 0x040008B2 RID: 2226
			Office2021,
			// Token: 0x040008B3 RID: 2227
			Downlevel,
			// Token: 0x040008B4 RID: 2228
			Win7SLP
		}
	}
}
