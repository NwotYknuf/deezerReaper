using System.Collections.Generic;

namespace deezerAPI{

    public class ParserArtist : Parser{

        public ParserArtist(Parser next) : base(next) { }

        public override bool canParse(Dictionary<string, dynamic> data){
            return data["type"] == "artist";
        }

        public override Idded parse(Dictionary<string, dynamic> data){
            return new Artist((int)data["id"], (string)data["name"]);
        }

    }

}