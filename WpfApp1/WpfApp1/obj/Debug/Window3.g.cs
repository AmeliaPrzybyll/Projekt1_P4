﻿#pragma checksum "..\..\Window3.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7F5E5FD2DD6C7DD4A7C53AA11F5CE86C7A5D78A3DB8CB5643918B0A18E438E4A"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

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
using WpfApp1;


namespace WpfApp1 {
    
    
    /// <summary>
    /// Window3
    /// </summary>
    public partial class Window3 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\Window3.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox Wyswietlanie_nazwy_lista;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\Window3.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Nazwa_Produktu_lista;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\Window3.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button dodawanie_do_listy;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\Window3.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button podglad_listy;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\Window3.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button usuwanie_listy;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\Window3.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button zgranie;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\Window3.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Kategorie;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApp1;component/window3.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Window3.xaml"
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
            this.Wyswietlanie_nazwy_lista = ((System.Windows.Controls.ListBox)(target));
            return;
            case 2:
            this.Nazwa_Produktu_lista = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.dodawanie_do_listy = ((System.Windows.Controls.Button)(target));
            
            #line 61 "..\..\Window3.xaml"
            this.dodawanie_do_listy.Click += new System.Windows.RoutedEventHandler(this.Dodawanie_Do_listy_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.podglad_listy = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\Window3.xaml"
            this.podglad_listy.Click += new System.Windows.RoutedEventHandler(this.Podglad_listy_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.usuwanie_listy = ((System.Windows.Controls.Button)(target));
            
            #line 77 "..\..\Window3.xaml"
            this.usuwanie_listy.Click += new System.Windows.RoutedEventHandler(this.Usuwanie_listy_click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.zgranie = ((System.Windows.Controls.Button)(target));
            
            #line 85 "..\..\Window3.xaml"
            this.zgranie.Click += new System.Windows.RoutedEventHandler(this.Zgranie_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Kategorie = ((System.Windows.Controls.ComboBox)(target));
            
            #line 89 "..\..\Window3.xaml"
            this.Kategorie.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Kategorie_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

