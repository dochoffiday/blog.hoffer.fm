using Statiq.App;
using Statiq.Common;
using Statiq.Web;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace blog.hoffer.fm
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
                                ? new NormalizedPath(doc.GetString("Category")).Combine(doc.Destination.FileName.ChangeExtension(".html"))
                                : doc.Destination.ChangeExtension(".html");
                        }

                        return doc.Destination;
                    }));

            factory.DeployToGitHubPagesBranch(
                "dochoffiday",
                "blog.hoffer.fm",
                Config.FromSetting<string>("GITHUB_TOKEN"),
                "live_site_deployed_from_statiq"
            );

            return await factory.RunAsync();
        }
    }
} 