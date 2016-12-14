using System;
using System.Collections.Generic;
using System.Web.Services;
using AJ.UtiliTools;
using BC.Models;
using BC.Models.Tag;

namespace BC.Services
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class AdminService : System.Web.Services.WebService
    {
        [WebMethod]
        public SearchResultString slug(String title)
        {
            String slug = Helper.UrlDecode(title).Slug(256);

            return new SearchResultString(slug);
        }

        [WebMethod]
        public List<BC_Tag> Tags(String q, int limit)
        {
            ITagService TagService = new TagService();

            return TagService.Search(q, limit, 1);
        }
    }
}
