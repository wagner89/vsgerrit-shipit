using VSGerrit.Api.Domain;

namespace VSGerrit.Features.ChangeBrowser.Services
{
    public interface IChangeBrowserNavigationService
    {
        void NavigateToSettings();

        void NavigateToDetails(ChangeInfo changeInfo);

        void NavigateToList();
    }
}
