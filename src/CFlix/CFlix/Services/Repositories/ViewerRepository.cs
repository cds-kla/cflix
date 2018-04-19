using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CFlix.Services.Repositories
{
    public class ViewerRepository : IViewerRepository
    {
        private readonly HttpClient client;

        public ViewerRepository(IOptions<CFlixConfiguration> conf)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(conf.Value.ImageAPI);
        }

        public async Task<List<string>> ListAsync(string mediaId)
        {
            try
            {
                var result = await client.GetAsync($"/Viewer/List?mediaId={mediaId}");
                var str = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<string>>(str);
            }
            catch (HttpRequestException)
            {
                return new List<string>();
            }
        }

        public async Task<Stream> GetAsync(string mediaId, string imageName)
        {
            try
            {
                return await client.GetStreamAsync($"/Viewer/?image={imageName}&mediaId={mediaId}");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}
