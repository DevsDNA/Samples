

namespace NotifyTask.Services
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    public static class MyStaticService
    {
            public static async Task<int> CountBytesInUrlAsync(string url)
            {
                await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);
            
                using (var client = new HttpClient())
                {
                    var data = await client.GetByteArrayAsync(url).ConfigureAwait(false);
                    return data.Length;
                }
            }
        
    }
}
