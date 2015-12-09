namespace VSGerrit.Features.ChangeBrowser.Services
{
    public interface IChangeBrowserNavigationService
    {
        void NavigateToSettings();

        void NavigateToDetails();

        void NavigateToList();
    }
}
