// See https://aka.ms/new-console-template for more information
using Woonnet;
using Woonnet.Models;

try
{
    var httpClient = new HttpClient()
    {
        BaseAddress = new Uri("https://www.woonnetrijnmond.nl/"),
    };
    var houseFinder = new HouseFinder(httpClient);
    var matches = (await houseFinder.GetMatches()).ToList();

    var vandaagNieuw = matches.Where(a => a.IsVandaagNieuw).ToList();

    var aanbodList = new List<Aanbod>();

    foreach (var match in matches)
    {
        var aanbod = await houseFinder.GetDetails(match.FrontendAdvertentieId);
        aanbodList.Add(aanbod);
    }
    aanbodList = aanbodList
        .Where(a => a.verdeelmodel == "DirectKans")
        .Where(a => a.PublStartDateTime > DateTime.Now.AddDays(-1))
        .OrderByDescending(a => a.PublStartDateTime)
        .ThenBy(a => a.AantalReactiesNumeric)
        .ToList();

    foreach (var aanbod in aanbodList)
    {
        Console.WriteLine($"{aanbod.objecttype} - {aanbod.straat} {aanbod.huisnummer}{aanbod.huisnummertoevoeging}{aanbod.huisletter} {aanbod.plaats} ({aanbod.wijk}, {aanbod.buurt}) - {aanbod.totalehuur} ({aanbod.aantalreacties} reactions since {aanbod.PublStartDateTime.ToString("dd-MM-yyyy HH:mm")}) - {aanbod.Url}");
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}