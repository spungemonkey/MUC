﻿#pragma checksum "C:\Users\MicheleG\documents\visual studio 2012\Projects\MappingApplication\MappingApplication\Pages\hosts.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FFC382878AEE9BBF9EE3EDC7D628F8CB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace MappingApplication.Pages {
    
    
    public partial class hosts : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton appDiary;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal Microsoft.Phone.Maps.Controls.Map hostMap;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/MappingApplication;component/Pages/hosts.xaml", System.UriKind.Relative));
            this.appDiary = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("appDiary")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.hostMap = ((Microsoft.Phone.Maps.Controls.Map)(this.FindName("hostMap")));
        }
    }
}

