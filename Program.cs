using Statiq.App;
using Statiq.Common;
using Statiq.Web;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace dochoffiday
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var factory = Bootstrapper.Factory
                .CreateWeb(args)
                .AddSetting(Keys.Host, new Uri(Constants.SiteUri).Host)
                .AddSetting(Keys.LinksUseHttps, true)
                .AddSetting(
                    Keys.DestinationPath,
                    Config.FromDocument((doc, ctx) =>
                    {
                        // Only applies to the content pipeline
                        if (ctx.PipelineName == nameof(Statiq.Web.Pipelines.Content))
                        {
                            return doc.Source.Parent.Segments.Last().SequenceEqual("posts".AsMemory())
                                ? new NormalizedPath(doc.GetDateTime(WebKeys.Published).ToString("yyyy")).Combine(doc.GetString("Category")).Combine(doc.Destination.FileName.ChangeExtension(".html"))
                                : doc.Destination.ChangeExtension(".html");
                        }

                        return doc.Destination;
                    }));

            factory.DeployToGitHubPagesBranch(
                "dochoffiday",
                "dochoffiday.com",
                "f9abec6afebaa513cc80077374997937fd90c7b4",
                "live_site_deployed_from_statiq"
            );

            return await factory.RunAsync();
        }
    }
} 