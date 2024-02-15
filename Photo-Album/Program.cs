using Newtonsoft.Json;

namespace PhotoAlbum
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                int albumId = 3;
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
                    HttpResponseMessage response = await httpClient.GetAsync($"/photos?albumId={albumId}");
                    if (response.IsSuccessStatusCode)
                    { 
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        Photo[] photos = JsonConvert.DeserializeObject<Photo[]>(jsonResponse);
                        foreach (Photo photo in photos)
                        {
                            Console.WriteLine($"photo-album {albumId} [{photo.Id}] {photo.Title}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to fetch photos. Status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        class Photo
        {
            public int AlbumId { get; set; }
            public int Id { get; set; }
            public string Title { get; set; }
            public string Url { get; set; }
            public string ThumbnailUrl { get; set; }
        }
    }
}
