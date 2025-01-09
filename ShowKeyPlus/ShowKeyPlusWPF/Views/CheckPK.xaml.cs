using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using ShowKeyPlusWPF.ViewModel;

namespace ShowKeyPlusWPF.Views
{
	// Token: 0x02000034 RID: 52
	public partial class CheckPK : UserControl
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x0000AA43 File Offset: 0x00008C43
		public CheckPK()
		{
			this.InitializeComponent();
			this.SetLabels();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000AA7C File Offset: 0x00008C7C
		public CheckPK(Resource res)
		{
			if (res == null)
			{
				throw new ArgumentNullException("res");
			}
			this.res = res;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000AABC File Offset: 0x00008CBC
		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			new CheckProductKeyInfo();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000AAC4 File Offset: 0x00008CC4
		private void BtnCheck_Click(object sender, RoutedEventArgs e)
		{
			this.checkProductKeyInfo.EULA = (this.checkProductKeyInfo.PKDescription = string.Empty);
			bool flag = this.txtCheckPK.Text.Length < 29;
			if (flag)
			{
				this.mainViewModel.MsgTitle = this.res.GetResourceString("txtError");
				this.mainViewModel.MsgContent = this.res.GetResourceString("txtShortKey");
				this.mainViewModel.ShowDialog.Execute(sender);
			}
			bool flag2 = this.txtCheckPK.Text.Length == 29;
			if (flag2)
			{
				this.btnCheck.IsEnabled = true;
				this.Progbar.Visibility = Visibility.Visible;
				this.grdEdition.Visibility = Visibility.Hidden;
				this.checkProductKeyInfo.ProductKey = this.txtCheckPK.Text;
			}
			else
			{
				this.grdEdition.Visibility = Visibility.Collapsed;
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000ABC0 File Offset: 0x00008DC0
		private void TransformEdition()
		{
			Storyboard storyboard = new Storyboard();
			ScaleTransform renderTransform = new ScaleTransform(1.0, 1.0);
			this.grdEdition.RenderTransformOrigin = new Point(-0.5, -0.5);
			this.grdEdition.RenderTransform = renderTransform;
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.Duration = TimeSpan.FromMilliseconds(500.0);
			doubleAnimation.From = new double?(2.0);
			doubleAnimation.To = new double?(1.0);
			storyboard.Children.Add(doubleAnimation);
			Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.ScaleY", Array.Empty<object>()));
			Storyboard.SetTarget(doubleAnimation, this.grdEdition);
			storyboard.Begin();
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000ACA0 File Offset: 0x00008EA0
		private void TransformProgress()
		{
			Storyboard storyboard = new Storyboard();
			ScaleTransform renderTransform = new ScaleTransform(1.0, 1.0);
			this.LblProgress.RenderTransformOrigin = new Point(-0.5, -0.5);
			this.LblProgress.RenderTransform = renderTransform;
			DoubleAnimation doubleAnimation = new DoubleAnimation();
			doubleAnimation.Duration = TimeSpan.FromMilliseconds(500.0);
			doubleAnimation.From = new double?(2.0);
			doubleAnimation.To = new double?(1.0);
			storyboard.Children.Add(doubleAnimation);
			Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("RenderTransform.ScaleY", Array.Empty<object>()));
			Storyboard.SetTarget(doubleAnimation, this.LblProgress);
			storyboard.Begin();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000AD80 File Offset: 0x00008F80
		private void TxtCheckPK_GotFocus(object sender, RoutedEventArgs e)
		{
			this.txtCheckPK.ClipToBounds = true;
			this.bdrCheckPK.BorderBrush = this.LblProductKey.Foreground;
			this.grdEdition.Visibility = Visibility.Hidden;
			this.Progbar.Visibility = Visibility.Hidden;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000ADCC File Offset: 0x00008FCC
		private void Image_MouseDown(object sender, MouseButtonEventArgs e)
		{
			bool flag = !string.IsNullOrEmpty(this.txtCheckPK.Text);
			if (flag)
			{
				this.checkProductKeyInfo.ProductKey = this.txtCheckPK.Text;
				this.grdEdition.Visibility = Visibility.Visible;
				this.TransformEdition();
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000AE1E File Offset: 0x0000901E
		private void TxtCheckPK_KeyDown(object sender, KeyEventArgs e)
		{
			this.TxtProdKey_KeyUp(sender, e);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000AE2C File Offset: 0x0000902C
		private void Progbar_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			bool flag = !this.Progbar.IsVisible && !this.grdEdition.IsVisible && this.txtCheckPK.Text.Length == 29;
			if (flag)
			{
				this.grdEdition.Visibility = Visibility.Visible;
				this.TransformEdition();
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000AE88 File Offset: 0x00009088
		private void TxtProdKey_KeyUp(object sender, KeyEventArgs e)
		{
			try
			{
				this.txtCheckPK.CharacterCasing = CharacterCasing.Upper;
				int length = this.txtCheckPK.Text.Length;
				int num = length;
				if (num <= 11)
				{
					if (num == 1)
					{
						this.txtCheckPK.SelectionStart = this.txtCheckPK.Text.Length;
						goto IL_AD;
					}
					if (num != 5 && num != 11)
					{
						goto IL_AB;
					}
				}
				else if (num != 17 && num != 23)
				{
					if (num != 29)
					{
						goto IL_AB;
					}
					goto IL_AD;
				}
				TextBox textBox = this.txtCheckPK;
				textBox.Text += "-";
				this.txtCheckPK.SelectionStart = this.txtCheckPK.Text.Length + 1;
				IL_AB:
				IL_AD:;
			}
			catch (Exception ex)
			{
				this.mainViewModel.MsgTitle = this.res.GetResourceString("txtError");
				this.mainViewModel.MsgContent = ex.Message;
				this.mainViewModel.ShowDialog.Execute(sender);
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000AF9C File Offset: 0x0000919C
		private void GdCheckPK_LayoutUpdated(object sender, EventArgs e)
		{
			bool isVisible = this.Progbar.IsVisible;
			if (isVisible)
			{
				this.grdEdition.Visibility = Visibility.Hidden;
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000AFC8 File Offset: 0x000091C8
		private void Progbar_Loaded(object sender, RoutedEventArgs e)
		{
			this.grdEdition.Visibility = Visibility.Hidden;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000AFD8 File Offset: 0x000091D8
		private void TxtCheckPK_LostFocus(object sender, RoutedEventArgs e)
		{
			this.bdrCheckPK.BorderBrush = Brushes.DarkGray;
			this.btnCheck.IsEnabled = true;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000AFF9 File Offset: 0x000091F9
		private void LblProgress_TargetUpdated(object sender, DataTransferEventArgs e)
		{
			this.TransformProgress();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000B003 File Offset: 0x00009203
		private void TxtCheckPK_LayoutUpdated(object sender, EventArgs e)
		{
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000B003 File Offset: 0x00009203
		private void TxtCheckPK_MouseEnter(object sender, MouseEventArgs e)
		{
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000B008 File Offset: 0x00009208
		private void SetLabels()
		{
			this.txtEnterKey.Text = this.res.GetResourceString("txtEnterKey.Text");
			this.txtNote.Text = this.res.GetResourceString("txtNote.Text");
			this.LblCPK.Text = this.res.GetResourceString("txtCPK.Text");
			this.LblProductKey.Text = this.res.GetResourceString("txtProductKey.Text");
			this.LblEdition.Text = this.res.GetResourceString("txtEdition.Text");
			this.LblEULA.Text = this.res.GetResourceString("txtEULA.Text");
			this.LblMAK.Text = this.res.GetResourceString("txtMAK.Text");
			this.txtCheck.Text = this.res.GetResourceString("txtCheck.Text").PadRight(10, ' ');
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000B003 File Offset: 0x00009203
		private void LblProductKey_GotFocus(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000B0FF File Offset: 0x000092FF
		private void txtCheckPK_MouseMove(object sender, MouseEventArgs e)
		{
			this.txtCheckPK.BorderBrush = this.LblProductKey.Foreground;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000B003 File Offset: 0x00009203
		private void txtCheckPK_TextChanged(object sender, TextChangedEventArgs e)
		{
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000B11C File Offset: 0x0000931C
		private void UserControl_LostFocus(object sender, RoutedEventArgs e)
		{
			bool flag = this.txtEdition.Text.Length > 0;
			if (flag)
			{
				this.txtCheckPK.Clear();
				this.TxtCheckPK_GotFocus(sender, e);
			}
		}

		// Token: 0x0400084E RID: 2126
		private Resource res = new Resource();

		// Token: 0x0400084F RID: 2127
		private readonly CheckProductKeyInfo checkProductKeyInfo = new CheckProductKeyInfo();

		// Token: 0x04000850 RID: 2128
		private readonly MainViewModel mainViewModel = new MainViewModel();
	}
}
