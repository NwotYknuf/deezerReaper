using System;
using System.Collections.Generic;
using System.IO;

namespace deezerAPI{

    class Program{
        static void Main(string[] args){

            Client client = new Client();
            string folder = "data\\";

            for(int i = 1; i < int.MaxValue;i++){
                try{
                    Artist artist = client.getRessourceByID("artist", i) as Artist;
                    StreamWriter sw = new StreamWriter(folder + artist.Name + ".txt");

                    try{
                        List<Album> albums = client.GetAlbumsByArtist(artist.Name);

                        foreach(Album album in albums){
                            for(int j = 0; j < album.NumberOfTracks; j++){
                                
                                sw.Write(album.getTrack(j).Title + " GENRE :  ");

                                if(album.NumberOfGenres > 0){

                                    for(int k = 0; k < album.NumberOfGenres; k++){
                                        if(k != album.NumberOfGenres - 1){
                                            sw.Write(album.getGenre(k).Name + ", ");
                                        }
                                        else{
                                            sw.Write(album.getGenre(k).Name + "\n");
                                        }
                                    }
                                }
                                else{
                                    sw.Write("Unknown\n");
                                }
                            }
                        }

                        sw.Close();
                        Console.WriteLine("Artist {0} ID : {1} DONE", artist.Name, artist.Id);
                    }
                    catch{
                        Console.WriteLine("could not fetch albums for artist : {0} ID : {1}",artist.Name, artist.Id);
                    }

                }
                catch{
                    Console.WriteLine("could not find artist {0}", i);
                }              

            }

        }
    }

}
