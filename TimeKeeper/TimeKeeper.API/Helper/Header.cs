using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeKeeper.API.Helper
{
    public class Header
    {
        
        public int nextPage { get; set; }
        public int prevPage { get; set; }
        public int pageSize { get; set; }
        public int totalPages { get; set; }
        public int page { get; set; }
        public int sort { get; set; }

        public Header(int totalPages, int pageSize, int page, int sort)
        {
            this.totalPages = totalPages;
            nextPage = (page >= totalPages) ? 0 : page + 1;
            prevPage = (page == 0) ? 0 : page - 1;
            this.pageSize = pageSize;
            this.page = page;
            this.sort = sort;
        }
    }
}