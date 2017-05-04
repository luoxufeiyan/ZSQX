using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStudio.Data
{
    public class X2DataSource : DataSourceBase<HtmlSchema>
    {
        private const string HtmlSource = "/Assets/Data/X2DataSource.htm";

        protected override string CacheKey
        {
            get { return "X2DataSource"; }
        }

        public override bool HasStaticData
        {
            get { return true; }
        }

        public async override Task<IEnumerable<HtmlSchema>> LoadDataAsync()
        {
            try
            {
                var serviceDataProvider = new StaticHtmlDataProvider(HtmlSource);
                return await serviceDataProvider.Load();
            }
            catch (Exception ex)
            {
                AppLogs.WriteError("X2DataSource.LoadData", ex.ToString());
                return new HtmlSchema[0];
            }
        }
    }
}
