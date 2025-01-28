using Dignite.Cms.Public.Entries;
using Dignite.Cms.Public.Sections;
using System.Collections.Generic;

namespace Dignite.Cms.Public.Web.Models
{
    public class EntryListViewModel
    {
        public EntryListViewModel(SectionDto section, IReadOnlyList<EntryDto> entries, int totalCount, int pageIndex, int pageSize)
        {
            Section = section;
            Entries = entries;
            TotalCount = totalCount;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public SectionDto Section { get; protected set; }

        /// <summary>
        /// Entry list
        /// </summary>
        public IReadOnlyList<EntryDto> Entries { get; protected set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageIndex { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; protected set; }


        public int CurrentPage
        {
            get
            {
                return PageIndex + 1;
            }
        }


        public int TotalPage
        {
            get
            {
                if (TotalCount <= PageSize)
                    return 1;
                else
                {
                    if (TotalCount % PageSize == 0)
                    {
                        return TotalCount / PageSize;
                    }
                    else
                    {
                        return TotalCount / PageSize + 1;
                    }
                }
            }
        }
    }
}
