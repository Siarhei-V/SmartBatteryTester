using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace SmartBatteryTesterDesktopApp.UI
{
    internal class DisplayRootRegistry : IDisplayRootRegistry
    {
        Dictionary<Type, Type> _vmToWindowMapping;

        public DisplayRootRegistry()
        {
            _vmToWindowMapping = new Dictionary<Type, Type>();
        }

        void IDisplayRootRegistry.RegisterWindowType<VM, Window>()
        {
            _vmToWindowMapping.Add(typeof(VM), typeof(Window));
        }

        async Task IDisplayRootRegistry.ShowModalPresentation(object vm)
        {
            var window = CreateWindow(vm);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            await window.Dispatcher.InvokeAsync(() => window.ShowDialog());
        }

        #region Private Methods
        private Window CreateWindow(object vm)
        {
            _vmToWindowMapping.TryGetValue(vm.GetType(), out var windowType);
            if (windowType == null) 
                throw new InvalidOperationException($"No registered window type for argument type {vm.GetType().FullName}");

            var window = (Window)Activator.CreateInstance(windowType);
            window.DataContext = vm;
            return window;
        }
        #endregion
    }
}
