﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleFolderSizeViewer.App
{
    class BindingProxy : Freezable
    {
        public static readonly DependencyProperty DataProperty =
                                DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy));

        public object Data
        {
            get { return GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }
    }
}
