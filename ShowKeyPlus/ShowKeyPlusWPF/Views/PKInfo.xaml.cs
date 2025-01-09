using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using ShowKeyPlusWPF.ViewModel;

namespace ShowKeyPlusWPF.Views
{
	// Token: 0x02000035 RID: 53
	public partial class PKInfo : UserControl
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x0000B44E File Offset: 0x0000964E
		public PKInfo()
		{
			this.InitializeComponent();
			base.DataContext = new ProductKeyInfoModel.ProductKeyInfo();
			this.SetLabels();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000B472 File Offset: 0x00009672
		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			new ProductKeyInfoModel.ProductKeyInfo();
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000B47C File Offset: 0x0000967C
		private void OriginalKey_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			bool flag = this.OriginalKey.Text.Count((char x) => x == '-') == 4;
			if (flag)
			{
				MainViewModel mainViewModel = (MainViewModel)base.DataContext;
				bool flag2 = mainViewModel.HideOriginal.CanExecute(sender);
				if (flag2)
				{
					mainViewModel.HideOriginal.Execute(sender);
				}
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000B4EC File Offset: 0x000096EC
		private void OEMKey_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			bool flag = this.OEMKey.Text.Count((char x) => x == '-') == 4;
			if (flag)
			{
				MainViewModel mainViewModel = (MainViewModel)base.DataContext;
				bool flag2 = mainViewModel.HideOEMKey.CanExecute(sender);
				if (flag2)
				{
					mainViewModel.HideOEMKey.Execute(sender);
				}
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000B55C File Offset: 0x0000975C
		private void InstalledKey_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			bool flag = this.InstalledKey.Text.Count((char x) => x == '-') == 4;
			if (flag)
			{
				MainViewModel mainViewModel = (MainViewModel)base.DataContext;
				bool flag2 = mainViewModel.HideCommand.CanExecute(sender);
				if (flag2)
				{
					mainViewModel.HideCommand.Execute(sender);
				}
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000B5CC File Offset: 0x000097CC
		private void SetLabels()
		{
			Resource resource = new Resource();
			this.LblProductName.Text = resource.GetResourceString("txtProductName.Text");
			this.LblProductID.Text = resource.GetResourceString("txtProductID.Text");
			this.LblVersion.Text = resource.GetResourceString("txtBuildVersion.Text");
			this.LblInstalledKey.Text = resource.GetResourceString("txtInstalledKey.Text");
			this.LblOriginalKey.Text = resource.GetResourceString("txtOriginalKey.Text");
			this.LblOriginalEdition.Text = resource.GetResourceString("txtOriginalEdition.Text");
			this.LblOEMKey.Text = resource.GetResourceString("txtOEMKey.Text");
			this.LblOEMDescription.Text = resource.GetResourceString("txtOEMDescription.Text");
			this.txtHeader.Text = resource.GetResourceString("txtHeader.Text");
			this.lblGeneric.Text = resource.GetResourceString("txtGeneric.Content");
			this.tpToggle2.Content = resource.GetResourceString("tpToggle.Content");
			this.tpToggle1.Content = resource.GetResourceString("tpToggle.Content");
			this.tpToggle.Content = resource.GetResourceString("tpToggle.Content");
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000B70C File Offset: 0x0000990C
		private void OriginalKey_MouseEnter(object sender, MouseEventArgs e)
		{
			this.tpToggle.Visibility = ((this.OriginalKey.Text.Count((char x) => x == '-') == 4) ? Visibility.Visible : Visibility.Collapsed);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000B75C File Offset: 0x0000995C
		private void OEMKey_MouseEnter(object sender, MouseEventArgs e)
		{
			this.tpToggle1.Visibility = ((this.OEMKey.Text.Count((char x) => x == '-') == 4) ? Visibility.Visible : Visibility.Collapsed);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000B7AC File Offset: 0x000099AC
		private void InstalledKey_MouseEnter(object sender, MouseEventArgs e)
		{
			this.tpToggle2.Visibility = ((this.InstalledKey.Text.Count((char x) => x == '-') == 4) ? Visibility.Visible : Visibility.Collapsed);
		}
	}
}
