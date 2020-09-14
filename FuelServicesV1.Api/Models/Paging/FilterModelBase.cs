using System;

namespace FuelServices.Api.Models.Paging
{
    public abstract class FilterModelBase : ICloneable
    {
        private int page { get; set; }

        public int Page { 
            get
            {
                return page <= 0 ? 1 : page;
            }
            set { }
        }
        public int Limit { get; set; }

        public FilterModelBase()
        {
            this.Page = 1;
            this.Limit = 10;
        }

        public abstract object Clone();
    }
}