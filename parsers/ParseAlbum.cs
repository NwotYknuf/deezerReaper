using System.Collections.Generic;
using System;

namespace deezerAPI{

    public class ParserAlbum : Parser{

        private ParserArtist _parserArtist;
        private ParserTrack _parserTrack;
        private ParserGenre _parserGenre;
        private Client _client;

        public ParserAlbum(Parser next, ParserArtist parserArtist, ParserTrack parserTrack, ParserGenre parserGenre, Client client) : base(next) {
            _parserArtist = parserArtist;
            _parserTrack = parserTrack;
            _parserGenre = parserGenre;
            _client = client;
         }

        public override bool canParse(Dictionary<string, dynamic> data){
            return data["type"] == "album";
        }

        public override Idded parse(Dictionary<string, dynamic> data){
            
            List<Genre> genres = new List<Genre>();

            if(data.ContainsKey("genres")){
                foreach(Dictionary<string,dynamic> genre in data["genres"]["data"]){
                    Genre g = _parserGenre.tryParsing(genre) as Genre;
                    genres.Add(g);
                }
            }
            else{
                try{
                    Genre g = _client.getRessourceByID("genre", (int)data["genre_id"]) as Genre;
                    if(g.Id != -1){
                        genres.Add(g);
                    }
                }
                catch{
                    //skip
                }
            }

            List<Artist> artists = new List<Artist>();
            if(data.ContainsKey("contributors")){
                 foreach(Dictionary<string,dynamic> artist in data["contributors"]){
                    Artist a = _parserArtist.tryParsing(artist) as Artist;
                    artists.Add(a);
                }
            }
            else{
                Artist a = _parserArtist.tryParsing(data["artist"]);
                artists.Add(a);
            }

            List<Track> tracks = new List<Track>();

            if(data.ContainsKey("tracks")){
                foreach(Dictionary<string,dynamic> track in data["tracks"]["data"]){
                    Track t = _parserTrack.tryParsing(track) as Track;
                    tracks.Add(t);
                }

            }

            return new Album((int)data["id"], (string)data["title"], genres, artists, tracks);
        }

    }

}