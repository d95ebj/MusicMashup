using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicMashup.DataProviders.CoverArtArchive;
using MusicMashup.Models;

namespace MusicMashup.DataProviders
{
    public interface ICoverArtInformationProvider
    {
        Task GetCoverArt(IEnumerable<Album> albums);
    }
    public class CoverArtArchiveInformationProvider : ICoverArtInformationProvider
    {
        private readonly ICoverArtArchiveProxy _proxy;
        public CoverArtArchiveInformationProvider( ICoverArtArchiveProxy proxy)
        {
            _proxy = proxy;
        }
        public async Task GetCoverArt(IEnumerable<Album> albums)
        {
            var tasks = albums.Select(a => Task.Run(async () => { a.CoverArt = await _proxy.GetCoverArt(a.Mbid); }));

            await Task.WhenAll(tasks.ToArray());

        }
    }
}