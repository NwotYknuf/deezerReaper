using System.Collections.Generic;

namespace deezerAPI{
    
    public class Album : Idded{
        private string _title;
        private List<Genre> _genres;
        private List<Track> _tracks;
        private List<Artist> _artist;

        public Album(int id, string title, List<Genre> genres, List<Artist> artist, List<Track> tracks) : base(id){
            _title = title;
            _genres = new List<Genre>(genres);
            _tracks = new List<Track>(tracks);
            _artist = new List<Artist>(artist);
        }

        public string Title { get{return _title; }}
        public int NumberOfGenres { get{ return _genres.Count; }}
        public int NumberOfArtists { get{return _artist.Count; }}
        public int NumberOfTracks { get{ return _tracks.Count; }}
        public Artist getArtist(int i){ return _artist[i]; }
        public Track getTrack(int i){ return _tracks[i]; }
        public Genre getGenre(int i){ return _genres[i]; }
        
    }

}