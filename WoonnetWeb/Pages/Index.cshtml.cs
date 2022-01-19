using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Concurrent;
using System.Diagnostics;
using Woonnet;
using Woonnet.Models;

namespace WoonnetWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly HouseFinder _houseFinder;

        public List<Aanbod> AanbodList = new List<Aanbod>();
        public double LoadTimeSec = 0;

        public IndexModel(ILogger<IndexModel> logger, HouseFinder houseFinder)
        {
            _logger = logger;
            _houseFinder = houseFinder;
        }

        public async Task OnGet(bool parallel = false)
        {
            await RefreshAanbodList(parallel);
        }

        private async Task RefreshAanbodList(bool parallel)
        {
            var matches = (await _houseFinder.GetMatches()).ToList();

            var aanbodList = new ConcurrentBag<Aanbod>();

            var sw = Stopwatch.StartNew();

            if (parallel)
            {
                matches.AsParallel().ForAll(async match =>
                {
                    var aanbod = await _houseFinder.GetDetails(match.FrontendAdvertentieId);
                    aanbodList.Add(aanbod);
                });
            }
            else
            {
                foreach (var match in matches)
                {
                    var aanbod = await _houseFinder.GetDetails(match.FrontendAdvertentieId);
                    aanbodList.Add(aanbod);
                }
            }

            sw.Stop();
            LoadTimeSec = sw.Elapsed.TotalSeconds;

            // todo: move filters up
            AanbodList = aanbodList
                .Where(a => a.verdeelmodel == "DirectKans")
                //.Where(a => a.PublStartDateTime >= DateTime.Now.AddDays(-1))
                .OrderByDescending(a => a.PublStartDateTime)
                .ThenBy(a => a.AantalReactiesNumeric)
                .ToList();

            //AanbodList = aanbodList;
        }
    }
}