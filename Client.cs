using System.Net;
using System.IO;
using System.Text;
using System;
using System.Web.Script.Serialization;
using System.Collections.Generic;

namespace deezerAPI{

    

    public class Client{
        private ParserGenre parseGenre;
        private ParserTrack parseTrack;
        private ParserArtist parseArtist;
        private ParserAlbum parseAlbum;

        private int nbr_requests = 0;

        private static string API_URL = "https://api.deezer.com/";
        private static int SLEEP_DELAY = 1500;

        public Client(){
            parseGenre = new ParserGenre(null);
            parseTrack = new ParserTrack(parseGenre);
            parseArtist = new ParserArtist(parseTrack);
            parseAlbum = new ParserAlbum(parseArtist, parseArtist, parseTrack, parseGenre, this);
        }

        private Dictionary<string, dynamic> getRequestData(string querry){

            if(nbr_requests > 49){
                System.Threading.Thread.Sleep(SLEEP_DELAY);
                nbr_requests = 0;
            }
            else{
                nbr_requests++;
            }

            nbr_requests++;

            Uri web_querry = new Uri(querry);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(web_querry);
            request.Method = "GET";

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            string str;
            if (response.StatusCode == HttpStatusCode.OK) {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                str = readStream.ReadToEnd();
                response.Close();
                readStream.Close();
            }
            else{
                throw new WebException("can't send request");
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, dynamic> data = jss.Deserialize<Dictionary<string, dynamic>>(str);

            checkForError(data);
            return data;

        }

        private void checkForError(Dictionary<string,dynamic> data){
            if(data.ContainsKey("error")){
                throw new WebException("Information not found");
            }
        }

        public Idded getRessourceByID(string type, int id){
            string querry = API_URL + type + "/" + id;
            Dictionary<string, dynamic> data = getRequestData(querry);
            return parseAlbum.tryParsing(data);
        }

        public List<Idded> getRessourceByName(string type, string search_value, int max_results = -1){

            string querry = API_URL + "search/" + type + "/?q=" + search_value;

            if(max_results != -1){
                querry += "&limit=" + max_results;
            }

            Dictionary<string,dynamic> data = getRequestData(querry);

            List<Idded> iddeds = new List<Idded>();

            foreach(Dictionary<string,dynamic> idded in data["data"]){
                Idded i = parseAlbum.tryParsing(idded);
                iddeds.Add(i);
            }

            return iddeds;
        }

        public List<Album> GetAlbumsByArtist(string artist, int max_results = -1){
            List<Album> res = new List<Album>();

            List<Idded> results = getRessourceByName("artist", artist, 1);

            int artist_id = ((Artist)results[0]).Id;

            results = getRessourceByName("album", artist, max_results);

            foreach(Idded idded in results){
                Album album = getRessourceByID("album", idded.Id) as Album;

                if(album.getArtist(0).Id == artist_id){
                    res.Add(album);
                }
            }

            return res;
        }
    }

    
}