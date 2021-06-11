using System;
using WPF.ViewModels;

namespace WPF.State.Navigation
{
    public interface INavigator
    {
        BaseViewModel CurrentViewModel { get; set; }
        event Action StateChanged;
    }

    public enum ViewType
    {
        Login,
        Home,
        User
    }
}