using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Win32;
using ModernWpf.Controls;
using ShowKeyPlusWPF.ViewModel;
using ShowKeyPlusWPF.Views;

namespace ShowKeyPlusWPF
{
	// Token: 0x0200002A RID: 42
	public partial class MainWindow : Window
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000729D File Offset: 0x0000549D
		private MainViewModel PKViewModel { get; } = new MainViewModel();

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000072A5 File Offset: 0x000054A5
		private MainViewModel CheckPKViewModel { get; } = new MainViewModel();

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000072AD File Offset: 0x000054AD
		private List<string> Win
		{
			get
			{
				return this.WinList;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000072B5 File Offset: 0x000054B5
		private List<string> Source
		{
			get
			{
				return this.SourceList();
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000072BD File Offset: 0x000054BD
		// (set) Token: 0x0600016D RID: 365 RVA: 0x000072C5 File Offset: 0x000054C5
		private int X { get; set; } = 0;

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000072CE File Offset: 0x000054CE
		// (set) Token: 0x0600016F RID: 367 RVA: 0x000072D6 File Offset: 0x000054D6
		private int Y { get; set; } = 0;

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000072DF File Offset: 0x000054DF
		// (set) Token: 0x06000171 RID: 369 RVA: 0x000072E7 File Offset: 0x000054E7
		private int More { get; set; } = 0;

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000072F0 File Offset: 0x000054F0
		public string RegDll { get; } = System.IO.Path.Combine(Environment.SystemDirectory, "offreg.dll");

		// Token: 0x06000173 RID: 371 RVA: 0x000072F8 File Offset: 0x000054F8
		public MainWindow()
		{
			this.InitializeComponent();
			this.PKViewModel.GetAccentBrush();
			base.DataContext = this.PKViewModel;
			this.rectbtnRetrieve.Visibility = (this.rectbtnCheckPK.Visibility = (this.rectbtnMore.Visibility = (this.rectbtnUpgrades.Visibility = (this.rectbtnAbout.Visibility = Visibility.Hidden))));
			base.Title = this.AboutBox.AssemblyProduct;
			this.SetLabels();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000073DC File Offset: 0x000055DC
		public void LoadPKInfo(object sender, RoutedEventArgs e)
		{
			try
			{
				this.ToggleControls(this.PKInfoControl);
				bool WinPE = false;
				int index = 0;
				this.Win.ForEach(delegate(string x)
				{
					bool flag4 = x.Contains("X:");
					if (flag4)
					{
						index = this.Win.IndexOf(x);
						WinPE = true;
					}
				});
				bool flag = !File.Exists(this.RegDll);
				if (flag)
				{
					PKChecker pkchecker = new PKChecker();
					pkchecker.GetResourceFile(this.RegDll);
				}
				bool winPE = WinPE;
				if (winPE)
				{
					bool flag2 = this.Win.Count > 1;
					if (flag2)
					{
						this.Win.RemoveAt(index);
					}
					else
					{
						this.PKViewModel.MsgTitle = this.res.GetResourceString("txtError");
						this.PKViewModel.MsgContent = this.res.GetResourceString("txtNoInstallations");
						this.PKViewModel.ShowDialog.Execute(sender);
						bool flag3 = this.PKViewModel.DialogResult == 1;
						if (flag3)
						{
							Application.Current.Shutdown();
						}
					}
				}
				this.ResetUpgrade();
				this.ResetMore();
				this.PKViewModel.Upgrades = true;
				this.PKViewModel.LoadPKInfo(null, true, false, string.Empty);
				this.PKInfoControl.DataContext = this.PKViewModel;
				this.CheckPKControl.Visibility = Visibility.Hidden;
				this.TransformControl(this.PKInfoControl);
			}
			catch (Exception ex)
			{
				this.PKViewModel.MsgTitle = this.res.GetResourceString("txtError");
				this.PKViewModel.MsgContent = ex.Message;
				this.PKViewModel.ShowDialog.Execute(sender);
				this.grdNavLinks.IsEnabled = true;
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000075C4 File Offset: 0x000057C4
		[DebuggerStepThrough]
		private void LoadCheckPKAsync(object sender, RoutedEventArgs e)
		{
			MainWindow.<LoadCheckPKAsync>d__29 <LoadCheckPKAsync>d__ = new MainWindow.<LoadCheckPKAsync>d__29();
			<LoadCheckPKAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<LoadCheckPKAsync>d__.<>4__this = this;
			<LoadCheckPKAsync>d__.sender = sender;
			<LoadCheckPKAsync>d__.e = e;
			<LoadCheckPKAsync>d__.<>1__state = -1;
			<LoadCheckPKAsync>d__.<>t__builder.Start<MainWindow.<LoadCheckPKAsync>d__29>(ref <LoadCheckPKAsync>d__);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000760C File Offset: 0x0000580C
		private void BtnSave_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.res.GetResourceString("txtTitle.Text") + " - " + this.res.GetResourceString("txtHeader.Text"));
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(Environment.NewLine);
				bool flag = this.PKInfoControl.Visibility == Visibility.Visible;
				if (flag)
				{
					stringBuilder.Append(this.PKInfoControl.LblProductName.Text);
					stringBuilder.Append("\t");
					stringBuilder.Append(this.PKInfoControl.ProductName.Text);
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append(this.PKInfoControl.LblProductID.Text);
					stringBuilder.Append("   \t");
					stringBuilder.Append(this.PKInfoControl.ProductID.Text);
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append(this.PKInfoControl.LblVersion.Text);
					stringBuilder.Append("\t");
					stringBuilder.Append(this.PKInfoControl.Version.Text);
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append(this.PKInfoControl.LblInstalledKey.Text + "\t");
					stringBuilder.Append(this.PKInfoControl.InstalledKey.Text);
					bool isVisible = this.PKInfoControl.lblGeneric.IsVisible;
					if (isVisible)
					{
						stringBuilder.Append(" *");
					}
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append(Environment.NewLine);
					bool flag2 = this.PKInfoControl.InstalledKey.Text != this.PKInfoControl.OriginalKey.Text;
					if (flag2)
					{
						stringBuilder.Append(this.PKInfoControl.LblOriginalKey.Text);
						stringBuilder.Append("\t");
						stringBuilder.Append(this.PKInfoControl.OriginalKey.Text);
						bool isVisible2 = this.PKInfoControl.lblGenricKey2.IsVisible;
						if (isVisible2)
						{
							stringBuilder.Append(" *");
						}
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append(this.PKInfoControl.LblOriginalEdition.Text);
						stringBuilder.Append("\t");
						stringBuilder.Append(this.PKInfoControl.OriginalEdition.Text);
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append(Environment.NewLine);
					}
					bool flag3 = this.PKInfoControl.OEMKey.Text.Contains("OEM") || this.PKInfoControl.OEMKey.Text.Contains(this.res.GetResourceString("txtMarker"));
					if (flag3)
					{
						stringBuilder.Append(this.PKInfoControl.LblOEMKey.Text);
						stringBuilder.Append("\t");
						stringBuilder.Append(this.PKInfoControl.OEMKey.Text);
					}
					else
					{
						stringBuilder.Append(this.PKInfoControl.LblOEMKey.Text);
						stringBuilder.Append("\t");
						stringBuilder.Append(this.PKInfoControl.OEMKey.Text);
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append(this.PKInfoControl.LblOEMDescription.Text);
						stringBuilder.Append("\t");
						stringBuilder.Append(this.PKInfoControl.OEMDescription.Text);
					}
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append(Environment.NewLine);
					stringBuilder.Append(Environment.NewLine);
					bool flag4 = this.PKInfoControl.lblGenricKey2.IsVisible || this.PKInfoControl.lblGeneric.IsVisible;
					if (flag4)
					{
						stringBuilder.Append(this.PKInfoControl.lblGeneric.Text);
					}
				}
				else
				{
					bool flag5 = this.CheckPKControl.Visibility == Visibility.Visible;
					if (flag5)
					{
						stringBuilder.Append(this.CheckPKControl.LblProductKey.Text);
						stringBuilder.Append("\t");
						stringBuilder.Append(this.CheckPKControl.txtCheckPK.Text);
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append(this.CheckPKControl.LblEdition.Text);
						stringBuilder.Append("   \t");
						stringBuilder.Append(this.CheckPKControl.txtEdition.Text);
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append(this.CheckPKControl.LblEULA.Text);
						stringBuilder.Append("\t");
						stringBuilder.Append(this.CheckPKControl.txtEULA.Text);
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append(Environment.NewLine);
						bool flag6 = this.CheckPKControl.LblMAK.Visibility == Visibility.Visible;
						if (flag6)
						{
							stringBuilder.Append(this.CheckPKControl.LblMAK.Text);
							stringBuilder.Append("\t");
							stringBuilder.Append(this.CheckPKControl.txtMAK.Text);
							stringBuilder.Append(Environment.NewLine);
							stringBuilder.Append(Environment.NewLine);
						}
					}
				}
				bool flag7 = this.PKInfoControl.Visibility == Visibility.Visible || this.CheckPKControl.Visibility == Visibility.Visible;
				if (flag7)
				{
					SaveFileDialog saveFileDialog = new SaveFileDialog();
					saveFileDialog.Filter = string.Concat(new string[]
					{
						this.res.GetResourceString("txtTextFile"),
						" | *.txt|",
						this.res.GetResourceString("txtWordDocument"),
						"| *.doc|",
						this.res.GetResourceString("txtExcelDocument"),
						"|*.csv"
					});
					bool isVisible3 = this.PKInfoControl.IsVisible;
					if (isVisible3)
					{
						saveFileDialog.FileName = this.PKInfoControl.ProductName.Text + " " + this.res.GetResourceString("txtKey");
					}
					bool isVisible4 = this.CheckPKControl.IsVisible;
					if (isVisible4)
					{
						bool isVisible5 = this.CheckPKControl.grdEdition.IsVisible;
						if (!isVisible5)
						{
							return;
						}
						saveFileDialog.FileName = this.CheckPKControl.txtEdition.Text.Replace(":", " ") + " " + this.res.GetResourceString("txtKey");
					}
					bool? flag8 = saveFileDialog.ShowDialog();
					bool flag9 = true;
					bool flag10 = flag8.GetValueOrDefault() == flag9 & flag8 != null;
					if (flag10)
					{
						bool flag11 = System.IO.Path.GetExtension(saveFileDialog.FileName).Contains("csv");
						if (flag11)
						{
							stringBuilder.Replace(':', ';');
						}
						using (StreamWriter streamWriter = new StreamWriter(saveFileDialog.FileName))
						{
							streamWriter.Write(stringBuilder.ToString());
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.PKViewModel.MsgTitle = this.res.GetResourceString("txtError");
				this.PKViewModel.MsgContent = ex.Message;
				this.PKViewModel.ShowDialog.Execute(sender);
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007E24 File Offset: 0x00006024
		[DebuggerStepThrough]
		private void BtnCheckPK_ClickAsync(object sender, RoutedEventArgs e)
		{
			MainWindow.<BtnCheckPK_ClickAsync>d__31 <BtnCheckPK_ClickAsync>d__ = new MainWindow.<BtnCheckPK_ClickAsync>d__31();
			<BtnCheckPK_ClickAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<BtnCheckPK_ClickAsync>d__.<>4__this = this;
			<BtnCheckPK_ClickAsync>d__.sender = sender;
			<BtnCheckPK_ClickAsync>d__.e = e;
			<BtnCheckPK_ClickAsync>d__.<>1__state = -1;
			<BtnCheckPK_ClickAsync>d__.<>t__builder.Start<MainWindow.<BtnCheckPK_ClickAsync>d__31>(ref <BtnCheckPK_ClickAsync>d__);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007E6C File Offset: 0x0000606C
		private void BtnHome_Click(object sender, RoutedEventArgs e)
		{
			bool flag = this.rectbtnHome.Visibility > Visibility.Visible;
			if (flag)
			{
				this.DoubleAnimation(this.rectbtnHome);
			}
			this.Toggle(sender as ToggleButton);
			this.PKInfoControl.DataContext = this.PKViewModel;
			this.LoadPKInfo(this, null);
			this.X = 1;
			this.ToggleControls(this.PKInfoControl);
			this.ResetMore();
			this.ResetUpgrade();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007EE4 File Offset: 0x000060E4
		private void TransformControl(UserControl userControl)
		{
			Storyboard storyboard = new Storyboard();
			ScaleTransform renderTransform = new ScaleTransform(1.0, 1.0);
			userControl.RenderTransformOrigin = new Point(0.5, 0.5);
			userControl.RenderTransform = renderTransform;
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.Duration = TimeSpan.FromMilliseconds(500.0);
			doubleAnimation.From = new double?(0.0);
			doubleAnimation.To = new double?(1.0);
			storyboard.Children.Add(doubleAnimation);
			Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.ScaleX", Array.Empty<object>()));
			Storyboard.SetTarget(doubleAnimation, userControl);
			storyboard.Begin();
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007FB4 File Offset: 0x000061B4
		[DebuggerStepThrough]
		private void BtnMore_ClickAsync(object sender, RoutedEventArgs e)
		{
			MainWindow.<BtnMore_ClickAsync>d__34 <BtnMore_ClickAsync>d__ = new MainWindow.<BtnMore_ClickAsync>d__34();
			<BtnMore_ClickAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<BtnMore_ClickAsync>d__.<>4__this = this;
			<BtnMore_ClickAsync>d__.sender = sender;
			<BtnMore_ClickAsync>d__.e = e;
			<BtnMore_ClickAsync>d__.<>1__state = -1;
			<BtnMore_ClickAsync>d__.<>t__builder.Start<MainWindow.<BtnMore_ClickAsync>d__34>(ref <BtnMore_ClickAsync>d__);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00007FFC File Offset: 0x000061FC
		[DebuggerStepThrough]
		private void BtnRetrieve_ClickAsync(object sender, RoutedEventArgs e)
		{
			MainWindow.<BtnRetrieve_ClickAsync>d__35 <BtnRetrieve_ClickAsync>d__ = new MainWindow.<BtnRetrieve_ClickAsync>d__35();
			<BtnRetrieve_ClickAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<BtnRetrieve_ClickAsync>d__.<>4__this = this;
			<BtnRetrieve_ClickAsync>d__.sender = sender;
			<BtnRetrieve_ClickAsync>d__.e = e;
			<BtnRetrieve_ClickAsync>d__.<>1__state = -1;
			<BtnRetrieve_ClickAsync>d__.<>t__builder.Start<MainWindow.<BtnRetrieve_ClickAsync>d__35>(ref <BtnRetrieve_ClickAsync>d__);
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00008044 File Offset: 0x00006244
		internal List<string> WinList
		{
			get
			{
				this.More = 0;
				List<string> list = new List<string>();
				try
				{
					DriveInfo[] drives = DriveInfo.GetDrives();
					foreach (DriveInfo driveInfo in drives)
					{
						bool flag = driveInfo.RootDirectory.ToString() != System.IO.Path.GetPathRoot(Environment.SystemDirectory);
						if (flag)
						{
							string text = System.IO.Path.Combine(driveInfo.RootDirectory.ToString(), "Windows\\System32\\config\\SOFTWARE");
							string text2 = System.IO.Path.Combine(driveInfo.RootDirectory.ToString(), "Windows.old\\System32\\config\\SOFTWARE");
							string text3 = System.IO.Path.Combine(driveInfo.RootDirectory.ToString(), "Windows.old\\Windows\\System32\\config\\SOFTWARE");
							bool flag2 = File.Exists(text) && this.HasAccess(text);
							if (flag2)
							{
								list.Add(text);
							}
							bool flag3 = File.Exists(text2) && this.HasAccess(text2);
							if (flag3)
							{
								list.Add(text2);
							}
							bool flag4 = File.Exists(text3) && this.HasAccess(text3);
							if (flag4)
							{
								list.Add(text3);
							}
						}
					}
				}
				catch (Exception)
				{
				}
				return list;
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000817C File Offset: 0x0000637C
		private bool HasAccess(string path)
		{
			try
			{
				using (new FileStream(path, FileMode.Open))
				{
					return true;
				}
			}
			catch (Exception)
			{
				int more = this.More;
				this.More = more + 1;
			}
			return false;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000081DC File Offset: 0x000063DC
		internal List<string> SourceList()
		{
			List<string> list = new List<string>();
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\Setup");
				foreach (string text in registryKey.GetSubKeyNames())
				{
					bool flag = text.ToString().Contains("Source OS");
					if (flag)
					{
						list.Add("SYSTEM\\Setup\\" + text.ToString());
					}
				}
			}
			catch (Exception ex)
			{
				this.PKViewModel.MsgTitle = this.res.GetResourceString("txtError");
				this.PKViewModel.MsgContent = ex.Message;
				this.PKViewModel.ShowDialog.Execute(this);
			}
			return list;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000082B0 File Offset: 0x000064B0
		private void Toggle(ToggleButton button)
		{
			this.rectbtnHome.Visibility = (this.rectbtnRetrieve.Visibility = (this.rectbtnCheckPK.Visibility = (this.rectbtnMore.Visibility = (this.rectbtnUpgrades.Visibility = (this.rectbtnAbout.Visibility = (this.rectbtnAbout.Visibility = Visibility.Hidden))))));
			this.btnHome.Background = (this.btnRetrieve.Background = (this.btnCheckPK.Background = (this.btnMore.Background = (this.btnUpgrades.Background = (this.btnAbout.Background = Brushes.Transparent)))));
			string name = button.Name;
			string a = name;
			if (!(a == "btnHome"))
			{
				if (!(a == "btnRetrieve"))
				{
					if (!(a == "btnCheckPK"))
					{
						if (!(a == "btnMore"))
						{
							if (!(a == "btnUpgrades"))
							{
								if (a == "btnAbout")
								{
									this.btnAbout.Background = this.PKViewModel.AccentBrushModel[0].AccentGradient;
									this.rectbtnAbout.Visibility = Visibility.Visible;
								}
							}
							else
							{
								this.btnUpgrades.Background = this.PKViewModel.AccentBrushModel[0].AccentGradient;
								this.rectbtnUpgrades.Visibility = Visibility.Visible;
							}
						}
						else
						{
							this.btnMore.Background = this.PKViewModel.AccentBrushModel[0].AccentGradient;
							this.rectbtnMore.Visibility = Visibility.Visible;
						}
					}
					else
					{
						this.btnCheckPK.Background = this.PKViewModel.AccentBrushModel[0].AccentGradient;
						this.rectbtnCheckPK.Visibility = Visibility.Visible;
					}
				}
				else
				{
					this.btnRetrieve.Background = this.PKViewModel.AccentBrushModel[0].AccentGradient;
					this.rectbtnRetrieve.Visibility = Visibility.Visible;
				}
			}
			else
			{
				this.btnHome.Background = this.PKViewModel.AccentBrushModel[0].AccentGradient;
				this.rectbtnHome.Visibility = Visibility.Visible;
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008530 File Offset: 0x00006730
		private void BtnAbout_Click(object sender, RoutedEventArgs e)
		{
			this.LoadPKInfo(this, null);
			bool flag = this.rectbtnAbout.Visibility > Visibility.Visible;
			if (flag)
			{
				this.DoubleAnimation(this.rectbtnAbout);
			}
			this.Toggle(sender as ToggleButton);
			this.ToggleControls(this.AboutBox);
			this.TransformControl(this.AboutBox);
			DoubleAnimation animation = new DoubleAnimation
			{
				From = new double?(0.0),
				To = new double?((double)1),
				Duration = new Duration(TimeSpan.FromSeconds(5.0))
			};
			this.AboutBox.Logo.BeginAnimation(UIElement.OpacityProperty, animation);
			this.ResetMore();
			this.ResetUpgrade();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x000085F8 File Offset: 0x000067F8
		private void ToggleControls(UserControl userControl)
		{
			this.CheckPKControl.Visibility = Visibility.Hidden;
			this.PKInfoControl.Visibility = Visibility.Hidden;
			this.AboutBox.Visibility = Visibility.Hidden;
			this.Prog.Visibility = Visibility.Hidden;
			userControl.Visibility = Visibility.Visible;
			bool flag = userControl != this.CheckPKControl;
			if (flag)
			{
				this.CheckPKControl.txtCheckPK.Text = string.Empty;
				this.CheckPKControl.grdEdition.Visibility = Visibility.Hidden;
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000867C File Offset: 0x0000687C
		[DebuggerStepThrough]
		private Task<bool> LoadMoreAsync(string path, CancellationToken ct)
		{
			MainWindow.<LoadMoreAsync>d__43 <LoadMoreAsync>d__ = new MainWindow.<LoadMoreAsync>d__43();
			<LoadMoreAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<LoadMoreAsync>d__.<>4__this = this;
			<LoadMoreAsync>d__.path = path;
			<LoadMoreAsync>d__.ct = ct;
			<LoadMoreAsync>d__.<>1__state = -1;
			<LoadMoreAsync>d__.<>t__builder.Start<MainWindow.<LoadMoreAsync>d__43>(ref <LoadMoreAsync>d__);
			return <LoadMoreAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000086D0 File Offset: 0x000068D0
		private void Prog_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			this.PKInfoControl.Visibility = (this.Prog.IsVisible ? Visibility.Hidden : Visibility.Visible);
			this.grdNavLinks.IsEnabled = !this.Prog.IsVisible;
			this.Prog.IsActive = this.Prog.IsVisible;
			bool flag = !this.Prog.IsVisible;
			if (flag)
			{
				this.TransformControl(this.PKInfoControl);
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000874C File Offset: 0x0000694C
		private void ResetMore()
		{
			this.btnMoreText.Text = this.res.GetResourceString("txtMore.Text") + " (" + this.Win.Count.ToString() + ")";
			this.X = 0;
			this.btnMore.IsEnabled = (this.Win.Count > 0);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000087C0 File Offset: 0x000069C0
		private void ResetUpgrade()
		{
			this.btnUpgradesText.Text = this.res.GetResourceString("txtUpgrades.Text") + " (" + this.Source.Count.ToString() + ")";
			this.Y = 0;
			this.PKInfoControl.lblUpgrades.Text = "";
			this.PKInfoControl.lblUpgrades.Visibility = Visibility.Hidden;
			this.btnUpgrades.IsEnabled = (this.Source.Count != 0);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000885C File Offset: 0x00006A5C
		[DebuggerStepThrough]
		private void BtnUpgrades_ClickAsync(object sender, RoutedEventArgs e)
		{
			MainWindow.<BtnUpgrades_ClickAsync>d__47 <BtnUpgrades_ClickAsync>d__ = new MainWindow.<BtnUpgrades_ClickAsync>d__47();
			<BtnUpgrades_ClickAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<BtnUpgrades_ClickAsync>d__.<>4__this = this;
			<BtnUpgrades_ClickAsync>d__.sender = sender;
			<BtnUpgrades_ClickAsync>d__.e = e;
			<BtnUpgrades_ClickAsync>d__.<>1__state = -1;
			<BtnUpgrades_ClickAsync>d__.<>t__builder.Start<MainWindow.<BtnUpgrades_ClickAsync>d__47>(ref <BtnUpgrades_ClickAsync>d__);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000088A4 File Offset: 0x00006AA4
		private void ReturnHome()
		{
			this.Toggle(this.btnHome);
			this.PKInfoControl.DataContext = this.PKViewModel;
			this.LoadPKInfo(this, null);
			this.ToggleControls(this.PKInfoControl);
			this.ResetMore();
			this.ResetUpgrade();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000088F8 File Offset: 0x00006AF8
		private string GetProductVersion()
		{
			string result = string.Empty;
			try
			{
				result = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			}
			catch (Exception ex)
			{
				this.PKViewModel.MsgTitle = this.res.GetResourceString("txtError");
				this.PKViewModel.MsgContent = ex.Message;
				this.PKViewModel.ShowDialog.Execute(this);
			}
			return result;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00008980 File Offset: 0x00006B80
		private void SetLabels()
		{
			this.txtHome.Text = this.res.GetResourceString("txtHome.Text");
			this.btnCheckEdition.Text = this.res.GetResourceString("txtCheckEdition.Text");
			this.txtRetrieveKey.Text = this.res.GetResourceString("txtRetrieveKey.Text");
			this.btnMoreText.Text = this.res.GetResourceString("txtMoreText.Text");
			this.txtSave.Text = this.res.GetResourceString("txtSave.Text");
			this.txtAbout.Text = this.res.GetResourceString("txtAbout.Text");
			this.btnUpgradesText.Text = this.res.GetResourceString("txtUpgradesText.Text");
			this.tpCurrentInstallation.Content = this.res.GetResourceString("tpCurrentInstallation.Content");
			this.tpCheckEdition.Content = this.res.GetResourceString("tpCheckEdition.Content");
			this.tpRetrieve.Content = this.res.GetResourceString("tpRetrieve.Content");
			this.tpFurtherWindowsInstallations.Content = this.res.GetResourceString("tpFurtherWindowsInstallations.Content");
			this.tpSave.Content = this.res.GetResourceString("tpSave.Content");
			this.tpTerms.Content = this.res.GetResourceString("tpTerms.Content");
			this.tpPreviousInstallations.Content = this.res.GetResourceString("tpPreviousInstallations.Content");
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00008B18 File Offset: 0x00006D18
		private string FormatDateString(string inputDate)
		{
			try
			{
				return DateTime.Parse(inputDate, CultureInfo.GetCultureInfo("en-US")).ToShortDateString();
			}
			catch (Exception ex)
			{
				this.PKViewModel.MsgTitle = this.res.GetResourceString("txtError");
				this.PKViewModel.MsgContent = "Unformatted Date: " + inputDate + Environment.NewLine + ex.Message;
				this.PKViewModel.ShowDialog.Execute(this);
			}
			return inputDate;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00008BAC File Offset: 0x00006DAC
		private void DoubleAnimation(DependencyObject sender)
		{
			int num = 0;
			int num2 = 0;
			Rectangle rectangle = sender as Rectangle;
			Thickness margin = rectangle.Margin;
			double left = margin.Left;
			double right = margin.Right;
			bool flag = this.rectbtnHome.Visibility == Visibility.Visible;
			if (flag)
			{
				num2 = 1;
			}
			bool flag2 = this.rectbtnCheckPK.Visibility == Visibility.Visible;
			if (flag2)
			{
				num2 = 2;
			}
			bool flag3 = this.rectbtnRetrieve.Visibility == Visibility.Visible;
			if (flag3)
			{
				num2 = 3;
			}
			bool flag4 = this.rectbtnMore.Visibility == Visibility.Visible;
			if (flag4)
			{
				num2 = 4;
			}
			bool flag5 = this.rectbtnUpgrades.Visibility == Visibility.Visible;
			if (flag5)
			{
				num2 = 5;
			}
			bool flag6 = this.rectbtnAbout.Visibility == Visibility.Visible;
			if (flag6)
			{
				num2 = 7;
			}
			string name = rectangle.Name;
			string a = name;
			if (!(a == "rectbtnHome"))
			{
				if (!(a == "rectbtnCheckPK"))
				{
					if (!(a == "rectbtnRetrieve"))
					{
						if (!(a == "rectbtnMore"))
						{
							if (!(a == "rectbtnUpgrades"))
							{
								if (a == "rectbtnAbout")
								{
									num = 7;
								}
							}
							else
							{
								num = 5;
							}
						}
						else
						{
							num = 4;
						}
					}
					else
					{
						num = 3;
					}
				}
				else
				{
					num = 2;
				}
			}
			else
			{
				num = 1;
			}
			Storyboard storyboard = new Storyboard();
			ThicknessAnimation thicknessAnimation = (num2 > num) ? new ThicknessAnimation(new Thickness(left, 30.0, right, 0.0), new Thickness(left, 0.0, right, 0.0), new Duration(TimeSpan.FromSeconds(0.2))) : new ThicknessAnimation(new Thickness(left, 0.0, right, 30.0), new Thickness(left, 0.0, right, 0.0), new Duration(TimeSpan.FromSeconds(0.2)));
			thicknessAnimation.BeginTime = new TimeSpan?(new TimeSpan(0L));
			thicknessAnimation.SetValue(Storyboard.TargetNameProperty, rectangle.Name);
			Storyboard.SetTargetProperty(thicknessAnimation, new PropertyPath(FrameworkElement.MarginProperty));
			storyboard.Children.Add(thicknessAnimation);
			storyboard.Begin(this);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00008DD8 File Offset: 0x00006FD8
		private void AcrylicWindow_Loaded(object sender, RoutedEventArgs e)
		{
			this.btnHome.Background = this.PKViewModel.AccentBrushModel[0].AccentGradient;
		}

		// Token: 0x040007D7 RID: 2007
		private CancellationTokenSource cts;

		// Token: 0x040007D8 RID: 2008
		private readonly Resource res = new Resource();
	}
}
