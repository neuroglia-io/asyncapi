using Microsoft.AspNetCore.Mvc.RazorPages;
using Neuroglia.AsyncApi.Services;

namespace Neuroglia.AsyncApi.AspNetCore.UI.Pages
{

    public class DashboardModel 
        : PageModel
    {

        public DashboardModel(IAsyncApiDocumentProvider documents)
        {
            this.Documents = documents;
        }

        public IAsyncApiDocumentProvider Documents { get; }

        public void OnGet()
        {

        }

    }

}
