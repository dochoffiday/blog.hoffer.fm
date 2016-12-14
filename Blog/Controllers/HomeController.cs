using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Linq;
using AJ.UtiliTools;
using BC.Models;
using BC.Models.Page;
using BC.Models.Post;
using Blog.Core;
using System;
using System.Xml;

namespace BC.Controllers
{
    public class HomeController : Controller
    {
        #region Initialize

        public IPostService PostService { get; set; }
        public IPageService PageService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (PostService == null) { PostService = new PostService(); }
            if (PageService == null) { PageService = new PageService(); }

            base.Initialize(requestContext);
        }

        #endregion

        #region Index

        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region Feed

        public ActionResult Feed(int? page, int? pageSize)
        {
            List<SyndicationItem> items = new List<SyndicationItem>();
            foreach (BC_Post post in PostService.Search(true, null, null, pageSize, page).Results)
            {
                SyndicationItem item = new SyndicationItem(post.Title, post.Body, Mvc.FullUrl("Details", "Post", new { category = post.Category().Name, slug = post.Slug }), post.PostID.ToString(), post.Modified);
                items.Add(item);
            }

            SyndicationFeed feed = new SyndicationFeed("dochoffiday", "The offical blog of AJ Hoffer", Mvc.FullUrl("Index", "Home"), items);

            return new RSSActionResult() { Feed = feed };
        }

        #endregion

        #region Sitemap

        public ActionResult Sitemap()
        {
            String xml;
            String url = "http://dochoffiday.com";

            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(ms))
                {
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Encoding = System.Text.Encoding.UTF8;
                    settings.Indent = true;

                    using (XmlWriter writer = XmlTextWriter.Create(sw, settings))
                    {
                        writer.WriteStartDocument();
                        writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");
                        writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                        writer.WriteAttributeString("xsi", "schemaLocation", null, "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd");

                        foreach (BC_Page page in PageService.Search(true, null, null, null).Results)
                        {
                            writer.WriteStartElement("url");
                            writer.WriteElementString("loc", url + Mvc.FullUrl("Details", "Page", new { slug = page.Slug }).PathAndQuery);
                            writer.WriteElementString("lastmod", page.Modified.ToString("yyyy-MM-dd"));
                            writer.WriteEndElement();
                        }

                        foreach (BC_Post post in PostService.Search(true, null, null, null, null).Results)
                        {

                            writer.WriteStartElement("url");
                            writer.WriteElementString("loc", url + Mvc.FullUrl("Details", "Post", new { category = post.Category().Name, slug = post.Slug }).PathAndQuery);
                            writer.WriteElementString("lastmod", post.Modified.ToString("yyyy-MM-dd"));
                            writer.WriteEndElement();
                        }

                        writer.WriteEndElement();
                        writer.Flush();
                        writer.Close();
                    }

                    using (StreamReader sr = new StreamReader(ms))
                    {
                        ms.Position = 0;
                        xml = sr.ReadToEnd();
                        sr.Close();
                    }
                }
            }

            return Content(xml, "text/xml", Encoding.UTF8);
        }

        #endregion
    }
}
