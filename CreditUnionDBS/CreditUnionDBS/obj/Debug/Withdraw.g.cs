﻿#pragma checksum "..\..\Withdraw.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FE57BE58D74F44507BB2B98229D995E4CCBD8540CB6F53BB798307698BC42451"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CreditUnionDBS;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CreditUnionDBS {
    
    
    /// <summary>
    /// Withdraw
    /// </summary>
    public partial class Withdraw : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\Withdraw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemFile;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\Withdraw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem menuItemAcc;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\Withdraw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas CanvasCVW;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\Withdraw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border bdBorderW;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\Withdraw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtAccType;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\Withdraw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblaccnumbW;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\Withdraw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label acctypeW;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\Withdraw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cboWithdraw;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\Withdraw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtbalanceW;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\Withdraw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblamountW;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\Withdraw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtamountW;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\Withdraw.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnWithdraw;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CreditUnionDBS;component/withdraw.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Withdraw.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.menuItemFile = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 2:
            
            #line 26 "..\..\Withdraw.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.LogOut_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 27 "..\..\Withdraw.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Exit_click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.menuItemAcc = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 5:
            
            #line 30 "..\..\Withdraw.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.NewAccount_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 31 "..\..\Withdraw.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.EditAccount_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 32 "..\..\Withdraw.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Deposit_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 34 "..\..\Withdraw.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.TransferFunds_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 35 "..\..\Withdraw.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ViewTransactions_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.CanvasCVW = ((System.Windows.Controls.Canvas)(target));
            return;
            case 11:
            this.bdBorderW = ((System.Windows.Controls.Border)(target));
            return;
            case 12:
            this.txtAccType = ((System.Windows.Controls.TextBox)(target));
            return;
            case 13:
            this.lblaccnumbW = ((System.Windows.Controls.Label)(target));
            return;
            case 14:
            this.acctypeW = ((System.Windows.Controls.Label)(target));
            return;
            case 15:
            this.cboWithdraw = ((System.Windows.Controls.ComboBox)(target));
            
            #line 46 "..\..\Withdraw.xaml"
            this.cboWithdraw.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cboWithSelectionchanged);
            
            #line default
            #line hidden
            return;
            case 16:
            this.txtbalanceW = ((System.Windows.Controls.TextBox)(target));
            return;
            case 17:
            this.lblamountW = ((System.Windows.Controls.Label)(target));
            return;
            case 18:
            this.txtamountW = ((System.Windows.Controls.TextBox)(target));
            return;
            case 19:
            this.btnWithdraw = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

